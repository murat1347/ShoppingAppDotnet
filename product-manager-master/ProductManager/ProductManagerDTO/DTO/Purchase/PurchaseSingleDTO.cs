using ProductManagerDTO.DTO.Product;
using ProductManagerDTO.DTO.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerDTO.DTO.Purchase
{
    public class PurchaseSingleDTO
    {
        /// <summary>
        /// Primary key of the purchase.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// SellerId of the purchase.
        /// </summary>
        public SellerNameIdDTO Seller { get; set; }

        /// <summary>
        /// ProductId of the purchase.
        /// </summary>
        public ProductNameIdDTO Product { get; set; }

        /// <summary>
        /// Amount of the purchase.
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
