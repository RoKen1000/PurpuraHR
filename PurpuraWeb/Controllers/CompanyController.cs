using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Models.ViewModels;

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

            var companyViewModel = await _companyService.GetByExternalReferenceAsync(companyReference);

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
                        var claimResult = await _userManagementService.AddUserClaimAsync(_userManager.GetUserId(User), result.Data);

                        if (claimResult.IsSuccess)
                        {
                            return RedirectToAction("Details", new { companyReference = result.Data });
                        }
                    }
                }
            }

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
