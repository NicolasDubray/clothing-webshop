using System;
using System.Collections.Generic;
using System.Text;

namespace Entities;

public class Shipping
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
