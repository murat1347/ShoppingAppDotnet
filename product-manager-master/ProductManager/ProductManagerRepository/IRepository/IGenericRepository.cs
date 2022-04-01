using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductManager.IRepository
{
    /// <summary>
    /// Generic repository for accesing enties
    /// </summary>
    /// <typeparam name="T">Any implementation of BaseEntity which also mentioned in DbContext</typeparam>
    public interface IGenericRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Get All Entities of type T
        /// </summary>
        /// <param name="expression">Filter by a expressions.</param>
        /// <param name="orderBy">Order entities for given parameter.</param>
        /// <returns>List contains type of T</returns>
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
         );

        /// <summary>
        /// Get All Entities of type T with paging.
        /// </summary>
        /// <param name="pageNumber">The number of the page, pageNumber*numberOfRecords are skipped.</param>
        /// <param name="numberOfRecords">Total number of records in a page.</param>
        /// <param name="expressions">Filter by expressions</param>
        /// <param name="orderBy">Order entities for given parameter.</param>
        /// <param name="includes">Included relationships</param>
        /// <returns></returns>
        Task<IList<T>> GetAllPaged(
            int pageNumber,
            int numberOfRecords,
            List<Expression<Func<T, bool>>> expressions = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null
            );

        /// <summary>
        /// Get single Entity of T.
        /// </summary>
        /// <param name="expression">Filter by a expression</param>
        /// <param name="includes">Included relationships</param>
        /// <returns></returns>
        Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null);

        /// <summary>
        /// Insert T.
        /// </summary>
        /// <param name="entity">The entity will inserted</param>
        /// <returns></returns>
        Task Insert(T entity);

        /// <summary>
        /// Insert multiple entities which is type of T.
        /// </summary>
        /// <param name="entities">Entitis will be inserted.</param>
        /// <returns></returns>
        Task InsertRange(IEnumerable<T> entities);

        /// <summary>
        /// Delete Entitiy with primary key.
        /// </summary>
        /// <param name="id">The Primary key of the entity which will be deleted.</param>
        /// <returns></returns>
        Task Delete(object id);

        /// <summary>
        /// Delete number of entities.
        /// </summary>
        /// <param name="entities">The entities which will be deleted.</param>
        void DeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// Update an entitiy type of T.
        /// </summary>
        /// <param name="entity">The entity will be updated.</param>
        void Update(T entity);

        /// <summary>
        /// The total number of entities given expression.
        /// </summary>
        /// <param name="expressions"></param>
        /// <returns>The long count of total number of entities.</returns>
        Task<long> Count(List<Expression<Func<T, bool>>> expressions = null);
    }
}
