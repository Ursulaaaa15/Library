using System.ComponentModel.DataAnnotations.Schema;

namespace Library.DataAccess.Entities
{
    [Table("libraries")]
    public class LibraryEntity : BaseEntity
    {
        public string Title { get; set; } //название библиотеки
        public string Address { get; set; } //адрес

        public virtual ICollection<UserEntity> Users { get; set; }
    }
}
