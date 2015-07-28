using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PUConnector.Commons.ApiModel.Enums;
using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Szczegółowe dane na temat uznania
    /// </summary>
    [DataContract]
    public class RefundInfoType
    {
        // constructors

        /// <summary>
        /// Tworzy obiekt typu RefundInfoType
        /// </summary>
        /// <param name="description">Opis wykonywanego uznania</param>
        public RefundInfoType(string description)
        {
            this.Description = description;
        }

        // properties

        /// <summary>
        /// Opis wykonywanego uznania
        /// </summary>
        [DataMember(Name = "description", IsRequired = true)]
        public string Description { get; set; }

        /// <summary>
        /// Kwota uznania. Jeśli zostanie pusta, zostanie wykonany zwrot całości zapłaconych środków
        /// </summary>
        [DataMember(Name = "amount")]
        public long? Amount { get; set; }

        /// <summary>
        /// Identyfikator zwrotu w systemie klienta, unikalny w obrębie zamówienia
        /// </summary>
        [DataMember(Name = "extRefundId")]
        public string ExtRefundId { get; set; }

        /// <summary>
        /// Tytuł przelewu bankowego
        /// </summary>
        [DataMember(Name = "bankDescription")]
        public string BankDescription { get; set; }

        /// <summary>
        /// Typ operacji (możliwa wartość: REFUND_PAYMENT_STANDARD)
        /// </summary>
        [DataMember(Name = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RefundTypes? Type { get; set; }
    }
}