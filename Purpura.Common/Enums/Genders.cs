using System;
using System.Collections.Generic;
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
        [Display(Name = "Non-Binary")]
        NonBinary,
        Other
    }
}
