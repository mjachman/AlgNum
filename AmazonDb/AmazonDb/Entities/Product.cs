using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AmazonDb.Entities
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; } 

        public static Product Parse(string input)
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];



            var dbFactory = new OrmLiteConnectionFactory(
            ConfigurationManager.AppSettings["connectionString"],
            SqlServerDialect.Provider);

            using (var db = dbFactory.Open())
            {
                var result = new Product();
                var keyValueParts = input
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(kvp => kvp.Trim());

                foreach (var keyValuePart in keyValueParts)
                {
                    if (keyValuePart.StartsWith("id:",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        int id;
                        var value = keyValuePart.Substring("id:".Length).Trim();
                        if (int.TryParse(value, out id))
                        {
                            result.ID = id;
                        }

                    }
                    else if (keyValuePart.StartsWith("title:",
                    StringComparison.OrdinalIgnoreCase))
                    {
                        var value = keyValuePart.Substring("title:".Length).Trim();
                        result.Name = value;

                    }
                
                    else if (keyValuePart.StartsWith("|",
                  StringComparison.OrdinalIgnoreCase))
                    {
                        Category category = new Category(keyValuePart);
                        
                            db.Insert(category);
                        
                        
                        
                        ProductCategory productCategory = new ProductCategory(result.ID, category.ID);
                        db.Insert(productCategory);

                    }

                    else if (keyValuePart.Contains("cutomer:"))
                    {
                        Regex regex = new Regex("cutomer:(.*)rating:(.*)votes:");
                        var v = regex.Match(keyValuePart);
                        string customer = v.Groups[1].ToString().Trim();
                        string rating = v.Groups[2].ToString().Trim();

                        int r = int.Parse(rating);
                        User User = new User(customer);

                        Review review = new Review(result.ID, customer, r);
                        //Lists.Users.Add(User.ID);

                        db.ExecuteSql($"INSERT INTO [dbo.User] (ID) values ('{User.ID}'); ");
                        
                        

                        db.Insert(review);
                    }

                    //else if (keyValuePart.Contains("cutomer:",

                    //StringComparison.OrdinalIgnoreCase))
                    //{
                    //    var customer = keyValuePart.Substring("cutomer:".Length).Trim();

                    //    Review review = new Review(result.ID,customer,3);

                    //    result.Reviews.Add(review);
                    //}

                }

                db.Insert(result);

                Console.WriteLine($"Product: {result.ID}");
                return result;
            }
        }
    }
}
