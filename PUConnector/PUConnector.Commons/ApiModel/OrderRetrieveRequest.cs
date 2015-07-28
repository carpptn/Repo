using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Komunikat żądania aktualnych danych zamówienia
    /// </summary>
    [DataContract]
    public class OrderRetrieveRequest
    {
        // constructor

        /// <summary>
        /// Tworzy obiekt komunikatu OrderRetrieveRequest
        /// </summary>
        /// <param name="orderId">Identyfikator zamówienia w systemie PayU</param>
        public OrderRetrieveRequest(string orderId)
        {
            this.OrderId = orderId;
        }

        // properties

        /// <summary>
        /// Identyfikator zamówienia w systemie PayU
        /// </summary>
        [DataMember(Name = "orderId", IsRequired = true)]
        public string OrderId { get; set; }
    }
}