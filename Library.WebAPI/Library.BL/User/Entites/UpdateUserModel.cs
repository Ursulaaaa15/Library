namespace Library.BL.User.Entites
{
    public class UpdateUserModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }
        public int ReadingNumber { get; set; }

        public DateTime Birthday { get; set; }
    }
}
