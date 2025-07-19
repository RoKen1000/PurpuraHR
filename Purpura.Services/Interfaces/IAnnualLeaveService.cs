using Purpura.Common.Results;
using Purpura.Models.ViewModels;

namespace Purpura.Services.Interfaces
{
    public interface IAnnualLeaveService
    {
        Task<Result> CreateAsync(AnnualLeaveViewModel bookedTimePeriod);
        Task<int> GetUserAnnualLeaveCountAsync(string userId);
        Task<List<AnnualLeaveViewModel>> GetBookedLeaveByUserIdAsync(string userId);
        Task<AnnualLeaveViewModel?> GetByExternalReferenceAsync(string externalReference);
        Task<OverlapResult> CheckForLeaveOverlapsAsync(string userId, DateTime startDate, DateTime endDate, string? leaveExtRef);
        Task<Result> EditAsync(AnnualLeaveViewModel viewModel);
        Task<Result> DeleteAsync(AnnualLeaveViewModel viewModel);
    }
}
