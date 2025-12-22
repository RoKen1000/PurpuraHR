using PurpuraWeb.Models.Entities;

namespace Purpura.Models.Entities
{
    public class CompanyEmployee : BaseEntity
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public int? ApplicationUserId { get; set; }
        public string Email { get; set; }
    }
}
