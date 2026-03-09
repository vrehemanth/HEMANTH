using EGI_Backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public UserStatus Status { get; set; }

        public bool MustChangePassword { get; set; } = false;

        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        public DateTime? LastLogin { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
