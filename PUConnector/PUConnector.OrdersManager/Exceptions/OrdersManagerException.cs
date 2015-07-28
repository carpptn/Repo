
using System;

namespace PUConnector.OrdersManager.Exceptions
{
    /// <summary>
    /// Klasa bazowa wyjątków specjalnych biblioteki PUConnector.OrdersManager
    /// </summary>
    public class OrdersManagerException : Exception
    {
        /// <summary>
        /// Tworzy obiekt wyjątku
        /// </summary>
        /// <param name="message">Komunikat błędu</param>
        public OrdersManagerException(string message)
            : base(message)
        {
        }
    }
}
