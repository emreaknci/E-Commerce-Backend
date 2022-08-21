using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginCommandRequest:IRequest<FacebookLoginCommandResponse>
    {
        public string AuthToken { get; set; }
    }
}
