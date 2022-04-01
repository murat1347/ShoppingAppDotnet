using ProductManager.Validators;
using ProductManagerDTO.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DTO.Purchase
{
    /// <summary>
    /// PurchaseUpdateDTO for updating a purchase.
    /// Request only.
    /// </summary>
    public class PurchaseUpdateDTO
    {
        /// <summary>
        /// SellerId of the purchase.
        /// </summary>
        [SellerExists]
        public long? SellerId { get; set; }

        /// <summary>
        /// ProductId of the purchase.
        /// </summary>
        [ProductExists]
        public long? ProductId { get; set; }

        /// <summary>
        /// Amount of the purchase.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
             ErrorMessageResourceType = typeof(ValidationResource))]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Cost of the purchase.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
             ErrorMessageResourceType = typeof(ValidationResource))]
        public decimal? Cost { get; set; }

        /// <summary>
        /// When the purchase happened.
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(ValidationMessageKey.Required),
             ErrorMessageResourceType = typeof(ValidationResource))]
        public DateTime? DateTime { get; set; }
    }
}
