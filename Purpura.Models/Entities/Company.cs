namespace Purpura.Models.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Details { get; set; }
        public List<CompanyEmployee> Employees { get; set; }
    }
}
