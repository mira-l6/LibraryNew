using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryNew.Models.ViewModel
{
    public class BookVM
    {
        public Book book { get; set; }
        public List<SelectListItem>? authors { get; set; }
        public List<SelectListItem>? categories { get; set; }

        [Required(ErrorMessage ="Author/s are required")]
        [DisplayName("Author/s")]
        public List<int> SelectedAuthorsId { get; set; }

        [DisplayName("PDF file")]
        public IFormFile? FilePDF { get; set; }

        [DisplayName("IMG file")]
        public IFormFile? FileIMG{ get; set; }


        public string PDFAcceptTypes { get; set; } = ".pdf";
        public string ImgAcceptType { get; set; } = "image/jpeg, image/png";
    }
}
