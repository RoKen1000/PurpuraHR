using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Purpura.Common;
using Purpura.DataAccess.DataContext;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
using Purpura.Utility.Resolvers;

namespace Purpura.Repositories
{
    public class AnnualLeaveRepository : BaseRepository<AnnualLeave>, IAnnualLeaveRepository
    {
        private readonly IMapper _mapper;
        private readonly PurpuraDbContext _dbContext;
        private readonly IUserManagementRepository _userManagementRepository;

        public AnnualLeaveRepository(IMapper mapper,
            PurpuraDbContext dbContext,
            IUserManagementRepository userManagementRepository) : base(dbContext, mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userManagementRepository = userManagementRepository;
        }

        public async Task<Result> BookTimeOff(AnnualLeaveViewModel annualLeavePeriod)
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

        public async Task<AnnualLeaveViewModel> GetByExternalReference(string externalReference)
        {
            var entity = await base.GetSingle(e => e.ExternalReference == externalReference);

            if(entity == null)
                throw new NullReferenceException("Annual Leave not found.");

            return _mapper.Map<AnnualLeave, AnnualLeaveViewModel>(entity);
        }

        public async Task<List<AnnualLeaveViewModel>> GetBookedLeave(string userId)
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

        public async Task<Result> Edit(AnnualLeaveViewModel annualLeavePeriod)
        {
            var annualLeaveEntity = await base.GetSingle(e => e.ExternalReference == annualLeavePeriod.ExternalReference);

            if (annualLeaveEntity == null)
                return Result.Failure("Annual Leave not found.");

            var user = await _userManagementRepository.GetUserEntity(u => u.Id == annualLeavePeriod.UserId);

            if (user == null)
                return Result.Failure("User not found.");

            var daysUsed = (annualLeavePeriod.EndDate - annualLeavePeriod.StartDate).Days;
            var newAnnualLeaveTotal = AnnualLeaveResolver.WorkOutNumberOfDaysLeft(user.AnnualLeaveDays, daysUsed);
            var validBookingErrors = AnnualLeaveResolver.IsValidBooking(user.AnnualLeaveDays, newAnnualLeaveTotal, annualLeavePeriod.StartDate, annualLeavePeriod.EndDate);

            if (!String.IsNullOrEmpty(validBookingErrors))
                return Result.Failure(validBookingErrors);

            var updatedEntity = _mapper.Map<AnnualLeaveViewModel, AnnualLeave>(annualLeavePeriod, annualLeaveEntity);
            updatedEntity.DateEdited = DateTime.Now;

            return await base.Edit(updatedEntity);
        }

        public async Task<Result> Delete(AnnualLeaveViewModel viewModel)
        {
            var annualLeaveEntity = await base.GetSingle(e => e.ExternalReference == viewModel.ExternalReference);

            if (annualLeaveEntity == null)
                return Result.Failure("Annual Leave not found.");

            return await base.Delete(annualLeaveEntity);
        }

        public async Task<Result> CheckForLeaveOverlaps(string userId, DateTime startDate, DateTime endDate, string? leaveExtRef)
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

            foreach(var leave in userCurrentLeave)
            {
                var hasOverlap = startDate < leave.EndDate && leave.StartDate < endDate;

                if (hasOverlap)
                    return Result.Failure("Current selection would cause an overlap in already-booked annual leave!");
            }

            return Result.Success();
        }
    }
}
