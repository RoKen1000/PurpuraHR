using Purpura.Models.ViewModels;
using PurpuraWeb.Models.Entities;
using System.Linq.Expressions;

namespace Purpura.Services.Interfaces
{
    public interface IUserManagementService
    {
        Task<ApplicationUserViewModel?> GetUser(Expression<Func<ApplicationUser, bool>> filter);
        Task<ApplicationUser?> GetUserEntity(Expression<Func<ApplicationUser, bool>> filter);
        Task UpdateUser(ApplicationUserViewModel user);
    }
}
