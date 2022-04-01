using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBusinessLayer.IService
{
    public interface ISaleBusinessService
    {
        Task Add(Sale sale);
        Task Delete(long id);
        Task Update(Sale sale);
    }
}
