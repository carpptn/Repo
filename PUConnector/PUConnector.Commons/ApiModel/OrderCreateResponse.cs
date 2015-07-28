using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Komunikat odpowiedzi na żądanie utworzenia nowego zamówienia
    /// </summary>
    [DataContract]
    public class OrderCreateResponse
    {
        /// <summary>
        /// Adres pod który należy przekierować kupującego
        /// </summary>
        [DataMember(Name = "redirectUri")]
        public string RedirectUri { get; set; }

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
        /// Obiekt statusu odpowiedzi
        /// </summary>
        [DataMember(Name = "status")]
        public StatusRecord Status { get; set; }
    }
}