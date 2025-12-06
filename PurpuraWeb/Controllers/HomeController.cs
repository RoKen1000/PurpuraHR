using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Common.Results;
using Purpura.Models.ViewModels;

namespace PurpuraWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserManagementService _userManagementService;
        private readonly ICompanyService _companyService;
        private readonly IAnnualLeaveService _annualLeaveService;
        private readonly IGoalService _goalService;

        public HomeController(ILogger<HomeController> logger,
            UserManager<IdentityUser> userManager,
            IUserManagementService userManagementService,
            ICompanyService companyService,
            IAnnualLeaveService annualLeaveService,
            IGoalService goalService)
        {
            _logger = logger;
            _userManager = userManager;
            _userManagementService = userManagementService;
            _companyService = companyService;
            _annualLeaveService = annualLeaveService;
            _goalService = goalService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard");
            }

            return Redirect("/Account/Login");
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var viewModel = new DashboardViewModel();

            if(user != null)
            {
                viewModel.User = await _userManagementService.GetUserViewModelByIdAsync(_userManager.GetUserId(User));

                var userClaims = await _userManager.GetClaimsAsync(user);
                var companyClaim = userClaims.FirstOrDefault(c => c.Type == "CompanyReference");

                if(companyClaim != null)
                {
                    viewModel.Company = await _companyService.GetByExternalReferenceAsync(companyClaim.Value);
                }

                viewModel.AnnualLeave = await _annualLeaveService.GetBookedLeaveByUserIdAsync(_userManager.GetUserId(User));
                viewModel.AnnualLeaveRemaining = await _annualLeaveService.GetUserAnnualLeaveCountAsync(_userManager.GetUserId(User));
                viewModel.Goals = await _goalService.GetAllGoalsByUserIdAsync(_userManager.GetUserId(User));
            }
            else
            {
                viewModel.Result = Result.Failure("User was not found. Please contact your line manager for guidance on how to proceed");
            }

            return View(viewModel);
        }
    }
}
