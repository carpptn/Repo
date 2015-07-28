
namespace PUConnector.OrdersManager.Exceptions
{
    /// <summary>
    /// Wyjątek próby dostępu do nieistniejącego rekordu w tabeli [PUOrder]
    /// </summary>
    public class OrderNotFoundException : OrdersManagerException
    {
        /// <summary>
        /// Tworzy obiekt wyjątku
        /// </summary>
        /// <param name="message">Komunikat błędu</param>
        public OrderNotFoundException(string message)
            : base(message)
        {
        }
    }
}
