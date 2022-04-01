using ProductManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.RequestParams
{
    /// <summary>
    /// ProductRequestParams helps paging and searching on product.
    /// Request only.
    /// </summary>
    public class CustomerRequestParams : SortPagingRequestParams
    {
        /// <summary>
        /// Id of the Customer
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// FirstName of the Customer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the customer.
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Email of the customer.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Phone of the customer.
        /// </summary>
        public string Phone { get; set; }
    }
}
