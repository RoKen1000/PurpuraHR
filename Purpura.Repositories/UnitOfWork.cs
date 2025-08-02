using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.Common.Results;
using Purpura.DataAccess.DataContext;

namespace Purpura.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PurpuraDbContext _dbContext;
        public IAnnualLeaveRepository AnnualLeaveRepository { get; private set; }
        public IUserManagementRepository UserManagementRepository { get; private set; }
        public IGoalRepository GoalRepository { get; private set; }
        public ICompanyRepository CompanyRepository { get; private set; }

        public UnitOfWork(PurpuraDbContext dbContext,
            IAnnualLeaveRepository annualLeaveRepository,
            IUserManagementRepository userManagementRepository,
            IGoalRepository goalRepository,
            ICompanyRepository companyRepository)
        {
            _dbContext = dbContext;
            AnnualLeaveRepository = annualLeaveRepository;
            UserManagementRepository = userManagementRepository;
            GoalRepository = goalRepository;
            CompanyRepository = companyRepository;
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
