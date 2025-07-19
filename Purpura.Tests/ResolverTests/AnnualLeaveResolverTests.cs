using Purpura.Common.Results;
using Purpura.Utility.Resolvers;

namespace Purpura.Tests.ResolverTests
{
    public class AnnualLeaveResolverTests
    {
        #region IsValidBooking
        [Fact]
        public void IsValidBooking_WithNegativeOrZeroCurrentDays_ReturnsErrorString()
        {
            //arrange & act
            var zeroResult = AnnualLeaveResolver.IsValidBooking(0, 1, DateTime.Now, DateTime.Now.AddDays(1));
            var negativeResult = AnnualLeaveResolver.IsValidBooking(-1, 1, DateTime.Now, DateTime.Now.AddDays(1));

            //assert
            Assert.Equal("Booking is invalid and would either exceed remaining leave or there is no more leave to take.", zeroResult);
            Assert.Equal("Booking is invalid and would either exceed remaining leave or there is no more leave to take.", negativeResult);
        }

        [Fact]
        public void IsValidBooking_NegativeNewTotal_ReturnsErrorString()
        {
            //arrange & act
            var negativeResult = AnnualLeaveResolver.IsValidBooking(2, -1, DateTime.Now, DateTime.Now.AddDays(1));

            //assert
            Assert.Equal("Booking is invalid and would either exceed remaining leave or there is no more leave to take.", negativeResult);
        }

        [Fact]
        public void IsValidBooking_EndDateBeforeStartDate_ReturnsErrorString()
        {
            //arrange & act
            var endBeforeStartResult = AnnualLeaveResolver.IsValidBooking(2, 1, DateTime.Now, DateTime.Now.AddDays(-1));

            //assert
            Assert.Equal("End date can not be before the start date.", endBeforeStartResult);
        }

        [Fact]
        public void IsValidBooking_InvalidTotalsAndEndBeforeStart_ReturnsErrorString()
        {
            //arrange & act
            var invalidTotalAndDateResult = AnnualLeaveResolver.IsValidBooking(-1, 1, DateTime.Now, DateTime.Now.AddDays(-1));

            //assert
            Assert.Equal("Booking is invalid and would either exceed remaining leave or there is no more leave to take. End date can not be before the start date.", invalidTotalAndDateResult);
        }

        [Fact]
        public void IsValidBooking_ValidDatesAndLeaveTotals_ReturnsEmptyErrorString()
        {
            //arrange & act
            var emptyResult = AnnualLeaveResolver.IsValidBooking(2, 1, DateTime.Now, DateTime.Now.AddDays(1));

            //assert
            Assert.True(String.IsNullOrEmpty(emptyResult));
        }

        #endregion

    }
}
