using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OBT.Products.Domain.DomainObjects
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        Task<T> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T obj);
        Task AddAsync(T obj);
        Task SaveAsync();
    }
}