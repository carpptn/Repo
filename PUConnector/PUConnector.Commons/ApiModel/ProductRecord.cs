using System.Runtime.Serialization;

namespace PUConnector.Commons.ApiModel
{
    /// <summary>
    /// Szczegóły produktu
    /// </summary>
    [DataContract]
    public class ProductRecord
    {
        // constructors

        /// <summary>
        /// Tworzy obiektu typu ProductRecord
        /// </summary>
        /// <param name="name">Nazwa</param>
        /// <param name="unitPrice">Cena jednostkowa</param>
        /// <param name="quantity">Ilość sztuk</param>
        public ProductRecord(
            string name,
            long unitPrice,
            long quantity
            )
        {
            this.Name = name;
            this.UnitPrice = unitPrice;
            this.Quantity = quantity;
        }

        // properties

        /// <summary>
        /// Nazwa
        /// </summary>
        [DataMember(Name = "name", IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// Cena jednostkowa
        /// </summary>
        [DataMember(Name = "unitPrice", IsRequired = true)]
        public long UnitPrice { get; set; }

        /// <summary>
        /// Ilość sztuk
        /// </summary>
        [DataMember(Name = "quantity", IsRequired = true)]
        public long Quantity { get; set; }
    }
}