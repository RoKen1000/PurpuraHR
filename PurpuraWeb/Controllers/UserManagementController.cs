using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Purpura.Common.Enums;
using Purpura.DataAccess.DataContext;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
using Purpura.Utility.Helpers;

namespace PurpuraWeb.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly PurpuraDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserManagementRepository _userManagementRepository;

        public UserManagementController(PurpuraDbContext dbContext, 
            IMapper mapper, 
            IUserManagementRepository userManagementRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManagementRepository = userManagementRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string userId)
        {
            return View();
        }

        [HttpGet]
        [Route("Details/{userId}")]
        public async Task<IActionResult> Details(string userId)
        {
            var userViewModel = await _userManagementRepository.GetUser(u => u.Id == userId);

            if (userViewModel == null)
                return NotFound();

            return View(userViewModel);
        }

        [HttpGet]
        [Route("Edit/{userId}")]
        public async Task<IActionResult> EditUserDetails(string userId)
        {
            var userViewModel = await _userManagementRepository.GetUser(u => u.Id == userId);

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

                await _userManagementRepository.UpdateUser(viewModel);

                return RedirectToAction("EditUserDetails");
            }

            viewModel.GenderList = EnumHelpers.GenerateGenderSelectList();
            viewModel.TitleList = EnumHelpers.GenerateTitleSelectList();

            return View(viewModel);
        }
    }
}
