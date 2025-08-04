using AutoMapper;
using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Common.Results;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Utility.Helpers;

namespace Purpura.Services
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {

        public CompanyService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {

        }

        public async Task<Result<string>> CreateAsync(CompanyViewModel viewModel)
        {
            var newEntity = _mapper.Map<Company>(viewModel);
            newEntity.ExternalReference = Guid.NewGuid().ToString();
            newEntity.DateCreated = DateTime.Now;
            newEntity.Address = AddressHelpers.ConstructAddressString(new string[4] {viewModel.AddressLine1, viewModel.AddressLine2, viewModel.AddressLine3, viewModel.Postcode});

            _unitOfWork.CompanyRepository.Create(newEntity);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result.IsSuccess)
            {
                return Result<string>.Success(new List<string> { newEntity.ExternalReference, newEntity.Id.ToString() });
            }
            else
            {
                return Result<string>.Failure("Entity Creation Failed");
            }
        }

        public async Task<Result> EditAsync(CompanyViewModel viewModel)
        {
            var companyEntity = await _unitOfWork.CompanyRepository.GetSingleAsync(c => c.ExternalReference == viewModel.ExternalReference);

            if(companyEntity == null)
            {
                return Result.Failure("Entity not found.");
            }

            _mapper.Map<CompanyViewModel, Company>(viewModel, companyEntity);
            companyEntity.DateEdited = DateTime.Now;

            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<CompanyViewModel?> GetByExternalReferenceAsync(string externalReference)
        {
            var companyEntity = await _unitOfWork.CompanyRepository.GetSingleAsync(c => c.ExternalReference == externalReference);

            if(companyEntity != null)
            {
                var viewModel = _mapper.Map<CompanyViewModel>(companyEntity);

                var (addressLine1, addressLine2, addressLine3, postcode) = AddressHelpers.DeconstructAddressString(companyEntity.Address);
                viewModel.AddressLine1 = addressLine1;
                viewModel.AddressLine2 = addressLine2;
                viewModel.AddressLine3 = addressLine3;
                viewModel.Postcode = postcode;

                return viewModel;
            }

            return null;
        }

        public async Task<CompanyViewModel?> GetByExternalReferenceWithCompanyEmployeesAsync(string companyReference)
        {
            var companyEntity = await _unitOfWork.CompanyRepository.GetByExternalReferenceWithEmployeesAsync(companyReference);

            if(companyEntity != null)
            {
                return _mapper.Map<CompanyViewModel>(companyEntity);
            }

            return null;
        }

        public async Task<string> GetExternalReferenceByIdAsync(int id)
        {
            var companyEntity = await _unitOfWork.CompanyRepository.GetSingleAsync(c => c.Id == id);

            if(companyEntity == null)
            {
                return "";
            }

            return companyEntity.ExternalReference;
        }
    }
}
