using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.DTO;
using ProductManager.DTO.Purchase;
using ProductManager.IRepository;
using ProductManager.Models;
using ProductManager.Repository;
using ProductManager.RequestParams;
using ProductManagerDTO.DTO.Purchase;
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
    /// PurchaseController manages crud operations of Purchase Entity
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route(ApiRoute.Purchase)]
    public class PurchaseController : AbstractController<Purchase,long>
    {
        private readonly IPurchaseService _purchaseService;

        /// <summary>
        /// DO NOT CALL. Create PurchaseController,
        /// </summary>
        /// <param name="unitOfWork"> Unitofwork implementation for retrieving repositories.</param>
        /// <param name="logger">Logger for errors and infos.</param>
        /// <param name="mapper"> Automapper for dto and entity convertions.</param>
        /// <param name="resourceManager">Resource manager for retreiving string values from resource.</param>
        public PurchaseController(IUnitOfWork unitOfWork, ILogger<PurchaseController> logger, 
            IMapper mapper,
             ResourceManager resourceManager,
             IPurchaseService purchaseService
            )
            : base(purchaseService, logger, mapper, resourceManager)
        {
            _purchaseService = purchaseService;
        }

        /// <summary>
        /// Get All Purchases with pagination.
        /// </summary>
        /// <returns>IActionResult contains Paged Purchases as PurchaseDTO</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PurchaseRequestParams requestParams)
        {
            try
            {
                var results = await _purchaseService.GetAllPurchases(requestParams);

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
                var result = await _purchaseService.GetOne(id);

                if (result == null)
                {
                    return NotFound();
                }

                var results = _mapper.Map<PurchaseSingleDTO>(result);

                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError("getOne exception on controller", e);
                return StatusCode(500, "Internal Status Error, Please try again");
            }
        }

        /// <summary>
        /// Create Purcahse 
        /// </summary>
        /// <param name="purchaseCreateDTO">Purchase information given as DTO</param>
        /// <returns>IActionResult contains single purchase which is created as DTO</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePurchase([FromBody] PurchaseCreateDTO purchaseCreateDTO)
        {
            return await ConcreateCreate<PurchaseCreateDTO, PurchaseSingleResultDTO>(purchaseCreateDTO);
        }

        /// <summary>
        /// Update Purchase with given DTO.
        /// </summary>
        /// <param name="id">Purchase Id which will be updated</param>
        /// <param name="purchaseUpdateDTO">Purchase information corresponding DTO</param>
        /// <returns>NoResult on Success</returns>
        [Authorize]
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PurchaseUpdate(long id, [FromBody] PurchaseUpdateDTO purchaseUpdateDTO)
        {
            return await ConcreateUpdate(id, purchaseUpdateDTO);
        }

        /// <summary>
        /// Delete Purchase with given primary key.
        /// </summary>
        /// <param name="id">Primary Key of the Purchase</param>
        /// <returns>NoResult on Success</returns>
        [Authorize]
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PurchaseDelete(long id)
        {
            return await ConcreateDelete(id);
        }
    }
}
