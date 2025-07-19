using Purpura.Common;
using System.Linq.Expressions;

namespace Purpura.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        void Create(TEntity entity);
    }
}
