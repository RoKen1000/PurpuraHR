using Purpura.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void DeconstructAddressString(ApplicationUserViewModel viewModel)
        {
            var splitAdress = viewModel.Address.Split(',');

            for (int i = 0; i < splitAdress.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        viewModel.AddressLine1 = splitAdress[i].Trim();
                        break;
                    case 1:
                        viewModel.AddressLine2 = splitAdress[i].Trim();
                        break;
                    case 2:
                        viewModel.AddressLine3 = splitAdress[i].Trim();
                        break;
                    case 3:
                        viewModel.Postcode = splitAdress[i].Trim();
                        break;
                }
            }
        }
    }
}
