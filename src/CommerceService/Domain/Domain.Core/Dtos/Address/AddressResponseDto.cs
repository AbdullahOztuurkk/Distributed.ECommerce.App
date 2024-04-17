namespace CommerceService.Domain.Dtos.Address;

public class AddressResponseDto
{
    public long Id { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }

    public AddressResponseDto Map(Concrete.Address address)
    {
        this.Id = address.Id;
        this.City = address.City;
        this.Street = address.Street;
        this.State = address.State;
        this.Country = address.Country;
        this.ZipCode = address.ZipCode;
        return this;
    }
}
