using Purpura.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purpura.Models.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public Genders Gender { get; set; }
        public Titles Title { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
