using Purpura.Common;
using Purpura.Models.ViewModels;

namespace Purpura.Services.Interfaces
{
    public interface IAnnualLeaveService
    {
        Task<Result> BookTimeOff(AnnualLeaveViewModel bookedTimePeriod);
        Task<int> GetUserAnnualLeaveCount(string userId);
        Task<List<AnnualLeaveViewModel>> GetBookedLeave(string userId);
        Task<AnnualLeaveViewModel> GetByExternalReference(string externalReference);
        Task<Result> CheckForLeaveOverlaps(string userId, DateTime startDate, DateTime endDate, string? leaveExtRef);
        Task<Result> Edit(AnnualLeaveViewModel viewModel);
        Task<Result> Delete(AnnualLeaveViewModel viewModel);
    }
}
