using ProductManager.DTO;
using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerDTO.DTO.Product
{
    public class ProductSummaryResultDTO
    {
        public ProductSingleResultDTO Product { get; set; }

        public long TotalSale { get; set; }

        public long TotalPurchase { get; set; }
    }
}
