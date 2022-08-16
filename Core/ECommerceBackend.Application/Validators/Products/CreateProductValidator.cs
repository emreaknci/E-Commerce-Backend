using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerceBackend.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotNull().NotEmpty().WithMessage("Lütfen ürün adını boş geçmeyiniz.")
                .MaximumLength(150).WithMessage("Lütfen 150 karakterden daha az bir ürün adı giriniz.")
                .MinimumLength(5).WithMessage("Lütfen 5 karakterden daha uzun bir ürün adı giriniz.");
            RuleFor(p => p.UnitInStock)
                .NotNull().NotEmpty().WithMessage("Lütfen stok adedini giriniz.")
                .Must(stock => stock > 0).WithMessage("Eklenecek ürünün stok adedi en az 1 olmalıdır.");
            RuleFor(p => p.Price)
                .NotNull().NotEmpty().WithMessage("Lütfen ürün fiyatını giriniz.")
                .Must(price => price > 0).WithMessage("Eklenecek ürünün fiyatı 0'dan büyük olmalıdır.");
        }
    }
}
