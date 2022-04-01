using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.DTO;
using ProductManager.IRepository;
using ProductManager.Models;
using ProductManager.Validators;
using ProductManagerServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace ProductManager.Controllers
{
    /// <summary>
    /// Abstract controller for controllers providing basic method list, create, update
    /// Do not add any abstract method so the implementors can not be effected.
    /// </summary>
    /// <typeparam name="E">Primary key type of entity used for determining url parameters' type.</typeparam>
    public class AbstractController<E,K> : Controller where E : BaseEntity 
    {
        /// <summary>
        /// Logger for errors and infos.
        /// </summary>
        protected readonly ILogger<AbstractController<E,K>> _logger;


        protected readonly IGenericService<E,K> _genericService;

        /// <summary>
        /// Automapper for dto and entity convertions.
        /// </summary>
        protected readonly IMapper _mapper;

        /// <summary>
        /// Resource manager for retreving string values.
        /// </summary>
        protected readonly ResourceManager _resourceManager;

        /// <summary>
        /// Only way to create abstract controller.
        /// </summary>
        /// <param name="unitOfWork"> Unitofwork implementation for retrieving repositories.</param>
        /// <param name="logger">Logger for errors and infos.</param>
        /// <param name="mapper"> Automapper for dto and entity convertions.</param>
        protected AbstractController(IGenericService<E,K> service,
                                  ILogger<AbstractController<E,K>> logger, 
                                  IMapper mapper, 
                                  ResourceManager resourceManager)
        {
            _genericService = service;
            _logger = logger;
            _mapper = mapper;
            _resourceManager = resourceManager;
        }

        /// <summary>
        /// Get all E with mapping to D
        /// </summary>
        /// <typeparam name="D">Result type of the entity will be mapped</typeparam>
        /// <returns>Action result contains elements of D</returns>
        protected async Task<IActionResult> ConcreateGetAll<D>()
        {
            try
            {
                var result = await _genericService.GetAll();

                var results = _mapper.Map<IList<D>>(result);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError("ConcreateGetAll exception on controller", e);
                return StatusCode(500, _resourceManager
                                .GetString(nameof(ValidationMessageKey.InternalServerError)));
            }
        }

        /// <summary>
        /// Get single E with mapping to D
        /// </summary>
        /// <typeparam name="D">Result type of the entity will be mapped</typeparam>
        /// <returns>Action result contains one D element</returns>
        protected async Task<IActionResult> ConcreateGet<D>(K id)
        {
            try
            {
                var result = await _genericService.GetOne(id);
                var results = _mapper.Map<D>(result);
                return Ok(results);
            }
            catch (Exception e)
            {
                _logger.LogError("ConcreateGet exception on controller", e);
                return StatusCode(500, _resourceManager
                 .GetString(nameof(ValidationMessageKey.InternalServerError)));
            }
        }

        /// <summary>
        /// Create entity E with parsing from D and sending result as R
        /// </summary>
        /// <typeparam name="D">Requested entity as DTO</typeparam>
        /// <typeparam name="R">Result DTO</typeparam>
        /// <returns>Action result contains one R element which is created.</returns>
        protected async Task<IActionResult> ConcreateCreate<D, R>([FromBody] D createDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(ConcreateCreate)}");
                return BadRequest(ModelState);
            }

            try
            {
                var result = _mapper.Map<E>(createDTO);
                await _genericService.Insert(result);
                await _genericService.Save();

                var resultDTO = _mapper.Map<R>(result);

                return StatusCode(201, resultDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(ConcreateCreate)}");

                return StatusCode(500, _resourceManager
                 .GetString(nameof(ValidationMessageKey.InternalServerError)));
            }
        }

        /// <summary>
        /// Update E with is given as D
        /// </summary>
        /// <typeparam name="D">Requested DTO</typeparam>
        /// <returns>Action result with NoResult status</returns>
        protected async Task<IActionResult>
            ConcreateUpdate<D>(K id, [FromBody] D categoryUpdateDTO)
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

                _mapper.Map(categoryUpdateDTO, result);
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
        /// Delete E with is given E
        /// </summary>
        /// <returns>Action result with NoResult status</returns>
        protected async Task<IActionResult> ConcreateDelete(K id)
        {
            try{

                var entity = await _genericService.GetOne(id);

                if (entity == null)
                {
                    _logger.LogError($"Invalid DELETE attempt in {nameof(ConcreateDelete)}");
                    return NotFound("Submitted data is invalid");
                }

                await _genericService.Delete(id);
                await _genericService.Save();
            
                return NoContent();

            }catch(Exception e){
                _logger.LogError($"Invalid Delete attempt in {nameof(ConcreateDelete)}");
                return BadRequest(_resourceManager
                                   .GetString(nameof(ValidationMessageKey.InvalidDeleteAttempt)));
            }
        }
    }
}
