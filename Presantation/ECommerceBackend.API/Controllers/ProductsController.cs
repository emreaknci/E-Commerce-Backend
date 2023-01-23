using System.Net;
using ECommerceBackend.Application.Consts;
using ECommerceBackend.Application.CustomAttributes;
using ECommerceBackend.Application.Enums;
using ECommerceBackend.Application.Features.Commands.Product.CreateProduct;
using ECommerceBackend.Application.Features.Commands.Product.RemoveProduct;
using ECommerceBackend.Application.Features.Commands.Product.UpdateProduct;
using ECommerceBackend.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage;
using ECommerceBackend.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ECommerceBackend.Application.Features.Commands.ProductImageFile.UploadProductImage;
using ECommerceBackend.Application.Features.Queries.Product.GetAllProduct;
using ECommerceBackend.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceBackend.Application.Features.Queries.ProductImageFile.GetProductImage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ECommerceBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : BaseController
    {

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest request)
        {
            //Thread.Sleep(750);
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Create Product")]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {

            var response = await Mediator!.Send(request);
            return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Update Product")]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            var response = await Mediator!.Send(updateProductCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Delete Product")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await Mediator!.Send(removeProductCommandRequest);
            return Ok();
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Writing, Definition = "Upload Product File")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest request)
        {
            request.Files = Request.Form.Files;
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Reading, Definition = "Get Products Images")]
        public async Task<IActionResult> GetImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            var response = await Mediator!.Send(getProductImagesQueryRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Deleting, Definition = "Delete Product Image")]
        public async Task<IActionResult> DeleteImage([FromRoute] RemoveProductImageCommandRequest request, [FromQuery] string imageId)
        {
            request.ImageId = imageId;
            var response = await Mediator!.Send(request);
            return Ok();
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Products, ActionType = ActionType.Updating, Definition = "Change Showcase Image")]
        public async Task<IActionResult> ChangeShowcaseImage([FromQuery] ChangeShowcaseImageCommandRequest changeShowcaseImageCommandRequest)
        {
            var response = await Mediator!.Send(changeShowcaseImageCommandRequest);
            return Ok(response);
        }
    }
}
