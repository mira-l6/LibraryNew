using LibraryNew.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Logging;

namespace LibraryNew.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly LibraryDbContext _context;
        public AdminController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult PendingBooks()
        {
            var books = _context.Books.Where(b => b.ApprovalStatus == "Pending" && b.IsPublic).ToList();
            return View(books);
        }

        [HttpPost]
        public IActionResult ApproveBook(int Id)
        {
            var book = _context.Books.Find(Id);
            
            if(book != null)
            {
                book.ApprovalStatus = "Approved";
                _context.SaveChanges();
            }

            return RedirectToAction("PendingBooks");
        }

        [HttpPost]
        public IActionResult RejectBook(int Id)
        {
            var book = _context.Books.Find(Id);

            if (book != null)
            {
                book.ApprovalStatus = "Rejected";
                _context.SaveChanges();
            }

            return RedirectToAction("PendingBooks");
        }

    }
}
