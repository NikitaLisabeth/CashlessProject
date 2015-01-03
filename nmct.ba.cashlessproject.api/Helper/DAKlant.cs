using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using nmct.ba.cashlessproject.Models;
using System.Text;
using System.Configuration;
using System.Security.Claims;

namespace nmct.ba.cashlessproject.api.Helper
{
    public class DAKlant
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
        public static List<Customers> GetKlanten(IEnumerable<Claim> claims)
        {
            List<Customers> list = new List<Customers>();
            string sql = "SELECT[Id],[CustomerName],[Address],[Balance],[Picture], [Sex], [BirthDate] FROM [Customers]";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }
        public static Customers GetKlantenByID(int id)
        {
            Customers c = new Customers();
            string sql = "SELECT [Id],[CustomerName],[Address],[Balance],[Picture], [Sex], [BirthDate] FROM [Klant].[dbo].[Customers] where Id = @ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);
            while (reader.Read())
            {
                c = Create(reader);
            }
            reader.Close();
            return c;
        }

        private static Customers Create(IDataRecord record)
        {
            Customers c = new Customers();
            c.Id = Int32.Parse(record["Id"].ToString());
            c.CustomerName = record["CustomerName"].ToString();
            if (!DBNull.Value.Equals(record["BirthDate"]))
                c.BirthDate = Convert.ToDateTime(record["BirthDate"]);
            else
                c.BirthDate = DateTime.Today;
                    
            c.Balance = Convert.ToInt32(record["Balance"].ToString());

            if (!DBNull.Value.Equals(record["Sex"]))
                c.Sex = record["Sex"].ToString();
            else
                c.Sex = "Niet bekend";
            
            c.Address = record["Address"].ToString();
            if (!DBNull.Value.Equals(record["Picture"]))
                c.Picture = (byte[])record["Picture"];
            else
                c.Picture = new byte[0];
            return c;
            //return new Customers()
            //{
            //    Id = Int32.Parse(record["Id"].ToString()),
            //    CustomerName = record["CustomerName"].ToString(),
            //    Address = record["Address"].ToString(),
            //    Balance = Convert.ToInt32(record["Balance"].ToString()),
            //    //
            //    Picture = Encoding.ASCII.GetBytes(record["Picture"].ToString()).ToArray(),
            //    Sex = record["Sex"].ToString(),
            //    BirthDate = Convert.ToDateTime( record["BirthDate"])
            //};
        }

        public static int UpdateAccount(Customers kl, IEnumerable<Claim> claims)
        {
            int rowsaffected = 0;

            int id = kl.Id;
            string name = kl.CustomerName;
            double balance = kl.Balance;
            byte[] picture = kl.Picture;
            string address = kl.Address;
            

            string sql = "UPDATE Customers SET CustomerName = @Name, Balance = @Balance, Address = @Address, Picture = @Picture WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@Name", name);
           // DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Name", name);
            DbParameter par2 = Database.AddParameter(CreateConnectionString(claims), "@Balance", balance);
            DbParameter par3 = Database.AddParameter(CreateConnectionString(claims), "@Picture", picture);
            DbParameter par4 = Database.AddParameter(CreateConnectionString(claims), "@Address", address);
            DbParameter par5 = Database.AddParameter(CreateConnectionString(claims), "@ID", id);

            rowsaffected += Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5);

            return rowsaffected;
        }

        public static void AddNewCustomer(Customers kl, IEnumerable<Claim> claims)
        {
            int id = kl.Id;
            string name = kl.CustomerName;
            string picture = "foto";
            double balance = kl.Balance;
            string address = kl.Address;

            string sql = "INSERT INTO Customers VALUES(@CustomerName, @Picture, @Balance, @Address)";

            //DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            DbParameter par2 = Database.AddParameter(CreateConnectionString(claims), "@CustomerName", name);
            DbParameter par3 = Database.AddParameter(CreateConnectionString(claims), "@Picture", picture);
            DbParameter par4 = Database.AddParameter(CreateConnectionString(claims), "@Balance", balance);
            DbParameter par5 = Database.AddParameter(CreateConnectionString(claims), "@Address", address);

            Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par2, par3, par4, par5);
        }
    }
}