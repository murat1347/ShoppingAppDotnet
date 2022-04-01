using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerDTO.DTO.Purchase
{
    /// <summary>
    /// PurchaseSingleResultDTO for returning single purchase.
    /// Response only.
    /// </summary>
    public class PurchaseSingleResultDTO
    {
        /// <summary>
        /// Primary key of the purchase.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// SellerId of the purchase.
        /// </summary>
        public long SellerId { get; set; }

        /// <summary>
        /// ProductId of the purchase.
        /// </summary>
        public long ProductId { get; set; }

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
