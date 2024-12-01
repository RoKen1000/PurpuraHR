using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Purpura.DataAccess.DataContext;
using Purpura.Models.ViewModels;

namespace PurpuraWeb.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly PurpuraDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserManagementController(PurpuraDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string userId)
        {
            var user = _dbContext.Users.FirstOrDefault(a => a.Id == userId);

            if (user == null)
                return NotFound();

            var viewModel = _mapper.Map<ApplicationUserViewModel>(user);

            return View(viewModel);
        }
    }
}
