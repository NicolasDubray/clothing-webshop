using System;
using System.Collections.Generic;
using System.Text;

namespace Entities;

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public bool OnSale { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public Brand Brand { get; set; }
    public Category Category { get; set; }
    public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
