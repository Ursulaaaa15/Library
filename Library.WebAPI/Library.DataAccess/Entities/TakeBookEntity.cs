using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DataAccess.Entities
{
    [Table("takebooks")]
    public class TakeBookEntity : BaseEntity
    {
        public DateTime Refund { get; set; }
        public Boolean Overdue { get; set; }
        public virtual ICollection<BookEntity> Books { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
