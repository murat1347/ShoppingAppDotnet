using HYS.API.Dtos;
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
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        public ProductController(IGenericService<Product> productGenericService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _productGenericService = productGenericService;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string search, int? CategoryId, string sortBy, int page, int PAGE_SIZE=10)
        {
            // return Ok(_productGenericService.GetAll());
            return Ok(_productGenericService.GetAll( search,CategoryId,sortBy,page,PAGE_SIZE));
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var values = _productGenericService.GetById(id);
            return Ok(values);
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
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

        [HttpGet("category/{id}")]
        public ActionResult GetProductByCategoryId(int id)

        {
            var values = _categoryService.GetPostsByCategory(id);
            return Ok(values);

        }

    }
}
