using System.ComponentModel.DataAnnotations;

namespace Library.DataAccess.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; } //ключ к базе
        public Guid ExternalId { get; set; }
        public DateTime ModificationTime { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
