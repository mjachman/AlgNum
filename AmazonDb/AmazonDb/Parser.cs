using AmazonDb.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AmazonDb
{
    public class Range
    {
        int startIndex;
        int endIndex;

        public Range(int startIndex, int endIndex)
        {
            this.startIndex = startIndex;
            this.endIndex = endIndex;
        }
    }
    public static class Parser
    {
        public static List<int> indexes = new List<int>();
        public static void Parse(string filePath)
        {
           

           
            int i = 0;
            foreach (var line in File.ReadLines(filePath))
            {
                if (line=="")
                {
                    indexes.Add(i);

                   // Console.WriteLine(i);
                };
                i++;
               
            }

            
                foreach (var index in indexes)
                {
                    
                    string product = "";
                    int current = indexes.IndexOf(index);
                if (current < indexes.Count-1)
                {
                    int next = indexes[current + 1];
                    List<string> lines = File.ReadLines(filePath).Skip(current).Take(next - current).ToList();
                    foreach (var line in lines)
                    {
                        product += line + "\n";
                    }
                    Product.Parse(product);
                    Console.WriteLine("p: " + current);
                }
                }
               

            
            //var productList = fileContents
            //.Select(Product.Parse)
            //.ToList();

            

            

        }

    }
}
