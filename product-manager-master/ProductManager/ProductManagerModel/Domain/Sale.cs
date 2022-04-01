using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Models
{
    /// <summary>
    /// Purchase is record of the sale of a product.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// The product Id of the product which has purchased.
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// The product which has purchased.
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Sale to which Customer Id.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Sale to which Customer.
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Amount of sale
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Income of the sale.
        /// </summary>
        public decimal Income { get; set; }

        /// <summary>
        /// When the sale happened.
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
