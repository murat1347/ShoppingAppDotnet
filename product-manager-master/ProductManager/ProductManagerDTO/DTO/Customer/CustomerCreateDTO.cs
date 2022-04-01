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

namespace ProductManagerDTO.DTO.Customer
{
    /// <summary>
    /// CustomerCreateDTO for only creating Customer.
    /// Request only.
    /// </summary>
    public class CustomerCreateDTO
    {
        /// <summary>
        /// First name of the customer.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
             ErrorMessageResourceType = typeof(ValidationResource))]
        [StringLength(maximumLength: CustomerConfiguration.FirstNameMax,
             ErrorMessageResourceName = nameof(ValidationMessageKey.TooLong),
             ErrorMessageResourceType = typeof(ValidationResource))]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the customer.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
             ErrorMessageResourceType = typeof(ValidationResource))]
        [StringLength(maximumLength: CustomerConfiguration.LastNameMax,
             ErrorMessageResourceName = nameof(ValidationMessageKey.TooLong),
             ErrorMessageResourceType = typeof(ValidationResource))]
        public string LastName { get; set; }

        /// <summary>
        /// Address of the customer.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
            ErrorMessageResourceType = typeof(ValidationResource))]
        [StringLength(maximumLength: CustomerConfiguration.AddressMax,
            ErrorMessageResourceName = nameof(ValidationMessageKey.TooLong),
            ErrorMessageResourceType = typeof(ValidationResource))]
        public string Address { get; set; }

        /// <summary>
        /// Email of the customer.
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
        /// Phone of the customer.
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
