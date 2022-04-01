using ProductManager.DTO;
using ProductManagerDTO.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    /// <summary>
    /// AuthManager interface creating abstraction for helping authentication process with ApiUser.
    /// </summary>
    public interface IAuthManager
    {
        /// <summary>
        /// Validates user information
        /// </summary>
        /// <param name="userDTO">Contains username and password information.</param>
        /// <returns>True if success, false if not.</returns>
        Task<bool> ValidateUser(LoginUserDTO userDTO);

        /// <summary>
        /// Create token for JWT Authentication.
        /// </summary>
        /// <returns>A new fresh jwt token</returns>
        Task<string> CreateToken();
    }
}
