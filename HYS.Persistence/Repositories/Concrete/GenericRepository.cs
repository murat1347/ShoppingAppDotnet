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
            return (_dbConnection.Query<TEntity>("SELECT * FROM dbo.Products").ToList());
        }

        public TEntity GetById(int id)
        {
            //return (_dbConnection.QueryFirst<TEntity>($"SELECT * FROM dbo.Products WHERE Id= '{id}' "));
            return GetById(id);
        }

        public void Insert(TEntity entity)
        {
            //_dbConnection.Query<Product>("sp_InsertVal", entity, commandType: CommandType.StoredProcedure);
            _dbConnection.Insert(entity);
        }

        public void Update(TEntity entity)
        {
            _dbConnection.Query("sp_UpdateVal", entity, commandType: CommandType.StoredProcedure);
        }

        public void Delete(TEntity entity)
        {
            _dbConnection.Query("sp_DeleteValue", entity, commandType: CommandType.StoredProcedure);
        }
    }
}
