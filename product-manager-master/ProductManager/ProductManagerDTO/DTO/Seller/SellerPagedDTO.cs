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
    public class SellerPagedDTO
    {
        /// <summary>
        /// List of Sellers
        /// </summary>
        public IList<SellerDTO> Sellers { get; set; }

        /// <summary>
        /// Total count of the Sellers in the storage.
        /// </summary>
        public long Count { get; set; }
    }
}
