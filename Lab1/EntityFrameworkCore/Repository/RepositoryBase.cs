using Domain.Entities;
using Domain.Repository;
using EntityFrameworkCore.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EntityFrameworkCore.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        protected DbSet<TEntity> _dbSet;
        private readonly DbContextApp _dbContextApp;
        public RepositoryBase(DbContextApp db)
        {
            _dbSet = db.Set<TEntity>();
            _dbContextApp = db;
        }
        public async Task<TEntity> CreateAsync(TEntity key)
        {
            await _dbSet.AddAsync(key);
            await _dbContextApp.SaveChangesAsync();
            return key;
        }

        public async Task<TEntity> DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if(entity == null)
            {
                throw new Exception("Information not exist");
            }
            _dbSet.Remove(entity);
            await _dbContextApp.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity key)
        {
            _dbSet.Update(key);
            await _dbContextApp.SaveChangesAsync();
            return key;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
