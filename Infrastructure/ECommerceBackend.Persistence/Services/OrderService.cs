using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.Abstractions.Services;
using ECommerceBackend.Application.DTOs.Order;
using ECommerceBackend.Application.Repositories;

namespace ECommerceBackend.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;


        public OrderService(IOrderWriteRepository orderWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
        }

        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrder.Address,
                Description = createOrder.Description,
                Id = Guid.Parse(createOrder.BasketId!)
            });
            await _orderWriteRepository.SaveAsync();
        }
    }
}
