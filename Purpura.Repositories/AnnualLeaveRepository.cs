using Purpura.Common;
using Purpura.DataAccess.DataContext;
using Purpura.Models.Entities;
using Purpura.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Purpura.Repositories
{
    public class AnnualLeaveRepository : BaseRepository<AnnualLeave>, IAnnualLeaveRepository
    {

        public AnnualLeaveRepository(PurpuraDbContext dbContext) : base(dbContext)
        {

        }
        
    }
}
