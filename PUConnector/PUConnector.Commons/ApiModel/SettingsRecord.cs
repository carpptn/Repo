using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Dodatkowe ustawienia
    /// </summary>
    [DataContract]
    public class SettingsRecord
    {
        // properties

        /// <summary>
        /// Ustawienie wartości true umożliwia usunięcie przełącznika "Chcę otrzymać fakturę" ze strony płatności
        /// </summary>
        [DataMember(Name = "invoiceDisabled")]
        public bool InvoiceDisabled { get; set; }
    }
}