using Purpura.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter);
        Task<Result> Edit(TEntity entity);
        Task<Result> Delete(TEntity entity);
    }
}
