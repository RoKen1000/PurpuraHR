namespace Purpura.Models.ViewModels
{
    public class CompanyEmployeeViewModel : BaseViewModel
    {
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string CompanyExternalReference { get; set; }
    }
}
