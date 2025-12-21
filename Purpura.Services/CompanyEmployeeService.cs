using AutoMapper;
using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Common.Results;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;

namespace Purpura.Services
{
    public class CompanyEmployeeService : BaseService<CompanyEmployee>, ICompanyEmployeeService
    {
        private readonly ICompanyEmployeeRepository _companyEmployeeRepository;
        private readonly ICompanyRepository _companyRepository;

        public CompanyEmployeeService(IMapper mapper, 
            IUnitOfWork unitOfWork, 
            ICompanyEmployeeRepository companyEmployeeRepository,
            ICompanyRepository companyRepository) : base(mapper, unitOfWork)
        {
            _companyEmployeeRepository = companyEmployeeRepository;
            _companyRepository = companyRepository;
        }

        public async Task<Result> Create(CompanyEmployeeViewModel viewModel)
        {
            var company = await _companyRepository.GetSingleAsync(c => c.ExternalReference == viewModel.CompanyExternalReference);

            if(company == null)
            {
                return Result.Failure("Company not found.");
            }

            var newEntity = _mapper.Map<CompanyEmployee>(viewModel);
            newEntity.DateCreated = DateTime.Now;
            newEntity.ExternalReference = Guid.NewGuid().ToString();
            newEntity.Company = company;

            _companyEmployeeRepository.Create(newEntity);

            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
