using Purpura.Common.Enums;
using PurpuraWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Models.Entities
{
    public class BookedTimeOff : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeOffTypes Type { get; set; }
        public string Details { get; set; }
        public ApplicationUser User { get; set; }
    }
}
