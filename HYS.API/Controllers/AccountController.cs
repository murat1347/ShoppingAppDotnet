using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HYS.API.Dtos;
using HYS.API.EmailService;
using HYS.API.JwtTokenHandler;
using HYS.Domain.Entities;
using HYS.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HYS.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[AutoValidateAntiforgeryToken]
    public class AccountController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        private JwtTokenGenerator _generator;

        //private IEmailSender _emailSender;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            JwtTokenGenerator generator /*, IEmailSender emailSender*/)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _generator = generator;
            //_emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {

            return Ok();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult<UserDto>> Login(UserDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized();



            return new UserDto
            {
                Email = user.Email,
                Token = await _generator.GenerateToken(user),

            };
        }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);



            return new UserDto
            {
                Email = user.Email,
                Token = await _generator.GenerateToken(user),

            };


        }



    }
}

