
using LibraryNew.Models.ViewModel;
using Moq;
using LibraryNew.Data;
using LibraryNew.Controllers;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryNew.Models;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework.Legacy;

namespace LibraryNew.NUnitTests.BookTest
{

    //public IActionResult Create()
    //{
    //    BookVM bookVM = new BookVM();
    //    bookVM.categories = _context.Categories.Select(c =>
    //    new SelectListItem
    //    {
    //            Value = c.Id.ToString(),
    //            Text = c.Genre
    //        }).ToList();

    //    bookVM.authors = _context.Authors.Select(a =>
    //        new SelectListItem
    //        {
    //            Value = a.Id.ToString(),
    //            Text = a.FirstName + " " + a.LastName
    //        }).ToList();

    //    return View(bookVM);
    //}
    [TestFixture]
    public class BookTest
    {
        private Mock<ApplicationDbContext> _mockContext;
        private BookController _controller;
        private Mock<Microsoft.AspNetCore.Hosting.IHostingEnvironment> _mockEnvironment;

        public void SetUp()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _mockEnvironment = new Mock<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
            _controller = new BookController(_mockContext.Object, _mockEnvironment.Object);
        }

        [Test]
        public async Task Create_InvalidModel_ReturnsViewWithModelErrors()
        {
            // Arrange
            var bookVM = new BookVM
            {
                book = new Book
                {
                    Title = "",  // Invalid input for Title
                    Description = "Test",
                    PublishingYear = 2024,
                    Pages = 150,
                    Language = "en",
                    BookAward = false,
                    CategoryId = 1
                },
                SelectedAuthorsId = new List<int> { 1 }
            };

            // Act
            _controller.ModelState.AddModelError("Title", "This field is required."); // Simulating model state error
            var result = await _controller.Create(bookVM);

            // Assert
            var viewResult = result as ViewResult; // Cast to ViewResult to access ViewData
            ClassicAssert.IsNotNull(viewResult); // Ensure that the result is a ViewResult
            ClassicAssert.AreEqual("Create", viewResult.ViewName); // Assuming the view name is "Create"

            // Check if ModelState contains errors
            ClassicAssert.IsFalse(viewResult.ViewData.ModelState.IsValid);
            ClassicAssert.IsTrue(viewResult.ViewData.ModelState.ContainsKey("Title")); // Check if the error for Title exists
            ClassicAssert.AreEqual("This field is required.", viewResult.ViewData.ModelState["Title"].Errors[0].ErrorMessage); // Check the error message
        }


    }
}
