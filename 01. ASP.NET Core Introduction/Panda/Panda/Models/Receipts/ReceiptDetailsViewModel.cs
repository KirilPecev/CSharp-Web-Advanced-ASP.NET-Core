namespace Panda.Models.Receipts
{
    public class ReceiptDetailsViewModel
    {
        public string Id { get; set; }

        public decimal Fee { get; set; }

        public string IssuedOn { get; set; }

        public string ShippingAddress { get; set; }

        public decimal Weight { get; set; }

        public string Description { get; set; }

        public string Recipient { get; set; }
    }
}
