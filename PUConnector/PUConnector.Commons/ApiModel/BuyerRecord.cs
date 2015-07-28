using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Dane kupującego
    /// </summary>
    [DataContract]
    public class BuyerRecord
    {
        // constructors

        /// <summary>
        /// Tworzy obiekt typu BuyerRecord
        /// </summary>
        /// <param name="email">Adres email kupującego</param>
        public BuyerRecord(string email)
        {
            this.Email = email;
        }

        // properties

        /// <summary>
        /// Id kupującego
        /// </summary>
        [DataMember(Name = "customerId")]
        public string CustomerId { get; set; }

        /// <summary>
        /// Identyfikator kupującego używany w systemie klienta
        /// </summary>
        [DataMember(Name = "extCustomerId")]
        public string ExtCustomerId { get; set; }

        /// <summary>
        /// Adres email kupującego
        /// </summary>
        [DataMember(Name = "email", IsRequired = true)]
        public string Email { get; set; }

        /// <summary>
        /// Numer telefonu
        /// </summary>
        [DataMember(Name = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Imię kupującego
        /// </summary>
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Nazwisko kupującego
        /// </summary>
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// PESEL lub zagraniczny ekwiwalent
        /// </summary>
        [DataMember(Name = "nin")]
        public string Nin { get; set; }

        /// <summary>
        /// Sekcja zawierająca dane adresowe do wysyłki towaru
        /// </summary>
        [DataMember(Name = "delivery")]
        public BuyerDeliveryRecord BuyerDelivery { get; set; }

        /// <summary>
        /// Sekcja zawierająca dane niezbędne do wystawienia rachunku
        /// </summary>
        [DataMember(Name = "billing")]
        public BuyerBillingRecord BuyerBilling { get; set; }

        /// <summary>
        /// Sekcja zawierająca dane do wystawienia faktury
        /// </summary>
        [DataMember(Name = "invoice")]
        public BuyerInvoiceRecord BuyerInvoice { get; set; }
    }
}