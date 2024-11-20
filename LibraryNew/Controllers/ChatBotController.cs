using GroqSharp;
using LibraryNew.Data;
using LibraryNew.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using GroqSharp.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text.RegularExpressions;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace LibraryNew.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatBotController : BaseController
    {

        private readonly IGroqClient _groqClient;
        private readonly LibraryDbContext _context;

        public ChatBotController(IGroqClient groqClient, LibraryDbContext context)
        {
            _groqClient = groqClient;
            _context = context;         
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] ChatRequest chatRequest)
        {

            if (chatRequest == null || string.IsNullOrEmpty(chatRequest.Question))
            {
                return BadRequest(new { error = "The 'question' field is required." });
            }
            string question = chatRequest.Question;

            bool IsModel = false;
            bool isMessage = false;

            //there can be a message which says i didnt understand the question maybe try using some of these keywords: (most keywords)
            string[] bookPhrases = { "all books", "books", "book collection", "my books", "all of my books", "the entirety of my collection", "my entire library", "all the books in my possession", "every book in my collection", "every book", "all types of books", "the full spectrum of books", "every single book", "my complete set of books" };
            bool containsBookKeyword = bookPhrases.Any(phrase => Regex.IsMatch(question, @"\b" + Regex.Escape(phrase) + @"\b", RegexOptions.IgnoreCase));

            string[] authorPhrases = { "all authors", "authors", "every author", "authors of every book", "all of the authors", "every single author", "all writers", "writers", "every writer", "writers of every book", "all of the writers", "every single writer" };
            bool containsAuthorKeyword = authorPhrases.Any(phrase => Regex.IsMatch(question, @"\b" + Regex.Escape(phrase) + @"\b", RegexOptions.IgnoreCase));

            if (containsBookKeyword)
            {
                var books = await _context.Books.Select(n => new { n.Title, n.Description, n.PublishingYear, n.Pages, n.Language, n.BookAward }).ToListAsync();
                var booksInfo = string.Join(", ", books.Select(n => $"{n.Title} with desc: {n.Description} pusblished by {n.PublishingYear} with  {n.Pages} pages. Language: {n.Language}  {n.BookAward}\n"));
                question = $"{booksInfo}";
                IsModel = true;
            }
            else if (containsAuthorKeyword)
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
            } else if (question.Contains("application", StringComparison.OrdinalIgnoreCase) || question.Contains("приложение", StringComparison.OrdinalIgnoreCase))
            {
                question = $"Welcome to Library—the place where you can create and curate your own personal library! Whether you're a book lover, researcher, or lifelong learner, our platform lets you build a collection that reflects your unique interests and needs.\r\n\r\nAdd books, articles, audiobooks, and other resources to your virtual shelves, organize them in a way that suits you, and track your reading progress all in one place. Share your library with friends or keep it private, and discover new titles based on your preferences.\r\n\r\nYour library, your way—start building your custom collection today and unlock a world of knowledge at your fingertips!";
                IsModel = true;
                isMessage = true;
            }
            else
            {
                question = string.Concat(question, " Не отговаряй! Напиши че можеш да отговаряш на въпроси само свързани с приложението!");
            }

            if (IsModel)
            {
                if (!isMessage)
                {
                    question = string.Concat(question, "I want you to return my exact question without this part and format it like a table( | ); i want every object separated with ----{newRowIndecator}---- every object starting with a number; only when the questions contains info certain books add the specification of an award( if book award is True say Rewarded if its False say Not rewarded ) and dont add any kind of notes ");
                    //question = string.Concat(question, "just return the info in the question and put every book object on a new row , and format em like a table( | ) every object  with a number. only when we talking about books( if book award is True say Rewarded if its False say Not rewarded ) and dont add any kind of notes!");
                }
                else
                {
                    question = string.Concat(question, " Върни само това което написах!");
                }
            }  
            

            string answer = await _groqClient.CreateChatCompletionAsync(new GroqSharp.Models.Message { Content = question });

            //ViewBag.Answer = answer;
            return Json(new { answer = answer });

        }
    }
}
