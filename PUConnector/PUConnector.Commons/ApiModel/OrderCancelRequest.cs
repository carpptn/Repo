using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Komunikat żądania anulowania zamówienia
    /// </summary>
    [DataContract]
    public class OrderCancelRequest
    {
        // constructors

        /// <summary>
        /// Tworzy obiekt komunikatu OrderCancelRequest
        /// </summary>
        /// <param name="orderId">Identyfikator anulowanego zamówienia</param>
        public OrderCancelRequest(string orderId)
        {
            this.OrderId = orderId;
        }

        // properties

        /// <summary>
        /// Identyfikator anulowanego zamówienia
        /// </summary>
        [DataMember(Name = "orderId", IsRequired = true)]
        public string OrderId { get; set; }
    }
}