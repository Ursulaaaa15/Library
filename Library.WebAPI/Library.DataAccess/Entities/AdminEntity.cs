using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DataAccess.Entities
{
    [Table("admins")]
    public class AdminEntity : BaseEntity
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
