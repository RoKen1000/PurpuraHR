using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Utility.Helpers
{
    public static class EnumHelpers
    {
        public static string GetEnumDescription(Enum value)
        {
            var description = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])description.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if(attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }
    }
}
