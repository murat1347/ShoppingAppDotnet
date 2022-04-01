using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Models
{
    //TODO ADD CACHING
    /// <summary>
    /// Category for helping products to be in a order.
    /// </summary>
    public class Category : BaseEntity
    {
        /// <summary>
        /// Primary Key of the Category.
        /// </summary>
        public new int Id { get; set; }

        /// <summary>
        /// Name of the category.
        /// Indexed
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ParentId of the category.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Parent of the category.
        /// </summary>
        public virtual Category Parent { get; set; }

        /// <summary>
        /// List of Children categories of the category.
        /// </summary>
        public virtual IList<Category> Children { get; set; }

        /// <summary>
        /// List of Products in this category.
        /// </summary>
        public virtual IList<Product> Products { get; set; }
    }
}
