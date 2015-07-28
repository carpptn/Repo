using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Komunikat powiadomienia o zmianie statusu zamówienia
    /// </summary>
    [DataContract]
    public class OrderNotification
    {
        /// <summary>
        /// Obiekt zamówienia
        /// </summary>
        [DataMember(Name = "order")]
        public OrderRecord Order { get; set; }

        /// <summary>
        /// Dodakowe właściwości
        /// </summary>
        [DataMember(Name = "properties")]
        public PropertyRecord[] Properties { get; set; }
    }
}