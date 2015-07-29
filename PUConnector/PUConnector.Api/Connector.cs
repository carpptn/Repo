using Newtonsoft.Json;
using PUConnector.Commons;
using PUConnector.Commons.ApiModel;
using PUConnector.Commons.WCF;
using PUConnector.OrdersManager;
using RestSharp;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;

namespace PUConnector.Api
{
    /// <summary>
    /// Wysyłanie żądań (request) do PayU oraz dbieranie odpowiedzi (response) na żądania od systemu PayU
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class Connector : IConnector
    {
        // consts

        private const string ordersRes = "api/v2_1/orders";
        private const string orderRes  = "api/v2_1/orders/{0}";
        private const string refundRes = "api/v2_1/orders/{0}/refund";
        private const string statusRes = "api/v2_1/orders/{0}/status";


        // variables

        private string apiEndpountBaseUrl = "https://secure.payu.com";
        private TraceSource ts = new TraceSource("PUConnector.Connector");


        // properties

        /// <summary>
        /// Bazowy adres Url usługi PayU Rest API
        /// </summary>
        public string ApiEndpountBaseUrl
        {
            get { return apiEndpountBaseUrl; }
            set { apiEndpountBaseUrl = value; }
        }

        /// <summary>
        /// Numer identyfikacyjny punktu płatności
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
        /// Opcjonalna komunikacja przez HTTP proxy 
        /// </summary>
        public WebProxy Proxy { get; set; }


        // constructors

        /// <summary>
        /// Domyślny konstruktor odczytujący parametry konfiguracyjne z pliku .config
        /// </summary>
        public Connector()
        {
            this.ts.TraceInformation("Created");
            this.ts.TraceInformation("Read configuration");

            this.MerchantPosId = ConfigurationManager.AppSettings["MerchantPosId"];
            this.SecondSecurityKey = ConfigurationManager.AppSettings["SecondSecurityKey"];

            if (ConfigurationManager.ConnectionStrings["OrdersManager"] != null &&
                !string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings["OrdersManager"].ConnectionString))
            {
                this.OrdersManager = new Manager();
            }
            
            string proxyAddress = ConfigurationManager.AppSettings["ProxyAddress"];
            if (!string.IsNullOrEmpty(proxyAddress))
            {
                int proxyPort = int.Parse(ConfigurationManager.AppSettings["ProxyPort"]);
                this.Proxy = new WebProxy(proxyAddress, proxyPort);
            }

            string writeExToEvLog = ConfigurationManager.AppSettings["WriteExceptionsToEventLog"];
            if (!string.IsNullOrEmpty(writeExToEvLog))
                ErrorHandler.WriteExceptionsToEventLog =
                    writeExToEvLog.Equals("true", StringComparison.OrdinalIgnoreCase);
        }


        /// <summary>
        /// Sparameteryzowany konstruktor nie wymagający konfiguracji w pliku .config
        /// </summary>
        /// <param name="merchantPosId">Identyfiaktor punktu płatności</param>
        /// <param name="secondSecurityKey">Klucz szyfrujący do komunikacji z PayU</param>
        /// <param name="ordersManager">Dostęp do bazy danych</param>
        /// <param name="proxy">Opcjonalna komunikacja przez HTTP proxy</param>
        public Connector(
            string merchantPosId,
            string secondSecurityKey,
            Manager ordersManager = null,
            WebProxy proxy = null
            )
        {
            this.ts.TraceInformation("Created");

            this.MerchantPosId = merchantPosId;
            this.SecondSecurityKey = secondSecurityKey;
            this.OrdersManager = ordersManager;
            this.Proxy = proxy;
        }


        // public methods

        /// <summary>
        /// Wysyła żądanie utworzenia nowego zamówienia/transakcji
        /// </summary>
        /// <param name="request">Żądanie utworzenia zamówienia/transakcji</param>
        /// <returns>Odpowiedź na żądanie utworzenia zamówienia/transakcji</returns>
        public OrderCreateResponse OrderCreate(OrderCreateRequest request)
        {
            string extOrderId = null;
            if (this.OrdersManager != null)
            {
                extOrderId = this.OrdersManager.OrderCreate(request.ExtOrderId);
                request.ExtOrderId = extOrderId;
            }

            if (string.IsNullOrEmpty(request.MerchantPosId))
                request.MerchantPosId = this.MerchantPosId;

            OrderCreateResponse response = 
                this.SendPayURequest<OrderCreateResponse>(
                    Method.POST, 
                    ordersRes, 
                    null, 
                    extOrderId, 
                    request
                    );

            if (this.OrdersManager != null && response != null)
            {
                this.OrdersManager.OrderUpdateOrderId(extOrderId, response.OrderId);
            }

            return response;
        }


        /// <summary>
        /// Wysyła żądanie anulowania zamówienia/transakcji
        /// </summary>
        /// <param name="request">Żadanie anulowania zamówienia/transakcji</param>
        /// <returns>Odpowiedź na żądanie anulowania zamówienia/transakcji</returns>
        public OrderCancelResponse OrderCancel(OrderCancelRequest request)
        {
            return this.SendPayURequest<OrderCancelResponse>(
                Method.DELETE,
                string.Format(orderRes, request.OrderId),
                request.OrderId,
                null, 
                request
                );
        }


