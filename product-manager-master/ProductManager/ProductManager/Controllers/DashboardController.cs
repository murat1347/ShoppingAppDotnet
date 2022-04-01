using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ProductManagerIntegrationTest.Endpoint;
using ProductManagerServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Controllers
{
    /// <summary>
    /// DashboardController manages Login operation of APIUser Entity
    /// </summary>
    /// 
    [ApiVersion("1")]
    [Route(ApiRoute.Dashboard)]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardCacheService _dashboardService;

        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IDashboardCacheService dashboardService, ILogger<DashboardController> logger)
        {
            _logger = logger;
            _dashboardService = dashboardService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var dashboardReportDTO = await _dashboardService.getCachedDashboardReport();

                return Ok(dashboardReportDTO);
            }
            catch (Exception e)
            {
                _logger.LogError("getAll exception on controller", e);
                return StatusCode(500, "Internal Status Error, Please try again");
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Invalidate")]
        public IActionResult InvalidateCache()
        {
            try
            {
                _dashboardService.InvalidateCache();

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError("getAll exception on controller", e);
                return StatusCode(500, "Internal Status Error, Please try again");
            }
        }
    }
}
