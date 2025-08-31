using Purpura.Common.Enums;
using Purpura.Utility.Helpers;

namespace Purpura.Tests.HelperTests
{
    public class EnumHelperTests
    {
        [Fact]
        public void GetEnumDescription_NoDescriptionFound_ReturnsCorrectString()
        {
            //arrane & act 
            var result = EnumHelpers.GetEnumDescription(LeaveTypes.Training);

            //assert
            Assert.NotNull(result);
            Assert.Equal(LeaveTypes.Training.ToString(), result);
        }

        [Fact]
        public void GetEnumDescription_WithDescriptionFound_ReturnsDescriptionAttributeValue()
        {
            //arrane & act 
            var result = EnumHelpers.GetEnumDescription(LeaveTypes.CompassionateLeave);

            //assert
            Assert.NotNull(result);
            Assert.NotEqual(LeaveTypes.CompassionateLeave.ToString(), result);
            Assert.Equal("Compassionate Leave", result);
        }
    }
}
