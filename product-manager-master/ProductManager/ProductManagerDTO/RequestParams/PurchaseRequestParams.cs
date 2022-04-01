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
    public class PurchaseRequestParams : SortPagingRequestParams
    {
        /// <summary>
        /// Id of the Customer
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Amount max of the customer
        /// </summary>
        public string SellerFirstName { get; set; }

        /// <summary>
        /// Amount min of the customer.
        /// </summary>
        public string SellerLastName { get; set; }

        /// <summary>
        /// Product name of the customer.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Minimum requested amount
        /// </summary>
        public decimal? AmountMin { get; set; }

        /// <summary>
        /// Maximumum requested amount
        /// </summary>
        public decimal? AmountMax { get; set; }

        /// <summary>
        /// Minimum requested cost
        /// </summary>
        public decimal? CostMin { get; set; }

        /// <summary>
        /// Maximum requested cost
        /// </summary>
        public decimal? CostMax { get; set; }

        /// <summary>
        /// Purchases After the date-time.
        /// </summary>
        public DateTime? DateTimeMin { get; set; }

        /// <summary>
        /// Purchases Before the date-time.
        /// </summary>
        public DateTime? DateTimeMax { get; set; }
    }
}
