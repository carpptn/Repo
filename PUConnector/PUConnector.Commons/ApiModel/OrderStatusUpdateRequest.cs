using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PUConnector.Commons.ApiModel.Enums;
using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Komunikat żądania aktualizacji statusu zamówienia
    /// </summary>
    [DataContract]
    public class OrderStatusUpdateRequest
    {
        // constructors

        /// <summary>
        /// Tworzy obiekt komunikatu OrderStatusUpdateRequest
        /// </summary>
        /// <param name="orderId">Identyfikator zamówienia w systemie PayU</param>
        /// <param name="orderStatus">Nowy status zamówienia. Możliwa wartość: COMPLETED</param>
        public OrderStatusUpdateRequest(
            string orderId,
            OrderStatuses orderStatus = OrderStatuses.COMPLETED
            )
        {
            this.OrderId = orderId;
            this.OrderStatus = orderStatus;
        }

        // properties

        /// <summary>
        /// Identyfikator zamówienia w systemie PayU
        /// </summary>
        [DataMember(Name = "orderId", IsRequired = true)]
        public string OrderId { get; set; }

        /// <summary>
        /// Nowy status zamówienia. Możliwa wartość: COMPLETED
        /// </summary>
        [DataMember(Name = "orderStatus", IsRequired = true)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatuses OrderStatus { get; set; }
    }
}