using AmazonDb.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AmazonDb
{
    public static class Parser2
    {
        public static List<Product> Parse(string filePath)
        {
            var fileContents = Regex
           .Split(File.ReadAllText(filePath), "\r\n\r\n", RegexOptions.IgnoreCase);


            var productList = fileContents
        .Select(Product.Parse)
        .ToList();



            return productList;

        }

    }
}
