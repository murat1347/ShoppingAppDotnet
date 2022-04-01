using ProductManager.IRepository;
using ProductManager.Models;
using ProductManager.Repository;
using ProductManagerServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagerServiceLayer.Services
{
    public abstract class GenericService<T,K> : IGenericService<T, K> where T: BaseEntity
    {

        protected readonly IUnitOfWork _unitOfWork;

        protected GenericService(IUnitOfWork unitOfWork){
            _unitOfWork = unitOfWork;
        }

        public async Task Delete(K id)
        {
           await _unitOfWork.Repository<T>().Delete(id);
        }

        public async Task<IList<T>> GetAll()
        {
           return await _unitOfWork.Repository<T>().GetAll();
        }

        public async Task<T> GetOne(K id)
        {
            return await _unitOfWork.Repository<T>().Get(e=>e.Id.Equals(id));
        }

        public async Task Insert(T t)
        {
             await _unitOfWork.Repository<T>().Insert(t);
        }

        public async Task Save()
        {
            await _unitOfWork.Save();
        }

        public void Update(T t)
        {
            _unitOfWork.Repository<T>().Update(t);
        }
    }
}
