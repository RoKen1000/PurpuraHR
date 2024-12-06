using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Purpura.Common;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;
using Purpura.Utility.Helpers;
using Purpura.Utility.Resolvers;

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
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> _IndexDayCount()
        {
            var currentUserId = _userManager.GetUserId(User);
            var currentUserAnnualLeave = await _annualLeaveRepository.GetUserAnnualLeaveCount(currentUserId);

            var viewModel = new AnnualLeaveIndexViewModel
            {
                AnnualLeaveDaysRemaining = currentUserAnnualLeave,
                AnnualLeaveDaysUsed = AnnualLeaveResolver.WorkOutNumberOfDaysUsed(currentUserAnnualLeave)
            };

            return PartialView(viewModel);
        }

        [HttpGet]
        public IActionResult _BookTimeOff()
        {
            var viewModel = new AnnualLeaveViewModel
            {
                LeaveTypeSelectList = EnumHelpers.GenerateLeaveTypeSelectList(),
                UserId = _userManager.GetUserId(User)
            };

            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<Result> BookTimeOff(AnnualLeaveViewModel bookedOffPeriod)
        {
            return await _annualLeaveRepository.BookTimeOff(bookedOffPeriod);
        }

        [HttpGet]
        public async Task<IActionResult> _BookedLeaveTable()
        {
            var viewModel = new AnnualLeaveIndexViewModel 
            { 
                BookedLeave = await _annualLeaveRepository.GetBookedLeave(_userManager.GetUserId(User)) 
            };

            return PartialView(viewModel);
        }
    }
}
