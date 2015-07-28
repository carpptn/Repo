using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Dodatkowe metadane
    /// </summary>
    [DataContract]
    public class PropertyRecord
    {
        /// <summary>
        /// Nazwa klucza
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Wartość klucza
        /// </summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}