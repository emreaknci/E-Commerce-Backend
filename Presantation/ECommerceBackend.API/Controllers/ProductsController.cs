using System.Net;
using ECommerceBackend.Application.Abstractions.Storage;
using ECommerceBackend.Application.Features.Commands.CreateProduct;
using ECommerceBackend.Application.Features.Queries.GetAllProduct;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Application.Repositories.File;
using ECommerceBackend.Application.RequestParameters;

using ECommerceBackend.Application.ViewModels.Products;
using ECommerceBackend.Domain.Entities.Concrete;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        private readonly IStorageService _storageService;

        private readonly IConfiguration _configuration;


        private readonly IMediator _mediator;
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _storageService = storageService;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var item = await _productReadRepository.GetByIdAsync(id, false);
            return Ok(item);
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
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            var item = await _productReadRepository.GetByIdAsync(model.Id);
            item.Name = model.Name;
            item.UnitInStock = model.UnitInStock;
            item.Price = model.Price;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.Remove(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            var results = await _storageService.UploadAsync("photo-images", Request.Form.Files);
            var product = await _productReadRepository.GetByIdAsync(id);

            await _productImageFileWriteRepository.AddRangeAsync(results.Select(p => new ProductImageFile()
            {
                FileName = p.fileName,
                Path = p.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetImages(string id)
        {
            var product = await _productReadRepository.Table.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            return Ok(product!.Images.Select(p => new
            {
                Path = $"{_configuration["Storage:Azure:Url"]}/{p.Path}",
                p.FileName,
                p.Id
            }).ToList());
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteImage(string id, string imageId)
        {
            var product = await _productReadRepository.Table.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            var image = product?.Images.FirstOrDefault(p => p.Id == Guid.Parse(imageId));

            await _storageService.DeleteAsync(image!.Path, image.FileName);
            //todo async oldugu icin imagei bulamıyor hata veriyor  


            await _fileWriteRepository.Remove(imageId);
            product?.Images.Remove(image!);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
