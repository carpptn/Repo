using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Komunikat odpowiedzi na żądanie anulowania zamówienia
    /// </summary>
    [DataContract]
    public class OrderCancelResponse
    {
        /// <summary>
        /// Identyfikator anulowanego zamówienia
        /// </summary>
        [DataMember(Name = "orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Zewnętrzny identyfikator zamówienia (nadawany przez sklep)
        /// </summary>
        [DataMember(Name = "extOrderId")]
        public string ExtOrderId { get; set; }

        /// <summary>
        /// Obiekt typu status odpowiedzi
        /// </summary>
        [DataMember(Name = "status")]
        public StatusRecord Status { get; set; }
    }
}