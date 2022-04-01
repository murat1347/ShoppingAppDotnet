using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerDTO.DTO.Product
{
    /// <summary>
    /// ProductNameIdDTO for returning only name and id of a product.
    /// Response only.
    /// </summary>
    public class ProductNameIdDTO
    {
        /// <summary>
        /// Primary key of the product.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; }
    }
}
