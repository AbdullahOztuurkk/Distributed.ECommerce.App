namespace CommerceService.Domain.Dtos.Vendor;

public class VendorResponseDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Region { get; set; }
    public string? Address { get; set; }

    public VendorResponseDto Map(Concrete.Vendor vendor)
    {
        this.Id = vendor.Id;
        this.Name = vendor.Name;
        this.Email = vendor.Email;
        this.PhoneNumber = vendor.PhoneNumber;
        this.Region = vendor.Region;
        this.Address = vendor.Address;
        return this;
    }
}
