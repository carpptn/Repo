using System;
using System.ServiceModel.Configuration;

namespace PUConnector.Commons.WCF
{
    /// <summary>
    /// Rozszerzenie konfiguracji zachowania (Behavior) usługi WCF o funkcjonalność obsługi błędów
    /// </summary>
    public class ErrorHandlerElement : BehaviorExtensionElement
    {
        // public methods

        /// <summary>
        /// Zwraca typ implementujacy funkcjonalność zachowania
        /// </summary>
        public override Type BehaviorType
        {
            get
            {
                return typeof(ErrorHandler);
            }
        }


        // protected methods

        protected override object CreateBehavior()
        {
            return new ErrorHandler();
        }
    }
}
