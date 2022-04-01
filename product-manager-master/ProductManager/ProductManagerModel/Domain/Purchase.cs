using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Models
{
    /// <summary>
    /// Purchase is record of the purchase of a product.
    /// </summary>
    public class Purchase : BaseEntity
    {
        /// <summary>
        /// The product Id of the product which has purchased.
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Seller Id of seller of the purchase.
        /// </summary>
        public int SellerId { get; set; }

        /// <summary>
        /// The product which has purchased.
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Seller of the purchase. Purchased product whom?
        /// </summary>
        public virtual Seller Seller { get; set; }

        /// <summary>
        /// Amount of the product.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Cost of the purchase.
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// When the purchase happened.
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
