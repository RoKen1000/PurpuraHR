using Purpura.Common.Results;

namespace Purpura.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task<Result> SaveChangesAsync();
        IAnnualLeaveRepository AnnualLeaveRepository { get; }
        IUserManagementRepository UserManagementRepository { get; }
        IGoalRepository GoalRepository { get; }
        ICompanyRepository CompanyRepository { get; }
    }
}
