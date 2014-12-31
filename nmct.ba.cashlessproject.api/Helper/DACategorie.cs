using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Helper
{
    public class DACategorie
    {
        private const string CONNECTIONSTRING = "KlantConnection";
        public static List<Products> GetProducts(int id)
        {
            List<Products> pr = new List<Products>();
            string sql = "SELECT products.[Id] ,[ProductName] ,[Price] ,[Stock] , Category.category FROM [Klant].[dbo].[Products] inner join Klant.dbo.Product_Category on Products.Id = Product_Category.ProductId inner join klant.dbo.Category on Product_Category.CategoryId = Category.id where Category.category=@category";
            DbParameter par1;
            if (id == 1)
            {
                 par1 = Database.AddParameter(CONNECTIONSTRING, "@category", "Drank");
            }
            else
            {
                par1 = Database.AddParameter(CONNECTIONSTRING, "@category", "Snack");
            }
           

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);
            while (reader.Read())
            {
                pr.Add(Create(reader));
            }
            reader.Close();
            return pr;
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
    }
}