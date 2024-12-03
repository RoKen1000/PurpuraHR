using Purpura.Common.Enums;
using PurpuraWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Models.ViewModels
{
    public class AnnualLeaveViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeOffTypes Type { get; set; }
        public string Details { get; set; }
        public string UserId { get; set; }
    }
}
