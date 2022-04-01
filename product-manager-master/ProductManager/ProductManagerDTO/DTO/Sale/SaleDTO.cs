using ProductManagerDTO.DTO.Customer;
using ProductManagerDTO.DTO.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DTO
{
    /// <summary>
    /// SaleDTO for returning single or list of Sales
    /// Response only.
    /// </summary>
    public class SaleDTO
    {
        /// <summary>
        /// Primary key of the sale.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Sale to which Customer with Name and Id.
        /// </summary>
        public string CustomerFirstName { get; set; }

        /// <summary>
        /// Sale to which Customer with Name and Id.
        /// </summary>
        public string CustomerLastName { get; set; }

        /// <summary>
        /// Which Product is sale with Name and Id;
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Amount of sale
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
