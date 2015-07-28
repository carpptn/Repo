using System;
using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Komunikat żądania utworzenia nowego zamówienia
    /// </summary>
    [DataContract]
    public class OrderCreateRequest
    {
        // constructors

        /// <summary>
        /// Tworzy obiekt komunikatu OrderCreateRequest
        /// </summary>
        /// <param name="notifyUrl">Adres pod który będą przesyłane powiadomienia</param>
        /// <param name="customerIp">Adres IP kupującego</param>
        /// <param name="description">Opis zamówienia</param>
        /// <param name="currencyCode">Symbol waluty używanej przez sklep w standardzie ISO 4217, np. "PLN".</param>
        /// <param name="totalAmount">Całkowity koszt zamówienia</param>
        /// <param name="products">Sekcja zawiera dane o produktach</param>
        public OrderCreateRequest(
            string notifyUrl,
            string customerIp,
            string description,
            string currencyCode,
            long totalAmount,
            params ProductRecord[] products
            )
        {
            this.NotifyUrl = notifyUrl;
            this.CustomerIp = customerIp;
            this.Description = description;
            this.CurrencyCode = currencyCode;
            this.TotalAmount = totalAmount;
            this.Products = products;
            this.ExtOrderId = Guid.NewGuid().ToString();
        }

        // properties

        /// <summary>
        /// Identyfikator żądania
        /// </summary>
        [DataMember(Name = "reqId")]
        public string ReqId { get; set; }

        /// <summary>
        /// Identyfikator początkowego żądania przy wysyłaniu sekwencji komunikatów
        /// </summary>
        [DataMember(Name = "refReqId")]
        public string RefReqId { get; set; }

        /// <summary>
        /// Identyfikator zamówienia w systemie sprzedawcy
        /// </summary>
        [DataMember(Name = "extOrderId")]
        public string ExtOrderId { get; set; }

        /// <summary>
        /// Adres pod który będą przesyłane powiadomienia
        /// </summary>
        [DataMember(Name = "notifyUrl", IsRequired = true)]
        public string NotifyUrl { get; set; }

        /// <summary>
        /// URL zamówienia na stronie sklepu
        /// </summary>
        [DataMember(Name = "orderUrl")]
        public string OrderUrl { get; set; }

        /// <summary>
        /// Adres IP kupującego
        /// </summary>
        [DataMember(Name = "customerIp", IsRequired = true)]
        public string CustomerIp { get; set; }

        /// <summary>
        /// Identyfikator punktu płatności na którym zostanie wykonana płatność
        /// </summary>
        [DataMember(Name = "merchantPosId", IsRequired = true)]
        public string MerchantPosId { get; set; }

        /// <summary>
        /// Czas w trakcie którego możliwe jest dokończenie zamówienia w sekundach
        /// </summary>
        [DataMember(Name = "validityTime")]
        public string ValidityTime { get; set; }

        /// <summary>
        /// Opis zamówienia
        /// </summary>
        [DataMember(Name = "description", IsRequired = true)]
        public string Description { get; set; }

        /// <summary>
        /// Dodatkowy opis zamówienia
        /// </summary>
        [DataMember(Name = "additionalDescription")]
        public string AdditionalDescription { get; set; }

        /// <summary>
        /// Symbol waluty używanej przez sklep w standardzie ISO 4217, np. "PLN".
        /// </summary>
        [DataMember(Name = "currencyCode", IsRequired = true)]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Całkowity koszt zamówienia
        /// </summary>
        [DataMember(Name = "totalAmount", IsRequired = true)]
        public long TotalAmount { get; set; }

        /// <summary>
        /// Adres na który kupujący zostanie przekierowany po zakończeniu procesu płatności. W przypadku braku autoryzacji do URL'a doklejany jest parametr error z wartością 501
        /// </summary>
        [DataMember(Name = "continueUrl")]
        public string ContinueUrl { get; set; }

        /// <summary>
        /// Dodatkowe ustawienia
        /// </summary>
        [DataMember(Name = "settings")]
        public SettingsRecord Settings { get; set; }

        /// <summary>
        /// Sekcja przechowująca dane kupującego
        /// </summary>
        [DataMember(Name = "buyer")]
        public BuyerRecord Buyer { get; set; }

        /// <summary>
        /// Sekcja zawiera informacje o metodach dostawy
        /// </summary>
        [DataMember(Name = "shippingMethods")]
        public ShippingMethodRecord[] ShippingMethods { get; set; }

        /// <summary>
        /// Sekcja zawiera dane o produktach
        /// </summary>
        [DataMember(Name = "products", IsRequired = true)]
        public ProductRecord[] Products { get; set; }
    }
}