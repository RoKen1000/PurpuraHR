using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.DataAccess.DataContext;
using Purpura.Models.Entities;

namespace Purpura.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(PurpuraDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
