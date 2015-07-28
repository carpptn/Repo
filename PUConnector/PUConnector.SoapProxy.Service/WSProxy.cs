using PUConnector.Api;
using PUConnector.Commons.WCF;
using PUConnector.OrdersManager;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace PUConnector.SoapProxy.Service
{
    /// <summary>
    /// Hostuje (uruchamia) usługi sieciowe WebService (soap) zdefiniowane w kontraktach <see cref="IConnector"/> oraz <see cref="IManager"/>
    /// </summary>
    public class WSProxy
    {
        // variables

        private ServiceHost connectorHost;
        private Connector connectorInstance;
        private string connectorEndpointAddress;

        private ServiceHost managerHost;
        private Manager managerInstance;
        private string managerEndpointAddress;

        private bool publishServiceMetadata;


        // constructors

        /// <summary>
        /// Domyślny konstruktor powodujący odczyt parametrów konfiguracyjnych z pliku .config
        /// </summary>
        public WSProxy()
        {
        }


        /// <summary>
        /// Sparameteryzowany konstruktor nie wymagający konfiguracji w pliku .config
        /// </summary>
        /// <param name="connectorInstance">Instancja obiektu typu <typeparamref name="Connector"/></param>
        /// <param name="connectorEndpointAddress">Adres Url pod którym uruchomina zostanie usługa dla kontraktu Connector</param>
        /// <param name="managerInstance">Instancja obiektu typu <typeparamref name="Manager"/></param>
        /// <param name="managerEndpointAddress">Adres Url pod którym uruchomiona zostanie usługa dla kontraktu Manager</param>
        /// <param name="writeExceptionsToEventLog">Zapisywanie błędów (wyjątków) w systemowym dzienniku zdarzeń EventLog</param>
        /// <param name="publishServiceMetadata">Publikowanie pod adresem Url usługi strony web opisującej usługę</param>
        public WSProxy(
            Connector connectorInstance,
            string connectorEndpointAddress,
            Manager managerInstance,
            string managerEndpointAddress,
            bool writeExceptionsToEventLog = false,
            bool publishServiceMetadata = true
            )
        {
            ErrorHandler.WriteExceptionsToEventLog = writeExceptionsToEventLog;

            this.connectorInstance = connectorInstance;
            this.connectorEndpointAddress = connectorEndpointAddress;

            this.managerInstance = managerInstance;
            this.managerEndpointAddress = managerEndpointAddress;

            this.publishServiceMetadata = publishServiceMetadata;
        }


        // public methods

        /// <summary>
        /// Uruchamia usługę WebService Proxy
        /// </summary>
        public void Start()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            if (string.IsNullOrEmpty(this.connectorEndpointAddress) && 
                string.IsNullOrEmpty(this.managerEndpointAddress))
            {
                Connector connector = new Connector();
                this.connectorHost =
                    new ServiceHost(connector, new Uri[] { });
                this.connectorHost.Open();

                Manager manager = new Manager();
                this.managerHost =
                    new ServiceHost(manager, new Uri[] { });
                this.managerHost.Open(); 
                
                return;
            }

            if (this.connectorInstance != null)
            {
                this.connectorHost =
                    new ServiceHost(this.connectorInstance, new Uri[] { new Uri(this.connectorEndpointAddress) });

                if (this.connectorHost.Description.Behaviors.Find<ErrorHandler>() == null)
                    this.connectorHost.Description.Behaviors.Add(new ErrorHandler());

                if (this.publishServiceMetadata)
                {
                    ServiceMetadataBehavior smb = 
                        this.connectorHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
                    if (smb == null) smb = new ServiceMetadataBehavior();
                    smb.HttpGetEnabled = true;
                    smb.HttpsGetEnabled = true;
                    this.connectorHost.Description.Behaviors.Add(smb);
                }

                BasicHttpBinding connectorBinding = new BasicHttpBinding();
                bool ssl =
                    connectorEndpointAddress.StartsWith("https:", StringComparison.OrdinalIgnoreCase);
                if (ssl)
                {
                    connectorBinding.Security.Mode = BasicHttpSecurityMode.Transport;
                    connectorBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                }

                ServiceEndpoint connectorEndpoint =
                    this.connectorHost.AddServiceEndpoint(typeof(IConnector), connectorBinding, this.connectorEndpointAddress);

                this.connectorHost.Open();
            }


            if (this.managerInstance != null)
            {
                this.managerHost =
                    new ServiceHost(this.managerInstance, new Uri[] { new Uri(this.managerEndpointAddress) });

                if (this.managerHost.Description.Behaviors.Find<ErrorHandler>() == null)
                    this.managerHost.Description.Behaviors.Add(new ErrorHandler());

                if (this.publishServiceMetadata)
                {
                    ServiceMetadataBehavior smb =
                        this.managerHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
                    if (smb == null) smb = new ServiceMetadataBehavior();
                    smb.HttpGetEnabled = true;
                    smb.HttpsGetEnabled = true;
                    this.managerHost.Description.Behaviors.Add(smb);
                }

                BasicHttpBinding managerBinding = new BasicHttpBinding();
                bool ssl =
                    managerEndpointAddress.StartsWith("https:", StringComparison.OrdinalIgnoreCase);
                if (ssl)
                {
                    managerBinding.Security.Mode = BasicHttpSecurityMode.Transport;
                    managerBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                }

                ServiceEndpoint managerEndpoint =
                    this.managerHost.AddServiceEndpoint(typeof(IManager), managerBinding, this.managerEndpointAddress);

                this.managerHost.Open();
            }
        }


        /// <summary>
        /// Zatrzymuje usługę WebService Proxy
        /// </summary>
        public void Stop()
        {
            if (this.connectorHost != null &&
                this.connectorHost.State == CommunicationState.Opened)
                this.connectorHost.Close();

            if (this.managerHost != null &&
                this.managerHost.State == CommunicationState.Opened)
                this.managerHost.Close();
        }


        // private methods

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            new ErrorHandler().HandleError(e.ExceptionObject as Exception);
        }
    }
}
