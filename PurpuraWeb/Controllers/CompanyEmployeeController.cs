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

        [HttpGet]
        public IActionResult Create(string companyReference)
        {
            var viewModel = new CompanyEmployeeViewModel { CompanyExternalReference = companyReference };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyEmployeeViewModel viewModel)
        {
            var companyEmployee = await _companyEmployeeService.GetAsync(ce => ce.Email == viewModel.Email);

            if(companyEmployee != null)
            {
                ModelState.AddModelError("Email", "A Company Employee has already been created with this email.");
            }

            if (ModelState.IsValid)
            {
                var result = await _companyEmployeeService.CreateAsync(viewModel);

                if (result.IsSuccess)
                {
                    return RedirectToAction("Details", "Company", new { companyReference = viewModel.CompanyExternalReference });
                }

                viewModel.Result = result;
            }

            return View(viewModel);
        }

        [HttpGet("Edit/{companyEmployeeRef}")]
        public async Task<IActionResult> Edit(string companyEmployeeRef)
        {
            var employee = await _companyEmployeeService.GetByExternalReferenceAsync(companyEmployeeRef);

            return View(employee);
        }

        [HttpPost("Edit/{companyEmployeeRef}")]
        public async Task<IActionResult> Edit(CompanyEmployeeViewModel viewModel)
        {
            var companyEmployee = await _companyEmployeeService.GetAsync(ce => ce.Email == viewModel.Email);

            if (companyEmployee != null && (companyEmployee.Email != viewModel.Email))
            {
                ModelState.AddModelError("Email", "A Company Employee has already been created with this email.");
            }

            if (ModelState.IsValid)
            {
                var result = await _companyEmployeeService.EditAsync(viewModel);

                if (result.IsSuccess)
                {
                    return RedirectToAction("Details", "Company", new { companyReference = viewModel.CompanyExternalReference });
                }

                viewModel.Result = result;
            }

            return View(viewModel);
        }

        [HttpGet("Details/{companyEmployeeRef}")]
        public async Task<IActionResult> Details(string companyEmployeeRef)
        {
            var employee = await _companyEmployeeService.GetByExternalReferenceAsync(companyEmployeeRef);

            return View(employee);
        }
    }
}
