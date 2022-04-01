using ProductManager.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DTO
{
    /// <summary>
    /// SellerDTO for returning single or list of Sellers
    /// Response only.
    /// </summary>
    public class SellerDTO
    {
        /// <summary>
        /// Primary key of the Seller
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name of the Seller.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the Seller.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Address of the Seller.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Email of the Seller.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone of the Seller.
        /// </summary>
        public string Phone { get; set; }
    }
}
