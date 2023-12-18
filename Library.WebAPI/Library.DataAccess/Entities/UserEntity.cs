using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DataAccess.Entities
{
    [Table("users")]
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }

        public string Email { get; set; }

        public DateTime Birthday { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public int LibraryId { get; set; }
        public LibraryEntity Library { get; set; }

        public int TakeBookId { get; set; }
        public TakeBookEntity TakeBook { get; set; }


    }
}
