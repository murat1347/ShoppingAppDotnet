using ProductManager.ModelConfiguration;
using ProductManager.Validators;
using ProductManagerDTO.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DTO.Product
{
    /// <summary>
    /// ProductUpdateDTO for updating a product.
    /// Request only.
    /// </summary>
    public class ProductUpdateDTO
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
        /// CategoryId of the product.
        /// </summary>
        [CategoryExists]
        public int? CategoryId { get; set; }

    }
}
