using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.DataAccess.DataContext;
using Purpura.Models.Entities;

namespace Purpura.Repositories
{
    public class AnnualLeaveRepository : BaseRepository<AnnualLeave>, IAnnualLeaveRepository
    {

        public AnnualLeaveRepository(PurpuraDbContext dbContext) : base(dbContext)
        {

        }
        
    }
}
