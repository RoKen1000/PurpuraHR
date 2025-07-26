using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Purpura.Abstractions.ServiceInterfaces;
using System.Security.Claims;

namespace Purpura.Utility.Factories
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser>
    {
        private readonly IUserManagementService _userManagementService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomClaimsPrincipalFactory(UserManager<IdentityUser> userManager, 
            IOptions<IdentityOptions> options,
            IUserManagementService userManagementService,
            IHttpContextAccessor httpContextAccessor) : base(userManager, options)
        {
            _userManager = userManager;
            _userManagementService = userManagementService;
            _httpContextAccessor = httpContextAccessor;
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
            }

            return identity;
        }
    }
}
