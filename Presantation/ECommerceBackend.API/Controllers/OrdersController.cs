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
        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery]GetAllOrdersQueryRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }
    }
}
