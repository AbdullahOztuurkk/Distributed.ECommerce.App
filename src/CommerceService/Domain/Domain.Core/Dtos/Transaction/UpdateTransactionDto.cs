﻿namespace CommerceService.Domain.Dtos.Transaction;

public class UpdateTransactionDto
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public int AddressId { get; set; }
}
