using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Komunikat odpowiedzi na żądanie aktualnych danych zamówienia
    /// </summary>
    [DataContract]
    public class OrderRetrieveResponse
    {
        /// <summary>
        /// Sekcja przechowująca dane zamówienia
        /// </summary>
        [DataMember(Name = "orders")]
        public OrderRecord[] Orders { get; set; }

        /// <summary>
        /// Obiekt statusu odpowiedzi
        /// </summary>
        [DataMember(Name = "status")]
        public StatusRecord Status { get; set; }

        /// <summary>
        /// Dodatkowe właściwości
        /// </summary>
        [DataMember(Name = "properties")]
        public PropertyRecord[] Properties { get; set; }
    }
}