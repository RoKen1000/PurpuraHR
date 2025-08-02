using Purpura.Models.Entities;

namespace Purpura.Abstractions.RepositoryInterfaces
{
    public interface ICompanyRepository : IBaseRepository<Company>
    {
        Task<Company> GetByExternalReferenceWithEmployeesAsync(string companyReference);
    }
}
