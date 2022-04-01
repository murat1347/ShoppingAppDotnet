using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerDTO.DTO.Customer
{
    /// <summary>
    /// CustomerNameIdDTO for returning only Customer's Name and Id.
    /// Response only.
    /// </summary>
    public class CustomerNameIdDTO
    {
        /// <summary>
        /// Primary Key of the Customer.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// FirstName of the Customer.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName of the Customer.
        /// </summary>
        public string LastName { get; set; }
    }
}
