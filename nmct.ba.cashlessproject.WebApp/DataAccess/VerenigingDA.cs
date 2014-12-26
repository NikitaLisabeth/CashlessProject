using nmct.ba.cashlessproject.database;
using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace nmct.ba.cashlessproject.WebApp.DataAccess
{
    public class VerenigingDA
    {
        private const string CONNECTIONSTRING = "DefaultConnection";
        public static List<Organisations> getVerenigingen()
        {
            List<Organisations> list = new List<Organisations>();

            string sql = "SELECT [ID] ,[Login] ,[Password] ,[DbName] ,[DbLogin] ,[DbPassword] ,[OrganisationName] ,[Address] ,[Email] ,[Phone] FROM [IT bedrijf].[dbo].[Organisations]";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }
        public static Organisations getVerenigingenMetId(int id)
        {
            Organisations Org = new Organisations();

            string sql = "SELECT [ID],[Login],[Password], [DbPassword] ,[DbName] ,[DbLogin],[OrganisationName] ,[Address] ,[Email] ,[Phone] FROM [IT bedrijf].[dbo].[Organisations] where ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            while (reader.Read())
            {
                Org = Create(reader);
            }
            reader.Close();
            return Org;
        }
        private static Organisations Create(IDataRecord record)
        {
            return new Organisations()
            {
                Id = Int32.Parse(record["ID"].ToString()),
                Login = record["Login"].ToString(),
                Password = record["Password"].ToString(),
                DbName = record["DbName"].ToString(),
                DbLogin = record["DbLogin"].ToString(),
                DbPassword = record["DbPassword"].ToString(),
                Address = record["Address"].ToString(),
                Email = record["Email"].ToString(),
                OrganisationName = record["OrganisationName"].ToString(),
                Phone = record["Phone"].ToString()
            };
        }

        public static int UpdateVereniging(Organisations o)
        {
            int rowsaffected = 0;

            string sql = "UPDATE [IT bedrijf].[dbo].[Organisations]  SET [Login]=@Login, [Password]=@Password, [DbPassword]=@DbPassword, [DbName]=@DbName, [DbLogin]=@DbLogin, [OrganisationName]=@OrganisationName, [Address]=@Address, [Email]=@Email, [Phone]=@Phone WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login",o.Login);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", o.Password);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbPassword", o.DbPassword);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbName", o.DbName);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@ID", o.Id);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", o.DbLogin);
            DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", o.OrganisationName);
            DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Address", o.Address);
            DbParameter par9 = Database.AddParameter(CONNECTIONSTRING, "@Email", o.Email);
            DbParameter par10 = Database.AddParameter(CONNECTIONSTRING, "@Phone", o.Phone);


            rowsaffected += Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5,par6,par7,par8,par9,par10);

            return rowsaffected;
        }

        public static int AddOrganisation(Organisations o)
        {
            string sql = "INSERT INTO [IT bedrijf].[dbo].[Organisations] VALUES(@Login, @Password, @DbName, @DbLogin, @DbPassword, @OrganisationName, @Address, @Email, @Phone)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Login", o.Login);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Password", o.Password);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@DbName", o.DbName);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@DbLogin", o.DbLogin);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@DbPassword", o.DbPassword);
            DbParameter par6 = Database.AddParameter(CONNECTIONSTRING, "@OrganisationName", o.OrganisationName);
            DbParameter par7 = Database.AddParameter(CONNECTIONSTRING, "@Address", o.Address);
            DbParameter par8 = Database.AddParameter(CONNECTIONSTRING, "@Email", o.Email);
            DbParameter par9 = Database.AddParameter(CONNECTIONSTRING, "@Phone", o.Phone);
            CreateDatabase(o);
            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5, par6, par7, par8, par9);
        }

        private static void CreateDatabase(Organisations o)
        {
            // create the actual database
            string create = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/create.txt")); //only for the web
           // string create = File.ReadAllText(@"..\..\Data\create.txt"); // only for desktop
            string sql = create.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);
            foreach (string commandText in RemoveGo(sql))
            {
                Database.ModifyData(Database.GetConnection("AdminDB"), commandText);
            }

            // create login, user and tables
            DbTransaction trans = null;
            try
            {
                trans = Database.BeginTransaction("AdminDB");

                string fill = File.ReadAllText(HostingEnvironment.MapPath(@"~/App_Data/fill.txt")); // only for the web
                //string fill = File.ReadAllText(@"..\..\Data\fill.txt"); // only for desktop
                string sql2 = fill.Replace("@@DbName", o.DbName).Replace("@@DbLogin", o.DbLogin).Replace("@@DbPassword", o.DbPassword);

                foreach (string commandText in RemoveGo(sql2))
                {
                    Database.ModifyData(trans, commandText);
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Console.WriteLine(ex.Message);
            }
        }

        private static string[] RemoveGo(string input)
        {
            //split the script on "GO" commands
            string[] splitter = new string[] { "\r\nGO\r\n" };
            string[] commandTexts = input.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            return commandTexts;
        }

    }
}