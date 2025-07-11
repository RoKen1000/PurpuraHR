using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Purpura.Models.ViewModels;
using Purpura.Services.Interfaces;

namespace PurpuraWeb.Controllers
{
    public class GoalController : Controller
    {
        private readonly IGoalService _goalService;
        private readonly UserManager<IdentityUser> _userManager;

        public GoalController(IGoalService goalService, UserManager<IdentityUser> userManager)
        {
            _goalService = goalService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userGoals = await _goalService.GetAllGoalsByUserId(_userManager.GetUserId(User));

            var viewModel = new GoalIndexViewModel()
            {
                Goals = userGoals
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new GoalViewModel()
            {
                UserId = _userManager.GetUserId(User)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GoalViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _goalService.Create(viewModel);

                if (result.IsSuccess)
                    return RedirectToAction("Index");
                else
                    viewModel.OperationFailure = true;
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string reference)
        {
            var goalViewModel = await _goalService.GetByExternalReference(reference);

            if(goalViewModel == null)
            {
                return RedirectToAction("Index");
            }

            return View(goalViewModel);
        }
    }
}
