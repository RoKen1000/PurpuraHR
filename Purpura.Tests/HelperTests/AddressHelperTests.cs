using Purpura.Utility.Helpers;

namespace Purpura.Tests.HelperTests
{
    public class AddressHelperTests
    {
        [Fact]
        public void ConstructAddressString_WithFullAddress_ReturnsSingleJoinedString()
        {
            //arrange
            var addressArray = new string[] { "123 Some Street", "Some Place", "Some Country", "ABC 123" };
            var expectedAddress = "123 Some Street, Some Place, Some Country, ABC 123";

            //act
            var result = AddressHelpers.ConstructAddressString(addressArray);

            //assert
            Assert.Equal(expectedAddress, result);
        }

        [Fact]
        public void DeconstructAddressString_WithFullAddress_ReturnsStringTupple() 
        {
            //arrange
            var fullAddress = "123 Some Street, Some Place, Some Country, ABC 123";
            var expectedTupple = ("123 Some Street", "Some Place", "Some Country", "ABC 123");

            //act
            var result = AddressHelpers.DeconstructAddressString(fullAddress);

            //assert
            Assert.Equal(result.Item1, expectedTupple.Item1);
            Assert.Equal(result.Item2, expectedTupple.Item2);
            Assert.Equal(result.Item3, expectedTupple.Item3);
            Assert.Equal(result.Item4, expectedTupple.Item4);
        }
    }
}
