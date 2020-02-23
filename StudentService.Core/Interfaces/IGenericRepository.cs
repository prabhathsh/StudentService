
using Azure.Cosmos;
using StudentService.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.Core.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable
       where TEntity : Entity
    {
        string DatabaseId { get; }
        string ContainerId { get; }
        string PartitionKey { get; }

        Task AddAsync(TEntity entity, PartitionKey partitionKey);

        Task AddRangeAsync(IEnumerable<TEntity> entities, PartitionKey partitionKey);

        Task UpdateAsync(TEntity entity, PartitionKey partitionKey);

        Task DeleteAsync(string id, PartitionKey partitionKey);

        Task<TEntity> GetByIdAsync(string id, PartitionKey partitionKey);

        IAsyncEnumerable<TEntity> GetAllAsync();
    }
}
