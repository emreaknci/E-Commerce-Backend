using System.Net;
using ECommerceBackend.Application.Features.Commands.Product.CreateProduct;
using ECommerceBackend.Application.Features.Commands.Product.RemoveProduct;
using ECommerceBackend.Application.Features.Commands.Product.UpdateProduct;
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
    [Authorize(AuthenticationSchemes = "Admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest request)
        {
            Thread.Sleep(750);
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {

            var response = await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);

        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            var response = await _mediator.Send(updateProductCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest request)
        {
            request.Files = Request.Form.Files;
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            var response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteImage([FromRoute] RemoveProductImageCommandRequest request, [FromQuery] string imageId)
        {
            //Ders sonrası not !
            //Burada RemoveProductImageCommandRequest sınıfı içerisindeki ImageId property'sini de 'FromQuery' attribute'u ile işaretleyebilirdik!

            request.ImageId = imageId;
            var response = await _mediator.Send(request);
            return Ok();
        }
    }
}
