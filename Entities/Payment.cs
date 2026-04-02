using Entities.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities;

public class Payment
{
    public int Id { get; set; }
    public PaymentType Type { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
