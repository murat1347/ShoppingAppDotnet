using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerDTO.DTO.Product
{
    /// <summary>
    /// ProductSingleResultDTO for returning single product.
    /// Response only.
    /// </summary>
    public class ProductSingleResultDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }
}
