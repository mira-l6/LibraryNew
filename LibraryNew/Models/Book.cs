﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.ComponentModel;
using static LibraryNew.Data.DataConstants.Constants;

namespace LibraryNew.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is a required field")]
        public string Title { get; set; }

        [MaxLength(DescMaxLenght)]
        public string? Description { get; set; }

        public string? PdfFilePath { get; set; }

        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "Publishing Year is required")]
        public int? PublishingYear { get; set; }

        [Range(1,10)]
        [DisplayName("Rating ( 1 - 10 )")]
        public int? Rating { get; set;}

        [DisplayName("Review / Comment")]
        public string? Review { get; set; }

        [Required(ErrorMessage ="Publicity choice is required")]
        [DisplayName("Private or public choice")]
        public bool IsPublic { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string ApprovalStatus { get; set; }

        [Required(ErrorMessage ="Pages are required")]
        public int? Pages { get; set; }

        [Required(ErrorMessage = "Language is required")]
        public string Language { get; set; }

        [DisplayName("Book award: ")]
        public bool BookAward { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

 
        public ICollection<BookAuthor>? BookAuthors { get; set; }
    }
}
