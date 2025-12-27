using Purpura.Common.Results;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using System.Linq.Expressions;

namespace Purpura.Abstractions.ServiceInterfaces
{
    public interface ICompanyEmployeeService
    {
        Task<Result> CreateAsync(CompanyEmployeeViewModel viewModel);
        Task<CompanyEmployeeViewModel?> GetByExternalReferenceAsync(string extRef);
        Task<Result> EditAsync(CompanyEmployeeViewModel viewModel);
        Task<CompanyEmployeeViewModel?> GetAsync(Expression<Func<CompanyEmployee, bool>> expression);
    }
}
