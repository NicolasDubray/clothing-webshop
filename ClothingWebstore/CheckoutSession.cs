using Entities;

namespace ClothingWebstore
{
    internal class CheckoutSession
    {
        public Cart Cart { get; set; } = new();
        public Customer? Customer { get; set; }
        public ShippingAddress? Address { get; set; }
        public Shipping? Shipping { get; set; }
        public Payment? Payment { get; set; }
    }
}
