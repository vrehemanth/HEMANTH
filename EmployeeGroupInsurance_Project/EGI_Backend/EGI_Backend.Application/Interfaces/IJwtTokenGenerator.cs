using EGI_Backend.Application.Contracts.Auth;
using EGI_Backend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        AuthResponse GenerateToken(User user);
    }
}
