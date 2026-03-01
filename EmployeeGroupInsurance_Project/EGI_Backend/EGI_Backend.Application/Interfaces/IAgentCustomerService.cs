using EGI_Backend.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IAgentCustomerService
    {
        Task CreateCustomerByAgentAsync(Guid agentId, AgentCreateCustomerDto dto);
        Task AssignLeastLoadedAgentAsync(Guid corporateClientId);
    }
}
