using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Models.ViewModels;
using System.Threading.Tasks;

namespace PurpuraWeb.Controllers
{
    public class CompanyController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICompanyService _companyService;
        private readonly IUserManagementService _userManagementService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CompanyController(UserManager<IdentityUser> userManager, ICompanyService companyService, IUserManagementService userManagementService, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _companyService = companyService;
            _userManagementService = userManagementService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Details(string? companyReference)
        {
            if(companyReference == null && User.IsInRole("Manager"))
            {
                return RedirectToAction("Create");
            }
            else if(companyReference == null && !User.IsInRole("Manager"))
            {
                return View(new CompanyViewModel { NotManagerAndNoCompany = true });
            }

            var companyViewModel = await _companyService.GetByExternalReferenceWithCompanyEmployeesAsync(companyReference);

            if (companyViewModel != null)
            {
                return View(companyViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            var viewModel = new CompanyViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create(CompanyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _companyService.CreateAsync(viewModel);

                if (result.IsSuccess)
                {
                    //if successful then add claim for company id
                    var user = await _userManagementService.GetUserEntityByIdAsync(_userManager.GetUserId(User));

                    if (user != null)
                    {
                        var claimResult = await _userManagementService.AddUserCompanyReferenceClaimAsync(_userManager.GetUserId(User), result.DataList.ElementAt(0), result.DataList.ElementAt(1));

                        if (claimResult.IsSuccess)
                        {
                            await _signInManager.RefreshSignInAsync(user);
                            return RedirectToAction("Details", new { companyReference = result.DataList.ElementAt(0) });
                        }
                    }
                }
            }

            return View(viewModel);
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(string reference)
        {
            var companyViewModel = await _companyService.GetByExternalReferenceAsync(reference);

            if(companyViewModel != null)
            {
                return View(companyViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(CompanyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _companyService.EditAsync(viewModel);

                if(result.IsSuccess)
                {
                    return RedirectToAction("Details", new { companyReference = viewModel.ExternalReference });
                }

                viewModel.Result = result;
            }

            return View(viewModel);
        }
    }
}
