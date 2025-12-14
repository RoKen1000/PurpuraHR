using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Purpura.Abstractions.ServiceInterfaces;
using System.Security.Claims;

namespace Purpura.Utility.Factories
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser, IdentityRole>
    {
        private readonly IUserManagementService _userManagementService;
        private readonly ICompanyService _companyService;

        public CustomUserClaimsPrincipalFactory(UserManager<IdentityUser> userManager, 
            IOptions<IdentityOptions> options,
            IUserManagementService userManagementService,
            RoleManager<IdentityRole> roleManager,
            ICompanyService companyService) : base(userManager, roleManager, options)
        {
            _userManagementService = userManagementService;
            _companyService = companyService;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var identityClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var applicationUserEntity = await _userManagementService.GetUserEntityByIdAsync(identityClaim.Value);
            var nameClaim = identity.FindFirst("Name");

            if (applicationUserEntity != null && nameClaim == null)
            {
                var nameString = applicationUserEntity.MiddleName != null ? $"{applicationUserEntity.FirstName} {applicationUserEntity.MiddleName} {applicationUserEntity.LastName}" : $"{applicationUserEntity.FirstName} {applicationUserEntity.LastName}";
                identity.AddClaim(new Claim("Name", nameString));
            }

            var companyRefClaim = identity.FindFirst("CompanyReference");

            if(companyRefClaim == null && (applicationUserEntity != null && applicationUserEntity.CompanyId != null))
            {
                var companyRef = await _companyService.GetExternalReferenceByIdAsync((int)applicationUserEntity.CompanyId);

                if (!string.IsNullOrEmpty(companyRef))
                {
                    identity.AddClaim(new Claim("CompanyReference", companyRef));
                }
            }

            return identity;
        }
    }
}
