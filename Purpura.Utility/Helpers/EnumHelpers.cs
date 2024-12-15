using Microsoft.AspNetCore.Mvc.Rendering;
using Purpura.Common.Enums;
using System.ComponentModel;

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

        public static IEnumerable<SelectListItem> GenerateGenderSelectList()
        {
            return Enum.GetValues(typeof(Genders)).Cast<Genders>().Where(g => g != Genders.Unknown).Select(g => new SelectListItem
            {
                Text = EnumHelpers.GetEnumDescription(g),
                Value = Enum.Parse<Genders>(g.ToString()).ToString(),
            });
        }

        public static IEnumerable<SelectListItem> GenerateTitleSelectList()
        {
            return Enum.GetValues(typeof(Titles)).Cast<Titles>().Where(t => t != Titles.Unknown).Select(t => new SelectListItem
            {
                Text = Enum.GetName(t),
                Value = Enum.Parse<Titles>(t.ToString()).ToString()
            });
        }

        public static IEnumerable<SelectListItem> GenerateLeaveTypeSelectList()
        {
            return Enum.GetValues(typeof(LeaveTypes)).Cast<LeaveTypes>().Where(l => l != LeaveTypes.Unknown).Select(l => new SelectListItem
            {
                Text = EnumHelpers.GetEnumDescription(l),
                Value = Enum.Parse<LeaveTypes>(l.ToString()).ToString()
            });
        }

    }
}
