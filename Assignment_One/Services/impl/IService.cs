using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_One.Services.impl
{
    public interface IService<TEntity, in TKey> where TEntity:class
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(TKey id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TKey id, TEntity entity);
        Task DeleteAsync(TKey id);
    }
}
