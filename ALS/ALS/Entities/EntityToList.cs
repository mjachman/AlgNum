using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALS.Entities
{
    public static class EntityToList
    {
        public static void Add(this Product p,List<Product> ps )
        {
            if(!ps.Contains(p))
            {
                ps.Add(p);
            }
        }
        public static void Add(this Customer c, List<Customer> cs)
        {
            if (!cs.Contains(c))
            {
                cs.Add(c);
            }
        }
    }
}
