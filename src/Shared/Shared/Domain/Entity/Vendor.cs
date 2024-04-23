using System.ComponentModel;

namespace Shared.Domain.Entity;

public class Vendor
{
    [Description("#VENDOR_NAME#")]
    public string Name { get; set; }

    [Description("#VENDOR_EMAIL#")]
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Region { get; set; }

    [Description("#VENDOR_ADDRESS#")]
    public string Address { get; set; }
}