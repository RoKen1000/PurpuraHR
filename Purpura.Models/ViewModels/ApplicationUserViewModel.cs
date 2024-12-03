using Microsoft.AspNetCore.Mvc.Rendering;
using Purpura.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Purpura.Models.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set; }
        [Required]
        public string AddressLine3 { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public Genders Gender { get; set; }
        [Required]
        public Titles Title { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public IEnumerable<SelectListItem>? TitleList { get; set; }
        public IEnumerable<SelectListItem>? GenderList { get; set; }
    }
}
