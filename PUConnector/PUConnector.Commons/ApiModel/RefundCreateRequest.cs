using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Komunikat żądania utworzenia nowego zwrotu
    /// </summary>
    [DataContract]
    public class RefundCreateRequest
    {
        // constructors

        /// <summary>
        /// Tworzy obiekt komunikatu RefundCreateRequest
        /// </summary>
        /// <param name="orderId">Identyfikator zamówienia dla którego będzie realizowany zwrot</param>
        /// <param name="refund">Szczegółowe dane na temat uznania</param>
        public RefundCreateRequest(
            string orderId,
            RefundInfoType refund
            )
        {
            this.OrderId = orderId;
            this.Refund = refund;
        }

        // properties

        /// <summary>
        /// Identyfikator zamówienia dla którego będzie realizowany zwrot
        /// </summary>
        [DataMember(Name = "orderId", IsRequired = true)]
        public string OrderId { get; set; }

        /// <summary>
        /// Szczegółowe dane na temat uznania
        /// </summary>
        [DataMember(Name = "refund", IsRequired = true)]
        public RefundInfoType Refund { get; set; }
    }
}