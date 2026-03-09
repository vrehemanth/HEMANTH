
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EGI_Backend.Domain.Entities
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Message { get; set; }
        public string Type { get; set; } // Info, Success, Warning, Error
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
