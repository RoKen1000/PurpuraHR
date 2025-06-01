using AutoMapper;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
using Purpura.Services.Interfaces;
using PurpuraWeb.Models.Entities;
using System.Linq.Expressions;

namespace Purpura.Services
{
    public class UserManagementService : BaseService<ApplicationUser>, IUserManagementService
    {

        public UserManagementService(IMapper mapper,
            IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            
        }

        public async Task<ApplicationUserViewModel> GetUser(Expression<Func<ApplicationUser, bool>> filter)
        {
            var user = await _unitOfWork.UserManagementRepository.GetSingle(filter);

            if (user == null)
                return null;

            return _mapper.Map<ApplicationUserViewModel>(user);
        }

        public async Task<ApplicationUser> GetUserEntity(Expression<Func<ApplicationUser, bool>> filter)
        {
            var user = await _unitOfWork.UserManagementRepository.GetSingle(filter);

            if (user == null)
                return null;

            return user;
        }

        public async Task UpdateUser(ApplicationUserViewModel userViewModel)
        {
            var userEntity = await _unitOfWork.UserManagementRepository.GetSingle(u => u.Id == userViewModel.Id);

            if (userEntity != null)
            {
                _mapper.Map<ApplicationUserViewModel, ApplicationUser>(userViewModel, userEntity);

                _unitOfWork.UserManagementRepository.Update(userEntity);

                await _unitOfWork.SaveChangesAsync();

                return;
            }

            throw new NullReferenceException();
        }
    }
}
