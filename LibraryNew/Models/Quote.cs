using System.ComponentModel.DataAnnotations;

namespace LibraryNew.Models
{
    public class QuoteSet
    {
        [Key]
        public int Id { get; set; }
        public required string Quote { get; set; }
        public required string Author { get; set; }
        public required string Category { get; set; }
    }
}
