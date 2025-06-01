using AutoMapper;
using Purpura.Common.Results;
using Purpura.Models.Entities;
using Purpura.Models.ViewModels;
using Purpura.Repositories;
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

        public async Task<Result> BookTimeOff(AnnualLeaveViewModel annualLeavePeriod)
        {
            try
            {
                var user = await _unitOfWork.UserManagementRepository.GetSingle(u => u.Id == annualLeavePeriod.UserId);

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
                _unitOfWork.AnnualLeaveRepository.Create(annualLeaveEntity);

                user.AnnualLeaveDays = newAnnualLeaveTotal;
                _unitOfWork.UserManagementRepository.Update(user);

                var result = await _unitOfWork.SaveChangesAsync();

                if (result > 0)
                    return Result.Success();
                else
                    return Result.Failure("Process failed.");
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        public async Task<OverlapResult> CheckForLeaveOverlaps(string userId, DateTime startDate, DateTime endDate, string? leaveExtRef)
        {
            var result = new OverlapResult();

            if (endDate < startDate)
            {
                result.Error = "End date can not be before the start date.";
                return result;
            }

            IEnumerable<AnnualLeave> userCurrentLeave;

            if (leaveExtRef == null) //check is being done from the create modal therefore all other exisiting leave needs to be checked
            {
                userCurrentLeave = await _unitOfWork.AnnualLeaveRepository.GetAll(al => al.UserId == userId);
            }
            else //if being called from edit then need to not include the entity being edited to avoid it comparing it against itself
            {
                userCurrentLeave = await _unitOfWork.AnnualLeaveRepository.GetAll(al => al.ExternalReference != leaveExtRef && al.UserId == userId);
            }

            if (userCurrentLeave == null || !userCurrentLeave.Any())
            {
                return result;
            }

            foreach (var leave in userCurrentLeave)
            {
                var hasOverlap = startDate < leave.EndDate && leave.StartDate < endDate;

                if (hasOverlap)
                {
                    result.Error = "Current selection would cause an overlap in already-booked annual leave!";
                    return result;
                }
            }

            return result;
        }

        public async Task<Result> Delete(AnnualLeaveViewModel viewModel)
        {
            var leaveEntity = await _unitOfWork.AnnualLeaveRepository.GetSingle(al => al.ExternalReference == viewModel.ExternalReference);

            if(leaveEntity != null)
            {
                _unitOfWork.AnnualLeaveRepository.Delete(leaveEntity);
                var result = await _unitOfWork.SaveChangesAsync();

                if (result > 0)
                    return Result.Success();
            }

            return Result.Failure("Entity not found.");
        }

        public async Task<Result> Edit(AnnualLeaveViewModel viewModel)
        {
            var annualLeaveEntity = await _unitOfWork.AnnualLeaveRepository.GetSingle(e => e.ExternalReference == viewModel.ExternalReference);

            if (annualLeaveEntity == null)
                return Result.Failure("Annual Leave not found.");

            var user = await _unitOfWork.UserManagementRepository.GetSingle(u => u.Id == viewModel.UserId);

            if (user == null)
                return Result.Failure("User not found.");

            var daysUsed = (viewModel.EndDate - viewModel.StartDate).Days;
            var newAnnualLeaveTotal = AnnualLeaveResolver.WorkOutNumberOfDaysLeft(user.AnnualLeaveDays, daysUsed);
            var validBookingErrors = AnnualLeaveResolver.IsValidBooking(user.AnnualLeaveDays, newAnnualLeaveTotal, viewModel.StartDate, viewModel.EndDate);

            if (!String.IsNullOrEmpty(validBookingErrors))
                return Result.Failure(validBookingErrors);

            var updatedEntity = _mapper.Map<AnnualLeaveViewModel, AnnualLeave>(viewModel, annualLeaveEntity);
            updatedEntity.DateEdited = DateTime.Now;

            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
                return Result.Success();
            else
                return Result.Failure("Update failed.");
        }

        public async Task<List<AnnualLeaveViewModel>> GetBookedLeave(string userId)
        {
            var bookedLeaveList = new List<AnnualLeaveViewModel>();
            var bookedLeave = await _unitOfWork.AnnualLeaveRepository.GetAll(al => al.UserId == userId);

            if (bookedLeave.Any())
            {
                foreach (var leave in bookedLeave)
                {
                    bookedLeaveList.Add(_mapper.Map<AnnualLeaveViewModel>(leave));
                }
            }

            return bookedLeaveList;
        }

        public async Task<AnnualLeaveViewModel> GetByExternalReference(string externalReference)
        {
            var annualLeaveEntity = await _unitOfWork.AnnualLeaveRepository.GetSingle(al => al.ExternalReference == externalReference);

            if(annualLeaveEntity != null)
                return _mapper.Map<AnnualLeaveViewModel>(annualLeaveEntity);

            throw new NullReferenceException("Leave not found.");
        }

        public async Task<int> GetUserAnnualLeaveCount(string userId)
        {
            var user = await _unitOfWork.UserManagementRepository.GetSingle(u => u.Id == userId);

            if (user == null)
            {
                throw new NullReferenceException("User not found.");
            }

            if (user.AnnualLeaveDays == 0)
                return 0;

            return user.AnnualLeaveDays;
        }
    }
}
