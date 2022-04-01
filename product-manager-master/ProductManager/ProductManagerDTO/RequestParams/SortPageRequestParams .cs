using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DTO
{
    /// <summary>
    /// Helps the paging operations for retreving list of elements from persistant storage.
    /// Request only.
    /// </summary>
    public class PagingRequestParams
    {
        /// <summary>
        /// Maximum page size limit's users' capability of requesting more elements.
        /// </summary>
        public const int MAX_PAGE_SIZE = 50;

        /// <summary>
        /// Default page size if the user didn't set the value
        /// </summary>
        public const int DEFAULT_PAGE_SIZE = 20;

        /// <summary>
        /// Requested page number, helps to skip elements of the data.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Determines how much data will be return.
        /// </summary>
        private int _pageSize = DEFAULT_PAGE_SIZE;

        /// <summary>
        /// Check if the pagesize is greater than max page size.
        /// </summary>
        public int PageSize{
            get{
                return _pageSize;
            }
            set{
                _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
            }
        }
    }
}
