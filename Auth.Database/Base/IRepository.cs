using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Auth.Database.Models;

namespace Auth.Database.Base
{
    public interface IRepository<TEntity> where TEntity : DbEntity
    {
        DbSet<TEntity> Entities { get; }
        Task<List<TEntity>> GetAllAsync();
        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<TEntity, TResult> selector);
        Task<TEntity> FindAsync(Guid id);
        TEntity Find(Guid id);
        Task RemoveAsync(Guid id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> CreateOrUpdateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}