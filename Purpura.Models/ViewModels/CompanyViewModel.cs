using System.ComponentModel.DataAnnotations;

namespace Purpura.Models.ViewModels
{
    public class CompanyViewModel : BaseViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set; }
        [Required]
        public string AddressLine3 { get; set; }
        [Required]
        public string Postcode { get; set; }
        public string? FullAddress { get; set; }
        [Required]
        [StringLength(350)]
        public string Details { get; set; }
        public List<CompanyEmployeeViewModel> Employees { get; set; } = new();
    }
}
