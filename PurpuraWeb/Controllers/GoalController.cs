using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Models.ViewModels;

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
            var userGoals = await _goalService.GetAllGoalsByUserIdAsync(_userManager.GetUserId(User));

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GoalViewModel viewModel)
        {
            if(viewModel.IsDateRequired && (viewModel.DueDate == null || viewModel.DueDate == DateTime.MinValue))
            {
                ModelState.AddModelError("DueDate", "Please provide a due date.");
            }

            if (ModelState.IsValid)
            {
                var result = await _goalService.CreateAsync(viewModel);

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
            var goalViewModel = await _goalService.GetByExternalReferenceAsync(reference);

            if (goalViewModel == null)
            {
                return RedirectToAction("Index");
            }

            return View(goalViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string reference)
        {
            var goalViewModel = await _goalService.GetByExternalReferenceAsync(reference);

            return View(goalViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GoalViewModel goalViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _goalService.EditAsync(goalViewModel);

                if (result.IsSuccess)
                {
                    return RedirectToAction("Details", new { reference = goalViewModel.ExternalReference });
                }
                else
                {
                    goalViewModel.Result = result;
                }
            }

            return View(goalViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string reference)
        {
            var goalViewModel = await _goalService.GetByExternalReferenceAsync(reference);

            if(goalViewModel == null)
            {
                return RedirectToAction("Index");
            }

            return View(goalViewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(GoalViewModel viewModel)
        {
            var result = await _goalService.DeleteAsync(viewModel);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            else
            {
                viewModel.Result = result;
            }

            return View(viewModel);
        }
    }
}
