using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;

namespace PurpuraWeb.Controllers
{
    public class AnnualLeaveController : Controller
    {
        private readonly IAnnualLeaveRepository _annualLeaveRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AnnualLeaveController(IAnnualLeaveRepository annualLeaveRepository,
            UserManager<IdentityUser> userManager)
        {
            _annualLeaveRepository = annualLeaveRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new AnnualLeaveIndexViewModel
            {
                UserId = _userManager.GetUserId(User),
                AnnualLeaveDays = await _annualLeaveRepository.GetUserAnnualLeaveCount(_userManager.GetUserId(User))
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult _BookTimeOff()
        {


            return View();
        }

        [HttpPost]
        public async Task<bool> BookTimeOff(AnnualLeaveViewModel bookedOffPeriod)
        {
            return await _annualLeaveRepository.BookTimeOff(bookedOffPeriod);
        }
    }
}
