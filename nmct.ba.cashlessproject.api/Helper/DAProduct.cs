using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Helper
{
    public class DAProduct
    {
        private const string CONNECTIONSTRING = "KlantConnection";
        public static List<Products> GetProducts()
        {
            List<Products> list = new List<Products>();
            string sql = "SELECT Products.[Id] ,[ProductName],[Price],[Stock], category FROM [Klant].[dbo].[Products] inner join Klant.dbo.Product_Category on Products.id = Product_Category.ProductId inner join Klant.dbo.Category on Product_Category.CategoryId  = Category.id;";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);
            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;

        }
        public static Products GetProducts(int id)
        {
            Products pr = new Products();
            string sql = "SELECT [Id],[ProductName],[Price],[Category],[Stock] FROM [Klant].[dbo].[Products] where Id=@id";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@id", id);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql,par1);
            while (reader.Read())
            {
                pr =Create(reader);
            }
            reader.Close();
            return pr;
        }
        public static void DeleteProduct(int id)
        {
            string sql = "DELETE FROM [Klant].[dbo].[Products] WHERE ID =@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            Database.InsertData(CONNECTIONSTRING, sql, par1);

            string sql2 = "DELETE FROM [Klant].[dbo].[Product_Category] WHERE [ProductId] =@ID";
            DbParameter par21 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            Database.InsertData(CONNECTIONSTRING, sql2, par21);

        }
        private static Products Create(IDataRecord record)
        {
            return new Products()
            {
                Id = Int32.Parse(record["Id"].ToString()),
                ProductName = record["ProductName"].ToString(),
                Price = Convert.ToDouble(record["Price"].ToString()),
                Category = record["Category"].ToString(),
                Stock = Int32.Parse(record["Stock"].ToString())

            };
        }
        public static int UpdateProduct(Products product)
        {
            int rowsaffected = 0;

            int id = product.Id;
            string naam = product.ProductName;
            string category = product.Category;
            double price = product.Price;
            int stock = product.Stock;


            string sql = "UPDATE [Klant].[dbo].[Products] SET Price = @Price, ProductName = @ProductName, Stock = @Stock WHERE Id=@Id";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Price", price);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@ProductName", naam);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Id", id);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Category", category);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@Stock", stock);

            rowsaffected += Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par5);


            string sql2 = "UPDATE [Klant].[dbo].[Product_Category] SET CategoryId = @CategoryId WHERE ProductId=@Id";
            DbParameter par21 = Database.AddParameter(CONNECTIONSTRING, "@Id", id);
            DbParameter par22;
            if (category == "Drank")
            {
                par22 = Database.AddParameter(CONNECTIONSTRING, "@CategoryId", 1);
            }
            else
            {
                par22 = Database.AddParameter(CONNECTIONSTRING, "@CategoryId", 2);
            }
            rowsaffected += Database.ModifyData(CONNECTIONSTRING, sql2, par21, par22);
            return rowsaffected;
        }
        public static void AddNewProduct(Products product)
        {
            int id = product.Id;
            string naam = product.ProductName;
            string category = product.Category;
            double price = product.Price;
            int stock = product.Stock;


            string sql = "INSERT INTO [Klant].[dbo].[Products] VALUES(@ProductName, @Price, @Stock)";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Price", price);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@ProductName", naam);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@Stock", stock);
            Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par5);

            string sql2 = "insert into Klant.dbo.Product_Category values(@Category);";
            DbParameter par21 = Database.AddParameter(CONNECTIONSTRING, "@Id", id);
            DbParameter par23;
            if(category == "Drank"){
                par23 = Database.AddParameter(CONNECTIONSTRING, "@Category", 1);
            }
            else
            {
                 par23 = Database.AddParameter(CONNECTIONSTRING, "@Category", 2);
            }
            
            Database.InsertData(CONNECTIONSTRING, sql2, par23);

            
        }

        public static List<string> GetCategorie()
        {
            List<string> List = new List<string>();
            string sql = "SELECT distinct [Category] FROM [Klant].[dbo].[Products]";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);
            while (reader.Read())
            {
                List.Add(reader["Category"].ToString());
            }
            reader.Close();
            return List;
        }
    }
}