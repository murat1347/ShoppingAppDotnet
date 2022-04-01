using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.DTO;
using ProductManager.IRepository;
using ProductManager.Models;
using ProductManager.RequestParams;
using ProductManagerDTO.DTO.Seller;
using ProductManagerIntegrationTest.Endpoint;
using ProductManagerServiceLayer.IServices;
using ProductManagerServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace ProductManager.Controllers
{
    /// <summary>
    /// Seller controller manages crud operations of Seller Entity
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route(ApiRoute.Seller)]
    public class SellerController : AbstractController<Seller,int>
    {

        /// <summary>
        /// Accessing seller data for persistance.
        /// </summary>
        private ISellerService _sellerService;

        /// <summary>
        /// DO NOT CALL. Create SellerController,
        /// </summary>
        /// <param name="unitOfWork"> Unitofwork implementation for retrieving repositories.</param>
        /// <param name="logger">Logger for errors and infos.</param>
        /// <param name="mapper"> Automapper for dto and entity convertions.</param>
        /// <param name="resourceManager">Resource manager for retreiving string values from resource.</param>
        public SellerController(IUnitOfWork unitOfWork, ILogger<SellerController> logger, IMapper mapper,
             ResourceManager resourceManager,
             ISellerService sellerService
            )
            : base(sellerService, logger, mapper,resourceManager)
        {
            _sellerService = sellerService;
        }

        /// <summary>
        /// Get All Sellers
        /// </summary>
        /// <returns>IActionResult contains All Sellers as SellerDTO</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] SellerRequestParams requestParams)
        {
            try
            {
                var sellerPagedDTO = await _sellerService.GetAllSellers(requestParams);

                return Ok(sellerPagedDTO);
            }
            catch (Exception e)
            {
                _logger.LogError("getAll exception on controller", e);
                return StatusCode(500, "Internal Status Error, Please try again");
            }
        }

        /// <summary>
        /// Get Single Seller by primary key 
        /// </summary>
        /// <param name="id">Primary Key of the Seller</param>
        /// <returns>IActionResult contains single Seller as SellerDTO</returns>
        [Authorize]
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSeller(int id)
        {
            return await ConcreateGet<SellerDTO>(id);
        }

        /// <summary>
        /// Create Seller 
        /// </summary>
        /// <param name="sellerCreateDTO">Seller information given as DTO</param>
        /// <returns>IActionResult contains single seller which is created as DTO</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSeller([FromBody] SellerCreateDTO sellerCreateDTO)
        {
            return await ConcreateCreate<SellerCreateDTO, SellerDTO>(sellerCreateDTO);
        }

        /// <summary>
        /// Update Seller with given DTO.
        /// </summary>
        /// <param name="id">Seller Id which will be updated</param>
        /// <param name="sellerUpdateDTO">Seller information corresponding DTO</param>
        /// <returns>NoResult on Success</returns>
        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SellerUpdate(int id, [FromBody] SellerUpdateDTO sellerUpdateDTO)
        {
            return await ConcreateUpdate(id, sellerUpdateDTO);
        }

        /// <summary>
        /// Delete Seller with given primary key.
        /// </summary>
        /// <param name="id">Primary Key of the seller</param>
        /// <returns>NoResult on Success</returns>
        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SellerDelete(int id)
        {
            return await ConcreateDelete(id);
        }
    }
}
