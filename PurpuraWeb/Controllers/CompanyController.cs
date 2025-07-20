using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;

namespace PurpuraWeb.Controllers
{
    public class CompanyController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICompanyService _companyService;

        public CompanyController(UserManager<IdentityUser> userManager, ICompanyService companyService)
        {
            _userManager = userManager;
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
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
