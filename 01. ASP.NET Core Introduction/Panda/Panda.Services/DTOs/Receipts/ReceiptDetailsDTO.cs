﻿namespace Panda.Services.DTOs.Receipts
{
    using System;

    public class ReceiptDetailsDTO
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public string ShippingAddress { get; set; }

        public decimal Weight { get; set; }

        public string Description { get; set; }

        public string Recipient { get; set; }
    }
}
