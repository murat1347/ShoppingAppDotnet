using ProductManagerDTO.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerDTO.DTO.Report
{
    public class DashboardReportDTO
    {
        public decimal TotalIncome { get; set; }

        public decimal TotalCost  { get; set; }

        public object SaleSummary { get; set; }

        public object PurchaseSummary { get; set; }

        public object MostProfit { get; set; }

        public object MostLoss { get; set; }

        public object CategoryIncomes { get; set; }

        public long CriticalStock { get; set; }

        public object MonthlyIncomes { get; set; }
    }
}
