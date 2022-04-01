using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Models
{
    /// <summary>
    /// ApiUser for authentication.
    /// </summary>
    public class ApiUser : IdentityUser
    {
        /// <summary>
        /// Fist name of the user.
        /// </summary>
        public string FirstName { get; set; }
    
        /// <summary>
        /// Last name of the user.
        /// </summary>
        public string LastName { get; set; }
    }
}
