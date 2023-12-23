namespace Library.BL.User.Entites
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }
        public int ReadingNumber { get; set; }

        public DateTime Birthday { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
