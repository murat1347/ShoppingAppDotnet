using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using HYS.Domain.Entities;
using HYS.Persistence.Repositories.Interfaces;

namespace HYS.Persistence.Repositories.Concrete
{
    public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly IDbConnection _dbConnection;
        public GenericRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<TEntity> GetAll()
        {
            return (_dbConnection.GetAll<TEntity>().ToList());
        }

        public TEntity GetById(int id)
        {
            //return (_dbConnection.QueryFirst<TEntity>($"SELECT * FROM dbo.Products WHERE Id= '{id}' "));
            return _dbConnection.Get<TEntity>(id);
        }

        public void Insert(TEntity entity)
        {
            var query = @"
             Insert Into Category (Name,Description,Stock,Price,CategoryId,AddedDate,AddedBy)
	VALUES(@Name,@Description,@Stock,@Price,@CategoryId,@AddedDate,@AddedBy)";
            //_dbConnection.Query(query,entity);
            _dbConnection.Insert(entity);
        }

        public void Update(TEntity entity)
        {
            _dbConnection.Update(entity);
            //_dbConnection.Query("sp_UpdateVal", entity, commandType: CommandType.StoredProcedure);
        }

        public void Delete(TEntity entity)
        {
            _dbConnection.Delete(entity);
            //_dbConnection.Query("sp_DeleteValue", entity, commandType: CommandType.StoredProcedure);
        }
    }
}
