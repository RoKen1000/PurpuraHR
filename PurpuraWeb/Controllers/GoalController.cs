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
        public IActionResult Index()
        {
            var viewModel = new GoalIndexViewModel()
            {

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

                if(result.IsSuccess)
                    return RedirectToAction("Index");
            }

            return View(viewModel);
        }
    }
}
