using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Helper
{
    public class DASales
    {
        private const string CONNECTIONSTRING = "KlantConnection";

        public static int UpdateProduct(Products product)
        {
            int rowsaffected = 0;
            int id = product.Id;
            int stock = product.Stock;
            int nieuweStock = stock - 1;

            string sql = "UPDATE klant.dbo.[Products] SET Stock = @Stock WHERE Id=@Id";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Stock", nieuweStock);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Id", id);

            rowsaffected += Database.ModifyData(CONNECTIONSTRING, sql, par1,par2);
            return rowsaffected;
        }

        public static void AddNewSale(Sales s)
        {
            double totalPrice = s.TotalPrice;
            int productId = s.ProductId;
            int registerId = s.RegisterId;
            int timeStamp = s.Timestamp;
            int amount = s.Amount;
            int customerId = s.CustomerId;

            string sql = "INSERT INTO klant.dbo.[Sales] ([TimeStamp] ,[CustomerId],[RegisterId],[ProductId] ,[Amount] ,[TotalPrice] ) VALUES(@timeStamp, @customerId, @registerId, @productId, @amount, @totalPrice)";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@totalPrice", totalPrice);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@productId", productId);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@registerId", registerId);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@timeStamp", timeStamp);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@amount", amount);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@customerId", customerId);

            Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par6,par5);
        }

        public static int UpdateAccount(Customers kl)
        {
            int rowsaffected = 0;

            int id = kl.Id;
            string name = kl.CustomerName;
            double balance = kl.Balance;
            byte[] picture = kl.Picture;
            string address = kl.Address;


            string sql = "UPDATE klant.dbo.Customers SET CustomerName = @Name, Balance = @Balance, Address = @Address, Picture = @Picture WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Name", name);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Balance", balance);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Picture", picture);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Address", address);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

            rowsaffected += Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5);

            return rowsaffected;
        }
    }
}