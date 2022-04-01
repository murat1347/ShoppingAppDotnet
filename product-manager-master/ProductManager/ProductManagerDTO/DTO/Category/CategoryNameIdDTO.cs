using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerDTO.DTO.Category
{
    /// <summary>
    /// CategoryNameIdDTO for returning only Name and Id values of the Category
    /// Response only.
    /// </summary>
    public class CategoryNameIdDTO
    {
        /// <summary>
        /// Primary Key of the Category
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the category
        /// </summary>
        public string Name { get; set; }
    }
}
