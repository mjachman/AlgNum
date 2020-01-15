using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALS.Entities { 
    public class Review
    {
        public int row;
        public int column;
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public double Rating { get; set; }

        public Review(string productId, string customerId, double rating)
        {
            ProductId = productId;
            CustomerId = customerId;
            Rating = rating;
        }
        public Review(int row,int column)
        {
            this.row= row;
            this.column=column;
            
        }
    }
}