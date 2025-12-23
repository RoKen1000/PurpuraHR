using Purpura.Common.Results;
using Purpura.Models.ViewModels;

namespace Purpura.Abstractions.ServiceInterfaces
{
    public interface ICompanyEmployeeService
    {
        Task<Result> CreateAsync(CompanyEmployeeViewModel viewModel);
        Task<CompanyEmployeeViewModel?> GetByExternalReferenceAsync(string extRef);
        Task<Result> EditAsync(CompanyEmployeeViewModel viewModel);
    }
}
