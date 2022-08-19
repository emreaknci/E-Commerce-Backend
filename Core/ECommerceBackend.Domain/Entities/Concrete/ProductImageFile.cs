namespace ECommerceBackend.Domain.Entities.Concrete;

public class ProductImageFile:File
{
    public virtual ICollection<Product> Products { get; set; } = null!;

}