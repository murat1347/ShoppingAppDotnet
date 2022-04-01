using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.DTO;
using ProductManager.DTO.Sale;
using ProductManager.IRepository;
using ProductManager.Models;
using ProductManager.Repository;
using ProductManager.RequestParams;
using ProductManager.Validators;

using ProductManagerDTO.DTO.Purchase;
using ProductManagerDTO.DTO.Sale;
using ProductManagerIntegrationTest.Endpoint;
using ProductManagerServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace ProductManager.Controllers
{
    /// <summary>
    /// SaleController manages crud operations of Seller Entity
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route(ApiRoute.Sale)]
    public class SaleController : AbstractController<Sale,long>
    {
        private readonly ISaleService _saleService;

        /// <summary>
        /// DO NOT CALL. Create SaleController,
        /// </summary>
        /// <param name="unitOfWork"> Unitofwork implementation for retrieving repositories.</param>
        /// <param name="logger">Logger for errors and infos.</param>
        /// <param name="mapper"> Automapper for dto and entity convertions.</param>
        /// <param name="resourceManager">Resource manager for retreiving string values from resource.</param>
        public SaleController(ILogger<SaleController> logger, IMapper mapper,
             ResourceManager resourceManager,
             ISaleService saleService
            )
            : base(saleService, logger, mapper, resourceManager)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// Get All Sales with pagination.
        /// </summary>
        /// <returns>IActionResult contains Paged Sales as SaleDTO</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SaleRequestParams requestParams)
        {
            try
            {
                var results = await _saleService.GetAllSales(requestParams);

                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError("getAll exception on controller", e);
                return StatusCode(500, "Internal Status Error, Please try again");
            }
        }

        /// <summary>
        /// Get Single Purchase by primary key 
        /// </summary>
        /// <param name="id">Primary Key of the Product</param>
        /// <returns>IActionResult contains single Product as ProductDTO</returns>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetOne(long id)
        {
            try
            {
                List<string> include = new List<string>();
                include.Add(nameof(Product));
                include.Add(nameof(Seller));

                var result = await _saleService.GetOne(id);

                if (result == null)
                {
                    return NotFound();
                }

                var results = _mapper.Map<SaleSingleDTO>(result);

                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError("getOne exception on controller", e);
                return StatusCode(500, "Internal Status Error, Please try again");
            }
        }

        /// <summary>
        /// Create Sale 
        /// </summary>
        /// <param name="saleCreateDTO">Sale information given as DTO</param>
        /// <returns>IActionResult contains single sale which is created as DTO</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSale([FromBody] SaleCreateDTO saleCreateDTO)
        {
            return await ConcreateCreate<SaleCreateDTO, SaleSingleResultDTO>(saleCreateDTO);
        }

        /// <summary>
        /// Update Sale with given DTO.
        /// </summary>
        /// <param name="id">Sale Id which will be updated</param>
        /// <param name="saleUpdateDTO">Sale information corresponding DTO</param>
        /// <returns>NoResult on Success</returns>
        [Authorize]
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaleUpdate(long id, [FromBody] SaleUpdateDTO saleUpdateDTO)
        {
            return await ConcreateUpdate(id, saleUpdateDTO);
        }

        /// <summary>
        /// Delete Sale with given primary key.
        /// </summary>
        /// <param name="id">Primary Key of the Sale</param>
        /// <returns>NoResult on Success</returns>
        [Authorize]
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaleDelete(long id)
        {
            return await ConcreateDelete(id);
        }
    }
}
