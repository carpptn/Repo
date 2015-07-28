using PUConnector.Notifications.Service;
using System;
using System.ServiceModel;
using System.ServiceProcess;

namespace PUConnector.Notifications.Host
{
    public partial class Service : ServiceBase
    {
        // variables

        private Receiver receiver;
        
        
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
            this.receiver = new Receiver();
            this.receiver.Start();
        }


        protected override void OnStop()
        {
            this.receiver.Stop();
        }
    }
}
