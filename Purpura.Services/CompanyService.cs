using AutoMapper;
using Purpura.Models.Entities;
using Purpura.Repositories.Interfaces;
using Purpura.Services.Interfaces;

namespace Purpura.Services
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {

        public CompanyService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {

        }
    }
}
