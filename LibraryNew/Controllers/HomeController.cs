using LibraryNew.Models;
using LibraryNew.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryNew.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ITimeNow _timeService;
        private readonly IGreeting _greeting;

        public HomeController(ITimeNow timeNow, IGreeting greeting)
        {
           _timeService = timeNow;
            _greeting = greeting;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.CurrentTime = _timeService.GetTimeNow();
            ViewBag.Greeting = _greeting.GetGreeted();
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
