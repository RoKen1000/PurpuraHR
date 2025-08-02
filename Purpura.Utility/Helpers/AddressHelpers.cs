using Purpura.Models.ViewModels;

namespace Purpura.Utility.Helpers
{
    public static class AddressHelpers
    {
        public static string ConstructAddressString(string[] addressStrings)
        {
            var completeAddressString = "";

            for (int i = 0; i < addressStrings.Length; i++)
            {
                completeAddressString += $"{addressStrings[i]}";

                if (i < addressStrings.Length - 1)
                    completeAddressString += ", ";
            }

            return completeAddressString;
        }

        public static (string, string, string, string) DeconstructAddressString(string fullAddress)
        {
            var splitAdress = fullAddress.Split(',');
            string addressLine1 = "", addressLine2 = "", addressLine3 = "", postcode = "";

            
            for (int i = 0; i < splitAdress.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        addressLine1 = splitAdress[i].Trim();
                        break;
                    case 1:
                        addressLine2 = splitAdress[i].Trim();
                        break;
                    case 2:
                        addressLine3 = splitAdress[i].Trim();
                        break;
                    case 3:
                        postcode = splitAdress[i].Trim();
                        break;
                }
            }

            return (addressLine1, addressLine2, addressLine3, postcode);
        }
    }
}
