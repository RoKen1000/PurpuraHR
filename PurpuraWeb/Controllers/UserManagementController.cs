using Microsoft.AspNetCore.Mvc;
using Purpura.Models.ViewModels;
using Purpura.Services.Interfaces;
using Purpura.Utility.Helpers;

namespace PurpuraWeb.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
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
        public async Task<IActionResult> EditUserDetails(string userId)
        {
            var userViewModel = await _userManagementService.GetUser(u => u.Id == userId);

            if (userViewModel == null)
                return NotFound();

            userViewModel.GenderList = EnumHelpers.GenerateGenderSelectList();
            userViewModel.TitleList = EnumHelpers.GenerateTitleSelectList();
            AddressHelpers.DeconstructAddressString(userViewModel);

            return View(userViewModel);
        }

        [HttpPost]
        [Route("Edit/{userId}")]
        public async Task<IActionResult> EditUserDetails(ApplicationUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Address = AddressHelpers.ConstructAddressString(new string[4] { viewModel.AddressLine1, viewModel.AddressLine2, viewModel.AddressLine3, viewModel.Postcode });

                await _userManagementService.UpdateUser(viewModel);

                return RedirectToAction($"Details", new { userId = viewModel.Id });
            }

            viewModel.GenderList = EnumHelpers.GenerateGenderSelectList();
            viewModel.TitleList = EnumHelpers.GenerateTitleSelectList();

            return View(viewModel);
        }
    }
}
