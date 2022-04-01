using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Constraints
{
    public class EnvironmentKeys
    {
        /// <summary>
        /// Jwt key's environment key.
        /// </summary>
        public const string JWTKey = "KEY";

        /// <summary>
        /// .Net core environment mode key.
        /// </summary>
        public static readonly string EnvironmentKey = "ASPNETCORE_ENVIRONMENT";
    }
}
