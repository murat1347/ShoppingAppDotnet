using Microsoft.Extensions.Caching.Memory;
using ProductManagerDTO.DTO.Report;
using ProductManagerServiceLayer.IServices;
using ProductManagerServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerCacheService.Service
{
    public class DashboardCacheService : IDashboardCacheService
    {
        private readonly IMemoryCache _cache;

        private readonly IDashboardService _dashboardService;
        public DashboardCacheService(IMemoryCache memoryCache, IDashboardService dashBoardService)
        {
           _cache = memoryCache;
            _dashboardService = dashBoardService;
        }

        public async Task<DashboardReportDTO> getCachedDashboardReport(){

            DashboardReportDTO dashboard;

            if (!_cache.TryGetValue(CacheKeys.DashboardReport, out dashboard))
            {
                dashboard = await _dashboardService.GetDashboardReport();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromDays(1));

                _cache.Set(CacheKeys.DashboardReport, dashboard, cacheEntryOptions);
            }

            return dashboard;
        }

        public void InvalidateCache()
        {
            _cache.Remove(CacheKeys.DashboardReport);
        }

    }
}
