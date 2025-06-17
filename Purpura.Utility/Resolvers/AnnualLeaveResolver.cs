using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Utility.Resolvers
{
    public static class AnnualLeaveResolver
    {
        public static int WorkOutNumberOfDaysLeft(int currentCount, int daysBeingTaken)
        {
            return currentCount - daysBeingTaken;
        }

        public static string IsValidBooking(int currentDays, int newTotal, DateTime startDate, DateTime endDate)
        {
            var errorString = "";

            if (currentDays <= 0 || newTotal < 0)
                errorString += "Booking is invalid and would either exceed remaining leave or there is no more leave to take.";

            var endBeforeStartOrSameDayError = "";

            if (endDate < startDate)
            {
                endBeforeStartOrSameDayError = "End date can not be before the start date.";
            }
            else if(endDate == startDate)
            {
                endBeforeStartOrSameDayError = "Start and end date can not be on the same day.";
            }

            if(!String.IsNullOrEmpty(endBeforeStartOrSameDayError))
                errorString += !String.IsNullOrEmpty(errorString) ? " " + endBeforeStartOrSameDayError : endBeforeStartOrSameDayError;

                return errorString;
        }

        public static int WorkOutNumberOfDaysUsed(int userCurrentTotal)
        {
            var daysUsed = 28 - userCurrentTotal;

            return daysUsed <= 28 ? daysUsed : 28;
        }
    }
}
