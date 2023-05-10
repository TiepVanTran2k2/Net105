using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> CreateAsync(TEntity key);
        Task<TEntity> UpdateAsync(TEntity key);
        Task<TEntity> DeleteAsync(Guid id);
        Task<List<TEntity>> GetAllAsync();
    }
}
