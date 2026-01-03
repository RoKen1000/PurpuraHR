using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Models.ViewModels;
using Purpura.Utility.Helpers;

namespace PurpuraWeb.Controllers
{
    [Route("[controller]")]
    public class UserManagementController : Controller
    {
        private readonly IUserManagementService _userManagementService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserManagementController(IUserManagementService userManagementService, 
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManagementService = userManagementService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("Details/{userId}")]
        public async Task<IActionResult> Details(string userId)
        {
            var userViewModel = await _userManagementService.GetUser(u => u.Id == userId);

            if (userViewModel == null)
                return NotFound();

            return View(userViewModel);
        }

        [HttpGet]
        [Route("Edit/{userId}")]
        public async Task<IActionResult> Edit(string userId)
        {
            var userViewModel = await _userManagementService.GetUser(u => u.Id == userId);

            if (userViewModel == null)
                return NotFound();

            userViewModel.GenderList = EnumHelpers.GenerateGenderSelectList();
            userViewModel.TitleList = EnumHelpers.GenerateTitleSelectList();

            return View(userViewModel);
        }

        [HttpPost]
        [Route("Edit/{userId}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Address = AddressHelpers.ConstructAddressString(new string[4] { viewModel.AddressLine1, viewModel.AddressLine2, viewModel.AddressLine3, viewModel.Postcode });

                var result = await _userManagementService.UpdateUser(viewModel);

                if (result.IsSuccess)
                {
                    var nameString = viewModel.MiddleName != null ? $"{viewModel.FirstName} {viewModel.MiddleName} {viewModel.LastName}" : $"{viewModel.FirstName} {viewModel.LastName}";
                    var nameClaim = User.Claims.FirstOrDefault(c => c.Type == "Name");

                    if(nameClaim != null && (nameString.ToLower() != nameClaim.Value.ToLower()))
                    {
                        var user = await _userManager.GetUserAsync(User);
                        await _signInManager.RefreshSignInAsync(user);
                    }

                    return RedirectToAction($"Details", new { userId = viewModel.Id });
                }

                viewModel.Result = result;
            }

            viewModel.GenderList = EnumHelpers.GenerateGenderSelectList();
            viewModel.TitleList = EnumHelpers.GenerateTitleSelectList();

            return View(viewModel);
        }

        [HttpPost("CheckUserEmailExists")]
        public async Task<IActionResult> CheckUserEmailExists(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var normalisedEmail = _userManager.NormalizeEmail(input);
                var user = await _userManager.FindByEmailAsync(normalisedEmail);
                return Json(user != null);
            }

            return Json(false);
        }
    }
}
