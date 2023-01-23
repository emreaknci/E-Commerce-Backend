using ECommerceBackend.Application.Abstractions.Services.Configurations;
using ECommerceBackend.Application.CustomAttributes;
using ECommerceBackend.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ApplicationServicesController : BaseController
    {
        readonly IApplicationService _applicationService;
        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Authorize Definition Endpoints", Menu = "Application Services")]
        public IActionResult GetAuthorizeDefinitionEndpoints()
        {
            var datas = _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}
