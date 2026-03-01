using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGI_Backend.Application.Contracts.Auth
{
    public class ChangePasswordRequest
    {
        public string NewPassword { get; set; } = string.Empty;
    }
}
