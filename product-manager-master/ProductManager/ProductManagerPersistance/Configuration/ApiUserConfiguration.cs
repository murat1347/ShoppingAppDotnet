using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.ModelConfiguration
{
    //TODO implement and add to the modelConfiguration.

    /// <summary>
    /// ApiUser configuration for configuration ApiUser Entity and ApiUser Table.
    /// </summary>
    public class ApiUserConfiguration
    {
        /// <summary>
        /// Minimum password length can be used in a ApiUser
        /// </summary>
        public const int MinPasswordLength = 6;

        /// <summary>
        /// MaxPasswordLength password length can be used in a ApiUser
        /// </summary>
        public const int MaxPasswordLength = 32;
    }
}
