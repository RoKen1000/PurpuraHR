using PurpuraWeb.Models.Entities;

namespace Purpura.Repositories.Interfaces
{
    public interface IUserManagementRepository : IBaseRepository<ApplicationUser>
    {
        //Task<ApplicationUserViewModel> GetUser(Expression<Func<ApplicationUser, bool>> filter);
        //Task<ApplicationUser> GetUserEntity(Expression<Func<ApplicationUser, bool>> filter);
        //Task UpdateUser(ApplicationUserViewModel user);
    }
}
