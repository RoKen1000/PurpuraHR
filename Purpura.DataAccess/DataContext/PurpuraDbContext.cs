using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Purpura.Models.Entities;
using PurpuraWeb.Models.Entities;

namespace Purpura.DataAccess.DataContext
{
    public class PurpuraDbContext : IdentityDbContext<IdentityUser>
    {
        public PurpuraDbContext(DbContextOptions<PurpuraDbContext> options) : base(options)
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<AnnualLeave> AnnualLeave { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyEmployee> CompanyEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>().HasData(
                new Company { Name = "JLB Finance", Address = "123 Some Street, Some Business Estate, London, ABC 123", DateCreated = DateTime.Now, ExternalReference = Guid.NewGuid().ToString(), Id = 1, Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis." }
            );

            modelBuilder.Entity<CompanyEmployee>().HasData(
                new CompanyEmployee { Id = 1, FirstName = "Allan", LastName="Johnson", CompanyId = 1, JobTitle = "Chief Executive Officer", ExternalReference = Guid.NewGuid().ToString(), DateCreated = DateTime.Now },
                new CompanyEmployee { Id = 2, FirstName = "Sophie", MiddleName = "Hortensia", LastName = "Chapman", CompanyId = 1, JobTitle = "Customer Service Representative", ExternalReference = Guid.NewGuid().ToString(), DateCreated = DateTime.Now },
                new CompanyEmployee { Id = 3, FirstName = "Mark", LastName = "Corrigan", CompanyId = 1, JobTitle = "Account Manager", ExternalReference = Guid.NewGuid().ToString(), DateCreated = DateTime.Now },
                new CompanyEmployee { Id = 4, FirstName = "Gerrard", LastName = "Matthew", CompanyId = 1, JobTitle = "Finance Auditor", ExternalReference = Guid.NewGuid().ToString(), DateCreated = DateTime.Now }
            );
        }
    }
}
