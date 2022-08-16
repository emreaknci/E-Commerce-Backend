namespace ECommerceBackend.Application.ViewModels.Products;

public class VM_Update_Product
{
    public string Id { get; set; }
    public string Name { get; set; } = null!;
    public int UnitInStock { get; set; }
    public decimal Price { get; set; }
}