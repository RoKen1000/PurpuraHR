using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Purpura.DataAccess.DataContext;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
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

        public AnnualLeaveRepository(IMapper mapper,
            PurpuraDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<bool> BookTimeOff(AnnualLeaveViewModel bookedTimePeriod)
        {
            try
            {
                var amountOfDays = (bookedTimePeriod.EndDate - bookedTimePeriod.StartDate).Days;
                var newEntities = new List<AnnualLeave>();

                for(var i = amountOfDays; i != amountOfDays + 1; i++)
                {
                    var bookedTimeOffEntity = _mapper.Map<AnnualLeave>(bookedTimePeriod);
                    bookedTimeOffEntity.DateCreated = DateTime.Now;
                    newEntities.Add(bookedTimeOffEntity);
                }

                _dbContext.AddRange(newEntities);
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
