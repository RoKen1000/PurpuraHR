using AutoMapper;
using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Models.Entities;

namespace Purpura.Services
{
    public class CompanyEmployeeService : BaseService<CompanyEmployee>, ICompanyEmployeeService
    {
        public CompanyEmployeeService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            
        }

    }
}
