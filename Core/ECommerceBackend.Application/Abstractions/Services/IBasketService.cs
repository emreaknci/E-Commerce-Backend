using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.ViewModels.Baskets;
using ECommerceBackend.Domain.Entities.Concrete;

namespace ECommerceBackend.Application.Abstractions.Services
{
    public interface IBasketService
    {
        public Task<List<BasketItem>> GetBasketItemsAsync();
        public Task AddItemToBasketAsync(VM_Create_BasketItem basketItem);
        public Task UpdateQuantityAsync(VM_Update_BasketItem basketItem);
        public Task RemoveBasketItemAsync(string basketItemId);
        public Basket? GetUserActiveBasket { get; }
    }
}
