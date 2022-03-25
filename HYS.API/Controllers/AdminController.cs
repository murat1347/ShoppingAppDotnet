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
   
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult RoleList()
        {
            return Ok(_roleManager.Roles);
        }
        
        

    }
}
