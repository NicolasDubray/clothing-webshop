using System;
using System.Collections.Generic;
using System.Text;

namespace Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public ICollection<AddressCustomer> Addresses { get; set; } = new List<AddressCustomer>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
