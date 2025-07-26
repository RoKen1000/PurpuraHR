using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Purpura.Abstractions.RepositoryInterfaces;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Common.Results;
using Purpura.Models.ViewModels;
using PurpuraWeb.Models.Entities;
using System.Linq.Expressions;

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

            return _mapper.Map<ApplicationUserViewModel>(user);
        }

        public async Task<Result> AddUserClaimAsync(string userId, string value)
        {
            var userEntity = await _unitOfWork.UserManagementRepository.GetSingleAsync(u => u.Id == userId);

            if(userEntity == null)
            {
                return Result.Failure("User not found.");
            }

            var result = await _userManager.AddClaimAsync(userEntity, new System.Security.Claims.Claim("CompanyReference", value));

            if (result.Succeeded)
            {
                return Result.Success();
            }

            return Result.Failure("Failed to add claim.");
        }

        public async Task UpdateUser(ApplicationUserViewModel userViewModel)
        {
            var userEntity = await _unitOfWork.UserManagementRepository.GetSingleAsync(u => u.Id == userViewModel.Id);

            if (userEntity != null)
            {
                _mapper.Map<ApplicationUserViewModel, ApplicationUser>(userViewModel, userEntity);

                _unitOfWork.UserManagementRepository.Update(userEntity);

                await _unitOfWork.SaveChangesAsync();

                return;
            }

            throw new NullReferenceException();
        }

        public async Task<ApplicationUser?> GetUserEntityByIdAsync(string id)
        {
            var user = await _unitOfWork.UserManagementRepository.GetSingleAsync(u => u.Id == id);

            if(user == null)
            {
                return null;
            }

            return user;
        }
    }
}
