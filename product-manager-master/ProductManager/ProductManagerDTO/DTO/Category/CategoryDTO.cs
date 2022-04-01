using ProductManager.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.DTO
{
    /// <summary>
    /// CategoryDTO For returning single or list of Category Entity Instances.
    /// Only For Response
    /// </summary>
    public class CategoryDTO
    {
        /// <summary>
        /// Primary Key of the Category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ParentId of the category.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Name of the category.
        /// </summary>
        public string Name { get; set; }
    }
}
