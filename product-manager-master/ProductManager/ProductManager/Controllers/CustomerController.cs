using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.DTO;
using ProductManager.IRepository;
using ProductManager.Models;
using ProductManager.RequestParams;
using ProductManagerDTO.DTO.Customer;
using ProductManagerDTO.DTO.Product;
using ProductManagerIntegrationTest.Endpoint;
using ProductManagerServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Threading.Tasks;

namespace ProductManager.Controllers
{
    /// <summary>
    /// Customer controller manages crud operations of Customer Entity
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route(ApiRoute.Customer)]
    public class CustomerController :  AbstractController<Customer, int>
    {

        /// <summary>
        /// Customer service for accesing persistant storage
        /// </summary>
        private ICustomerService _customerService;

        /// <summary>
        /// DO NOT CALL. Create CustomerController,
        /// </summary>
        /// <param name="unitOfWork"> Unitofwork implementation for retrieving repositories.</param>
        /// <param name="logger">Logger for errors and infos.</param>
        /// <param name="mapper"> Automapper for dto and entity convertions.</param>
        /// <param name="resourceManager">Resource manager for retreiving string values from resource.</param>
        public CustomerController(
            ILogger<CustomerController> logger, 
            IMapper mapper, 
            ResourceManager resourceManager,
            ICustomerService customerService
            )
            : base(customerService, logger, mapper, resourceManager)
        {
           _customerService = customerService;
        }

        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns>IActionResult contains All Customers as CustomerDTO</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CustomerRequestParams requestParams)
        {
            try
            {
                var customerPagedDTO = await _customerService.GetAllCustomers(requestParams);

                return Ok(customerPagedDTO);
            }
            catch (Exception e)
            {
                _logger.LogError("getAll exception on controller", e);
                return StatusCode(500, "Internal Status Error, Please try again");
            }
        }

        /// <summary>
        /// Get Single Customer by primary key 
        /// </summary>
        /// <param name="id">Primary Key of the Customer</param>
        /// <returns>IActionResult contains single Customer as CustomerDTO</returns>
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            return await ConcreateGet<CustomerDTO>(id);
        }

        /// <summary>
        /// Create Customer 
        /// </summary>
        /// <param name="customerCreateDTO">Customer information given as DTO</param>
        /// <returns>IActionResult contains single customer which is created as DTO</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDTO customerCreateDTO)
        {
            return await ConcreateCreate<CustomerCreateDTO, CustomerDTO>(customerCreateDTO);
        }

        /// <summary>
        /// Update Customer with given DTO.
        /// </summary>
        /// <param name="id">Customer Id which will be updated</param>
        /// <param name="customerUpdateDTO">Customer information corresponding DTO</param>
        /// <returns>NoResult on Success</returns>
        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CustomerUpdate(int id, [FromBody] CustomerUpdateDTO customerUpdateDTO)
        {
            return await ConcreateUpdate(id, customerUpdateDTO);
        }

        /// <summary>
        /// Delete Customer with given primary key.
        /// </summary>
        /// <param name="id">Primary Key of the customer</param>
        /// <returns>NoResult on Success</returns>
        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CustomerDelete(int id)
        {
            return await ConcreateDelete(id);
        }
    }
}
