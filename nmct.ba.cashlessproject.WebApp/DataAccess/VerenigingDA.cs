using nmct.ba.cashlessproject.database;
using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

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

        //public static int UpdateVereniging(int id)
        //{
        //    int rowsaffected = 0;

        //    string sql = "UPDATE [IT bedrijf].[dbo].[Organisations]  SET [Login],[Password], [DbPassword] ,[DbName] ,[DbLogin],[OrganisationName] ,[Address] ,[Email] ,[Phone] WHERE ID=@ID";
        //    DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Name", name);
        //    DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Balance", balance);
        //    DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Picture", picture);
        //    DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Address", address);
        //    DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

        //    rowsaffected += Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5);

        //    return rowsaffected;
        //}
    }
}