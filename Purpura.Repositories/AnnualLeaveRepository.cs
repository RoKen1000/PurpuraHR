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
                var isValidBooking = AnnualLeaveResolver.IsValidBooking(user.AnnualLeaveDays, newAnnualLeaveTotal);

                if (!isValidBooking)
                    return Result.Failure("Booking is invalid and would either exceed remaining leave or there is no more leave to take.");

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
            var entity = await base.GetByExternalReference(e => e.ExternalReference == externalReference);

            if(entity == null)
                throw new NullReferenceException("Annual Leave not found.");

            return _mapper.Map<AnnualLeave, AnnualLeaveViewModel>(entity);
        }

        public async Task<List<AnnualLeaveViewModel>> GetBookedLeave(string userId)
        {
            var bookedLeaveList = new List<AnnualLeaveViewModel>();
            var bookedLeave = _dbContext.AnnualLeave.Where(al => al.UserId == userId);

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

        public async Task<Result> Edit(AnnualLeaveViewModel viewModel)
        {
            var annualLeaveEntity = await base.GetByExternalReference(e => e.ExternalReference == viewModel.ExternalReference);

            if (annualLeaveEntity == null)
                return Result.Failure("Annual Leave not found.");

            var updatedEntity = _mapper.Map<AnnualLeaveViewModel, AnnualLeave>(viewModel, annualLeaveEntity);
            updatedEntity.DateEdited = DateTime.Now;

            return await base.Edit(updatedEntity);
        }
    }
}
