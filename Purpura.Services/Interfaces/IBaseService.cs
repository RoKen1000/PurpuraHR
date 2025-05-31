using Purpura.Common;
using System.Linq.Expressions;

namespace Purpura.Services.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<Result> Edit(TEntity entity);
        Task<Result> Delete(TEntity entity);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter);
        Task<Result> Create(TEntity entity);
    }
}
