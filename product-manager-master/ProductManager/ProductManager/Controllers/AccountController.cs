using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductManager.DTO;
using ProductManager.Models;
using ProductManager.Services;
using ProductManager.Validators;
using ProductManagerDTO.DTO.User;
using ProductManagerIntegrationTest.Endpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace ProductManager.Controllers
{
    /// <summary>
    /// AccountController manages Login operation of APIUser Entity
    /// </summary>
    [Route(ApiRoute.Account)]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// UserManager for managing ApiUsers from persistant storage.
        /// </summary>
        private readonly UserManager<ApiUser> _userManager;

        /// <summary>
        /// Logger for errors and infos.
        /// </summary>
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// Automapper for dto and entity convertions.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Authentication manager for providing authentication.
        /// </summary>
        private readonly IAuthManager _authManager;

        /// <summary>
        /// Resource manager for retreving string values.
        /// </summary>
        protected readonly ResourceManager _resourceManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager">UserManager for managing ApiUsers from persistant storage.</param>
        /// <param name="logger">Logger for errors and infos.</param>
        /// <param name="mapper"> Automapper for dto and entity convertions.</param>
        /// <param name="authManager">Authentication manager for providing authentication.</param>
        /// <param name="resourceManager">Resource manager for retreving string values.</param>
        public AccountController(UserManager<ApiUser> userManager,
            ILogger<AccountController> logger,
            IMapper mapper,
            IAuthManager authManager,
            ResourceManager resourceManager
            )
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
            _resourceManager = resourceManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            _logger.LogInformation($"Login Attempt for {userDTO.Email} ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _authManager.ValidateUser(userDTO))
                {
                    return BadRequest(); // todo fix this send error message
                }

                return Accepted(new { Token = await _authManager.CreateToken() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Login)}");
                return StatusCode(500, _resourceManager
                .GetString(nameof(ValidationMessageKey.InternalServerError)));
            }
        }
    }
}
