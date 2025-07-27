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
                var appicationUserEntity = await _userManagementService.GetUserEntityByIdAsync(_userManager.GetUserId(_httpContextAccessor.HttpContext.User));

                if(appicationUserEntity != null && (appicationUserEntity.CompanyId != null && appicationUserEntity.CompanyId > 0))
                {
                    var companyExternalReference = await _companyService.GetExternalReferenceByIdAsync(appicationUserEntity.CompanyId);
                    identity.AddClaim(new Claim("CompanyReference", companyExternalReference));
                }
            }

            return identity;
        }
    }
}