        /// <summary>
        /// Wysyła żądanie pobrania szczegółów zamówienia/transakcji
        /// </summary>
        /// <param name="request">Żądanie pobrania szczegółów zamówienia/transakcji</param>
        /// <returns>Odpowiedź na żądanie pobrania szczegółów zamówienia/transakcji</returns>
        public OrderRetrieveResponse OrderRetrieve(OrderRetrieveRequest request)
        {
            OrderRetrieveResponse response = 
                this.SendPayURequest<OrderRetrieveResponse>(
                    Method.GET,
                    string.Format(orderRes, request.OrderId),
                    request.OrderId, 
                    null, 
                    request
                    );

            if (this.OrdersManager != null && response != null && response.Orders != null)
            {
                foreach (OrderRecord order in response.Orders)
                    this.OrdersManager.OrderUpdate(order);
            }

            return response;
        }


        /// <summary>
        /// Wysyła żądanie aktualizacji statusu zamówienia/transakcji
        /// </summary>
        /// <param name="request">Żądanie aktualizacji statusu zamówienia/transakcji</param>
        /// <returns>Odpowiedź na żądanie aktualizacji stutusu zamówienia/transakcji</returns>
        public OrderStatusUpdateResponse OrderStatusUpdate(OrderStatusUpdateRequest request)
        {
            return this.SendPayURequest<OrderStatusUpdateResponse>(
                Method.PUT,
                string.Format(statusRes, request.OrderId),
                request.OrderId, 
                null, 
                request
                );
        }


        /// <summary>
        /// Wysyła żądanie utworzenia nowego zwrotu dla zamównia/transakcji
        /// </summary>
        /// <param name="request">Żądanie utworzenia zwrotu dla zamówienia/transakcji</param>
        /// <returns>Odpowiedź na żądanie utworzenia zwrotu dla zamówienia/transakcji</returns>
        public RefundCreateResponse RefundCreate(RefundCreateRequest request)
        {
            string extRefundId = null;
            if (this.OrdersManager != null)
            {
                extRefundId = this.OrdersManager.RefundCreate(request.OrderId);
                request.Refund.ExtRefundId = extRefundId;
            }

            RefundCreateResponse response =
                this.SendPayURequest<RefundCreateResponse>(
                    Method.POST,
                    string.Format(refundRes, request.OrderId),
                    request.OrderId,
                    null,
                    request
                );

            if (this.OrdersManager != null && response != null && response.Refund != null)
            {
                this.OrdersManager.RefundUpdateRefundId(extRefundId, response.Refund.RefundId);
            }

            return response;
        }


        // protected methods
        
        protected T SendPayURequest<T>(
            Method method,
            string apiEndpointResource,
            string orderId,
            string extOrderId,
            object request
            )
        {
            string json =
                JsonConvert.SerializeObject(
                    request,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
                    );

            string response =
                this.SendPayURawRequest(
                    method, 
                    apiEndpointResource, 
                    orderId, 
                    extOrderId, 
                    json, 
                    request.GetType().Name
                    );

            if (string.IsNullOrEmpty(response))
                return default(T);

            T respValue =
                JsonConvert.DeserializeObject<T>(response);

            return respValue;
        }


        protected string SendPayURawRequest(
            Method method,
            string apiEndpointResource, 
            string orderId, 
            string extOrderId, 
            string json,
            string requestType
            )
        {
            this.ts.TraceInformation(
                "{0} request. orderId:{1}, extOrderId:{2}",
                requestType,
                orderId,
                extOrderId
                );

            RestClient restClient = new RestClient(this.ApiEndpountBaseUrl);
            restClient.FollowRedirects = false;
            restClient.Proxy = this.Proxy;

            RestRequest restRequest = new RestRequest(apiEndpointResource, method);
            restRequest.RequestFormat = DataFormat.Json;

            restRequest.AddParameter(
                new Parameter
                {
                    Name = "application/json",
                    Value = json,
                    Type = ParameterType.RequestBody
                });

            restRequest.AddHeader(
                "Authorization", 
                Security.BasicAuthHdrValueCreate(this.MerchantPosId, this.SecondSecurityKey)
                );

            long? commLogId = null;
            if (this.OrdersManager != null)
            {
                if (orderId == null)
                    commLogId = this.OrdersManager.CommLogCreateForExtOrderId(
                        extOrderId, requestType, json, null, null
                        );
                else
                    commLogId = this.OrdersManager.CommLogCreateForOrderId(
                        orderId, requestType, json, null, null
                        );
            }

            IRestResponse response = restClient.Execute(restRequest);

            this.ts.TraceInformation(
                "{0} response. httpStatus:{1}, orderId:{2}, extOrderId:{3}",
                requestType,
                ((int)response.StatusCode).ToString() + " " + response.StatusDescription,
                orderId,
                extOrderId
                );

            if (this.OrdersManager != null && response != null)
                this.OrdersManager.CommLogUpdate(
                    (long)commLogId, requestType, response.Content
                    );

            if (response.ErrorException != null)
                throw response.ErrorException;

            int[] validHttpStatusCodes = new int[] { 200, 201, 422, 302, 400, 404 };

            if (!validHttpStatusCodes.Contains((int)response.StatusCode))
            {
                throw new HttpException(
                    (int)response.StatusCode,
                    string.Format(
                        "Unexpected HTTP code response: {0} {1}", 
                        response.StatusCode, 
                        response.StatusDescription
                        )
                    );
            }

            if (string.IsNullOrEmpty(response.Content))
                return null;

            return response.Content;
        }
    }
}
