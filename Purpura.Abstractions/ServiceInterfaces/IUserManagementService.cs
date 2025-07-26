using Purpura.Common.Results;
using Purpura.Models.ViewModels;
using PurpuraWeb.Models.Entities;
using System.Linq.Expressions;

namespace Purpura.Abstractions.ServiceInterfaces
{
    public interface IUserManagementService
    {
        Task<ApplicationUserViewModel?> GetUser(Expression<Func<ApplicationUser, bool>> filter);
        Task UpdateUser(ApplicationUserViewModel user);
        Task<Result> AddUserClaimAsync(string userId, string value);
        Task<ApplicationUser?> GetUserEntityByIdAsync(string id);
    }
}
