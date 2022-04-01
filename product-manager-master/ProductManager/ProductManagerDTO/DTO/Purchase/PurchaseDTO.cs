using ProductManagerDTO.DTO.Product;
using ProductManagerDTO.DTO.Seller;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DTO
{
    /// <summary>
    /// PurchaseDTO for returning single or list of Purchase
    /// Response only.
    /// </summary>
    public class PurchaseDTO
    {
        /// <summary>
        /// Primary key of the purchase
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Seller of the purchase.
        /// </summary>
        public string SellerFirstName { get; set; }

        /// <summary>
        /// Seller's lastname of the purchase.
        /// </summary>
        public string SellerLastName { get; set; }

        /// <summary>
        /// The product's name which has purchased.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Amount of the product.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Cost of the purchase.
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// When the purchase happened.
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
