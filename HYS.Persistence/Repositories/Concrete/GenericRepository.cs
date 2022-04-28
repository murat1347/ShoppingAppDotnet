using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using HYS.Domain.Entities;
using HYS.Domain.Params;
using HYS.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HYS.Persistence.Repositories.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly IDbConnection _dbConnection;

        public GenericRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public List<Product> GetAll(string search, int? CategoryId, string sortBy, int page, int PAGE_SIZE)
        {
            List<Product> allProducts = _dbConnection.GetAll<Product>().ToList();

            if (!string.IsNullOrEmpty(search))
            {
                allProducts = (List<Product>)_dbConnection.Query<Product>("SELECT * FROM Products WHERE Name LIKE @n", new { n = "%" + search + "%" });
            }
            if (CategoryId.HasValue)
            {

                allProducts =
                    (List<Product>)_dbConnection.Query<Product>("SELECT * FROM Products WHERE CategoryId LIKE @n",
                        new { n = "%" + CategoryId + "%" });

                //    "SELECT * FROM dbo.Products ORDER BY id OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY;");
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "asc":
                        if (CategoryId.HasValue)
                        {
                            var query = "SELECT * FROM Products WHERE CategoryId=" + CategoryId + " ORDER BY Price ASC";
                            allProducts =
                                (List<Product>)_dbConnection.Query<Product>(query);
                            break;

                        }
                        else
                        {
                            allProducts =
                                (List<Product>)_dbConnection.Query<Product>(
                                    "SELECT * FROM Products WHERE Price > 0 ORDER BY Price ASC");
                            break;
                        }

                    case "desc":
                        if (CategoryId.HasValue)
                        {
                            var query = "SELECT * FROM Products WHERE CategoryId="+CategoryId+" ORDER BY Price DESC";
                            allProducts =
                                (List<Product>) _dbConnection.Query<Product>(query);
                            break;

                        }
                        else
                        { allProducts =
                                    (List<Product>)_dbConnection.Query<Product>(
                                        "SELECT * FROM Products WHERE Price > 0 ORDER BY Price DESC");
                                break;
                        }


                }
            }

            var result = PaginatedList<Product>.Create(allProducts, page, PAGE_SIZE);

            return result.ToList();
            //return (_dbConnection.GetAll<Product>().ToList());

        }

        public List<TEntity> GetAlls()
        {
            return (List<TEntity>) _dbConnection.GetAll<TEntity>();
        }

        public TEntity GetById(int id)
        {
            //return (_dbConnection.QueryFirst<TEntity>($"SELECT * FROM dbo.Products WHERE Id= '{id}' "));
            return _dbConnection.Get<TEntity>(id);
        }

        public void Insert(TEntity entity)
        {
            //             var query = @"
            //            Insert Into Category (Name,Description,Stock,Price,CategoryId,AddedDate,AddedBy)
            //VALUES(@Name,@Description,@Stock,@Price,@CategoryId,@AddedDate,@AddedBy)";
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

