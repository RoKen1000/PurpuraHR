using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.DataAccess.DataContext;
using Purpura.Models.Entities;
using System.Threading.Tasks;

namespace Purpura.Repositories
{
    public class CompanyEmployeeRepository : BaseRepository<CompanyEmployee>, ICompanyEmployeeRepository
    {
        private readonly PurpuraDbContext _dbContext;

        public CompanyEmployeeRepository(PurpuraDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}
