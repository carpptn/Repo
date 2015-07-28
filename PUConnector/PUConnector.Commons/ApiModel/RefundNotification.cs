using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Komunikat powiadomienia o zmianie statusu zwrotu
    /// </summary>
    [DataContract]
    public class RefundNotification
    {
        /// <summary>
        /// Identyfikator zamówienia nadany przez system PayU
        /// </summary>
        [DataMember(Name = "orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Zewnętrzny identyfikator zamówienia (nadawany przez sklep)
        /// </summary>
        [DataMember(Name = "extOrderId")]
        public string ExtOrderId { get; set; }

        /// <summary>
        /// Szczegółowe dane na temat uznania
        /// </summary>
        [DataMember(Name = "refund")]
        public RefundRecord_Type Refund { get; set; }

        /// <summary>
        /// Dodatkowe metadane
        /// </summary>
        [DataMember(Name = "properties")]
        public PropertyRecord[] Properties { get; set; }
    }
}