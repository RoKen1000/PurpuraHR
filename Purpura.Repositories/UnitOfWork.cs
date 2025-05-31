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

        public UnitOfWork(PurpuraDbContext dbContext,
            IAnnualLeaveRepository annualLeaveRepository,
            IUserManagementRepository userManagementRepository)
        {
            _dbContext = dbContext;
            AnnualLeaveRepository = annualLeaveRepository;
            UserManagementRepository = userManagementRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
