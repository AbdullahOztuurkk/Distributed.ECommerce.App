﻿namespace CommerceService.Domain.Dtos.Menu;

public class UpdateMenuDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int CategoryId { get; set; }
}
