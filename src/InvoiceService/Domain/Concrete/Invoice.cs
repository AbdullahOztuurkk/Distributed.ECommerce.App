﻿using Shared.Domain.Entity;

namespace InvoiceWorkerService.Domain.Concrete;

public class Invoice : MongoDbEntity
{
    public Transaction Transaction { get; set; }
    public Product Product { get; set; }
    public Vendor Vendor { get; set; }
    public Address Address { get; set; }
    public Coupon Coupon { get; set; }
    public string? BuyerEmail { get; set; }
}
