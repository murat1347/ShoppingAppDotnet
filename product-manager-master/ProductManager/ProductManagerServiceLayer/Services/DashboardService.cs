using ProductManager.IRepository;
using ProductManager.Models;
using ProductManager.Repository;
using ProductManagerDTO.DTO.Report;
using ProductManagerRepository.Repository;
using ProductManagerServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerServiceLayer.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DashboardReportDTO> GetDashboardReport()
        {
            var totalIncome = await _unitOfWork.Repository<Sale,SaleRepository>().TotalIncome();

            var totalCost = await _unitOfWork.Repository<Purchase,PurchaseRepository>().TotalCost();

            var saleSummary = await _unitOfWork.Repository<Product,ProductRepository>().ProductSummarySaleResult(10);

            var purchaseSummary = await _unitOfWork.Repository<Product, ProductRepository>().ProductSummaryPurchaseResult(10);

            var criticalStock  = await _unitOfWork.Repository<Product, ProductRepository>().ProductCriticalStockCount(100);

            var mostProfitProducts = await _unitOfWork.Repository<Product, ProductRepository>().ProductMostProfitResult(10);

            var mostLossProducts = await _unitOfWork.Repository<Product, ProductRepository>().ProductMostLossProfitResult(10);

            var monthlyIncomes = await _unitOfWork.Repository<Sale, SaleRepository>().MounthlyIncomes(10);

            var dashboardDTO = new DashboardReportDTO{
                TotalIncome = totalIncome,
                TotalCost = totalCost,
                SaleSummary = saleSummary,
                PurchaseSummary = purchaseSummary,
                CriticalStock = criticalStock,
                MostProfit = mostProfitProducts,
                MostLoss = mostLossProducts,
                MonthlyIncomes = monthlyIncomes
            };

            return dashboardDTO;
        }

    }
}
