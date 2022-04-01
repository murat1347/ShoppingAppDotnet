using ProductManager.ModelConfiguration;
using ProductManager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductManager.Validators;
using ProductManagerDTO.Validation;

namespace ProductManagerDTO.DTO.User
{
    /// <summary>
    /// Used for login operation for authentication.
    /// Response only.
    /// </summary>
    public class LoginUserDTO
    {
        /// <summary>
        /// Email of the User
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
             ErrorMessageResourceType = typeof(ValidationResource))]
        public string Email { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
             ErrorMessageResourceType = typeof(ValidationResource))]
        [StringLength(maximumLength: ApiUserConfiguration.MaxPasswordLength,
            MinimumLength = ApiUserConfiguration.MinPasswordLength,
             ErrorMessageResourceName = nameof(ValidationMessageKey.Limited),
             ErrorMessageResourceType = typeof(ValidationResource))]
        public string Password { get; set; }
    }

}
