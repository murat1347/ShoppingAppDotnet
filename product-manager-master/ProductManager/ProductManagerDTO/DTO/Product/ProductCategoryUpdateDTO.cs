using ProductManager.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DTO.Product
{
    /// <summary>
    /// ProductCategoryUpdateDTO for updating category of the product.
    /// Request only.
    /// </summary>
    public class ProductCategoryUpdateDTO
    {
        /// <summary>
        /// CategoryId of the product.
        /// </summary>
        [CategoryExists]
        public int CategoryId { get; set; }
    }
}
