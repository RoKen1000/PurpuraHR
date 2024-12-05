using Purpura.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Repositories.Interfaces
{
    public interface IAnnualLeaveRepository
    {
        Task<bool> BookTimeOff(AnnualLeaveViewModel bookedTimePeriod);
        Task<int> GetUserAnnualLeaveCount(string userId);
        Task<List<AnnualLeaveViewModel>> GetBookedLeave(string userId);
    }
}
