using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Models
{
    /// <summary>
    /// Customer is who we sell our products.
    /// </summary>
    public class Customer : Person
    {
       /// <summary>
       /// List of sales made to this customer.
       /// </summary>
       public virtual IList<Sale> Purchases { get; set; }
    }
}
