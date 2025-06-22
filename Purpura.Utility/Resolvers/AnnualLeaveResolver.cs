using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Utility.Resolvers
{
    public static class AnnualLeaveResolver
    {
        public static string IsValidBooking(int currentDays, int newTotal, DateTime startDate, DateTime endDate)
        {
            var errorString = "";

            if (currentDays <= 0 || newTotal < 0)
                errorString += "Booking is invalid and would either exceed remaining leave or there is no more leave to take.";

            if (endDate < startDate)
            {
                var endLessThanStartError = "End date can not be before the start date.";
                errorString += !String.IsNullOrEmpty(errorString) ? " " + endLessThanStartError : endLessThanStartError;
            }

            return errorString;
        }
    }
}
