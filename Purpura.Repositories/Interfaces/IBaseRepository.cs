using Purpura.Common;
using System.Linq.Expressions;

namespace Purpura.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        void Create(TEntity entity);
    }
}
