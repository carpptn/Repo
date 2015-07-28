using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PUConnector.Commons.ApiModel.Enums;
using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Status odpowiedzi
    /// </summary>
    [DataContract]
    public class StatusRecord
    {
        /// <summary>
        /// Kod odpowiedzi
        /// </summary>
        [DataMember(Name = "statusCode")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ResponseStatuses StatusCode { get; set; }

        /// <summary>
        /// Opis statusu odpowiedzi
        /// </summary>
        [DataMember(Name = "statusDesc")]
        public string StatusDesc { get; set; }

        /// <summary>
        /// Waga błędu
        /// </summary>
        [DataMember(Name = "severity")]
        public string Severity { get; set; }

        /// <summary>
        /// Kod błędu
        /// </summary>
        [DataMember(Name = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Opis kodu błędu
        /// </summary>
        [DataMember(Name = "codeLiteral")]
        public string CodeLiteral { get; set; }
    }
}