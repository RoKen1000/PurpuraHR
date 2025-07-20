using Purpura.DataAccess.DataContext;
using Purpura.Models.Entities;
using Purpura.Repositories.Interfaces;

namespace Purpura.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyService
    {
        public CompanyRepository(PurpuraDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
