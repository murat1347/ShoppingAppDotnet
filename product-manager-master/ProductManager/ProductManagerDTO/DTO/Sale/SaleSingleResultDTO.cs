using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerDTO.DTO.Sale
{
    /// <summary>
    /// SaleSingleResultDTO for returning sale purchase.
    /// Response only.
    /// </summary>
    public class SaleSingleResultDTO
    {
        /// <summary>
        /// Primary key of the sale.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// CustomerId of the sale.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Product Id of the sale.
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Amount of the sale products.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Income of the sale.
        /// </summary>
        public decimal Income { get; set; }

        /// <summary>
        /// When the sale happened.
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
