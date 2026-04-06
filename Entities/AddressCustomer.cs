using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class AddressCustomer
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public Address Adress { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
