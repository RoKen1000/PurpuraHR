using AutoMapper;
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
        private readonly IMapper _mapper;

        public BaseRepository(PurpuraDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.dbSet = _dbContext.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<TEntity> GetByExternalReference(Expression<Func<TEntity, bool>> filter)
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
    }
}
