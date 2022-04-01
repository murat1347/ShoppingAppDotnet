using ProductManager.Models;
using ProductManager.Repository;
using ProductManagerRepository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.IRepository
{
    /// <summary>
    /// Implementation of UnitOfWork pattern.
    /// </summary>
    public interface IUnitOfWork
    {
        ProductRepository ProductRepository { get; }
        SaleRepository SaleRepository { get; }
        PurchaseRepository PurchaseRepository { get; }

        /// <summary>
        /// Get Repository of Entity by giving Entity type.
        /// </summary>
        /// <typeparam name="T">Type of the Entity</typeparam>
        /// <returns>Repository that can do operations on entity T</returns>
        IGenericRepository<T> Repository<T>() where T : BaseEntity;

        /// <summary>
        /// Get Spesific type of the implementation of repository.
        /// </summary>
        /// <typeparam name="T">The Type of the entity.</typeparam>
        /// <typeparam name="R">Type of the Repository implementation.</typeparam>
        /// <returns>IGenericRepository implementation that type of R</returns>
        public R Repository<T, R>() where T : BaseEntity where R : IGenericRepository<T>;

        /// <summary>
        /// Called when persisting dirty values.
        /// </summary>
        /// <returns></returns>
        Task Save();
    }
}
