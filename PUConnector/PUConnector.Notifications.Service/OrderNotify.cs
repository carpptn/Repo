using Newtonsoft.Json;
using PUConnector.Commons;
using PUConnector.Commons.ApiModel;
using PUConnector.Commons.WCF;
using PUConnector.OrdersManager;
using PUConnector.OrdersManager.Exceptions;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace PUConnector.Notifications.Service
{
    /// <summary>
    /// Delegat dla funkcji obsługi zdarzenia odebrania nowego powiadomienia (notyfikacji) o zmianie stanu zamówienia/transakcji
    /// </summary>
    /// <param name="orderRecord"></param>
    public delegate void OnOrderNotifyReceivedHandler(OrderRecord orderRecord);

    /// <summary>
    /// Delegat dla funkcji obsługi zdarzenia odebrania nowego powiadomienia (notyfikacji) o zmianie stanu zwrotu
    /// </summary>
    /// <param name="refundRecord"></param>
    public delegate void OnRefundNotifyReceivedHandler(RefundRecord_Type refundRecord);


    /// <summary>
    /// Implementuję usługę sieciową Web typu Rest odbierającą powiadomienia (notifications) o zmianie stanu zamówienia/transakcji bądź zwrotu od PayU
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults=true)]
    public class OrderNotify : IOrderNotify
    {
        // variables

        private TraceSource ts = new TraceSource("PUConnector.Receiver");


        // events

        /// <summary>
        /// Zdarzenie odebrania powiadomienia o zmianie stanu zamówienia/transakcji
        /// </summary>
        public event OnOrderNotifyReceivedHandler OnOrderNotifyReceived;

        /// <summary>
        /// Zdarzenie odebrania powiadomienia o zmianie stanu zwrotu
        /// </summary>
        public event OnRefundNotifyReceivedHandler OnRefundNotifyReceived;


        // properties

        /// <summary>
        /// Identyfikator punktu płaności
        /// </summary>
        public string MerchantPosId { get; set; }

        /// <summary>
        /// Klucz szyfrujący do komunikacji z PayU
        /// </summary>
        public string SecondSecurityKey { get; set; }

        /// <summary>
        /// Dostęp do bazy danych
        /// </summary>
        public Manager OrdersManager { get; set; }

        /// <summary>
        /// Ignorowanie błędnych powiadomień. Ustawione na wartość true włącza zwracanie odpowiedzi
        /// o poprawnym odebraniu powiadomienia nawet jeśli było ono błędne. Zachowanie takie powoduje, że
        /// system PayU nie będzie podejmował więcej prób dostarczenia błędnego powiadomienia w późniejszym czasie
        /// </summary>
        public bool IgnoreIncorrectNotifications { get; set; }


        // constructors

        /// <summary>
        /// Domyślny konstruktor odczytujący parametry konfiguracyjne z pliku .config
        /// </summary>
        public OrderNotify()
        {
            this.ts.TraceInformation("Created");
            this.ts.TraceInformation("Read configuration");

            this.MerchantPosId = ConfigurationManager.AppSettings["MerchantPosId"];
            this.SecondSecurityKey = ConfigurationManager.AppSettings["SecondSecurityKey"];
            if (ConfigurationManager.ConnectionStrings["OrdersManager"] != null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["OrdersManager"].ConnectionString;
                string dbProvider = ConfigurationManager.ConnectionStrings["OrdersManager"].ProviderName;

                if (!string.IsNullOrEmpty(connectionString))
                    this.OrdersManager = new Manager(connectionString, dbProvider);
            }
            this.IgnoreIncorrectNotifications = 
                ConfigurationManager.AppSettings["IgnoreIncorrectNotifications"].Equals(
                    "true", StringComparison.OrdinalIgnoreCase
                );
            ErrorHandler.WriteExceptionsToEventLog =
                ConfigurationManager.AppSettings["WriteExceptionsToEventLog"].Equals(
                    "true", StringComparison.OrdinalIgnoreCase
                );
        }


        /// <summary>
        /// Sparameteryzowany konstruktor nie wymagający konfiguracji w pliku .config
        /// </summary>
        /// <param name="merchantPosId">Identyfikator płatności</param>
        /// <param name="secondSecurityKey">Klucz szyfrujący do komunikacji z PayU</param>
        /// <param name="orderManager">Dostęp do bazy danych</param>
        /// <param name="onOrderNotifyReceived">Zdarzenie odebrania powiadomienia o zmianie stanu zamówienia/transakcji</param>
        /// <param name="onRefundNotifyReceived">Zdarzenie odebrania powiadomienia o zmianie stanu zwrotu</param>
        /// <param name="ignoreIncorrectNotifications">Ignorowanie błędnych powiadomień</param>
        public OrderNotify(
            string merchantPosId,
            string secondSecurityKey,
            Manager orderManager,
            OnOrderNotifyReceivedHandler onOrderNotifyReceived = null,
            OnRefundNotifyReceivedHandler onRefundNotifyReceived = null,
            bool ignoreIncorrectNotifications = true
            )
        {
            this.ts.TraceInformation("Created");

            this.MerchantPosId = merchantPosId;
            this.SecondSecurityKey = secondSecurityKey;
            if (onOrderNotifyReceived != null)
                this.OnOrderNotifyReceived += onOrderNotifyReceived;
            if (onRefundNotifyReceived != null)
                this.OnRefundNotifyReceived += onRefundNotifyReceived;
            this.OrdersManager = orderManager;
            this.IgnoreIncorrectNotifications = ignoreIncorrectNotifications;
        }


        // public methods

        /// <summary>
        /// Zwraca napis "OK" - pozwala na szybki test dostępności usługi z poziomu przeglądarki internetowej
        /// </summary>
        /// <returns>"OK"</returns>
        public string Test()
        {
            this.ts.TraceInformation("Test request");
            return "OK";
        }


        /// <summary>
        /// Odbiera powiadomienia (notyfikacje) o zmianie stanu zamówienia/transakcji bądź zwrotu
        /// </summary>
        /// <param name="stream">Strumień z treścią powiadomienia</param>
        /// <returns>Pusty łańcuch tekstowy w przypadku poprawnego przetworzenia powiadomienia lub opis błędu</returns>
        public string Receive(Stream stream)
        {
            try
            {
                string json = null;
                StreamReader reader = new StreamReader(stream);
                json = reader.ReadToEnd();

                this.ts.TraceInformation("Notification received. Authorize request...");

                if (!this.AuthorizeRequest(json))
                {
                    this.ts.TraceEvent(TraceEventType.Warning, 2, "Unauthorized. Notification is ignored");
                    return string.Empty;
                }

                if (json.ToLower().Contains("refundid"))
                    this.RefundNotification(json);
                else
                    this.OrderNotification(json);

                return string.Empty;
            }
            catch(Exception e)
            {
                this.ts.TraceEvent(
                    TraceEventType.Error,
                    1,
                    "Exception {0}: {1}, {2}", e.GetType().Name, e.Message, e.StackTrace
                    );

                if (e is WebFaultException<string>)
                {
                    if (this.IgnoreIncorrectNotifications)
                    {
                        string message = e.Message;
                        message += ". Details: " + ((WebFaultException<string>)e).Detail;
                        return "Warning. Request was not processed. Error: " + message;
                    }
                    else
                        throw;
                }

                if (e is WebFaultException)
                    throw;

                throw new WebFaultException<string>(
                    e.Message,
                    HttpStatusCode.BadRequest
                    );
            }
        }


        // protected methods

        protected bool AuthorizeRequest(string json)
        {
            WebHeaderCollection headers =
                WebOperationContext.Current.IncomingRequest.Headers;

            bool ssl =
                WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.
                    ToString().StartsWith("https:", StringComparison.OrdinalIgnoreCase);

            if (!ssl)
            {
                string signatureHeader = headers.Get("OpenPayu-Signature");

                if (!Security.VerifySignature(signatureHeader, json, this.SecondSecurityKey))
                {
                    throw new WebFaultException<string>(
                        "OpenPayu Signature verification failed",
                        HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                string authHeader = headers.Get("Authorization");

                if (!Security.VerifyAuthorization(authHeader, this.MerchantPosId, this.SecondSecurityKey))
                {
                    throw new WebFaultException<string>(
                        "OpenPayu Signature verification failed",
                        HttpStatusCode.Unauthorized);
                }
            }

            return true;
        }


        protected void OrderNotification(string json)
        {
            OrderNotification notification =
                JsonConvert.DeserializeObject<OrderNotification>(json);

            if (notification.Order == null)
            {
                throw new WebFaultException<string>(
                    "Incorrect order notification object", 
                    HttpStatusCode.BadRequest
                    );
            }

            this.ts.TraceInformation(
                "Order notification. orderId:{0}, extOrderId:{1}, status:{2}", 
                notification.Order.OrderId,
                notification.Order.ExtOrderId,
                notification.Order.Status
                );

            if (this.OrdersManager != null)
            {
                this.OrdersManager.CommLogCreateForExtOrderId(
                    notification.Order.ExtOrderId,
                    null,
                    null,
                    notification.GetType().Name,
                    json
                    );

                try
                {
                    if (!this.OrdersManager.OrderUpdate(
                        notification.Order))
                        return;
                }
                catch (OrderNotFoundException e)
                {
                    throw new WebFaultException<string>(
                        e.Message,
                        HttpStatusCode.BadRequest);
                }
            }

            if (OnOrderNotifyReceived != null)
                OnOrderNotifyReceived(notification.Order);
        }


        protected void RefundNotification(string json)
        {
            RefundNotification notification =
                JsonConvert.DeserializeObject<RefundNotification>(json);

            if (notification.Refund == null)
            {
                throw new WebFaultException<string>(
                    "Incorrect refund notification object",
                    HttpStatusCode.BadRequest
                    );
            }

            this.ts.TraceInformation(
                "Refund notification. refundId:{0}, extRefundId:{1}, status:{2}",
                notification.Refund.RefundId,
                notification.Refund.ExtRefundId,
                notification.Refund.Status
                );

            if (this.OrdersManager != null)
            {
                this.OrdersManager.CommLogCreateForExtOrderId(
                    notification.ExtOrderId,
                    null,
                    null,
                    notification.GetType().Name,
                    json
                    );

                try
                {
                    if (!this.OrdersManager.RefundUpdate(
                        notification.Refund))
                        return;
                }
                catch (OrdersManagerException e)
                {
                    throw new WebFaultException<string>(
                        e.Message,
                        HttpStatusCode.BadRequest);
                }
            }

            if (OnRefundNotifyReceived != null)
                OnRefundNotifyReceived(notification.Refund);
        }
    }
}
