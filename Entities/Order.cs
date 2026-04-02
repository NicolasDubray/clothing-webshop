using System;
using System.Collections.Generic;
using System.Text;

namespace Entities;

public class Order
{
    public int Id { get; set; }
    public string OrderNummer { get; set; }
    public Customer Customer { get; set; }
    public Payment Payment { get; set; }
    public Shipping Shipping { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
