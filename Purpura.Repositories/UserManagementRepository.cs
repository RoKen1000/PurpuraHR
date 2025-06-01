using Purpura.DataAccess.DataContext;
using Purpura.Repositories.Interfaces;
using PurpuraWeb.Models.Entities;

namespace Purpura.Repositories
{
    public class UserManagementRepository : BaseRepository<ApplicationUser>, IUserManagementRepository
    {
        public UserManagementRepository(PurpuraDbContext dbContext) : base(dbContext)
        {

        }
    }
}
