using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonDb.Entities
{
    public class ProductCategory
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public ProductCategory(int bookId, int categoryId)
        {
            ProductId = bookId;
            CategoryId = categoryId;
        }

        public override bool Equals(object obj)
        {
            var category = obj as ProductCategory;
            return category != null &&
                   ProductId == category.ProductId &&
                   CategoryId == category.CategoryId;
        }
    }
}
