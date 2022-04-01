using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductManager.Constraints;
using ProductManager.DTO;
using ProductManager.Helpers;
using ProductManager.Models;
using ProductManagerDTO.DTO.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    /// <summary>
    /// AuthManager for helping authentication process with ApiUser.
    /// </summary>
    public class AuthManager : IAuthManager
    {
        /// <summary>
        /// User Manager for helping user operations.
        /// </summary>
        private readonly UserManager<ApiUser> _userManager;

        /// <summary>
        /// Reading configuration files such as JWT key.
        /// </summary>
        private readonly IConfiguration _configuration;
        
        /// <summary>
        /// The user which is trying to authenticate.
        /// </summary>
        private ApiUser _user;

        /// <summary>
        /// Create AuthManager
        /// </summary>
        /// <param name="userManager">User Manager for helping user operations.</param>
        /// <param name="configuration">Reading configuration files such as JWT key.</param>
        public AuthManager(UserManager<ApiUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Create token for JWT Authentication.
        /// </summary>
        /// <returns>A new fresh jwt token</returns>
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Creates token options for helping create token method.
        /// </summary>
        /// <param name="signingCredentials">Information about JWT key signing.</param>
        /// <param name="claims">Roles of the user.</param>
        /// <returns>JwtSecurityKey token</returns>
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection(AppConfigurationKeys.JWT);
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(
                jwtSettings.GetSection(AppConfigurationKeys.JWTLifetime).Value));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection(AppConfigurationKeys.JWTIssuer).Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );

            return token;
        }

        /// <summary>
        /// Get claims from user.
        /// </summary>
        /// <returns>List of claims of the user</returns>
        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, _user.UserName)
             };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        /// <summary>
        /// Read signing credentials from configuration.
        /// </summary>
        /// <returns>SigningCredentials</returns>
        private SigningCredentials GetSigningCredentials()
        {
            // TODO FIX HARDCODED
            string key = _configuration.GetValue<string>(AppConfigurationKeys.JWTKey);
           
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        /// <summary>
        /// Validates user information
        /// </summary>
        /// <param name="userDTO">Contains username and password information.</param>
        /// <returns>True if success, false if not.</returns>
        public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            _user = await _userManager.FindByNameAsync(userDTO.Email);
            var validPassword = await _userManager.CheckPasswordAsync(_user, userDTO.Password);
            return (_user != null && validPassword);
        }
    }
}
