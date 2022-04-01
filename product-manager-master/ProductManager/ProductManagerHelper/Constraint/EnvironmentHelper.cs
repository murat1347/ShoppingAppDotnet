using ProductManager.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Helpers
{
    /// <summary>
    /// Helping and managing accesing environment.
    /// </summary>
    public static class EnvironmentHelper
    {
        /// <summary>
        /// Production key for production environment.
        /// </summary>
        public static readonly string Production = "Production";

        /// <summary>
        /// Development key for production environment.
        /// </summary>
        public static readonly string Development = "Development";

        /// <summary>
        /// Test key for production environment.
        /// </summary>
        public static readonly string Test =  "Test";

        /// <summary>
        /// Get current environment with using Environment.GetEnvironmentVariable.
        /// </summary>
        public static string CurrentEnvironment { get => Environment.GetEnvironmentVariable(EnvironmentKeys.EnvironmentKey); }

        /// <summary>
        /// Check if the current environment is Production
        /// </summary>
        public static bool IsProduction { get => CurrentEnvironment == Production; }

        /// <summary>
        /// Check if the current environment is Development
        /// </summary>
        public static bool IsDevelopment { get=> CurrentEnvironment == Development; }

        /// <summary>
        /// Check if the current environment is Test
        /// </summary>
        public static bool IsTest { get=> CurrentEnvironment == Test;}
    }
}
