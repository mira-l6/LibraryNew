using LibraryNew.Data;
using LibraryNew.Models;
using LibraryNew.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace LibraryNew.Controllers
{
    public class BookController : BaseController
    {
        private ApplicationDbContext _context;

        private readonly IHostingEnvironment _environment;

        public BookController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _environment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            List<Book> books = _context.Books.ToList();
            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            BookVM bookVM = new BookVM();
            bookVM.categories = _context.Categories.Select(c =>
                new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Genre
                }).ToList();

            bookVM.authors = _context.Authors.Select(a =>
                new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FirstName + " " + a.LastName
                }).ToList();

            return View(bookVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookVM model)
        {
            if(!ModelState.IsValid)
            {
                model.categories = _context.Categories.Select(c =>
                new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Genre
                }).ToList();

                model.authors = _context.Authors.Select(a =>
                    new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.FirstName + " " + a.LastName
                    }).ToList();
                return View(model);
            }


            List<Author> authorList = new List<Author>();

            foreach (var id in model.SelectedAuthorsId)
            {
                authorList.Add(_context.Authors.FirstOrDefault(a => a.Id == id));
            }

            List<BookAuthor> bookAuthors = authorList.Select(a => new BookAuthor { Author = a }).ToList();

            Book book = new Book()
            {
                Title = model.book.Title,
                Description = model.book.Description,
                Category = _context.Categories.FirstOrDefault(c => c.Id == model.book.CategoryId),
                PublishingYear = model.book.PublishingYear,
                Pages = model.book.Pages,
                Language = model.book.Language,
                BookAward = model.book.BookAward,
                BookAuthors = bookAuthors,
                PdfFilePath = await GetFilePath("Pdf", model.FilePDF),
                ImagePath = await GetFilePath("Img", model.FileIMG),
            };

            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int Id)
        {
            Book book = _context.Books.Include(b=>b.Category).Include(b => b.BookAuthors).FirstOrDefault( b=> b.Id == Id );  
            
            foreach(var ba in book.BookAuthors)
            {
                ba.Author = _context.Authors.FirstOrDefault(a => a.Id == ba.AuthorId);
            }

            return View(book);
        }

        [HttpGet]
        public IActionResult Read(int Id)
        {
            Book book = _context.Books.FirstOrDefault(b=>b.Id == Id);
            return View(book);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Book currentBook = _context.Books.Include(b => b.Category).Include(b => b.BookAuthors).FirstOrDefault(b => b.Id == Id);
            var modelCategories = _context.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Genre
            }).ToList();

            var modelAauthors = _context.Authors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FirstName + " " + a.LastName
            }).ToList();


            //трябва да оправя файловете

            BookVM model = new BookVM()
            {
                book = currentBook,
                categories = modelCategories,
                authors = modelAauthors
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookVM model)
        {
            Book currentBook= _context.Books.Include(b => b.BookAuthors).Include(b => b.Category).FirstOrDefault(b => b.Id == model.book.Id);

            if (!ModelState.IsValid)
            {
                var modelCategories = _context.Categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Genre
                }).ToList();

                var modelAauthors = _context.Authors.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FirstName + " " + a.LastName
                }).ToList();

                BookVM bookVM = new BookVM()
                {
                    book = currentBook,
                    categories = modelCategories,
                    authors = modelAauthors
                };

                return View(bookVM);
            }

            List<Author> authorList = new List<Author>();

            foreach (var id in model.SelectedAuthorsId)
            {
                authorList.Add(_context.Authors.FirstOrDefault(a => a.Id == id));
            }

            List<BookAuthor> bookAuthors = authorList.Select(a => new BookAuthor { Author = a, Book = currentBook }).ToList();

            currentBook.Title = model.book.Title;
            currentBook.Description = model.book.Description;
            currentBook.CategoryId = model.book.CategoryId;
            currentBook.BookAuthors = bookAuthors;
            currentBook.Pages = model.book.Pages;
            currentBook.Language = model.book.Language;
            currentBook.BookAward = model.book.BookAward;
            currentBook.PdfFilePath = await GetFilePath("Pdf", model.FilePDF);
            currentBook.ImagePath = await GetFilePath("Img", model.FileIMG);

            _context.Books.Update(currentBook);
            _context.SaveChanges();

            return RedirectToAction("Details",new {id = model.book.Id});
        }

        public async Task<string> GetFilePath(string folderName, IFormFile formFile)
        {
            var filePath = "";

            if (formFile != null)
            {
                string fileExt = Path.GetExtension(formFile.FileName);
                var fileName = $"{formFile.Name}_{DateTime.Now:ddMMyyyymms}{fileExt}";

                filePath = Path.Combine(_environment.WebRootPath, folderName, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                filePath = $"/{folderName}/" + fileName;
            }
            return filePath;
        }

    }

   
}
