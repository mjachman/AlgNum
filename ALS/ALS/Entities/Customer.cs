using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALS.Entities
{
    public class Customer
    {
        public int row;
        public string ID;

        public Customer(string iD)
        {
            ID = iD;
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null &&
                   //column == customer.column &&
                   ID == customer.ID;
        }
    }
}
