using ProductManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerDTO.DTO.Product
{
    /// <summary>
    /// Only used for returning paged products.
    /// Response only.
    /// </summary>
    public class PurchasePagedDTO
    {
        /// <summary>
        /// List of Customers;
        /// </summary>
        public IList<PurchaseDTO> Purchases { get; set; }

        /// <summary>
        /// Total count of the Customers in the storage.
        /// </summary>
        public long Count { get; set; }
    }
}
