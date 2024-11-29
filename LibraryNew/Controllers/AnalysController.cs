using LibraryNew.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryNew.Controllers
{
    public class AnalysController : Controller
    {
        private readonly LibraryDbContext _context;

        public AnalysController(LibraryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
