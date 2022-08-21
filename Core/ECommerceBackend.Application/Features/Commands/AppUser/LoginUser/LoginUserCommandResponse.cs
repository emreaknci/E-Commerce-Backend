using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.DTOs;

namespace ECommerceBackend.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }

    }
}
