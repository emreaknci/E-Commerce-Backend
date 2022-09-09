using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;

        public TestController(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> TestData()
        {
            var priceAndStock = 0;
            var index = 0;
            var name = "Product " + index;
            List<Product> dizi = new();
            for (int i = 0; i < 1000; i++)
            {
                priceAndStock += 12;
                index++;
                name = "Product " + index;
                Product product = new() { Name = name, Price = priceAndStock, UnitInStock = priceAndStock };
                dizi.Add(product);
            }
            await _productWriteRepository.AddRangeAsync(dizi);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
