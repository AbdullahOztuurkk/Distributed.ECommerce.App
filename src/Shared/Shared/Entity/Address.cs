using System.ComponentModel;

namespace Shared.Entity;

public class Address
{
    [Description("#ADDRESS_CITY#")]
    public string City { get; set; }

    [Description("#ADDRESS_STREET#")]
    public string Street { get; set; }

    [Description("#ADDRESS_STATE#")]
    public string State { get; set; }

    [Description("#ADDRESS_COUNTRY#")]
    public string Country { get; set; }

    [Description("#ADDRESS_ZIPCODE#")]
    public string ZipCode { get; set; }
}