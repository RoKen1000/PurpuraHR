using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Purpura.DataAccess.DataContext;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
using PurpuraWeb.Models.Entities;
using System.Linq.Expressions;

namespace Purpura.Repositories
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly PurpuraDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserManagementRepository(PurpuraDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApplicationUserViewModel> GetUser(Expression<Func<ApplicationUser, bool>> filter)
        {
            var user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(filter);

            if(user == null)
                return null;

            return _mapper.Map<ApplicationUserViewModel>(user);
        }

        public async Task UpdateUser(ApplicationUserViewModel user)
        {
            var userEntity = _mapper.Map<ApplicationUser>(user);

            _dbContext.ApplicationUsers.Update(userEntity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
