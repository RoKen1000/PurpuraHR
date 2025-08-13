using Purpura.Common.Results;

namespace Purpura.Models.ViewModels
{
    public class DashboardViewModel
    {
        public Result? Result { get; set; }
        public ApplicationUserViewModel User { get; set; }
        public CompanyViewModel Company { get; set; }
        public int AnnualLeaveRemaining { get; set; }
        public List<AnnualLeaveViewModel> AnnualLeave { get; set; } = new();
        public List<GoalViewModel> Goals { get; set; } = new();
    }
}
