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

            var predefinedDate = new DateTime(2025, 10, 27);
            var hasher = new PasswordHasher<ApplicationUser>();
            var userId = Guid.NewGuid().ToString();
            var companyExtRef = Guid.NewGuid().ToString();

            modelBuilder.Entity<Company>().HasData(
                new Company { Name = "JLB Finance", Address = "123 Some Street, Some Business Estate, London, ABC 123", DateCreated = predefinedDate, ExternalReference = companyExtRef, Id = 1, Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis." }
            );

            var preseededUser = new ApplicationUser
            {
                Id = userId,
                FirstName = "Joe",
                LastName = "Bloggs",
                DateOfBirth = predefinedDate,
                Address = "456 Some Flat, Some Building, London, EFG 456",
                Gender = Common.Enums.Genders.Unknown,
                Title = Common.Enums.Titles.Unknown,
                AnnualLeaveDays = 22,
                CompanyId = 1,
                DateCreated = predefinedDate,
                Email = "joe@testuser.com",
                UserName = "joe@testuser.com",
                NormalizedEmail = "JOE@TESTUSER.COM",
                PhoneNumber = "123456789",
                ConcurrencyStamp = Guid.NewGuid().ToString("N"),
                LockoutEnabled = true,
                NormalizedUserName = "JOE@TESTUSER.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            preseededUser.PasswordHash = hasher.HashPassword(preseededUser, "Test123!");

            modelBuilder.Entity<ApplicationUser>().HasData(preseededUser);

            modelBuilder.Entity<CompanyEmployee>().HasData(
                new CompanyEmployee { Id = 1, FirstName = "Allan", LastName="Johnson", CompanyId = 1, JobTitle = "Chief Executive Officer", ExternalReference = Guid.NewGuid().ToString(), DateCreated = predefinedDate },
                new CompanyEmployee { Id = 2, FirstName = "Sophie", MiddleName = "Hortensia", LastName = "Chapman", CompanyId = 1, JobTitle = "Customer Service Representative", ExternalReference = Guid.NewGuid().ToString(), DateCreated = predefinedDate },
                new CompanyEmployee { Id = 3, FirstName = "Mark", LastName = "Corrigan", CompanyId = 1, JobTitle = "Account Manager", ExternalReference = Guid.NewGuid().ToString(), DateCreated = predefinedDate },
                new CompanyEmployee { Id = 4, FirstName = "Gerrard", LastName = "Matthew", CompanyId = 1, JobTitle = "Finance Auditor", ExternalReference = Guid.NewGuid().ToString(), DateCreated = predefinedDate }
            );

            modelBuilder.Entity<Goal>().HasData(
                new Goal { Id = 1, DueDate = predefinedDate.AddDays(10), Name = "Goal 1", PercentageComplete = 75, UserId = userId, DateCreated = predefinedDate, ExternalReference = Guid.NewGuid().ToString(), Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis." },
                new Goal { Id = 2, DueDate = predefinedDate.AddDays(15), Name = "Goal 2", PercentageComplete = 30, UserId = userId, DateCreated = predefinedDate, ExternalReference = Guid.NewGuid().ToString(), Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis." },
                new Goal { Id = 3, DueDate = predefinedDate.AddDays(20), Name = "Goal 3", PercentageComplete = 0, UserId = userId, DateCreated = predefinedDate, ExternalReference = Guid.NewGuid().ToString(), Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis." }
            );

            modelBuilder.Entity<AnnualLeave>().HasData(
                new AnnualLeave { Id = 1, StartDate = predefinedDate.AddMonths(1), EndDate = predefinedDate.AddMonths(1).AddDays(5), Type = Common.Enums.LeaveTypes.Holiday, UserId = userId, ExternalReference = Guid.NewGuid().ToString(), DateCreated = predefinedDate },
                new AnnualLeave { Id = 2, StartDate = predefinedDate.AddMonths(2), EndDate = predefinedDate.AddMonths(1).AddDays(1), Type = Common.Enums.LeaveTypes.Training, UserId = userId, ExternalReference = Guid.NewGuid().ToString(), DateCreated = predefinedDate, Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis." }
            );

            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string>
                {
                    UserId = userId,
                    ClaimType = "CompanyReference",
                    ClaimValue = companyExtRef,
                    Id = 1
                }    
            );
        }
    }
}
