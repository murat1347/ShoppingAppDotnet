using ProductManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerDTO.DTO.Product
{
    /// <summary>
    /// ProductCategoryDTO for returning product with category.
    /// Response only.
    /// </summary>
    public class ProductCategoryDTO
    {
        /// <summary>
        /// Primary key of the product.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Category of the product.
        /// </summary>
        public CategoryDTO Category { get; set; }
    }
}
