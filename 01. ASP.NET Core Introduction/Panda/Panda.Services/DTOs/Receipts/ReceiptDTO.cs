namespace Panda.Services.DTOs.Receipts
{
    using System;

    public class ReceiptDTO
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public string Recipient { get; set; }
    }
}
