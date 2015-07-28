
namespace PUConnector.OrdersManager.Exceptions
{
    /// <summary>
    /// Wyjątek próby dostępu do nieistniejącego rekordu w tabeli [PURefund]
    /// </summary>
    public class RefundNotFoundException : OrdersManagerException
    {
        /// <summary>
        /// Tworzy obiekt wyjątku
        /// </summary>
        /// <param name="message">Komunikat błędu</param>
        public RefundNotFoundException(string message)
            : base(message)
        {
        }
    }
}
