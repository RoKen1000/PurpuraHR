using Purpura.Common;
using Purpura.Models.ViewModels;

namespace Purpura.Repositories.Interfaces
{
    public interface IAnnualLeaveRepository
    {
        Task<Result> BookTimeOff(AnnualLeaveViewModel bookedTimePeriod);
        Task<int> GetUserAnnualLeaveCount(string userId);
        Task<List<AnnualLeaveViewModel>> GetBookedLeave(string userId);
        Task<AnnualLeaveViewModel> GetByExternalReference(string externalReference);
        Task<Result> Edit(AnnualLeaveViewModel viewModel);
        Task<Result> Delete(AnnualLeaveViewModel viewModel);
        Task<Result> CheckForLeaveOverlaps(string userId, DateTime startDate, DateTime endDate, string? leaveExtRef);
    }
}
