using Purpura.Common.Results;
using Purpura.DataAccess.DataContext;
using Purpura.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PurpuraDbContext _dbContext;
        public IAnnualLeaveRepository AnnualLeaveRepository { get; private set; }
        public IUserManagementRepository UserManagementRepository { get; private set; }
        public IGoalRepository GoalRepository { get; private set; }

        public UnitOfWork(PurpuraDbContext dbContext,
            IAnnualLeaveRepository annualLeaveRepository,
            IUserManagementRepository userManagementRepository,
            IGoalRepository goalRepository)
        {
            _dbContext = dbContext;
            AnnualLeaveRepository = annualLeaveRepository;
            UserManagementRepository = userManagementRepository;
            GoalRepository = goalRepository;
        }

        public async Task<Result> SaveChangesAsync()
        {
            var result = await _dbContext.SaveChangesAsync();

            if(result == 0)
            {
                return Result.Failure("Database save failed.");
            }

            return Result.Success();
        }
    }
}
