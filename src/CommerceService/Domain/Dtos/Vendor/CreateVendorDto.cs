﻿namespace CommerceService.Domain.Dtos.Vendor;

public class CreateVendorDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Region { get; set; }
    public string? Address { get; set; }
}
