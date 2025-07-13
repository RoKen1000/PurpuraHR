using Purpura.DataAccess.DataContext;
using Purpura.Models.Entities;
using Purpura.Repositories.Interfaces;

namespace Purpura.Repositories
{
    public class GoalRepository : BaseRepository<Goal>, IGoalRepository
    {
        public GoalRepository(PurpuraDbContext dbContext) : base(dbContext)
        {

        }
    }
}
