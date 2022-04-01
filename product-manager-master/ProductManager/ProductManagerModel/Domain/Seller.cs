using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Models
{
    /// <summary>
    /// Seller is who we need to buy products.
    /// </summary>
    public class Seller : Person
    {
        /// <summary>
        /// Total purchases made from particilar seller.
        /// </summary>
        public virtual IList<Purchase> Sales { get; set; }
    }
}
