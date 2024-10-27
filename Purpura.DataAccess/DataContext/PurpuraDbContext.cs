using Microsoft.EntityFrameworkCore;
using PurpuraWeb.Models.Entities;

namespace Purpura.DataAccess.DataContext
{
    public class PurpuraDbContext : DbContext
    {
        public PurpuraDbContext(DbContextOptions<PurpuraDbContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee { Id = 1, FirstName = "Joe", LastName = "Bloggs", DateOfBirth = DateTime.Now, Email = "something@random.com", Address = "742 Evergreen Terrace", PhoneNumber = "123456789", ExternalReference = new Guid().ToString()  }
                );
        }
    }
}
