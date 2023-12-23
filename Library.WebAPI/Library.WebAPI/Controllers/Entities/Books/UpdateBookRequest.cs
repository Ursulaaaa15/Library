using Library.DataAccess.Entities;

namespace Library.WebAPI.Controllers.Entities.Books
{
    public class UpdateBookRequest
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Plot { get; set; }

        public DateTime PublicationYear { get; set; }

    }
}
