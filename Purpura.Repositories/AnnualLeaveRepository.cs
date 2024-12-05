using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Purpura.DataAccess.DataContext;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
using Purpura.Utility.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Repositories
{
    public class AnnualLeaveRepository : IAnnualLeaveRepository
    {
        private readonly IMapper _mapper;
        private readonly PurpuraDbContext _dbContext;
        private readonly IUserManagementRepository _userManagementRepository;

        public AnnualLeaveRepository(IMapper mapper,
            PurpuraDbContext dbContext,
            IUserManagementRepository userManagementRepository)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userManagementRepository = userManagementRepository;
        }

        public async Task<bool> BookTimeOff(AnnualLeaveViewModel annualLeavePeriod)
        {
            try
            {
                var user = await _userManagementRepository.GetUserEntity(u => u.Id == annualLeavePeriod.UserId);

                if (user == null)
                    return false;

                var daysUsed = (annualLeavePeriod.EndDate - annualLeavePeriod.StartDate).Days;
                var newAnnualLeaveTotal = AnnualLeaveResolver.WorkOutNumberOfDaysLeft(user.AnnualLeaveDays, daysUsed);
                var isValidBooking = AnnualLeaveResolver.IsValidBooking(user.AnnualLeaveDays, newAnnualLeaveTotal);

                if (!isValidBooking)
                    return false;

                var annualLeaveEntity = _mapper.Map<AnnualLeave>(annualLeavePeriod);

                annualLeaveEntity.DateCreated = DateTime.Now;
                annualLeaveEntity.ExternalReference = Guid.NewGuid().ToString();
                annualLeaveEntity.User = user;
                _dbContext.Add(annualLeaveEntity);

                user.AnnualLeaveDays = newAnnualLeaveTotal;
                _dbContext.Update(user);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> GetUserAnnualLeaveCount(string userId)
        {
            var user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException("User not found.");
            }

            if(user.AnnualLeaveDays == null || user.AnnualLeaveDays == 0)
                return 0;

            return user.AnnualLeaveDays;
        }
    }
}
