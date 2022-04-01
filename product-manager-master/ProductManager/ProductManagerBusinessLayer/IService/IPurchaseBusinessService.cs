using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerBusinessLayer.IService
{
    public interface IPurchaseBusinessService
    {
        void Add(Purchase purchase);
        void Delete(long id);
        void Update(Purchase purchase);
    }
}
