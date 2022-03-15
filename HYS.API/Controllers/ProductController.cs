using HYS.Application.Services.Interfaces;
using HYS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HYS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericService<Product> _productGenericService;

        public ProductController(IGenericService<Product> productGenericService)
        {
            _productGenericService = productGenericService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_productGenericService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var values = _productGenericService.GetById(id);
            return Ok(values);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post(Product product)
        {
            _productGenericService.Insert(product);
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Product product)
        {
            _productGenericService.Update(product);
            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productGenericService.Delete(id);
            return Ok();
        }
    }
}
