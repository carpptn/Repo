using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PUConnector.Commons.ApiModel.Enums;
using System;
using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Szczegóły zamówienia
    /// </summary>
    [DataContract]
    public class OrderRecord
    {
        /// <summary>
        /// Identyfikator zamówienia nadany przez system PayU
        /// </summary>
        [DataMember(Name = "orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Zewnętrzny identyfikator zamówienia (nadawany przez sklep)
        /// </summary>
        [DataMember(Name = "extOrderId")]
        public string ExtOrderId { get; set; }

        /// <summary>
        /// Znacznik czasu dla utworzenia zamówienia
        /// </summary>
        [DataMember(Name = "orderCreateDate")]
        public DateTime OrderCreateDate { get; set; }

        /// <summary>
        /// Adres pod który będą przesyłane powiadomienia
        /// </summary>
        [DataMember(Name = "notifyUrl")]
        public string NotifyUrl { get; set; }

        /// <summary>
        /// URL zamówienia na stronie sklepu
        /// </summary>
        [DataMember(Name = "orderUrl")]
        public string OrderUrl { get; set; }

        /// <summary>
        /// IP kupującego
        /// </summary>
        [DataMember(Name = "customerIp")]
        public string CustomerIp { get; set; }

        /// <summary>
        /// Identyfikator punktu płatności na którym zostanie wykonana płatność
        /// </summary>
        [DataMember(Name = "merchantPosId")]
        public string MerchantPosId { get; set; }

        /// <summary>
        /// Czas w trakcie którego możliwe jest dokończenie zamówienia w sekundach
        /// </summary>
        [DataMember(Name = "validityTime")]
        public string ValidityTime { get; set; }

        /// <summary>
        /// Opis wykonywanego uznania
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Dodatkowy opis zamówienia
        /// </summary>
        [DataMember(Name = "additionalDescription")]
        public string AdditionalDescription { get; set; }

        /// <summary>
        /// Symbol waluty używanej przez sklep w standardzie ISO 4217, np. "PLN"
        /// </summary>
        [DataMember(Name = "currencyCode")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Całkowity koszt zamówienia
        /// </summary>
        [DataMember(Name = "totalAmount")]
        public long TotalAmount { get; set; }

        /// <summary>
        /// Status zamówienia
        /// </summary>
        [DataMember(Name = "status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatuses Status { get; set; }

        /// <summary>
        /// Sekcja przechowująca dane kupującego
        /// </summary>
        [DataMember(Name = "buyer")]
        public BuyerRecord Buyer { get; set; }

        /// <summary>
        /// Sekcja zawiera dane o produktach
        /// </summary>
        [DataMember(Name = "products")]
        public ProductRecord[] Products { get; set; }

        /// <summary>
        /// Sekcja zawiera informacje o metodach dostawy
        /// </summary>
        [DataMember(Name = "shippingMethods")]
        public ShippingMethodRecord[] ShippingMethods { get; set; }
    }
}