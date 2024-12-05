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

        public static bool IsValidBooking(int currentDays, int newTotal)
        {
            if (currentDays <= 0 || newTotal <= 0)
            {
                return false;
            }

            return true;
        }

        public static int WorkOutNumberOfDaysUsed(int userCurrentTotal)
        {
            var daysUsed = 28 - userCurrentTotal;

            return daysUsed <= 28 ? daysUsed : 28;
        }
    }
}
