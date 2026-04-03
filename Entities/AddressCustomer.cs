using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class AddressCustomer
    {
        public int AddressesId { get; set; }
        public Address Adress { get; set; }
        public int CustomersId { get; set; }
        public Customer Customer { get; set; }
    }
}
