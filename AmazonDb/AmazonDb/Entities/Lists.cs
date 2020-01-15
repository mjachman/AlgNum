using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonDb.Entities
{
    public static class Lists
    {
        public static List<Product> Products = new List<Product>();
        public static List<Review> Reviews = new List<Review>();
        public static List<string> Users = new List<string>(new string[5000000]);

        public static List<string> Categories = new List<string>(new string[2510000]);

        public static List<ProductCategory> ProductCategories = new List<ProductCategory>();


    }
}
