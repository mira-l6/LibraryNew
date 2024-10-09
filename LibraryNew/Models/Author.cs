using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryNew.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("First Name: ")]

        [Required(ErrorMessage = "The first name is required")]

        public string FirstName { get; set; }

        [DisplayName("Last name: ")]
        [Required(ErrorMessage = "The last name is required")]

        public string LastName { get; set; }

        [DisplayName("Year of birth: ")]
        public int BirthYear { get; set; }

        public ICollection<BookAuthor>? BookAuthors { get; set; }

    }
}
