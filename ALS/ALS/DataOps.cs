using ALS.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALS
{
   public  class DataOps
    {
        public List<Customer> Customers;
        public List<Product> Products;
        public List<Review> Reviews;
        public void GetData(string SqlString)
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Products;Integrated Security=True;Pooling=False";



            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand command = new SqlCommand(SqlString, conn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string UserId = reader["CustomerId"].ToString();
                    string productId = reader["ProductId"].ToString();
                    string rating = reader["rating"].ToString();

                    Review r = new Review(productId, UserId, int.Parse(rating));
                    Review t = new Review(productId, UserId, int.Parse(rating));
                    Reviews.Add(r);
                    Customer c = new Customer(UserId);
                    Product p = new Product(productId);

                    c.Add(Customers);
                    p.Add(Products);

                }
            }
        }

    }
}
