﻿using Microsoft.AspNetCore.Mvc;
using LibraryNew.Models;
using LibraryNew.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;
using LibraryNew.Models.MappingClasses;
using LibraryNew.Services.AuthorSetHelper;
using Microsoft.EntityFrameworkCore;

namespace LibraryNew.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthorController : Controller
    {
        private readonly LibraryDbContext _context;
        public AuthorController(LibraryDbContext db) { 
            _context = db;
        }
        public IActionResult Index()
        {
            List<Author> authorList = _context.Authors.ToList();
            return View(authorList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            var authorToSave = new Author
            {
                BirthYear = author.BirthYear,
                FirstName = author.FirstName,
                LastName = author.LastName,
                BookAuthors = author.BookAuthors
            };

            _context.Authors.Add(authorToSave);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        //трябва да направя edit и update
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var authorId = _context.Authors.Where( i => i.Id == id).FirstOrDefault();
            return View(authorId);
        }

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            _context.Authors.Update(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var authorId = _context.Authors.Where(authorId => authorId.Id == id).FirstOrDefault();
            return View(authorId);
        }
        [HttpPost]
        public IActionResult Delete(Author author)
        {
            _context.Authors.Remove(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
