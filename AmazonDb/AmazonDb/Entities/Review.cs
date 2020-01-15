using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonDb.Entities
{
    public class Review
    {
        public int ProductId { get; set; }
        public string CustomerId { get; set; }
        public int Rating { get; set; }

        public Review(int productId, string customerId, int rating)
        {
            ProductId = productId;
            CustomerId = customerId;
            Rating = rating;
        }
    }
}
