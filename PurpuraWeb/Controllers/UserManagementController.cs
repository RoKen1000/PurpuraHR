using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Purpura.DataAccess.DataContext;
using Purpura.Models.ViewModels;
using Purpura.Repositories;
using Purpura.Repositories.Interfaces;

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
        public async Task<IActionResult> ViewUserDetails(string userId)
        {
            var user = _userManagementRepository.GetUser(u => u.Id == userId);

            if (user == null)
                return NotFound();

            var viewModel = _mapper.Map<ApplicationUserViewModel>(user);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserDetails(ApplicationUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _userManagementRepository.UpdateUser(viewModel);

                return RedirectToAction("EditUserDetails");
            }

            return RedirectToAction("Index");
        }
    }
}
