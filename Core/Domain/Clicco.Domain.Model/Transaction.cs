﻿using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class Transaction : BaseEntity, ISoftDeletable
    {
        public string Code { get; set; }
        public int TotalAmount { get; set; }
        public string Dealer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public TransactionStatus TransactionStatus { get; set; } = TransactionStatus.Pending;
        public bool IsDeleted { get; set; } = false;

        //Relationship
        public User User { get; set; }
        public int UserId { get; set; }
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public Coupon Coupon { get; set; }
        public int CouponId { get; set; }
    }
}
