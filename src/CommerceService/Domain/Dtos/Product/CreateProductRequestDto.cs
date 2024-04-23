namespace CommerceService.Domain.Dtos.Product;

public class CreateProductRequestDto
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
    public int CategoryId { get; set; }
    public int VendorId { get; set; }

}