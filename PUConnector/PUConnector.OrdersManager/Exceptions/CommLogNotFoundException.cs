
namespace PUConnector.OrdersManager.Exceptions
{
    /// <summary>
    /// Wyjątek próby dostępu do nieistniejącego rekordu w tabeli [PUCommLog]
    /// </summary>
    public class CommLogNotFoundException : OrdersManagerException
    {
        /// <summary>
        /// Tworzy obiekt wyjątku
        /// </summary>
        /// <param name="message">Komunikat błędu</param>
        public CommLogNotFoundException(string message)
            : base(message)
        {
        }
    }
}
