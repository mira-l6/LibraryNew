using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LibraryNew.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "* Genre is required.")]
        public string Genre { get; set; }

        public string? Description { get; set; }

        [ForeignKey("ParentCategory")]
        public int? ParentId { get; set; }

        [DisplayName("Parent category")]
        public Category? ParentCategory { get; set; }

        public ICollection<Category>? ChildCategories { get; set;}
    }
}
