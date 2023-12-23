using Library.DataAccess.Entities;

namespace Library.WebAPI.Controllers.Entities.Users
{
    public class RegisretUserRecuest
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }

        public string Email { get; set; }
        public int ReadingNumber { get; set; }

        public DateTime Birthday { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public int LibraryId { get; set; }

        public int TakeBookId { get; set; }
    }
}
