using ECommerceBackend.Application.Abstractions.Services;
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
        private readonly IMailService _mailService;

        public TestController(IProductWriteRepository productWriteRepository, IMailService mailService)
        {
            _productWriteRepository = productWriteRepository;
            _mailService = mailService;
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

        [HttpGet]
        public async Task<IActionResult> MailTest()
        {
            await _mailService.SendMailAsync("emreakinci696@gmail.com", "Örnek Mail",
                "<strong> Bu bir </strong> deneme mailidir.");
            return Ok();
        }
    }
}
