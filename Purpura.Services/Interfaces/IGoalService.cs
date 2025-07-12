using Purpura.Common.Results;
using Purpura.Models.ViewModels;

namespace Purpura.Services.Interfaces
{
    public interface IGoalService
    {
        Task<Result> CreateAsync(GoalViewModel viewModel);
        Task<List<GoalViewModel>> GetAllGoalsByUserIdAsync(string userId);
        Task<GoalViewModel?> GetByExternalReferenceAsync(string goalReference);
        Task<Result> EditAsync(GoalViewModel viewModel);
    }
}
