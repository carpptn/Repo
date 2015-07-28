using PUConnector.SoapProxy.Service;
using System;
using System.ServiceModel;
using System.ServiceProcess;

namespace PUConnector.SoapProxy.Host
{
    public partial class Service : ServiceBase
    {
        // variables

        private WSProxy wsProxy;


        // constructors

        public Service()
        {
            InitializeComponent();
        }


        // public methods

        public void OnStartDebug(string[] args)
        {
            this.OnStart(args);
        }


        public void OnStopDebug()
        {
            this.OnStop();
        }


        // protected methods

        protected override void OnStart(string[] args)
        {
            this.wsProxy = new WSProxy();
            this.wsProxy.Start();
        }


        protected override void OnStop()
        {
            this.wsProxy.Stop();
        }
    }
}
