using HYS.Application.Services.Interfaces;
using HYS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HYS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IGenericService<Category> _categoryGenericService;
        private readonly ICategoryService _categoryService;
        public CategoryController(IGenericService<Category> categoryGenericService, ICategoryService categoryService)
        {
            _categoryGenericService = categoryGenericService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_categoryGenericService.GetAll());
        }

        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var values = _categoryGenericService.GetById(id);
        //    return Ok(values);
        //}
        [HttpPost]
        public IActionResult Post(Category category)
        {
            _categoryGenericService.Insert(category);
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Category category)
        {
            _categoryGenericService.Update(category);
            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryGenericService.Delete(id);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetProductByCategory(int id)
        {
            return Ok(_categoryService.GetPostsByCategory(id));
        }    
    }
}