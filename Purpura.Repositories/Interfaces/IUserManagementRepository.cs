using Purpura.Models.ViewModels;
using PurpuraWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Repositories.Interfaces
{
    public interface IUserManagementRepository
    {
        Task<ApplicationUserViewModel> GetUser(Expression<Func<ApplicationUser, bool>> filter);
        Task<ApplicationUser> GetUserEntity(Expression<Func<ApplicationUser, bool>> filter);
        Task UpdateUser(ApplicationUserViewModel user);
    }
}
