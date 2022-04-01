using ProductManagerDTO.DTO.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerServiceLayer.IServices
{
    public interface IDashboardCacheService
    {
        Task<DashboardReportDTO> getCachedDashboardReport();
        void InvalidateCache();
    }
}
