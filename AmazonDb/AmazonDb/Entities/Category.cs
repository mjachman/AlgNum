using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AmazonDb.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static int globalCatID;

        public Category(string Name)
        {
            this.Name = Name;
            this.ID = Interlocked.Increment(ref globalCatID);
        }

        public override bool Equals(object obj)
        {
            var category = obj as Category;
            return category != null &&
                   Name == category.Name;
        }
    }
}
