using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Domain.Entities
{
    public class IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int Status { get; set; } = 1;
    }
}
