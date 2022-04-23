using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HYS.Application.Services.Interfaces;
using HYS.Domain.Entities;
using HYS.Domain.Params;
using HYS.Persistence.Repositories.Interfaces;

namespace HYS.Application.Services.Concrete
{
    public class GenericManager<TEntity>:IGenericService<TEntity> where TEntity : class, new()
    {
        private readonly IGenericRepository<TEntity> _genericService;


        public GenericManager(IGenericRepository<TEntity> genericService)
        {
            _genericService = genericService;
        }
        public List<Product> GetAll(string search, int? CategoryId, string sortBy, int page, int PAGE_SIZE)
        {
           return _genericService.GetAll(search,CategoryId,  sortBy, page,PAGE_SIZE  );
        }

        public TEntity GetById(int id)
        {
            return _genericService.GetById(id);
        }

        public void Insert(TEntity entity)
        {
            _genericService.Insert(entity);
        }

        public void Update(TEntity entity)
        {
            _genericService.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _genericService.Delete(entity);
        }

        public void Delete(int id)
        {
            var values = _genericService.GetById(id);
            _genericService.Delete(values);
        }

        //public async Task<dynamic> Pagging(ProductRequestParams requestParams)
        //{
        //    return await _genericService.Pagging(requestParams);
        //}
    }
}
