using ProductManager.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DTO
{
    /// <summary>
    /// CustomerDTO for returning single or list of Customers
    /// Response only.
    /// </summary>
    public class CustomerDTO
    {
        /// <summary>
        /// Primary key of the Customer
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name of the Customer.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the Customer.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Address of the Customer.
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Email of the Customer.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone of the Customer.
        /// </summary>
        public string Phone { get; set; }
    }
}
