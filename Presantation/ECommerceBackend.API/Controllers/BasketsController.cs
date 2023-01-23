using ECommerceBackend.Application.Consts;
using ECommerceBackend.Application.CustomAttributes;
using ECommerceBackend.Application.Enums;
using ECommerceBackend.Application.Features.Commands.Basket.AddItemToBasket;
using ECommerceBackend.Application.Features.Commands.Basket.RemoveBasketItem;
using ECommerceBackend.Application.Features.Commands.Basket.UpdateQuantity;
using ECommerceBackend.Application.Features.Queries.Basket.GetBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")] 
    public class BasketsController : BaseController
    {

        [HttpGet]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets,ActionType = ActionType.Reading,Definition = "Get Basket Items")]
        public async Task<IActionResult> GetBasketItems([FromQuery] GetBasketItemsQueryRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Writing, Definition = "Add Item To Basket")]
        public async Task<IActionResult> AddItemToBasket(AddItemToBasketCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Updating, Definition = "Update Quantity")]
        public async Task<IActionResult> UpdateQuantity(UpdateQuantityCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpDelete("{BasketItemId}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Baskets, ActionType = ActionType.Deleting, Definition = "Remove Basket Item")]
        public async Task<IActionResult> RemoveBasketItem([FromRoute] RemoveBasketItemCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }
    }
}
