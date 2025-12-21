using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Purpura.Abstractions.ServiceInterfaces;
using Purpura.Models.ViewModels;
using System.Threading.Tasks;

namespace PurpuraWeb.Controllers
{
    [Authorize(Roles = "Manager")]
    [Route("/[controller]/")]
    public class CompanyEmployeeController : Controller
    {
        private readonly ICompanyEmployeeService _companyEmployeeService;

        public CompanyEmployeeController(ICompanyEmployeeService companyEmployeeService)
        {
            _companyEmployeeService = companyEmployeeService;
        }

        [HttpGet("Create/{companyReference}")]
        public IActionResult Create(string companyReference)
        {
            var viewModel = new CompanyEmployeeViewModel { CompanyExternalReference = companyReference };

            return View(viewModel);
        }

        [HttpPost("Create/{companyReference}")]
        public async Task<IActionResult> Create(CompanyEmployeeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _companyEmployeeService.Create(viewModel);

                if (result.IsSuccess)
                {
                    return RedirectToAction("Details", "Company", new { companyReference = viewModel.CompanyExternalReference });
                }

                viewModel.Result = result;
            }

            return View(viewModel);
        }
    }
}
