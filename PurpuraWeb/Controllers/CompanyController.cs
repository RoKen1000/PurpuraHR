using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;

namespace PurpuraWeb.Controllers
{
    public class CompanyController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICompanyRepository _companyService;

        public CompanyController(UserManager<IdentityUser> userManager, ICompanyRepository companyService)
        {
            _userManager = userManager;
            _companyService = companyService;
        }

        public IActionResult Details(string? companyReference)
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CompanyViewModel viewModel)
        {
            //if successful then add claim for company id
            return View();
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
