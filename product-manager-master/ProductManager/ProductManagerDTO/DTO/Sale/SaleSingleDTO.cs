using ProductManagerDTO.DTO.Customer;
using ProductManagerDTO.DTO.Product;
using ProductManagerDTO.DTO.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerDTO.DTO.Purchase
{
    public class SaleSingleDTO
    {
        /// <summary>
        /// Primary key of the purchase.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// SellerId of the purchase.
        /// </summary>
        public CustomerNameIdDTO Customer { get; set; }

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
        public decimal Income { get; set; }

        /// <summary>
        /// When the purchase happened.
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
