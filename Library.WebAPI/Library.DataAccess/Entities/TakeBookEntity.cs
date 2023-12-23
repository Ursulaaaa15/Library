using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DataAccess.Entities
{
    [Table("takebooks")]
    public class TakeBookEntity : BaseEntity
    {
        public DateTime Taken { get; set; }
        public virtual ICollection<BookEntity> Books { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
