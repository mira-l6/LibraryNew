using GroqSharp;
using LibraryNew.Data;
using LibraryNew.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace LibraryNew.Controllers
{
    public class ChatBotController : BaseController
    {

        private readonly IGroqClient _groqClient;
        private readonly ApplicationDbContext _context;

        public ChatBotController(IGroqClient groqClient, ApplicationDbContext context)
        {
            _groqClient = groqClient;
            _context = context;
           
        }





        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Index(string question)
        {



            bool IsModel = false;
            if (question.Contains("All books", StringComparison.OrdinalIgnoreCase))
            {
                var books = await _context.Books.Select(n => new { n.Title, n.Description, n.PublishingYear, n.Pages, n.Language, n.BookAward }).ToListAsync();


                var booksInfo = string.Join(", ", books.Select(n => $"{n.Title} with desc: {n.Description} pusblished by {n.PublishingYear} with  {n.Pages} pages. Language: {n.Language}  {n.BookAward}"));
                question = $"Here are all the books: {booksInfo}";
                IsModel = true;
            }
            else if (question.Contains("All authors", StringComparison.OrdinalIgnoreCase))
            {

                var authors = await _context.Authors.Select(a => new { a.FirstName, a.LastName, a.BirthYear }).ToListAsync();

                var authorsInfo = string.Join(", ", authors.Select(a => $"{a.FirstName} {a.LastName} {a.BirthYear}"));
                question = $"Here are all the authors: {authorsInfo}";
                IsModel = true;

            }
            else if (question.Contains("All categories", StringComparison.OrdinalIgnoreCase))
            {
                var categories = await _context.Categories.Select(c => new { c.Genre, c.Description }).ToListAsync();

                var categoriesInfo = string.Join(", ", categories.Select(c => $"{c.Genre} {c.Description}"));
                question = $"Here are all the categories: {categoriesInfo}";
                IsModel = true;
            }
            else
            {
                question = string.Concat(question, " Please answer in 50 characters or less.");
            }

            if (IsModel)
            {
                question = string.Concat(question, " just print the information and separate every different object with ,");
            }        
            string answer = await _groqClient.CreateChatCompletionAsync(new GroqSharp.Models.Message { Content = question });
            ViewBag.Answer = answer;
            return View();
        }
    }
}
