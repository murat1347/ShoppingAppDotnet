using ProductManager.Helpers;
using ProductManager.IRepository;
using ProductManager.Models;
using ProductManagerRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductManagerDBContext _context;

        private readonly Dictionary<Type, dynamic> storage;

        public ProductRepository ProductRepository { get=> Repository<Product,ProductRepository>();}

        public SaleRepository SaleRepository { get=> Repository<Sale,SaleRepository>(); }

        public PurchaseRepository PurchaseRepository { get=> Repository<Purchase,PurchaseRepository>(); }

        private IGenericRepository<T> FindRepository<T>() where T : BaseEntity
        {
            IGenericRepository<T> repository;

            if (typeof(T) == typeof(Sale))
            {
                repository = (IGenericRepository<T>)new SaleRepository(_context);
            }
            else if (typeof(T) == typeof(Purchase))
            {
                repository = (IGenericRepository<T>)new PurchaseRepository(_context);
            }
            else if(typeof(T) == typeof(Product)){
                repository = (IGenericRepository<T>)new ProductRepository(_context);
            }
            else if(typeof(T) == typeof(Category))
            {
                repository = (IGenericRepository<T>)new CategoryRepository(_context);
            }
            else
            {
                repository = new GenericRepository<T>(_context);
            }

            return repository;
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            return Repository<T,GenericRepository<T>>();
        }

        public R Repository<T, R>() where T : BaseEntity where R : IGenericRepository<T>
        {
            Type t = typeof(T);
            if (storage.ContainsKey(t))
            {
                dynamic o;
                storage.TryGetValue(t, out o);
                return o;
            }
            else
            {
                IGenericRepository<T> repository = FindRepository<T>();

                storage.Add(t, repository);

                return (R)repository;
            }
        }


        public UnitOfWork(ProductManagerDBContext context)
        {
            _context = context;
            this.storage = new Dictionary<Type, dynamic>();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
