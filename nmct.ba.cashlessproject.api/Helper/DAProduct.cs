using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace nmct.ba.cashlessproject.api.Helper
{
    public class DAProduct
    {
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;
            return Database.CreateConnectionString("System.Data.SqlClient", @"NIKITAPC", dbname, dblogin, dbpass);

            //return Database.CreateConnectionString("System.Data.SqlClient", @"NIKITAPC", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }
        private const string CONNECTIONSTRING = "KlantConnection";
        public static List<Products> GetProducts(IEnumerable<Claim> claims)
        {
            List<Products> list = new List<Products>();
            string sql = "SELECT Products.[Id] ,[ProductName],[Price],[Stock], category FROM [Products] inner join Product_Category on Products.id = Product_Category.ProductId inner join Category on Product_Category.CategoryId  = Category.id;";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;

        }
        public static Products GetProducts(int id, IEnumerable<Claim> claims)
        {
            Products pr = new Products();
            string sql = "SELECT [Id],[ProductName],[Price],[Category],[Stock] FROM [Products] where Id=@id";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@id", id);

            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par1);
            while (reader.Read())
            {
                pr =Create(reader);
            }
            reader.Close();
            return pr;
        }
        
        public static void DeleteProduct(int id, IEnumerable<Claim> claims)
        {
            string sql = "DELETE FROM [Products] WHERE ID =@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1);

            string sql2 = "DELETE FROM [Product_Category] WHERE [ProductId] =@ID";
            DbParameter par21 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql2, par21);

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
        public static int UpdateProduct(Products product, IEnumerable<Claim> claims)
        {
            int rowsaffected = 0;

            int id = product.Id;
            string naam = product.ProductName;
            string category = product.Category;
            double price = product.Price;
            int stock = product.Stock;


            string sql = "UPDATE [Products] SET Price = @Price, ProductName = @ProductName, Stock = @Stock WHERE Id=@Id";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Price", price);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@ProductName", naam);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Id", id);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Category", category);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@Stock", stock);

            rowsaffected += Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par5);


            string sql2 = "UPDATE [Product_Category] SET CategoryId = @CategoryId WHERE ProductId=@Id";
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
            rowsaffected += Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql2, par21, par22);
            return rowsaffected;
        }
        public static void AddNewProduct(Products product, IEnumerable<Claim> claims)
        {
            int id = product.Id;
            string naam = product.ProductName;
            string category = product.Category;
            double price = product.Price;
            int stock = product.Stock;

            string sql = "INSERT INTO [Products] VALUES(@ProductName, @Price, @Stock)";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Price", price);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@ProductName", naam);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@Stock", stock);
            Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par5);
            //Categorieen table wordt standaard ingevuld door het IT bedrijf
            string sql2 = "insert into Product_Category values(@Category);";
            DbParameter par21 = Database.AddParameter(CONNECTIONSTRING, "@Id", id);
            DbParameter par23;
            if(category == "Drank"){
                par23 = Database.AddParameter(CONNECTIONSTRING, "@Category", 1);
            }
            else
            {
                 par23 = Database.AddParameter(CONNECTIONSTRING, "@Category", 2);
            }

            Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql2, par23);            
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