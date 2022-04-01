using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.DTO;
using ProductManager.DTO.Product;
using ProductManager.IRepository;
using ProductManager.Models;
using ProductManager.RequestParams;
using ProductManager.Validators;
using ProductManagerDTO.DTO.Product;
using ProductManagerIntegrationTest.Endpoint;
using ProductManagerPersistance.Helpers;
using ProductManagerServiceLayer.IServices;
using ProductManagerServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Threading.Tasks;

namespace ProductManager.Controllers
{
    /// <summary>
    /// Product controller manages crud operations of Product Entity
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route(ApiRoute.Product)]
    public class ProductController : AbstractController<Product,long>
    {

        private readonly IProductService _productService;

        /// <summary>
        /// DO NOT CALL. Create ProductController,
        /// </summary>
        /// <param name="unitOfWork"> Unitofwork implementation for retrieving repositories.</param>
        /// <param name="logger">Logger for errors and infos.</param>
        /// <param name="mapper"> Automapper for dto and entity convertions.</param>
        /// <param name="resourceManager">Resource manager for retreiving string values from resource.</param>
        public ProductController(IUnitOfWork unitOfWork, 
            ILogger<ProductController> logger, IMapper mapper,
             ResourceManager resourceManager,
             IProductService productService
            )
            : base(productService, logger, mapper, resourceManager)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get All Products with Pagination
        /// </summary>
        /// <returns>IActionResult contains Paged Products as ProductDTO</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductRequestParams requestParams)
        {
            try
            {
                var productPagedDTO = await _productService.GetAllProducts(requestParams);

                return Ok(productPagedDTO);
            }
            catch (Exception e)
            {
                _logger.LogError("getAll exception on controller", e);
                return StatusCode(500, "Internal Status Error, Please try again");
            }
        }

        /// <summary>
        /// Get Single Product by primary key 
        /// </summary>
        /// <param name="id">Primary Key of the Product</param>
        /// <returns>IActionResult contains single Product as ProductDTO</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOne(int id)
        {
            try
            {
                List<string> include = new List<string>();
                include.Add(nameof(Category));

                var result = await _productService.GetOne(id);

                if (result == null)
                {
                    return NotFound();
                }

                _logger.LogInformation(result.ToString());
                var results = _mapper.Map<ProductDTO>(result);

                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError("getAll exception on controller", e);
                return StatusCode(500, "Internal Status Error, Please try again");
            }
        }

        /// <summary>
        /// Create Product 
        /// </summary>
        /// <param name="productCreateDTO">Product information given as DTO</param>
        /// <returns>IActionResult contains single product which is created as DTO</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO productCreateDTO)
        {
            return await ConcreateCreate<ProductCreateDTO, ProductSingleResultDTO>(productCreateDTO);
        }

        /// <summary>
        /// Update Product with given DTO.
        /// </summary>
        /// <param name="id">Product Id which will be updated</param>
        /// <param name="productUpdateDTO">Product information corresponding DTO</param>
        /// <returns>NoResult on Success</returns>
        [Authorize]
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProductUpdate(long id, [FromBody] ProductUpdateDTO productUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(ConcreateUpdate)}");
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _genericService.GetOne(id);

                if (result == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(ConcreateUpdate)}");
                    return BadRequest(_resourceManager
                                       .GetString(nameof(ValidationMessageKey.NotExists)));
                }

                _mapper.Map(productUpdateDTO, result);

                result.Category = null;
                _genericService.Update(result);
                await _genericService.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the ConcreateUpdate");
                return StatusCode(500, _resourceManager
                 .GetString(nameof(ValidationMessageKey.InternalServerError)));
            }
        }

        
        /// <summary>
        /// Delete Product with given primary key.
        /// </summary>
        /// <param name="id">Primary Key of the product</param>
        /// <returns>NoResult on Success</returns>
        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            return await ConcreateDelete(id);
        }

        /// <summary>
        /// Update Product's category
        /// </summary>
        /// <param name="id">Product's id</param>
        /// <param name="updateDTO">New category of the Product.</param>
        /// <returns></returns>
        [HttpPut("{id:int}/category")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductCategoryUpdateDTO updateDTO)
        {
            return await ConcreateUpdate<ProductCategoryUpdateDTO>(id, updateDTO);
        }
    }
}
