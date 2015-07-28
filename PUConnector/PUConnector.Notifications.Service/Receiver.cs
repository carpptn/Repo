using PUConnector.Commons.WCF;
using PUConnector.OrdersManager;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace PUConnector.Notifications.Service
{
    /// <summary>
    /// Hostuje (uruchamia) usługę sieciową Web typu Rest zdefiniowaną w kontrakcie WCF <see cref="IOrderNotify"/>
    /// </summary>
    public class Receiver
    {
        // variables

        private ServiceHost host;
        private OrderNotify serviceInstance;
        private string endpointAddress;
        private bool publishServiceMetadata;

        // properties

        /// <summary>
        /// Instancja usługi sieciowej Web typu Rest odbierającą powiadomienia (notifications) od PayU. Dostępna po wykonaniu metody Start.
        /// </summary>
        public OrderNotify ServiceInstance
        {
            get { return serviceInstance; }
            set { serviceInstance = value; }
        }


        // constructors

        /// <summary>
        /// Domyślny konstruktor powodujący odczyt parametrów konfiguracyjnych z pliku .config
        /// </summary>
        public Receiver()
        {
        }


        /// <summary>
        /// Sparameteryzowany konstruktor nie wymagający konfiguracji w pliku .config
        /// </summary>
        /// <param name="endpointAddress">Adres Url pod którym uruchomiona zostanie usługa odbierania powiadomień</param>
        /// <param name="merchantPosId">Identyfikator punktu płatności</param>
        /// <param name="secondSecurityKey">Klucz szyfrujący do komunikacj z PayU</param>
        /// <param name="ordersManager">Dostęp do bazy danych</param>
        /// <param name="onOrderNotifyReceived">Zdarzenie odebrania powiadomienia o zmianie stanu zamówienia/transakcji</param>
        /// <param name="onRefundNotifyReceived">Zdarzenie odebrania powiadomienia o zmianie stanu zwrotu</param>
        /// <param name="ignoreIncorrectNotifications">Ignorowanie błędnych powiadomień</param>
        /// <param name="writeExceptionsToEventLog">Zapisywanie błędów (wyjątków) w systemowym dzienniku zdarzeń EventLog</param>
        /// <param name="publishServiceMetadata">Publikowanie pod adresem Url usługi strony web opisującej usługę</param>
        public Receiver(
            string endpointAddress,
            string merchantPosId,
            string secondSecurityKey,
            Manager ordersManager = null,
            OnOrderNotifyReceivedHandler onOrderNotifyReceived = null,
            OnRefundNotifyReceivedHandler onRefundNotifyReceived = null,
            bool ignoreIncorrectNotifications = true,
            bool writeExceptionsToEventLog = false,
            bool publishServiceMetadata = true
            )
        {
            ErrorHandler.WriteExceptionsToEventLog = writeExceptionsToEventLog;

            this.endpointAddress = endpointAddress;
            this.publishServiceMetadata = publishServiceMetadata;

            this.serviceInstance = new OrderNotify(
                merchantPosId,
                secondSecurityKey,
                ordersManager,
                onOrderNotifyReceived,
                onRefundNotifyReceived,
                ignoreIncorrectNotifications
                );
        }


        // public methods

        /// <summary>
        /// Uruchamia usługę odbierania powiadomień od PayU
        /// </summary>
        public void Start()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            if (this.serviceInstance == null)
            {
                this.serviceInstance = new OrderNotify();
                this.host =
                    new ServiceHost(serviceInstance, new Uri[] { });
                this.host.Open();
                return;
            }

            this.host =
                new ServiceHost(this.serviceInstance, new Uri[] { new Uri(this.endpointAddress) });

            if (this.host.Description.Behaviors.Find<ErrorHandler>() == null)
                this.host.Description.Behaviors.Add(new ErrorHandler());

            if (this.publishServiceMetadata)
            {
                ServiceMetadataBehavior smb = this.host.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (smb == null) smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.HttpsGetEnabled = true;
                this.host.Description.Behaviors.Add(smb);
            }

            WebHttpBinding binding = new WebHttpBinding();
            binding.ContentTypeMapper = new WebContentTypeMapperExt();

            bool ssl = 
                endpointAddress.StartsWith("https:", StringComparison.OrdinalIgnoreCase);
            if (ssl)
            {
                binding.Security.Mode = WebHttpSecurityMode.Transport;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            }

            ServiceEndpoint endpoint = 
                this.host.AddServiceEndpoint(typeof(IOrderNotify), binding, this.endpointAddress);

            WebHttpBehavior endpointBehavior = new WebHttpBehavior();
            endpointBehavior.FaultExceptionEnabled = false;
            endpointBehavior.AutomaticFormatSelectionEnabled = false;
            endpointBehavior.DefaultOutgoingRequestFormat =
                System.ServiceModel.Web.WebMessageFormat.Json;
            endpointBehavior.DefaultOutgoingResponseFormat = 
                System.ServiceModel.Web.WebMessageFormat.Json;
            endpoint.EndpointBehaviors.Add(endpointBehavior);

            this.host.Open();
        }


        /// <summary>
        /// Zatrzymuje usługę odbierania powiadomień od PayU
        /// </summary>
        public void Stop()
        {
            if (this.host != null &&
                this.host.State == CommunicationState.Opened)
                this.host.Close();
        }


        // private methods

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            new ErrorHandler().HandleError(e.ExceptionObject as Exception);
        }
    }
}
