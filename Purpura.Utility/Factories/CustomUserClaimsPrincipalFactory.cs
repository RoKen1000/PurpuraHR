using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Purpura.Abstractions.ServiceInterfaces;
using System.Security.Claims;

namespace Purpura.Utility.Factories
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser>
    {
        private readonly IUserManagementService _userManagementService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICompanyService _companyService;

        public CustomUserClaimsPrincipalFactory(UserManager<IdentityUser> userManager, 
            IOptions<IdentityOptions> options,
            IUserManagementService userManagementService,
            IHttpContextAccessor httpContextAccessor,
            ICompanyService companyService) : base(userManager, options)
        {
            _userManager = userManager;
            _userManagementService = userManagementService;
            _httpContextAccessor = httpContextAccessor;
            _companyService = companyService;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            if(user.Email == "test@testuser.co.uk")
            {
                identity.AddClaim(new Claim("CompanyReference", "05595D26-131C-45EA-B78C-C4E912FC2438"));
            }
            else
            {
                var identityClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                var applicationUserEntity = await _userManagementService.GetUserEntityByIdAsync(identityClaim.Value);
                var nameClaim = identity.FindFirst("Name");
                var companyClaim = identity.FindFirst("CompanyReference");

                if(companyClaim == null)
                {
                    if (applicationUserEntity != null && (applicationUserEntity.CompanyId != null && applicationUserEntity.CompanyId > 0))
                    {
                        var companyExternalReference = await _companyService.GetExternalReferenceByIdAsync((int)applicationUserEntity.CompanyId);
                        identity.AddClaim(new Claim("CompanyReference", companyExternalReference));
                    }
                }

                if (applicationUserEntity != null && nameClaim == null)
                {
                    var nameString = applicationUserEntity.MiddleName != null ? $"{applicationUserEntity.FirstName} {applicationUserEntity.MiddleName} {applicationUserEntity.LastName}" : $"{applicationUserEntity.FirstName} {applicationUserEntity.LastName}";
                    identity.AddClaim(new Claim("Name", nameString));
                }
            }

            return identity;
        }
    }
}
