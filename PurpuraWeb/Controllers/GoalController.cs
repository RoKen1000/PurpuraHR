using Microsoft.AspNetCore.Mvc;

namespace PurpuraWeb.Controllers
{
    public class GoalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
