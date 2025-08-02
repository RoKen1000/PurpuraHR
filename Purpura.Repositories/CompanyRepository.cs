using Microsoft.EntityFrameworkCore;
using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.DataAccess.DataContext;
using Purpura.Models.Entities;

namespace Purpura.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        private readonly PurpuraDbContext _context;
        public CompanyRepository(PurpuraDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<Company> GetByExternalReferenceWithEmployeesAsync(string companyReference)
        {
            IQueryable<Company> query = _context.Companies;

            query = query.Where(c => c.ExternalReference == companyReference)
                .Include(c => c.Employees);

            var entity = await query.FirstOrDefaultAsync();

            return entity;
        }
    }
}
