using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Purpura.Models.ViewModels
{
    public class CompanyEmployeeViewModel : BaseViewModel
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string? MiddleName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string JobTitle { get; set; }
        public string CompanyExternalReference { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public bool EmailExists { get; set; }
    }
}
