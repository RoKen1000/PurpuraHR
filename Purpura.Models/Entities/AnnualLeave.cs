using Purpura.Common.Enums;
using PurpuraWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Models.Entities
{
    public class AnnualLeave : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The date specifying a certain day in a chain of dates for a period of time booked off.
        /// </summary>
        public DateTime CurrentDate { get; set; }

        public TimeOffTypes Type { get; set; }
        public string Details { get; set; }
        public ApplicationUser User { get; set; }
    }
}
