using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Dane do wystawienia rachunku
    /// </summary>
    [DataContract]
    public class BuyerBillingRecord
    {
        // constructors

        /// <summary>
        /// Tworzy rekord typu BuyerBillingRecord
        /// </summary>
        /// <param name="street">Ulica</param>
        /// <param name="postalCode">Kod pocztowy</param>
        /// <param name="city">Miasto</param>
        /// <param name="countryCode">Kod kraju</param>
        /// <param name="recipientName">Nazwisko odbiorcy</param>
        public BuyerBillingRecord(
            string street,
            string postalCode,
            string city,
            string countryCode,
            string recipientName
            )
        {
            this.Street = street;
            this.PostalCode = postalCode;
            this.City = city;
            this.CountryCode = countryCode;
            this.RecipientName = recipientName;
        }

        // properties

        /// <summary>
        /// Ulica
        /// </summary>
        [DataMember(Name = "street", IsRequired = true)]
        public string Street { get; set; }

        /// <summary>
        /// Skrytka pocztowa
        /// </summary>
        [DataMember(Name = "postalBox")]
        public string PostalBox { get; set; }

        /// <summary>
        /// Kod pocztowy
        /// </summary>
        [DataMember(Name = "postalCode", IsRequired = true)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Miasto
        /// </summary>
        [DataMember(Name = "city", IsRequired = true)]
        public string City { get; set; }

        /// <summary>
        /// Województwo
        /// </summary>
        [DataMember(Name = "state")]
        public string State { get; set; }

        /// <summary>
        /// Kod kraju
        /// </summary>
        [DataMember(Name = "countryCode", IsRequired = true)]
        public string CountryCode { get; set; }

        /// <summary>
        /// Nazwa adresu w systemie PayU
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Nazwisko odbiorcy
        /// </summary>
        [DataMember(Name = "recipientName", IsRequired = true)]
        public string RecipientName { get; set; }

        /// <summary>
        /// Adres email odbiorcy
        /// </summary>
        [DataMember(Name = "recipientEmail")]
        public string RecipientEmail { get; set; }

        /// <summary>
        /// Numer telefonu odbiorcy
        /// </summary>
        [DataMember(Name = "recipientPhone")]
        public string RecipientPhone { get; set; }
    }
}