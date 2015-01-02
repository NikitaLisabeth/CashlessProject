using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Helper
{
    public class DAKlantUI
    {
        private const string CONNECTIONSTRING = "KlantConnection";
        public static void AddNewCustomer(Customers kl)
        {
            int id = kl.Id;
            string name = kl.CustomerName;
            byte[] picture = kl.Picture;
            double balance = kl.Balance;
            string address = kl.Address;
            string sex = kl.Sex;
            DateTime birthDate = kl.BirthDate;
            string kaartNummer = kl.KaartNummer;

            string sql = "INSERT INTO [Klant].[dbo].[Customers]([CustomerName],[Address] ,[Balance],[Picture] ,[Sex] ,[BirthDate], [KaartNummer]) VALUES(@CustomerName, @Address, @Balance, @Picture, @Sex, @BirthDate, @KaartNummer)";

            //DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@CustomerName", name);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Picture", picture);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Balance", balance);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@Address", address);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@Sex", sex);
            DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@BirthDate", birthDate);
            DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@KaartNummer", kaartNummer);



            Database.InsertData(CONNECTIONSTRING, sql, par2, par3, par4, par5, par6, par7,par8);
        }

        public static Customers GetCustomer(string kaartNummer)
        {
            string sql = "SELECT [Id],[CustomerName],[Address],[Balance] ,[Picture],[Sex],[BirthDate],[KaartNummer] FROM [Klant].[dbo].[Customers]  WHERE KaartNummer=@KaartNummer";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@KaartNummer", kaartNummer);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            Customers c = new Customers();
            if (reader.Read())
            {
                c.Id = Convert.ToInt32(reader["Id"]);
                c.CustomerName = reader["CustomerName"].ToString();
                c.BirthDate = DateTime.Parse(reader["BirthDate"].ToString());
                c.Sex = reader["Sex"].ToString();
                c.Address = reader["Address"].ToString();
                c.KaartNummer = reader["KaartNummer"].ToString(); ;
                if (!DBNull.Value.Equals(reader["Picture"]))
                    c.Picture = (byte[])reader["Picture"];
                else
                    c.Picture = new byte[0];
                c.Balance = Double.Parse(reader["Balance"].ToString());
            }
            return c;
        }

        public static int UpdateAccount(Customers kl)
        {
            int rowsaffected = 0;
            int id = kl.Id;
            double balance = kl.Balance;

            string sql = "UPDATE Customers SET Balance = @Balance WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Balance", balance);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

            rowsaffected += Database.ModifyData(CONNECTIONSTRING, sql, par1, par2);

            return rowsaffected;
        }
    }
}