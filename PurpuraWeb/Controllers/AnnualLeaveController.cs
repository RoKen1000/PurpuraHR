using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Common.Results;
using Purpura.Models.ViewModels;
using Purpura.Utility.Helpers;

namespace PurpuraWeb.Controllers
{
    public class AnnualLeaveController : Controller
    {
        private readonly IAnnualLeaveService _annualLeaveService;
        private readonly UserManager<IdentityUser> _userManager;

        public AnnualLeaveController(IAnnualLeaveService annualLeaveService,
            UserManager<IdentityUser> userManager)
        {
            _annualLeaveService = annualLeaveService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> _IndexDayCount()
        {
            var currentUserId = _userManager.GetUserId(User);
            var currentUserAnnualLeave = await _annualLeaveService.GetUserAnnualLeaveCountAsync(currentUserId);

            var viewModel = new AnnualLeaveIndexViewModel
            {
                AnnualLeaveDaysRemaining = currentUserAnnualLeave,
                AnnualLeaveDaysUsed = 28 - currentUserAnnualLeave
            };

            return PartialView(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> _Create()
        {
            var viewModel = new AnnualLeaveViewModel
            {
                LeaveTypeSelectList = EnumHelpers.GenerateLeaveTypeSelectList(),
                UserId = _userManager.GetUserId(User)
            };

            var overlapPreCheck = await _annualLeaveService.CheckForLeaveOverlapsAsync(viewModel.UserId, viewModel.StartDate, viewModel.EndDate, null);
            viewModel.HasOverlap = overlapPreCheck.HasOverlap;

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

                return await _annualLeaveService.CreateAsync(bookedOffPeriod);
            }

            return Result.Failure("Fields are missing.");
        }

        [HttpGet]
        public async Task<IActionResult> _BookedLeaveTable()
        {
            var viewModel = new AnnualLeaveIndexViewModel 
            { 
                BookedLeave = await _annualLeaveService.GetBookedLeaveByUserIdAsync(_userManager.GetUserId(User)) 
            };

            return PartialView(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> _Edit(string externalReference)
        {
            AnnualLeaveViewModel? viewModel = null;
            viewModel = await _annualLeaveService.GetByExternalReferenceAsync(externalReference);

            if(viewModel == null)
            {
                viewModel = new();
                viewModel.Result = Result.Failure("Can not find entity");
            }
            else
            {
                viewModel.LeaveTypeSelectList = EnumHelpers.GenerateLeaveTypeSelectList();

                var overlapPreCheck = await _annualLeaveService.CheckForLeaveOverlapsAsync(viewModel.UserId, viewModel.StartDate, viewModel.EndDate, viewModel.ExternalReference);
                viewModel.HasOverlap = overlapPreCheck.HasOverlap;
            }

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

                return await _annualLeaveService.EditAsync(bookedOffPeriod);
            }

            return Result.Failure("Fields are missing.");
        }

        [HttpGet]
        public async Task<IActionResult> _Delete(string externalReference)
        {
            AnnualLeaveViewModel? viewModel = null;

            viewModel = await _annualLeaveService.GetByExternalReferenceAsync(externalReference);

            if(viewModel == null)
            {
                viewModel = new();
                viewModel.Result = Result.Failure("Can not find entity");
            }
            else
            {
                viewModel.LeaveTypeSelectList = EnumHelpers.GenerateLeaveTypeSelectList();
                viewModel.UserId = _userManager.GetUserId(User);
            }

            return PartialView("_BookTimeOffForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> _Delete(AnnualLeaveViewModel bookedOffPeriod)
        {
            ModelState.Remove("HasOverlap");

            if (ModelState.IsValid)
            {
                return await _annualLeaveService.DeleteAsync(bookedOffPeriod);
            }

            return Result.Failure("Delete failed.");
        }

        [HttpPost]
        public async Task<OverlapResult> CheckForLeaveOverlaps(string userId, DateTime startDate, DateTime endDate, string? leaveExtRef)
        {
            return await _annualLeaveService.CheckForLeaveOverlapsAsync(userId, startDate, endDate, leaveExtRef);
        }
    }
}
