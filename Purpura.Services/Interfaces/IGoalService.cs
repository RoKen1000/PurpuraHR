using Purpura.Common.Results;
using Purpura.Models.ViewModels;

namespace Purpura.Services.Interfaces
{
    public interface IGoalService
    {
        Task<Result> Create(GoalViewModel viewModel);
    }
}
