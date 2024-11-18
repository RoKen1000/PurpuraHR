using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Common.Enums
{
    public enum Genders
    {
        Unknown,
        Male,
        Female,
        Transgender,
        [Description("Non-Binary")]
        NonBinary,
        Other
    }
}
