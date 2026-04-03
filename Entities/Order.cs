namespace Entities;

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int PaymentId { get; set; }
    public Payment Payment { get; set; }
    public int ShippingId { get; set; }
    public Shipping Shipping { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
