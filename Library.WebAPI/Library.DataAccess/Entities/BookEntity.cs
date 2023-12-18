using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DataAccess.Entities
{
    [Table("books")]
    public class BookEntity : BaseEntity
    {
        public string Title { get; set; }

        public string Autor { get; set; }
        public string Genre { get; set; }

        public DateTime PublicationYear { get; set; }

        public int TakeBookId { get; set; }
        public TakeBookEntity TakeBook { get; set; }
    }
}
