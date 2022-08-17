using System.Net;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Application.RequestParameters;
using ECommerceBackend.Application.ViewModels.Products;
using ECommerceBackend.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var item = _productReadRepository.GetByIdAsync(id, false);
            return Ok(item);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            Thread.Sleep(750);
            var totalCount = _productReadRepository.GetAll(false).Count();
            var list = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).ToList();
            //    .Select(p=>new
            //{
            //    p.Id,
            //    p.Name,
            //    p.UnitInStock,
            //    p.Price,
            //});
            return Ok(new{
                list,
                totalCount
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {

            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                UnitInStock = model.UnitInStock
            });
            await _productWriteRepository.SaveAsync();
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
        public async Task<IActionResult> Upload()
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,"resource/product-images");
            Random rnd = new ();
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (IFormFile formFile in Request.Form.Files)
            {
                var fullPath = Path.Combine(uploadPath, $"{rnd.Next()}{Path.GetExtension(formFile.Name)}");
                await using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None,1024*1024,useAsync:false);
                await formFile.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }

            return Ok();
        }

    }
}
