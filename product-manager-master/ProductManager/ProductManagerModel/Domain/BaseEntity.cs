using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Models
{
    /// <summary>
    /// Base entity creates abstraction for models.
    /// </summary>
    public abstract class BaseEntity
    {
        public long Id { get; set; }
    }
}
