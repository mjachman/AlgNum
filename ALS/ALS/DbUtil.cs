using ALS.Entities;
using MathNet.Numerics.Statistics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALS
{
    public  class DbUtil
    {
        public List<Review> TestData;
        public List<Review> TrainData;
        public List<Review> ResultData;
        public int percentage;
        public double[,] ratings;
        public double[,] hiddenRatings;
        public List<double[,]> iterSolved;
        public TestResult testResult;
        public int iterations;
        Random rnd;
        public DbUtil()
        {
            percentage = 0;
            rnd = new Random();
            TestData = new List<Review>();
            TrainData = new List<Review>();
            ResultData = new List<Review>();
            testResult = new TestResult();
            iterSolved = new List<double[,]>();
            
        }

        public double[,] GenerateRatingMatrix(string sqlString,int percentage, int d, double reg, int iter)
        {
            iterations = iter;
            this.percentage = percentage;
            testResult.d = d;
            testResult.reg = reg;
             List<Product> Products = new List<Product>();
             List<Customer> Customers = new List<Customer>();
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Products;Integrated Security=True;Pooling=False";



            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand command = new SqlCommand(sqlString, conn);
 

            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {                 
                    string UserId = reader["CustomerId"].ToString();
                    string productId = reader["ProductId"].ToString();
                    string rating = reader["rating"].ToString();

                    Review r = new Review(productId, UserId, int.Parse(rating));
                    Review t= new Review(productId, UserId, int.Parse(rating));
                    TestData.Add(r);
                    TrainData.Add(t);
             
                    Customer c = new Customer(UserId);
                    Product p = new Product(productId);

                    c.Add(Customers);
                    p.Add(Products);
        
                }
            }


           
            

            int i = 0;
            foreach (var c in Customers)
            {
                c.row = i;
                i++;
            }

            i = 0;
            foreach (var p in Products)
            {
                p.column = i;
                i++;
            }
            ratings = new double[Customers.Count, Products.Count];

            foreach (var t in TestData)
            {
                var customer = Customers.Find(x => x.ID == t.CustomerId);
                var product = Products.Find(x => x.ID == t.ProductId);

                int row = customer.row;
                int col = product.column;
                t.column = col;
                t.row = row;

                ratings[row, col] = t.Rating;

           

            }

            hiddenRatings = new double[Customers.Count, Products.Count];

            MakeTrainData();

            foreach (var r in TrainData)
            {
                var customer = Customers.Find(x => x.ID == r.CustomerId);
                var product = Products.Find(x => x.ID == r.ProductId);
               
                int row = customer.row;
                int col = product.column;
                r.column = col;
                r.row = row;
                hiddenRatings[row, col] = r.Rating;


            }
            

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

           double[,] solvedRatings= ALS.Solve(hiddenRatings, d, reg, iterations);
 
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine(ts);
            testResult.TimeSpan = ts;
            for (int a = 0; a < solvedRatings.GetLength(0); a++)
            {
                for (int b = 0; b < solvedRatings.GetLength(1); b++)
                {
                    var train = TestData.Find(x => x.row == a && x.column == b);
          

                    if (TestData.Contains(train))
                    {
                        Review rev = new Review(train.ProductId, train.CustomerId, solvedRatings[a, b]);
                        rev.row = a;
                        rev.column = b;
                        ResultData.Add(rev);
                    }
                }
            }

            return hiddenRatings;
            
        }
      
        public void MakeTrainData()
        {
 
            int removals = 0;
            double max = percentage;
            while (removals < percentage * 0.01 * TestData.Count)
            {
                var random = rnd.Next(0, TrainData.Count);

                TrainData[random].Rating = 0.0;

                removals++;
            }



        }
        public TestResult Check()
        {


            var Differences = new List<double>();
            foreach (var t in TrainData)
            {
                if (t.Rating==0.0)
                {
                    var result = ResultData.Find(x => x.row == t.row && x.column == t.column);
                    var test=TestData.Find(x => x.row == t.row && x.column == t.column);

                  
                       
                                double diff = Math.Pow(result.Rating - test.Rating, 2);
                                double diffpow = Math.Pow(diff, 2);

                                Console.WriteLine($"{result.Rating} | {test.Rating} | {diff}");
                                Differences.Add(diffpow);

                        

                }
            }
    
            testResult.accuracy= Math.Sqrt(Differences.Average());
            return testResult;
   
        }


    }
}
