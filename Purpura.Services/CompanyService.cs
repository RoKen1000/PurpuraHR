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
            newEntity.Address = AddressHelpers.ConstructAddressString(new string[4] {viewModel.AddressLine1, viewModel.AddressLine2, viewModel.AddressLine3, viewModel.Postcode});

            _unitOfWork.CompanyRepository.Create(newEntity);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result.IsSuccess)
            {
                return Result<string>.Success(newEntity.ExternalReference);
            }
            else
            {
                return Result<string>.Failure("Entity Creation Failed");
            }
        }

        public async Task<CompanyViewModel?> GetByExternalReferenceAsync(string externalReference)
        {
            var companyEntity = await _unitOfWork.CompanyRepository.GetSingleAsync(c => c.ExternalReference == externalReference);

            if(companyEntity != null)
            {
                return _mapper.Map<CompanyViewModel>(companyEntity);
            }

            return null;
        }
    }
}
