using AutoMapper;
using Purpura.Common.Results;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
using Purpura.Services.Interfaces;
using Purpura.Utility.Resolvers;

namespace Purpura.Services
{
    public class AnnualLeaveService : BaseService<AnnualLeave>, IAnnualLeaveService
    {

        public AnnualLeaveService(
            IMapper mapper, 
            IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        public async Task<Result> CreateAsync(AnnualLeaveViewModel annualLeavePeriod)
        {
            try
            {
                var user = await _unitOfWork.UserManagementRepository.GetSingleAsync(u => u.Id == annualLeavePeriod.UserId);

                if (user == null)
                    return Result.Failure("User not found.");

                var daysUsed = (annualLeavePeriod.EndDate - annualLeavePeriod.StartDate).Days;
                var newAnnualLeaveTotal = user.AnnualLeaveDays - daysUsed;
                var validBookingErrors = AnnualLeaveResolver.IsValidBooking(user.AnnualLeaveDays, newAnnualLeaveTotal, annualLeavePeriod.StartDate, annualLeavePeriod.EndDate);

                if (!String.IsNullOrEmpty(validBookingErrors))
                    return Result.Failure(validBookingErrors);

                var annualLeaveEntity = _mapper.Map<AnnualLeave>(annualLeavePeriod);

                annualLeaveEntity.DateCreated = DateTime.Now;
                annualLeaveEntity.ExternalReference = Guid.NewGuid().ToString();
                annualLeaveEntity.User = user;
                _unitOfWork.AnnualLeaveRepository.Create(annualLeaveEntity);

                user.AnnualLeaveDays = newAnnualLeaveTotal;
                _unitOfWork.UserManagementRepository.Update(user);

                return await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<OverlapResult> CheckForLeaveOverlapsAsync(string userId, DateTime startDate, DateTime endDate, string? leaveExtRef)
        {
            var result = new OverlapResult();

            if (endDate.Date <= startDate.Date)
            {
                result.HasOverlap = true;
                result.Error = "End date can not be before or the same day as the start date.";
                return result;
            }

            IEnumerable<AnnualLeave> userCurrentLeave;

            if (leaveExtRef == null) //check is being done from the create modal therefore all other exisiting leave needs to be checked
            {
                userCurrentLeave = await _unitOfWork.AnnualLeaveRepository.GetAllAsync(al => al.UserId == userId);
            }
            else //if being called from edit then need to not include the entity being edited to avoid it comparing it against itself
            {
                userCurrentLeave = await _unitOfWork.AnnualLeaveRepository.GetAllAsync(al => al.ExternalReference != leaveExtRef && al.UserId == userId);
            }

            if (userCurrentLeave == null || !userCurrentLeave.Any())
            {
                return result;
            }

            foreach (var leave in userCurrentLeave)
            {
                var hasOverlap = startDate.Date <= leave.EndDate.Date && leave.StartDate.Date <= endDate.Date;

                if (hasOverlap)
                {
                    result.HasOverlap = true;
                    result.Error = "Current selection would cause an overlap in already-booked annual leave!";
                    return result;
                }
            }

            return result;
        }

        public async Task<Result> DeleteAsync(AnnualLeaveViewModel viewModel)
        {
            var leaveEntity = await _unitOfWork.AnnualLeaveRepository.GetSingleAsync(al => al.ExternalReference == viewModel.ExternalReference);

            if(leaveEntity != null)
            {
                _unitOfWork.AnnualLeaveRepository.Delete(leaveEntity);
                return await _unitOfWork.SaveChangesAsync();
            }

            return Result.Failure("Entity not found.");
        }

        public async Task<Result> EditAsync(AnnualLeaveViewModel viewModel)
        {
            var annualLeaveEntity = await _unitOfWork.AnnualLeaveRepository.GetSingleAsync(e => e.ExternalReference == viewModel.ExternalReference);

            if (annualLeaveEntity == null)
                return Result.Failure("Annual Leave not found.");

            var user = await _unitOfWork.UserManagementRepository.GetSingleAsync(u => u.Id == viewModel.UserId);

            if (user == null)
                return Result.Failure("User not found.");

            var daysUsed = (viewModel.EndDate - viewModel.StartDate).Days;
            var newAnnualLeaveTotal = user.AnnualLeaveDays - daysUsed;
            var validBookingErrors = AnnualLeaveResolver.IsValidBooking(user.AnnualLeaveDays, newAnnualLeaveTotal, viewModel.StartDate, viewModel.EndDate);

            if (!String.IsNullOrEmpty(validBookingErrors))
                return Result.Failure(validBookingErrors);

            var updatedEntity = _mapper.Map<AnnualLeaveViewModel, AnnualLeave>(viewModel, annualLeaveEntity);
            updatedEntity.DateEdited = DateTime.Now;

            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<AnnualLeaveViewModel>> GetBookedLeaveByUserIdAsync(string userId)
        {
            var bookedLeaveList = new List<AnnualLeaveViewModel>();
            var bookedLeave = await _unitOfWork.AnnualLeaveRepository.GetAllAsync(al => al.UserId == userId);

            if (bookedLeave.Any())
            {
                foreach (var leave in bookedLeave)
                {
                    bookedLeaveList.Add(_mapper.Map<AnnualLeaveViewModel>(leave));
                }
            }

            return bookedLeaveList;
        }

        public async Task<AnnualLeaveViewModel?> GetByExternalReferenceAsync(string externalReference)
        {
            var annualLeaveEntity = await _unitOfWork.AnnualLeaveRepository.GetSingleAsync(al => al.ExternalReference == externalReference);

            if(annualLeaveEntity != null)
                return _mapper.Map<AnnualLeaveViewModel>(annualLeaveEntity);

            return null;
        }

        public async Task<int> GetUserAnnualLeaveCountAsync(string userId)
        {
            var user = await _unitOfWork.UserManagementRepository.GetSingleAsync(u => u.Id == userId);

            if (user == null || user.AnnualLeaveDays < 0)
            {
                return 0;
            }

            return user.AnnualLeaveDays;
        }
    }
}
