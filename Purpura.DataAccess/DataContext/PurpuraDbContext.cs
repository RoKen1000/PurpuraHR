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
            var userId = "18267BE3-DD54-45C4-8842-EEE2BAC13B3F";
            var companyExtRef = "0AFA8D32-5A1F-4B32-9429-452A59523B27";

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
                ConcurrencyStamp = "27B9DBA7-2167-4E33-8699-8CF850F7788F",
                LockoutEnabled = true,
                NormalizedUserName = "JOE@TESTUSER.COM",
                SecurityStamp = "9F317C5D-F614-4E5F-A30C-1DD85A685D6E",
                PasswordHash = "AQAAAAIAAYagAAAAEPlmXdK35aJhUN8CUUlPCQE0kZRpiBqCQI1cGUCvonoV0jC0MTUgLdiP3sVl3nug/Q=="
            };

            modelBuilder.Entity<ApplicationUser>().HasData(preseededUser);

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "F9B314A8-3844-47B4-B9A5-CC3DB2DE35AE", Name = "Manager", NormalizedName = "MANAGER", ConcurrencyStamp = null }    
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "F9B314A8-3844-47B4-B9A5-CC3DB2DE35AE", UserId = userId }
            );

            modelBuilder.Entity<CompanyEmployee>().HasData(
                new CompanyEmployee { Id = 1, FirstName = "Allan", LastName="Johnson", CompanyId = 1, JobTitle = "Chief Executive Officer", ExternalReference = "64B9D90F-FADC-461D-96B8-C9467AF44894", DateCreated = predefinedDate, Email = "allan@jlb.com" },
                new CompanyEmployee { Id = 2, FirstName = "Sophie", MiddleName = "Hortensia", LastName = "Chapman", CompanyId = 1, JobTitle = "Customer Service Representative", ExternalReference = "7CCEFDF3-89CF-4CFA-B641-1394EECB39D9", DateCreated = predefinedDate, Email = "sophie@jlb.com" },
                new CompanyEmployee { Id = 3, FirstName = "Mark", LastName = "Corrigan", CompanyId = 1, JobTitle = "Account Manager", ExternalReference = "63454D95-3087-4681-A93A-6E2C8ED761F0", DateCreated = predefinedDate, Email = "mark@jlb.com" },
                new CompanyEmployee { Id = 4, FirstName = "Gerrard", LastName = "Matthew", CompanyId = 1, JobTitle = "Finance Auditor", ExternalReference = "57DA2F16-7990-4E6E-A47B-EC7949FB8B39", DateCreated = predefinedDate, Email = "gerrard@jlb.com" }
            );

            modelBuilder.Entity<Goal>().HasData(
                new Goal { Id = 1, DueDate = predefinedDate.AddDays(10), Name = "Goal 1", PercentageComplete = 75, UserId = userId, DateCreated = predefinedDate, ExternalReference = "4BF723F7-1BB7-4595-87A2-84F220882927", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis." },
                new Goal { Id = 2, DueDate = predefinedDate.AddDays(15), Name = "Goal 2", PercentageComplete = 30, UserId = userId, DateCreated = predefinedDate, ExternalReference = "6BC4C3EE-5A44-4AA9-B9D7-DBE55597CB6B", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis." },
                new Goal { Id = 3, DueDate = predefinedDate.AddDays(20), Name = "Goal 3", PercentageComplete = 0, UserId = userId, DateCreated = predefinedDate, ExternalReference = "5441F99B-A8D7-4F73-A065-85935986749E", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis." }
            );

            modelBuilder.Entity<AnnualLeave>().HasData(
                new AnnualLeave { Id = 1, StartDate = predefinedDate.AddMonths(1), EndDate = predefinedDate.AddMonths(1).AddDays(5), Type = Common.Enums.LeaveTypes.Holiday, UserId = userId, ExternalReference = "C06B1BA6-AB02-4B1D-A7D9-08F9A430844E", DateCreated = predefinedDate },
                new AnnualLeave { Id = 2, StartDate = predefinedDate.AddMonths(2), EndDate = predefinedDate.AddMonths(1).AddDays(1), Type = Common.Enums.LeaveTypes.Training, UserId = userId, ExternalReference = "042005B1-AA6B-49CA-8AB5-32A291C3D2C4", DateCreated = predefinedDate, Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis nec augue a arcu aliquam consequat a sit amet ante. Nullam eget tincidunt ante. Donec sed malesuada nibh. Cras rhoncus auctor lorem, vel ullamcorper ipsum egestas in. Cras lobortis justo enim, sed vulputate magna sagittis ac. Ut imperdiet sapien sed ante posuere porta. Praesent ultricies sagittis venenatis. Suspendisse potenti. Nulla viverra, mi ac pellentesque fringilla, purus tortor blandit enim, non lacinia augue lacus et felis." }
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
