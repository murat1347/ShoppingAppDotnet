using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// TODO: FIX THIS NAME SPACE
namespace ProductManagerIntegrationTest.Endpoint
{
    /// <summary>
    /// Routes used in controller defined here for helping tests.
    /// </summary>
    public static class ApiRoute
    {
        /// <summary>
        /// The ROOT of the routes.
        /// </summary>
        public const string API = "api/v{version:apiVersion}";

        /// <summary>
        /// Category Route Root
        /// </summary>
        public const string Category = API + "/Category";

        /// <summary>
        /// Product Route Root.
        /// </summary>
        public const string Product = API + "/Product";

        /// <summary>
        /// Customer Route Root.
        /// </summary>
        public const string Customer = API + "/Customer";

        /// <summary>
        /// Seller Route Root.
        /// </summary>
        public const string Seller = API + "/Seller";

        /// <summary>
        /// Sale Route Root.
        /// </summary>
        public const string Sale = API + "/Sale";

        /// <summary>
        /// Purchase Route Root.
        /// </summary>
        public const string Purchase = API + "/Purchase";

        /// <summary>
        /// Account Route Root.
        /// </summary>
        public const string Account = API + "/Account";

        /// <summary>
        /// Dashboard route root.
        /// </summary>
        public const string Dashboard = API + "/Dashboard";
    }
}
