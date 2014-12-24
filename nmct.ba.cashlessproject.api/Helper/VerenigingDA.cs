using nmct.ba.cashlessproject.apicall;
using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Helper
{
    public class VerenigingDA
    {
        private const string CONNECTIONSTRING = "KlantConnection";

        public static Organisations CheckCredentials(string username, string password)
        {
            string un = Cryptography.Encrypt(username);
            string pw = Cryptography.Encrypt(password);
            string sql = "SELECT [ID],[Login],[Password],[DbName],[DbLogin],[DbPassword],[OrganisationName],[Address],[Email],[Phone] FROM [IT bedrijf].dbo.Organisations WHERE Login=@Login AND Password=@Password";
            DbParameter par1 = Database.AddParameter("ITBedrijfConnection", "@Login", username);
            DbParameter par2 = Database.AddParameter("ITBedrijfConnection", "@Password", password);
            //DbParameter par1 = Database.AddParameter("IT bedrijf", "@Login", Cryptography.Encrypt(username));
            //DbParameter par2 = Database.AddParameter("IT bedrijf", "@Password", Cryptography.Encrypt(password));
            try
            {
                //DbDataReader reader = Database.GetData(Database.GetConnection("AdminDB"), sql, par1, par2);
                DbDataReader reader = Database.GetData("ITBedrijfConnection", sql, par1, par2);
                reader.Read();
                return new Organisations()
                {
                    Id = Int32.Parse(reader["ID"].ToString()),
                    Login = reader["Login"].ToString(),
                    Password = reader["Password"].ToString(),
                    DbName = reader["DbName"].ToString(),
                    DbLogin = reader["DbLogin"].ToString(),
                    DbPassword = reader["DbPassword"].ToString(),
                    OrganisationName = reader["OrganisationName"].ToString(),
                    Address = reader["Address"].ToString(),
                    Email = reader["Email"].ToString(),
                    Phone = reader["Phone"].ToString()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}