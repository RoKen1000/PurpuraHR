using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.DataAccess.DataContext;
using Purpura.Models.Entities;

namespace Purpura.Repositories
{
    public class GoalRepository : BaseRepository<Goal>, IGoalRepository
    {
        public GoalRepository(PurpuraDbContext dbContext) : base(dbContext)
        {

        }
    }
}
