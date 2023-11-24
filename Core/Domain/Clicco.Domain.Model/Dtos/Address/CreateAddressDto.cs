﻿namespace Clicco.Domain.Model.Dtos.Address
{
    public class CreateAddressDto
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
