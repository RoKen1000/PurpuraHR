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

        //public async Task<ApplicationUserViewModel> GetUser(Expression<Func<ApplicationUser, bool>> filter)
        //{
        //    var user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(filter);

        //    if(user == null)
        //        return null;

        //    return _mapper.Map<ApplicationUserViewModel>(user);
        //}

        //public async Task<ApplicationUser> GetUserEntity(Expression<Func<ApplicationUser, bool>> filter)
        //{
        //    var user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(filter);

        //    if (user == null)
        //        return null;

        //    return user;
        //}

        //public async Task UpdateUser(ApplicationUserViewModel userViewModel)
        //{
        //    var userEntity = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userViewModel.Id);

        //    if(userEntity != null)
        //    {
        //        _mapper.Map<ApplicationUserViewModel, ApplicationUser>(userViewModel, userEntity);

        //        _dbContext.ApplicationUsers.Update(userEntity);

        //        await _dbContext.SaveChangesAsync();

        //        return;
        //    }

        //    throw new NullReferenceException();
        //}
    }
}
