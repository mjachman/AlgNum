using AmazonDb.Entities;
using System;
using System.Configuration;
using System.Data.Common;
using ServiceStack.OrmLite;

namespace AmazonDb
{
    class Program
    {
        static void Main(string[] args)
        {


            string provider = ConfigurationManager.AppSettings["provider"];

            string connectionString = ConfigurationManager.AppSettings["connectionString"];



            var dbFactory = new OrmLiteConnectionFactory(
            ConfigurationManager.AppSettings["connectionString"],
            SqlServerDialect.Provider);


            //            using (var db = dbFactory.Open())
            //            {

            //                //db.DropAndCreateTable<User>();
            //                db.DropAndCreateTable<Product>();
            //                db.DropAndCreateTable<Review>();
            //                db.DropAndCreateTable<Category>();
            //                db.DropAndCreateTable<ProductCategory>();
            //                db.ExecuteSql("drop table [dbo.User]");
            //                db.ExecuteSql("CREATE TABLE [dbo.User](ID NVARCHAR(15))");
            //                db.ExecuteSql(@"DECLARE @table NVARCHAR(512), @sql NVARCHAR(MAX);

            //SELECT @table = N'dbo.Review';

            //SELECT @sql = 'ALTER TABLE ' + @table 
            //    + ' DROP CONSTRAINT ' + name + ';'
            //    FROM sys.key_constraints
            //    WHERE [type] = 'PK'
            //    AND [parent_object_id] = OBJECT_ID(@table);

            //EXEC sp_executeSQL @sql;");

            //                db.ExecuteSql(@"DECLARE @table NVARCHAR(512), @sql NVARCHAR(MAX);

            //SELECT @table = N'dbo.ProductCategory';

            //SELECT @sql = 'ALTER TABLE ' + @table 
            //    + ' DROP CONSTRAINT ' + name + ';'
            //    FROM sys.key_constraints
            //    WHERE [type] = 'PK'
            //    AND [parent_object_id] = OBJECT_ID(@table);

            //EXEC sp_executeSQL @sql;");

            //                db.ExecuteSql(@"DECLARE @table NVARCHAR(512), @sql NVARCHAR(MAX);

            //SELECT @table = N'[dbo.User]'

            //SELECT @sql = 'ALTER TABLE ' + @table 
            //    + ' DROP CONSTRAINT ' + name + ';'
            //    FROM sys.key_constraints
            //    WHERE [type] = 'PK'
            //    AND [parent_object_id] = OBJECT_ID(@table);

            //EXEC sp_executeSQL @sql;");

            //                db.ExecuteSql(@"DECLARE @table NVARCHAR(512), @sql NVARCHAR(MAX);

            //SELECT @table = N'[dbo.Product]'

            //SELECT @sql = 'ALTER TABLE ' + @table 
            //    + ' DROP CONSTRAINT ' + name + ';'
            //    FROM sys.key_constraints
            //    WHERE [type] = 'PK'
            //    AND [parent_object_id] = OBJECT_ID(@table);

            //EXEC sp_executeSQL @sql;");
            Parser2.Parse(@"C:\Users\Administrator\Desktop\amazon-meta\xaa.txt");
            //Parser.Parse(@"C:\Users\Administrator\Desktop\trim.txt");
            Console.WriteLine(Lists.Products);
            Console.WriteLine(Lists.Users);
            Console.WriteLine(Lists.Reviews);
            Console.WriteLine(Lists.Categories);
            Console.WriteLine(Lists.ProductCategories);

           

        //}

            Console.WriteLine("Finito");
            Console.ReadKey();
        }
    }
}


