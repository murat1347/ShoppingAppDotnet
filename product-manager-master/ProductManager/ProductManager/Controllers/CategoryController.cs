using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.DTO;
using ProductManager.DTO.Category;
using ProductManager.Helpers;
using ProductManager.IRepository;
using ProductManager.Models;
using ProductManagerDTO.DTO.Category;
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
    /// Category controller manages crud operations of Category Entity
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route(ApiRoute.Category)]
    public class CategoryController : AbstractController<Category, int>
    {
        /// <summary>
        /// DO NOT CALL. Create CategoryController,
        /// </summary>
        /// <param name="unitOfWork"> Unitofwork implementation for retrieving repositories.</param>
        /// <param name="logger">Logger for errors and infos.</param>
        /// <param name="mapper"> Automapper for dto and entity convertions.</param>
        /// <param name="resourceManager">Resource manager for retreiving string values from resource.</param>
        public CategoryController(ICategoryService categoryService,
            ILogger<CategoryController> logger, IMapper mapper,
            ResourceManager resourceManager)
                        : base(categoryService, logger, mapper, resourceManager)
        {
        }

        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns>IActionResult contains All Categories as CategoryDTO</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await ConcreateGetAll<CategoryDTO>();
        }

        /// <summary>
        /// Get Single Category by primary key 
        /// </summary>
        /// <param name="id">Primary Key of the Category</param>
        /// <returns>IActionResult contains single Category as CategoryDTO</returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return await ConcreateGet<CategoryDTO>(id);
        }

        /// <summary>
        /// Create Category 
        /// </summary>
        /// <param name="categoryCreateDTO">Category information given as DTO</param>
        /// <returns>IActionResult contains single category which is creates as DTO</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO categoryCreateDTO)
        {
            return await ConcreateCreate<CategoryCreateDTO, CategoryDTO>(categoryCreateDTO);
        }

        /// <summary>
        /// Update Category with given DTO.
        /// </summary>
        /// <param name="id">Category Id which will be updated</param>
        /// <param name="categoryUpdateDTO">Category information corresponding DTO</param>
        /// <returns>NoResult on Success</returns>
        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CategoryUpdate(int id, [FromBody] CategoryUpdateDTO categoryUpdateDTO)
        {
            return await ConcreateUpdate<CategoryUpdateDTO>(id, categoryUpdateDTO);
        }

        /// <summary>
        /// Delete Category with given primary key.
        /// </summary>
        /// <param name="id">Primary Key of the category</param>
        /// <returns>NoResult on Success</returns>
        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CategoryDelete(int id)
        {
            return await ConcreateDelete(id);
        }
    }
}
