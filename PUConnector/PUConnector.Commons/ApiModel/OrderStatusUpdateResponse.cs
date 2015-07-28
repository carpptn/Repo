using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Komunikat odpowiedzi na żądanie aktualizacji statusu zamówienia
    /// </summary>
    [DataContract]
    public class OrderStatusUpdateResponse
    {
        /// <summary>
        /// Obiekt typu status odpowiedzi
        /// </summary>
        [DataMember(Name = "status")]
        public StatusRecord Status { get; set; }
    }
}