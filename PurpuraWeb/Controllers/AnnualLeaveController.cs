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
        public IActionResult _Create()
        {
            var viewModel = new AnnualLeaveViewModel
            {
                LeaveTypeSelectList = EnumHelpers.GenerateLeaveTypeSelectList(),
                UserId = _userManager.GetUserId(User)
            };

            return PartialView("_BookTimeOffForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> _Create(AnnualLeaveViewModel bookedOffPeriod)
        {
            if (ModelState.IsValid)
            {
                if (bookedOffPeriod.HasOverlap)
                    return Result.Failure("Overlapping leave periods can not be submitted.");

                return await _annualLeaveRepository.BookTimeOff(bookedOffPeriod);
            }

            return Result.Failure("Fields are missing.");
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

        [HttpGet]
        public async Task<IActionResult> _Edit(string externalReference)
        {
            var viewModel = await _annualLeaveRepository.GetByExternalReference(externalReference);
            viewModel.LeaveTypeSelectList = EnumHelpers.GenerateLeaveTypeSelectList();

            return PartialView("_BookTimeOffForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> _Edit(AnnualLeaveViewModel bookedOffPeriod)
        {
            if (ModelState.IsValid)
            {
                if (bookedOffPeriod.HasOverlap)
                    return Result.Failure("Overlapping leave periods can not be submitted.");

                return await _annualLeaveRepository.Edit(bookedOffPeriod);
            }

            return Result.Failure("Fields are missing.");
        }

        [HttpGet]
        public async Task<IActionResult> _Delete(string externalReference)
        {
            var viewModel = await _annualLeaveRepository.GetByExternalReference(externalReference);
            viewModel.LeaveTypeSelectList = EnumHelpers.GenerateLeaveTypeSelectList();

            return PartialView("_BookTimeOffForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> _Delete(AnnualLeaveViewModel bookedOffPeriod)
        {
            ModelState.Remove("HasOverlap");

            if (ModelState.IsValid)
            {
                return await _annualLeaveRepository.Delete(bookedOffPeriod);
            }

            return Result.Failure("Delete failed.");
        }

        [HttpPost]
        public async Task<Result> CheckForLeaveOverlaps(string userId, DateTime startDate, DateTime endDate, string? leaveExtRef)
        {
            return await _annualLeaveRepository.CheckForLeaveOverlaps(userId, startDate, endDate, leaveExtRef);
        }
    }
}
