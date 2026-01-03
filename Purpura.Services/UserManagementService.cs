using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Common.Results;
using Purpura.Models.ViewModels;
using Purpura.Utility.Helpers;
using PurpuraWeb.Models.Entities;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Purpura.Services
{
    public class UserManagementService : BaseService<ApplicationUser>, IUserManagementService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserManagementService(IMapper mapper,
            IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager) : base(mapper, unitOfWork)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUserViewModel?> GetUser(Expression<Func<ApplicationUser, bool>> filter)
        {
            var user = await _unitOfWork.UserManagementRepository.GetSingleAsync(filter);

            if (user == null)
                return null;

            var viewModel = _mapper.Map<ApplicationUserViewModel>(user);

            var (addressLine1, addressLine2, addressLine3, postcode) = AddressHelpers.DeconstructAddressString(user.Address);
            viewModel.AddressLine1 = addressLine1;
            viewModel.AddressLine2 = addressLine2;
            viewModel.AddressLine3 = addressLine3;
            viewModel.Postcode = postcode;

            return viewModel;
        }

        public async Task<Result> AddUserCompanyReferenceClaimAsync(string userId, string companyReference, string companyId)
        {
            var userEntity = await _unitOfWork.UserManagementRepository.GetSingleAsync(u => u.Id == userId);

            if(userEntity == null)
            {
                return Result.Failure("User not found.");
            }

            var parsedCompanyId = int.Parse(companyId);

            var updateResult = await SetCompanyIdToUserAsync(userEntity, parsedCompanyId);

            if (!updateResult.IsSuccess)
            {
                return updateResult;
            }

            var result = await _userManager.AddClaimAsync(userEntity, new Claim("CompanyReference", companyReference));

            if (result.Succeeded)
            {
                return Result.Success();
            }

            return Result.Failure("Failed to add claim.");
        }

        private async Task<Result> SetCompanyIdToUserAsync(ApplicationUser userEntity, int companyId)
        {
            userEntity.CompanyId = companyId;
            _unitOfWork.UserManagementRepository.Update(userEntity);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Result> UpdateUser(ApplicationUserViewModel userViewModel)
        {
            var userEntity = await _unitOfWork.UserManagementRepository.GetSingleAsync(u => u.Id == userViewModel.Id);

            if (userEntity != null)
            {
                _mapper.Map<ApplicationUserViewModel, ApplicationUser>(userViewModel, userEntity);
                userEntity.DateEdited = DateTime.Now;

                _unitOfWork.UserManagementRepository.Update(userEntity);

                return await _unitOfWork.SaveChangesAsync();
            }

            return Result.Failure("User not found.");
        }

        public async Task<ApplicationUser?> GetUserEntityByIdAsync(string id)
        {
            return await _unitOfWork.UserManagementRepository.GetSingleAsync(u => u.Id == id);
        }

        public async Task<ApplicationUserViewModel?> GetUserViewModelByIdAsync(string id)
        {
            var user = await _unitOfWork.UserManagementRepository.GetSingleAsync(u => u.Id == id);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<ApplicationUserViewModel>(user);
        }
    }
}
