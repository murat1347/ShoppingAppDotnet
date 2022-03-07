using System.Threading.Tasks;
using HYS.Domain.Entities;
using HYS.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HYS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleCreateController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
       
        [HttpGet]
        public IActionResult RoleCreate()
        {

            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                if (result.Succeeded)
                {
                    return Redirect("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return Ok();
        }
    }
}
