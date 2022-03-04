using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HYS.Domain.Entities;
using HYS.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HYS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {

            return Ok();
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {

            return Ok();
        }
        [HttpGet]
        public IActionResult Register()
        {

            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User()
            {
                Name = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("","Unkown Error");
            return Ok();
        }
    }
}
