using ProductManager.ModelConfiguration;
using ProductManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManagerDTO.Validation;
using ProductManager.Validators;

namespace ProductManagerDTO.DTO.Category
{
    /// <summary>
    /// CategoryCreateDTO for creating a category.
    /// Request only.
    /// </summary>
    public class CategoryCreateDTO
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
        /// Parent id of the Category.
        /// </summary>
        public int? ParentId { get; set; }
    }
}
