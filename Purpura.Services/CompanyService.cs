using AutoMapper;
using Purpura.Common.Results;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
using Purpura.Services.Interfaces;

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

            _unitOfWork.CompanyRepository.Create(newEntity);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result.IsSuccess)
            {
                return Result<string>.Success(newEntity.ExternalReference);
            }
            else
            {
                return Result<string>.Failure("EntityCreationFailed");
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
