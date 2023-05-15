﻿using Clicco.Domain.Core;

namespace Clicco.Application.ViewModels
{
    public class CouponViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int? TypeId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
