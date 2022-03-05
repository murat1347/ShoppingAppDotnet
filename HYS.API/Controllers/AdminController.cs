using System.Threading.Tasks;
using HYS.Domain.Entities;
using HYS.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HYS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        public IActionResult RoleList()
        {
            return Ok(_roleManager.Roles);
        }
        
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
                              ModelState.AddModelError("",error.Description);
                    }
                }
            }
            return Ok();
        }

    }
}
