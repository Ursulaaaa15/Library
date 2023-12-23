using Library.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.WebAPI.Controllers.Entities.Books
{
    public class CreateBookRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Autor { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Plot { get; set; }
        [Required]
        public string SimilarWorks { get; set; }


        public DateTime PublicationYear { get; set; }

        public int TakeBookId { get; set; }
    }
}
