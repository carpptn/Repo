using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Komunikat odpowiedzi na żądanie utworzenia nowego zwrotu
    /// </summary>
    [DataContract]
    public class RefundCreateResponse
    {
        /// <summary>
        /// Identyfikator zamówienia dla którego będzie realizowany zwrot
        /// </summary>
        [DataMember(Name = "orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Szczegółowe dane na temat uznania
        /// </summary>
        [DataMember(Name = "refund")]
        public RefundRecord_Type Refund { get; set; }

        /// <summary>
        /// Obiekt typu status odpowiedzi
        /// </summary>
        [DataMember(Name = "status")]
        public StatusRecord Status { get; set; }
    }
}