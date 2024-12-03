using Microsoft.AspNetCore.Mvc;
using Purpura.Models.ViewModels;
using Purpura.Repositories.Interfaces;

namespace PurpuraWeb.Controllers
{
    public class BookTimeOffController : Controller
    {
        private readonly IBookTimeOffRepository _bookTimeOffRepository;

        public BookTimeOffController(IBookTimeOffRepository bookTimeOffRepository)
        {
            _bookTimeOffRepository = bookTimeOffRepository;
        }

        [HttpGet]
        public IActionResult BookTimeOff()
        {
            


            return View(new BookedTimeOffViewModel());
        }

        [HttpPost]
        public async Task<bool> BookTimeOff(BookedTimeOffViewModel bookedOffPeriod)
        {
            return await _bookTimeOffRepository.BookTimeOff(bookedOffPeriod);
        }
    }
}
