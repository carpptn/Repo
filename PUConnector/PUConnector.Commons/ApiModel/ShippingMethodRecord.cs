using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Informacje o metodach dostawy
    /// </summary>
    [DataContract]
    public class ShippingMethodRecord
    {
        // constructors

        /// <summary>
        /// Tworzy obiekt typu ShippingMethodRecord
        /// </summary>
        /// <param name="country">Kod kraju</param>
        /// <param name="price">Koszt</param>
        /// <param name="name">Nazwa sposobu dostawy</param>
        public ShippingMethodRecord(
            string country,
            long price,
            string name
            )
        {
            this.Country = country;
            this.Price = price;
            this.Name = name;
        }

        // properties

        /// <summary>
        /// Kod kraju
        /// </summary>
        [DataMember(Name = "country", IsRequired = true)]
        public string Country { get; set; }

        /// <summary>
        /// Koszt
        /// </summary>
        [DataMember(Name = "price", IsRequired = true)]
        public long Price { get; set; }

        /// <summary>
        /// Nazwa sposobu dostawy
        /// </summary>
        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; set; }
    }
}