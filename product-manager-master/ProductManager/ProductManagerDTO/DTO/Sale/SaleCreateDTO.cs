using ProductManager.Validators;
using ProductManagerDTO.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DTO.Sale
{
    /// <summary>
    /// SaleCreateDTO for creating a sale.
    /// Request only.
    /// </summary>
    public class SaleCreateDTO
    {
        /// <summary>
        /// CustomerId of the sale.
        /// </summary>
        [CustomerExists]
        public int? CustomerId { get; set; }

        /// <summary>
        /// Product Id of the sale.
        /// </summary>
        [ProductExists]
        public long? ProductId { get; set; }

        /// <summary>
        /// Amount of the sale products.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
             ErrorMessageResourceType = typeof(ValidationResource))]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Income of the sale.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
             ErrorMessageResourceType = typeof(ValidationResource))]
        public decimal? Income { get; set; }

        /// <summary>
        /// When the sale happened.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
             ErrorMessageResourceType = typeof(ValidationResource))]
        public DateTime? DateTime { get; set; }
    }
}
