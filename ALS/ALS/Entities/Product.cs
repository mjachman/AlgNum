using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALS.Entities
{
    public class Product
    {
        public int column;
        public string ID;
        public string Name;

        public Product(string iD)
        {
            ID = iD;
        }


        public override bool Equals(object obj)
        {
            var product = obj as Product;
            return product != null &&  ID == product.ID;
                 
        }
        
    }
}
