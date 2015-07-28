using System;
using System.ServiceProcess;

namespace PUConnector.Notifications.Host
{
    static class Program
    {
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                using (Service service = new Service())
                {
                    service.OnStartDebug(null);
                    Console.WriteLine("Press ENTER to stop...");
                    Console.ReadLine();
                    service.OnStopDebug();
                }
            }
            else
            {
                ServiceBase[] servicesToRun;
                servicesToRun = new ServiceBase[] { new Service() };
                ServiceBase.Run(servicesToRun);
            }
        }
    }
}
