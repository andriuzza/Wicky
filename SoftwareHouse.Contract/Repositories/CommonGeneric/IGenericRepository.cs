using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoftwareHouse.Contract.Repositories.CommonGeneric
{
    public interface IGenericRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity model);
        Task DeleteAsync(int id);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
