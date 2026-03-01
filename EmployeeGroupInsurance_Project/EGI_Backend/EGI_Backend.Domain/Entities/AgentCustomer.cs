using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EGI_Backend.Domain.Entities
{
    public class AgentCustomer
    {
        [Key]
        public Guid Id { get; set; }

        public Guid AgentId { get; set; }
        public User Agent { get; set; }

        public Guid CorporateClientId { get; set; }
        public CorporateClient CorporateClient { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
