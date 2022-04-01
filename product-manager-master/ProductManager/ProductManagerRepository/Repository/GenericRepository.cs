using Microsoft.EntityFrameworkCore;
using ProductManager.Helpers;
using ProductManager.IRepository;
using ProductManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductManager.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ProductManagerDBContext _context;

        protected readonly DbSet<T> _db;

        public GenericRepository(ProductManagerDBContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task Delete(object id)
        {
            var entity = await _db.FindAsync(id);
            _db.Remove(entity);

            _context.ChangeTracker.DetectChanges();

            var a = _context.ChangeTracker.DebugView.LongView;
            Console.WriteLine(a);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task<T> GetById(object id)
        {
         
            return await _db.FindAsync(id);
        }


        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query = _db;
            if (includes != null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IList<T>> GetAllPaged(int pageNumber,
            int numberOfRecords,
            List<Expression<Func<T, bool>>> expressions = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null
            )
        {
            IQueryable<T> query = _db;

            if (expressions != null)
            {
                foreach(var expression in expressions)
                    query = query.Where(expression);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }else{
                query = query.OrderBy(p=>p.Id);
            }

            if (includes != null)
            {
                foreach(var str in includes)
                    query = query.Include(str);
            }

            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            if (numberOfRecords <= 0)
            {
                numberOfRecords = 1;
            }

            return await query
                .Skip((pageNumber - 1) * numberOfRecords)
                .Take(numberOfRecords)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<long> Count(
           List<Expression<Func<T, bool>>> expressions = null
           )
        {
            IQueryable<T> query = _db;

            if (expressions != null)
            {
                foreach (var expression in expressions)
                    query = query.Where(expression);
            }
           
            return await query.LongCountAsync();
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

    }
}
