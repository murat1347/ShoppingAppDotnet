using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Models
{
    /// <summary>
    /// Product is the core Entity in this application, everyother is dependent on this.
    /// </summary>
    public class Product : BaseEntity
    {
        /// <summary>
        /// Name of the Product.
        /// Indexed.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ImageURL of the product.
        /// </summary>  
        public string ImageUrl { get; set; }

        public decimal Stock { get; set; }

        /// <summary>
        /// Category Id of the product.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Category of the product.
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Number of purchases made with this product.
        /// </summary>
        public virtual IList<Purchase> Purchases { get; set; }

        /// <summary>
        /// Number of sales made with this product.
        /// </summary>
        public virtual IList<Sale> Sales { get; set; }
    }
}
