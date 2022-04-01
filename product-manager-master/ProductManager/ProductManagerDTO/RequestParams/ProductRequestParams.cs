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
    public class ProductRequestParams : SortPagingRequestParams
    {
        /// <summary>
        /// Category Name of the product.
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Category Name of the product.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// ProductName of the product.
        /// </summary>
        public string ProductName { get; set; }
    }
}
