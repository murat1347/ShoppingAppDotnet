using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Constraints
{
    /// <summary>
    /// Provides strongly typed application configuration keys.
    /// </summary>
    public class AppConfigurationKeys
    {
        public const string JWT = "Jwt";

        public const string JWTKey = "JwtKey";

        public const string JWTLifetime = "lifetime";

        public const string JWTIssuer = "Issuer";
    }
}
