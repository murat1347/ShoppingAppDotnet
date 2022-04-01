using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerServiceLayer.IServices
{
    public interface IGenericService<T,K> where T : BaseEntity
    {
        Task <IList<T>> GetAll();

        Task<T> GetOne(K id);

        Task Delete(K id);

        void Update(T t);

        Task Insert(T t);

        Task Save();
    }
}
