using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerDTO.DTO.Seller
{
    /// <summary>
    /// SellerNameIdDTO for returning only Seller's Name and Id.
    /// Response only.
    /// </summary>
    public class SellerNameIdDTO
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
    }
}
