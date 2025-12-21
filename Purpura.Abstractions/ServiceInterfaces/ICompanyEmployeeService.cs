using Purpura.Common.Results;
using Purpura.Models.ViewModels;

namespace Purpura.Abstractions.ServiceInterfaces
{
    public interface ICompanyEmployeeService
    {
        Task<Result> Create(CompanyEmployeeViewModel viewModel);
    }
}
