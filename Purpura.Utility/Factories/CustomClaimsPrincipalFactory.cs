using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Purpura.Utility.Factories
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<IdentityUser>
    {
        public CustomClaimsPrincipalFactory(UserManager<IdentityUser> userManager, IOptions<IdentityOptions> options) : base(userManager, options)
        {
            
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            if(user.Email == "test@testuser.co.uk")
            {
                identity.AddClaim(new Claim("CompanyReference", "05595D26-131C-45EA-B78C-C4E912FC2438"));
            }

            return identity;
        }
    }
}
