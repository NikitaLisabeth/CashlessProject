using nmct.ba.cashlessproject.database;
using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.WebApp.DataAccess
{
    public class KassaDA
    {
        private const string CONNECTIONSTRING = "DefaultConnection";
        public static List<RegistersManagement> getKassas()
        {
            List<RegistersManagement> list = new List<RegistersManagement>();

            string sql = "SELECT [ID],[RegisterName],[Device],[PurchaseDate] ,[ExpiresDate] FROM [IT bedrijf].[dbo].[Registers]";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;
        }
        public static RegistersManagement getKassasMetId(int id)
        {
            RegistersManagement reg = new RegistersManagement();

            string sql = "SELECT [ID],[RegisterName] ,[Device] ,[PurchaseDate]  ,[ExpiresDate] FROM [IT bedrijf].[dbo].[Registers] where ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par1);

            while (reader.Read())
            {
                reg = Create(reader);
            }
            reader.Close();
            return reg;
        }
        private static RegistersManagement Create(IDataRecord record)
        {
            return new RegistersManagement()
            {
                Id = Int32.Parse(record["ID"].ToString()),
                RegisterName = record["RegisterName"].ToString(),
                Device = record["Device"].ToString(),
                PurchaseDate = Convert.ToDateTime( record["PurchaseDate"].ToString()),
                ExpiresDate = Convert.ToDateTime(record["ExpiresDate"].ToString())
            };
        }
    }
}