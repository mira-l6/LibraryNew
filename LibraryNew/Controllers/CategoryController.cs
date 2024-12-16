using LibraryNew.Data;
using LibraryNew.Models;
using LibraryNew.Models.ViewModel;
using LibraryNew.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Microsoft.AspNetCore.Authorization;

namespace LibraryNew.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        public readonly LibraryDbContext _context;
        public CategoryController(LibraryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CategoryVM categoryVM = new CategoryVM();
            categoryVM.AllCategories = _context.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Genre
            }).ToList();
            return View(categoryVM);
        }
        [HttpPost]
        public IActionResult Create(CategoryVM model)
        {
            if (!ModelState.IsValid)
            {
                model.AllCategories = _context.Categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Genre
                }).ToList();
                ModelState.AddModelError("my_key", "All fields with * are requred");
                return View(model);
            }

            Category c = new Category()
            {
                Genre = model.Category.Genre,
                Description = model.Category.Description,
                ParentId = model.Category.ParentId
            };
            _context.Categories.Add(c);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            Category? category = _context.Categories.Include(c=>c.ChildCategories).FirstOrDefault(c => c.Id == Id);
            List<Category> childCategories = category.ChildCategories.ToList();

            foreach(var child in childCategories)
            {
                child.ParentId = null;
                _context.Categories.Update(child);

            }
            
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var currentCategory = _context.Categories.FirstOrDefault(c => c.Id == Id);
            if (currentCategory == null)
            {
                return RedirectToAction("Index");
            }
            else {
                CategoryVM model = new CategoryVM()
                {
                    Category = currentCategory,
                    AllCategories = _context.Categories.Where(c=>c.Id != Id).Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Genre
                    }).ToList()
                 };
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult Edit(CategoryVM model)
        {

            model.AllCategories = _context.Categories.Where(c => c.Id != model.Category.Id).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Genre
            }).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var category = _context.Categories.FirstOrDefault(x => x.Id == model.Category.Id);

            if (category == null)
            {
                return NotFound();
            }

            category.Genre = model.Category.Genre;
            category.Description = model.Category.Description;
            if(model.Category.ParentId == category.Id)
            {
                ModelState.AddModelError("", "The Parent cannot be the same as the current Category");
                return View(model);
            }
            category.ParentId = model.Category.ParentId;
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
