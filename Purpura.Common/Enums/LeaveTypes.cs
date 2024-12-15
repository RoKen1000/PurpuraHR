using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Common.Enums
{
    public enum LeaveTypes
    {
        Unknown,
        Holiday,
        [Description("Compassionate Leave")]
        CompassionateLeave,
        [Description("Day Off In Lieu")]
        DayOffInLieu,
        Training,
    }
}
