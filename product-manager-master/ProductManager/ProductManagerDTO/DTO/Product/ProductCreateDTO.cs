using ProductManager.ModelConfiguration;
using ProductManager.Validators;
using ProductManagerDTO.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DTO
{
    /// <summary>
    /// ProductCreateDTO for creating a product.
    /// Request only.
    /// </summary>
    public class ProductCreateDTO
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
                     ErrorMessageResourceType = typeof(ValidationResource))]
        [StringLength(maximumLength: ProductConfiguration.NameMaxLength,
                     ErrorMessageResourceName = nameof(ValidationMessageKey.TooLong),
                     ErrorMessageResourceType = typeof(ValidationResource))]
        public string Name { get; set; }

        /// <summary>
        /// ImageUrl of the product.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
             ErrorMessageResourceType = typeof(ValidationResource))]
        [StringLength(maximumLength: ProductConfiguration.ImageUrlMaxLength,
             ErrorMessageResourceName = nameof(ValidationMessageKey.TooLong),
             ErrorMessageResourceType = typeof(ValidationResource))]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Stock of the product, has value 0 and can't be set for a new product.
        /// </summary>
        public readonly int Stock = 0;

        /// <summary>
        /// CategoryId of the product.
        /// </summary>
        [CategoryExists]
        public int? CategoryId { get; set; }
    }
}
