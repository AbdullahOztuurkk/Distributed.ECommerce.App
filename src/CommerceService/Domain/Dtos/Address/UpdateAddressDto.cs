namespace CommerceService.Domain.Dtos.Address;

public class UpdateAddressDto
{
    public int Id { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
}
