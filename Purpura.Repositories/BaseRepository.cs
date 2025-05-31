using Microsoft.EntityFrameworkCore;
using Purpura.Common;
using Purpura.DataAccess.DataContext;
using Purpura.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Purpura.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly PurpuraDbContext _dbContext;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(PurpuraDbContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = dbSet;

            query = query.Where(filter);

            var entity = await query.FirstOrDefaultAsync();

            return entity;
        }

        public async Task<Result> Edit(TEntity entity)
        {
            dbSet.Update(entity);

            await _dbContext.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> Delete(TEntity entity)
        {
            dbSet.Remove(entity);

            await _dbContext.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = dbSet;

            query = query.Where(filter);

            return await query.ToListAsync();
        }
    }
}
