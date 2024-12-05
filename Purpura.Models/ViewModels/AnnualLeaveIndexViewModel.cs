using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Models.ViewModels
{
    public class AnnualLeaveIndexViewModel : BaseViewModel
    {
        public int AnnualLeaveDaysRemaining { get; set; }
        public int AnnualLeaveDaysUsed { get; set; }
        public List<AnnualLeaveViewModel> BookedLeave { get; set; }
    }
}
