namespace ECommerceBackend.Domain.Entities.Concrete;

public class ProductImageFile : File
{
    public bool ShowCase { get; set; }
    public virtual ICollection<Product> Products { get; set; } = null!;

}