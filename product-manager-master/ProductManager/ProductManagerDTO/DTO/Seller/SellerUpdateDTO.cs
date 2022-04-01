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

namespace ProductManagerDTO.DTO.Seller
{
    /// <summary>
    /// SellerUpdateDTO for updating existed customer.
    /// Request only.
    /// </summary>
    public class SellerUpdateDTO
    {
        /// <summary>
        /// First name of the Seller.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
             ErrorMessageResourceType = typeof(ValidationResource))]
        [StringLength(maximumLength: CustomerConfiguration.FirstNameMax,
             ErrorMessageResourceName = nameof(ValidationMessageKey.TooLong),
             ErrorMessageResourceType = typeof(ValidationResource))]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the Seller.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
             ErrorMessageResourceType = typeof(ValidationResource))]
        [StringLength(maximumLength: CustomerConfiguration.LastNameMax,
             ErrorMessageResourceName = nameof(ValidationMessageKey.TooLong),
             ErrorMessageResourceType = typeof(ValidationResource))]
        public string LastName { get; set; }

        /// <summary>
        /// Address of the Seller.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
            ErrorMessageResourceType = typeof(ValidationResource))]
        [StringLength(maximumLength: CustomerConfiguration.AddressMax,
            ErrorMessageResourceName = nameof(ValidationMessageKey.TooLong),
            ErrorMessageResourceType = typeof(ValidationResource))]
        public string Address { get; set; }

        /// <summary>
        /// Email of the Seller.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
            ErrorMessageResourceType = typeof(ValidationResource))]
        [StringLength(maximumLength: CustomerConfiguration.EmailMax,
            ErrorMessageResourceName = nameof(ValidationMessageKey.TooLong),
            ErrorMessageResourceType = typeof(ValidationResource))]
        [EmailAddress(ErrorMessageResourceName = nameof(ValidationMessageKey.InvalidEmailAdresssFormat),
            ErrorMessageResourceType = typeof(ValidationResource))]
        public string Email { get; set; }

        /// <summary>
        /// Phone of the Seller.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
            ErrorMessageResourceType = typeof(ValidationResource))]
        [StringLength(maximumLength: CustomerConfiguration.PhoneMax,
            ErrorMessageResourceName = nameof(ValidationMessageKey.TooLong),
            ErrorMessageResourceType = typeof(ValidationResource))]
        [Phone(ErrorMessageResourceName = nameof(ValidationMessageKey.InvalidPhoneNumberFormat),
            ErrorMessageResourceType = typeof(ValidationResource))]
        public string Phone { get; set; }
    }
}
