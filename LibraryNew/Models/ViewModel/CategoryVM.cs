using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryNew.Models.ViewModel
{
    public class CategoryVM
    {
        public Category Category { get; set; } 
        public List<SelectListItem> AllCategories = new List<SelectListItem>();

    }
}
