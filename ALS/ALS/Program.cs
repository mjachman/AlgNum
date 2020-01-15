using ALS.Entities;
using System;
using System.Configuration;
using System.Collections.Generic;
using ServiceStack.OrmLite;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Linq;
using MathNet.Numerics.Statistics;

namespace ALS
{
    public class Program
    {



        static void Main(string[] args)
        {
            Random rnd = new Random();

            string Sql1 = @"select * from SelectedReviews";
         



            for (int d = 1; d < 15; d++)
            {
                var generator = new DbUtil();
                var Ratings = generator.GenerateRatingMatrix(Sql1, 4, d, 0.1, 100);
                var acc = generator.Check();
                Console.WriteLine($"{d}  {acc.accuracy} {acc.TimeSpan}");

            }

            Console.ReadKey();
            
        }
    }
    }
    
