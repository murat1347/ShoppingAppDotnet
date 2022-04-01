using ProductManager.ModelConfiguration;
using ProductManager.Validators;
using ProductManagerDTO.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DTO.Category
{
    /// <summary>
    /// CategoryUpdateDTO for updating category.
    /// Request only.
    /// </summary>
    public class CategoryUpdateDTO
    {
        /// <summary>
        /// Name of the category.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required)
            , ErrorMessageResourceType = typeof(ValidationResource))]
        [StringLength(maximumLength: CategoryConfiguration.NameMaxLength
           , ErrorMessageResourceName = nameof(ValidationMessageKey.TooLong)
            , ErrorMessageResourceType = typeof(ValidationResource))]
        public string Name { get; set; }

        /// <summary>
        /// Parent id of the category.
        /// </summary>
        [CategoryExists]
        public int? ParentId { get; set; }
    }
}
