using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IAnnualLeaveRepository AnnualLeaveRepository { get; }
        IUserManagementRepository UserManagementRepository { get; }
        IGoalRepository GoalRepository { get; }
    }
}
