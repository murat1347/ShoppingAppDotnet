using ProductManagerDTO.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace ProductManager.Services
{
    /// <summary>
    /// Helps getting resource values with using dependency injection.
    /// </summary>
    public class PMResourceManager : ResourceManager
    {
        /// <summary>
        /// Create a resource manager.
        /// </summary>
        public PMResourceManager (): base ( typeof(ValidationResource) ){
        }
    }
}
