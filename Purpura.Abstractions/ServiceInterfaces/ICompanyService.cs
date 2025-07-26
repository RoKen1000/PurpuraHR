using Purpura.Common.Results;
using Purpura.Models.ViewModels;

namespace Purpura.Abstractions.ServiceInterfaces
{
    public interface ICompanyService
    {
        Task<Result<string>> CreateAsync(CompanyViewModel viewModel);
        Task<CompanyViewModel?> GetByExternalReferenceAsync(string externalReference);
    }
}
