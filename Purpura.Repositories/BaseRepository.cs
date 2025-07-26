using Microsoft.EntityFrameworkCore;
using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.DataAccess.DataContext;
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

        public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = dbSet;

            query = query.Where(filter);

            var entity = await query.FirstOrDefaultAsync();

            return entity;
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = dbSet;

            query = query.Where(filter);

            return await query.ToListAsync();
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public void Create(TEntity entity)
        {
            _dbContext.Add(entity);
        }
    }
}
