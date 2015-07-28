using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PUConnector.Commons.ApiModel.Enums;
using System;
using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Szczegółowe dane na temat uznania
    /// </summary>
    [DataContract]
    public class RefundRecord_Type
    {
        /// <summary>
        /// Identyfikator zwrotu
        /// </summary>
        [DataMember(Name = "refundId")]
        public string RefundId { get; set; }

        /// <summary>
        /// Zewnetrzny identyfikator zwrotu nadany w komunikacie RefundCreateRequest
        /// </summary>
        [DataMember(Name = "extRefundId")]
        public string ExtRefundId { get; set; }

        /// <summary>
        /// Kwota uznania
        /// </summary>
        [DataMember(Name = "amount")]
        public long? Amount { get; set; }

        /// <summary>
        /// Kod waluty (ISO 4217)
        /// </summary>
        [DataMember(Name = "currencyCode")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Opis wykonywanego uznania
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Data utworzenia zwrotu
        /// </summary>
        [DataMember(Name = "creationDateTime")]
        public DateTime CreationDateTime { get; set; }

        /// <summary>
        /// Kod statusu przetwarzania (PENDING, CANCELED, FINALIZED)
        /// </summary>
        [DataMember(Name = "status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RefundStatuses Status { get; set; }

        /// <summary>
        /// Data statusu
        /// </summary>
        [DataMember(Name = "statusDateTime")]
        public DateTime StatusDateTime { get; set; }
    }
}