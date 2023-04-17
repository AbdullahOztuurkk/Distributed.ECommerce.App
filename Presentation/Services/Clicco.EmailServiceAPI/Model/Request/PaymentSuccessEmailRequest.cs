﻿using static Clicco.EmailServiceAPI.Model.Common.Global;

namespace Clicco.EmailServiceAPI.Model.Request
{
    public class PaymentSuccessEmailRequest
    {
        public string To { get; set; }
        public string FullName { get; set; }
        public string OrderNumber { get; set; }
        public string Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string ProductName { get; set; }
    }
}