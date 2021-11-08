﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chats.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Chats.Database.Base
{
public abstract class BaseRepository<TEntity> where TEntity : DbEntity
    {
        private readonly IChatDbContextFactory _dbContextFactory;

        public DbSet<TEntity> Entities { get; set; }

        protected BaseRepository(IChatDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        protected ChatDbContext CreateDbContext()
        {
            return _dbContextFactory.Create();
        }

        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<TEntity, TResult> selector)
        {
            await using ChatDbContext dbContext = CreateDbContext();
            List<TEntity> result = await dbContext.Set<TEntity>()
                .Where(c => c.IsDeleted == false)
                .OrderBy(c => c.Created)
                .ToListAsync();

            return result.Select(selector);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            await using ChatDbContext dbContext = CreateDbContext();
            return await dbContext.Set<TEntity>()
                .Where(c => c.IsDeleted == false)
                .OrderBy(c => c.Created)
                .ToListAsync();
        }

        public async Task<TEntity> FindAsync(Guid id)
        {
            await using ChatDbContext dbContext = CreateDbContext();
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public TEntity Find(Guid id)
        {
            using ChatDbContext dbContext = CreateDbContext();
            return dbContext.Set<TEntity>().Find(id);
        }

        public Task<TEntity> CreateOrUpdateAsync(TEntity entity)
        {
            return entity.IsNew()
                ? CreateAsync(entity)
                : UpdateAsync(entity);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            await using ChatDbContext dbContext = CreateDbContext();
            entity.Created = DateTime.UtcNow;
            await dbContext.Set<TEntity>().AddAsync(entity);
            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await using ChatDbContext context = CreateDbContext();
            TEntity attachedEntity = await context.Set<TEntity>().FindAsync(entity.GetPrimaryKey());
            context.Entry(attachedEntity).CurrentValues.SetValues(entity);

            await context.SaveChangesAsync(true);

            return entity;
        }


        public async Task RemoveAsync(Guid id)
        {
            await using ChatDbContext dbContext = CreateDbContext();
            TEntity entity = await dbContext.Set<TEntity>().FindAsync(id);
            entity.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }
    }
}