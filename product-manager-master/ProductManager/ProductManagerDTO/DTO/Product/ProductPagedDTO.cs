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
    public class ProductPagedDTO
    {
        /// <summary>
        /// List of ProductDTOs
        /// </summary>
        public IList<ProductDTO> Products { get; set; }

        /// <summary>
        /// Total count of the Products in the storage.
        /// </summary>
        public long Count { get; set; }
    }
}
