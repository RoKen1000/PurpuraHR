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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CustomUserClaimsPrincipalFactory(UserManager<IdentityUser> userManager, 
            IOptions<IdentityOptions> options,
            IUserManagementService userManagementService,
            RoleManager<IdentityRole> roleManager) : base(userManager, roleManager, options)
        {
            _userManager = userManager;
            _userManagementService = userManagementService;
            _roleManager = roleManager;
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

            return identity;
        }
    }
}
