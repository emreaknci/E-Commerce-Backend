using ECommerceBackend.Application.Consts;
using ECommerceBackend.Application.CustomAttributes;
using ECommerceBackend.Application.Enums;
using ECommerceBackend.Application.Features.Commands.Order.CompleteOrder;
using ECommerceBackend.Application.Features.Commands.Order.CreateOrder;
using ECommerceBackend.Application.Features.Queries.Order.GetAllOrders;
using ECommerceBackend.Application.Features.Queries.Order.GetOrderById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class OrdersController : BaseController
    {
        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Writing, Definition = "Create Order")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get All Orders")]
        public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Reading, Definition = "Get Order By Id")]
        public async Task<ActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpGet("complete-order/{Id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Orders, ActionType = ActionType.Updating, Definition = "Complete Order")]
        public async Task<ActionResult> CompleteOrder([FromRoute] CompleteOrderCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }
    }
}
