using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace PUConnector.Commons.WCF
{
    /// <summary>
    /// Delegat dla funkcji handlera przechwyconych błędów (wyjątków)
    /// </summary>
    /// <param name="error">Obiekt wyjątku</param>
    /// <returns>Znacznik obsłużenia błędu przez handler</returns>
    public delegate bool HandleErrorHandler(Exception error);


    /// <summary>
    /// Rozszerzenie zachowania (Behavior) usługi WCF o funkcjonalność obsługi błędów
    /// </summary>
    public class ErrorHandler : IErrorHandler, IServiceBehavior
    {
        // events

        /// <summary>
        /// Zdarzenie przechwycenia błędu (wyjątku) w aplikacji
        /// </summary>
        public static event HandleErrorHandler OnHandleError;


        // properties

        /// <summary>
        /// Włącza lub wyłącza zapisywanie przechwyconych błędów w systemowym dzienniku zdarzeń EventLog
        /// </summary>
        public static bool WriteExceptionsToEventLog { get; set; }


        // public methods

        /// <summary>
        /// Zapisuje treść wyjątku w systemowym dzienniku zdarzeń EventLog
        /// </summary>
        /// <param name="e">Obiekt wyjątku</param>
        public static void WriteExceptionToEventLog(Exception e)
        {
            if (e != null)
            {
                string message =
                    string.Format("Unhandled exception {0}: {1} {2}",
                    e.GetType().Name,
                    e.Message,
                    e.StackTrace
                    );

                TraceSource ts = new TraceSource("PUConnector");
                ts.TraceEvent(TraceEventType.Error, 99, message);
                
                if (ErrorHandler.WriteExceptionsToEventLog)
                {
                    if (EventLog.SourceExists("PUConnector"))
                        EventLog.WriteEntry("PUConnector", message, EventLogEntryType.Error, 1);
                }
            }
        }


        /// <summary>
        /// Aplikuje zachowanie usługi
        /// </summary>
        /// <param name="serviceDescription">Obiekt opisu usługi</param>
        /// <param name="serviceHostBase">Obiekt hostowania usługi</param>
        public void ApplyDispatchBehavior(
            ServiceDescription serviceDescription, 
            ServiceHostBase serviceHostBase
            )
        {
            IErrorHandler errorHandler = new ErrorHandler();

            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;

                if (channelDispatcher != null)
                {
                    channelDispatcher.ErrorHandlers.Add(errorHandler);
                }
            }
        }


        /// <summary>
        /// Funkcja  przechwytywania błędu (wyjątku)
        /// </summary>
        /// <param name="error">Obiekt wyjątku</param>
        /// <returns>Znacznik obsłużenia błędu</returns>
        public bool HandleError(Exception error)
        {
            ErrorHandler.WriteExceptionToEventLog(error);

            if (ErrorHandler.OnHandleError != null)
                return ErrorHandler.OnHandleError(error);

            return true;
        }


        /// <summary>
        /// Zwraca komunikat błędu usługi dla przechwyconego błędu (wyjątku)
        /// </summary>
        /// <param name="error">Obiekt wyjątku</param>
        /// <param name="version">Obiekt wersji komunikatu</param>
        /// <param name="fault">Zwracany komunikat błędu</param>
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            FaultException faultException = new FaultException(
                "Server error encountered: " + error.GetType().Name + ": " + error.Message
                );

            MessageFault messageFault = faultException.CreateMessageFault();

            fault = Message.CreateMessage(version, messageFault, faultException.Action);
        }


        public void AddBindingParameters(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters
            )
        {
        }


        public void Validate(
            ServiceDescription serviceDescription, 
            ServiceHostBase serviceHostBase
            )
        {
        }
    }
}
