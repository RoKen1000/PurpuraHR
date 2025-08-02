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

        public CompanyController(UserManager<IdentityUser> userManager, ICompanyService companyService, IUserManagementService userManagementService)
        {
            _userManager = userManager;
            _companyService = companyService;
            _userManagementService = userManagementService;
        }

        public async Task<IActionResult> Details(string? companyReference)
        {
            if(companyReference == null)
            {
                return RedirectToAction("Create");
            }

            var companyViewModel = await _companyService.GetByExternalReferenceWithCompanyEmployeesAsync(companyReference);

            if (companyViewModel != null)
            {
                return View(companyViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Create()
        {
            var viewModel = new CompanyViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _companyService.CreateAsync(viewModel);

                if (result.IsSuccess)
                {
                    //if successful then add claim for company id
                    var user = await _userManagementService.GetUser(u => u.Id == _userManager.GetUserId(User));

                    if (user != null)
                    {
                        var claimResult = await _userManagementService.AddUserCompanyReferenceClaimAsync(_userManager.GetUserId(User), result.DataList.ElementAt(0), result.DataList.ElementAt(1));

                        if (claimResult.IsSuccess)
                        {
                            return RedirectToAction("Details", new { companyReference = result.Data });
                        }
                    }
                }
            }

            return View(viewModel);
        }

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
