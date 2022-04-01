using ProductManager.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DTO
{
    /// <summary>
    /// ProductDTO for returning single or list of Products
    /// Response only.
    /// </summary>
    public class ProductDTO
    {
        /// <summary>
        /// Primary Key of the Product.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of the Product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ImageURL of the product.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Category of the product.
        /// </summary>
        public CategoryDTO Category { get; set; }
    }
}
