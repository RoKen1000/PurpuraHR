using Microsoft.EntityFrameworkCore;
using Purpura.Common;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Services.Interfaces;
using Purpura.Utility.Resolvers;

namespace Purpura.Services
{
    public class AnnualLeaveService : IAnnualLeaveService
    {
        public async Task<Result> BookTimeOff(AnnualLeaveViewModel bookedTimePeriod)
        {
            try
            {
                var user = await _userManagementRepository.GetUserEntity(u => u.Id == annualLeavePeriod.UserId);

                if (user == null)
                    return Result.Failure("User not found.");

                var daysUsed = (annualLeavePeriod.EndDate - annualLeavePeriod.StartDate).Days;
                var newAnnualLeaveTotal = AnnualLeaveResolver.WorkOutNumberOfDaysLeft(user.AnnualLeaveDays, daysUsed);
                var validBookingErrors = AnnualLeaveResolver.IsValidBooking(user.AnnualLeaveDays, newAnnualLeaveTotal, annualLeavePeriod.StartDate, annualLeavePeriod.EndDate);

                if (!String.IsNullOrEmpty(validBookingErrors))
                    return Result.Failure(validBookingErrors);

                var annualLeaveEntity = _mapper.Map<AnnualLeave>(annualLeavePeriod);

                annualLeaveEntity.DateCreated = DateTime.Now;
                annualLeaveEntity.ExternalReference = Guid.NewGuid().ToString();
                annualLeaveEntity.User = user;
                _dbContext.Add(annualLeaveEntity);

                user.AnnualLeaveDays = newAnnualLeaveTotal;
                _dbContext.Update(user);

                await _dbContext.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public Task<Result> CheckForLeaveOverlaps(string userId, DateTime startDate, DateTime endDate, string? leaveExtRef)
        {
            if (endDate < startDate)
            {
                return Result.Failure("End date can not be before the start date.");
            }

            IEnumerable<AnnualLeave> userCurrentLeave;

            if (leaveExtRef == null) //check is being done from the create modal therefore all other exisiting leave needs to be checked
            {
                userCurrentLeave = await base.GetAll(al => al.UserId == userId);
            }
            else //if being called from edit then need to not include the entity being edited to avoid it comparing it against itself
            {
                userCurrentLeave = await base.GetAll(al => al.ExternalReference != leaveExtRef && al.UserId == userId);
            }

            if (userCurrentLeave == null || !userCurrentLeave.Any())
            {
                return Result.Success();
            }

            foreach (var leave in userCurrentLeave)
            {
                var hasOverlap = startDate < leave.EndDate && leave.StartDate < endDate;

                if (hasOverlap)
                    return Result.Failure("Current selection would cause an overlap in already-booked annual leave!");
            }

            return Result.Success();
        }

        public Task<Result> Delete(AnnualLeaveViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Edit(AnnualLeaveViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<AnnualLeaveViewModel>> GetBookedLeave(string userId)
        {
            var bookedLeaveList = new List<AnnualLeaveViewModel>();
            var bookedLeave = await base.GetAll(al => al.UserId == userId);

            if (bookedLeave.Any())
            {
                foreach (var leave in bookedLeave)
                {
                    bookedLeaveList.Add(_mapper.Map<AnnualLeaveViewModel>(leave));
                }
            }

            return bookedLeaveList;
        }

        public Task<AnnualLeaveViewModel> GetByExternalReference(string externalReference)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUserAnnualLeaveCount(string userId)
        {
            var user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException("User not found.");
            }

            if (user.AnnualLeaveDays == null || user.AnnualLeaveDays == 0)
                return 0;

            return user.AnnualLeaveDays;
        }
    }
}
