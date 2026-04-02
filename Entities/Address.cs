using System;
using System.Collections.Generic;
using System.Text;

namespace Entities;

public class Address
{
    public int Id { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
