using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Common.Results;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using PurpuraWeb.Models.Entities;
using System.Linq.Expressions;

namespace Purpura.Services
{
    public class CompanyEmployeeService : BaseService<CompanyEmployee>, ICompanyEmployeeService
    {
        private readonly ICompanyEmployeeRepository _companyEmployeeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserManagementRepository _userManagementRepository;

        public CompanyEmployeeService(IMapper mapper,
            IUnitOfWork unitOfWork,
            ICompanyEmployeeRepository companyEmployeeRepository,
            ICompanyRepository companyRepository,
            IUserManagementRepository userManagementRepository) : base(mapper, unitOfWork)
        {
            _companyEmployeeRepository = companyEmployeeRepository;
            _companyRepository = companyRepository;
            _userManagementRepository = userManagementRepository;
        }

        public async Task<Result> CreateAsync(CompanyEmployeeViewModel viewModel)
        {
            var company = await _companyRepository.GetSingleAsync(c => c.ExternalReference == viewModel.CompanyExternalReference);

            if (company == null)
            {
                return Result.Failure("Company not found.");
            }

            var newEntity = _mapper.Map<CompanyEmployee>(viewModel);
            newEntity.DateCreated = DateTime.Now;
            newEntity.ExternalReference = Guid.NewGuid().ToString();
            newEntity.Company = company;

            if (viewModel.EmailExists)
            {
                var appUser = await _userManagementRepository.GetSingleAsync(u => u.NormalizedEmail == viewModel.Email.ToUpper());

                if (appUser == null)
                {
                    return Result.Failure("User not found.");
                }

                newEntity.ApplicationUser = appUser;
            }

            _companyEmployeeRepository.Create(newEntity);

            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Result> EditAsync(CompanyEmployeeViewModel viewModel)
        {
            var entity = await _companyEmployeeRepository.GetSingleAsync(ce => ce.ExternalReference == viewModel.ExternalReference);

            if (entity == null)
            {
                return Result.Failure("Employee not found.");
            }

            _mapper.Map<CompanyEmployeeViewModel, CompanyEmployee>(viewModel, entity);
            entity.DateEdited = DateTime.Now;

            _companyEmployeeRepository.Update(entity);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CompanyEmployeeViewModel?> GetAsync(Expression<Func<CompanyEmployee, bool>> expression)
        {
            var entity = await _companyEmployeeRepository.GetSingleAsync(expression);

            if (entity == null)
            {
                return null;
            }

            return _mapper.Map<CompanyEmployeeViewModel>(entity);
        }

        public async Task<CompanyEmployeeViewModel?> GetByExternalReferenceAsync(string extRef)
        {
            var entity = await _companyEmployeeRepository.GetSingleAsync(ce => ce.ExternalReference == extRef);

            if (entity != null)
            {
                var companyEmployeeViewModel = _mapper.Map<CompanyEmployeeViewModel>(entity);

                var company = await _companyRepository.GetSingleAsync(c => c.Id == entity.CompanyId);

                if (company != null)
                {
                    companyEmployeeViewModel.CompanyExternalReference = company.ExternalReference;
                }

                return companyEmployeeViewModel;
            }

            return null;
        }
    }
}
