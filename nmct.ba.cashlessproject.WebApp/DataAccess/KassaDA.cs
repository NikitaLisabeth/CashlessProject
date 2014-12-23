using nmct.ba.cashlessproject.database;
using nmct.ba.cashlessproject.Models;
using nmct.ba.cashlessproject.WebApp.PresentationModels;
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
        public static List<KassaPM> getKassas()
        {
            List<KassaPM> list = new List<KassaPM>();

            string sql = "SELECT Registers.[ID],[RegisterName],[Device],[PurchaseDate],[ExpiresDate], Organisations.OrganisationName, Organisation_Register.FromDate, Organisation_Register.UntilDate, Organisation_Register.OrganisationID, [Organisations].ID ,[Login],[Password],[DbName],[DbLogin],[DbPassword],[Address] ,[Email],[Phone]  , Organisation_Register.OrganisationID , Organisation_Register.RegisterID FROM [IT bedrijf].[dbo].[Registers] inner join [IT bedrijf].dbo.Organisation_Register on organisation_register.RegisterID = registers.ID inner join [IT bedrijf].dbo.Organisations on Organisation_Register.OrganisationID = Organisations.ID";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                RegistersManagement reg = new RegistersManagement()
                {
                    Id = Int32.Parse(reader["ID"].ToString()),
                    RegisterName = reader["RegisterName"].ToString(),
                    Device = reader["Device"].ToString(),
                    PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"].ToString()),
                    ExpiresDate = Convert.ToDateTime(reader["ExpiresDate"].ToString())
                };

                Organisations org = new Organisations()
                {
                    Id = Int32.Parse(reader["ID"].ToString()),
                    Login = reader["Login"].ToString(),
                    Password = reader["Password"].ToString(),
                    DbName = reader["DbName"].ToString(),
                    DbLogin = reader["DbLogin"].ToString(),
                    DbPassword = reader["DbPassword"].ToString(),
                    Address = reader["Address"].ToString(),
                    Email = reader["Email"].ToString(),
                    OrganisationName = reader["OrganisationName"].ToString(),
                    Phone = reader["Phone"].ToString()
                };
                Organisation_Register orgreg = new Organisation_Register()
                {
                    OrganisationID = Int32.Parse(reader["OrganisationID"].ToString()),
                    RegisterId = Int32.Parse(reader["RegisterId"].ToString()),
                    FromDate = DateTime.Parse(reader["FromDate"].ToString()),
                    UntilDate = DateTime.Parse(reader["UntilDate"].ToString())

                };

                KassaPM pm = new KassaPM();
                pm.Kassa = reg;
                pm.Organisatie = org;
                pm.Organisatie_Register = orgreg;

                list.Add(pm);
            }
            reader.Close();
            return list;
        }
        public static List<RegistersManagement> getKassasZonderVereniging()
        {
            List<RegistersManagement> list = new List<RegistersManagement>();

            string sql = "SELECT Registers.[ID],[RegisterName],[Device],[PurchaseDate],[ExpiresDate] FROM [IT bedrijf].[dbo].[Registers] WHERE NOT EXISTS (SELECT [OrganisationID] ,[RegisterID] ,[FromDate] ,[UntilDate]  FROM [IT bedrijf].dbo.Organisation_Register  WHERE [Registers].ID = Organisation_Register.RegisterID);";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
            {
                RegistersManagement reg = new RegistersManagement()
                {
                    Id = Int32.Parse(reader["ID"].ToString()),
                    RegisterName = reader["RegisterName"].ToString(),
                    Device = reader["Device"].ToString(),
                    PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"].ToString()),
                    ExpiresDate = Convert.ToDateTime(reader["ExpiresDate"].ToString())
                };
                list.Add(reg);
            }
            reader.Close();
            return list;
        }
        public static List<KassaPM> getKassasMetId(int id)
        {
            List<KassaPM> list = new List<KassaPM>();
            string sql = "SELECT Registers.[ID],[RegisterName],[Device],[PurchaseDate],[ExpiresDate], Organisations.OrganisationName, Organisation_Register.FromDate, Organisation_Register.UntilDate, Organisation_Register.OrganisationID, [Organisations].ID ,[Login],[Password],[DbName],[DbLogin],[DbPassword],[Address] ,[Email],[Phone]  , Organisation_Register.OrganisationID , Organisation_Register.RegisterID FROM [IT bedrijf].[dbo].[Registers] inner join [IT bedrijf].dbo.Organisation_Register on organisation_register.RegisterID = registers.ID inner join [IT bedrijf].dbo.Organisations on Organisation_Register.OrganisationID = Organisations.ID where Organisations.ID =@Id";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Id", id);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql,par1);

            while (reader.Read())
            {
                KassaPM pm = new KassaPM();
                RegistersManagement reg = new RegistersManagement()
                {
                    Id = Int32.Parse(reader["ID"].ToString()),
                    RegisterName = reader["RegisterName"].ToString(),
                    Device = reader["Device"].ToString(),
                    PurchaseDate = Convert.ToDateTime(reader["PurchaseDate"].ToString()),
                    ExpiresDate = Convert.ToDateTime(reader["ExpiresDate"].ToString())
                };

                Organisations org = new Organisations()
                {
                    Id = Int32.Parse(reader["ID"].ToString()),
                    Login = reader["Login"].ToString(),
                    Password = reader["Password"].ToString(),
                    DbName = reader["DbName"].ToString(),
                    DbLogin = reader["DbLogin"].ToString(),
                    DbPassword = reader["DbPassword"].ToString(),
                    Address = reader["Address"].ToString(),
                    Email = reader["Email"].ToString(),
                    OrganisationName = reader["OrganisationName"].ToString(),
                    Phone = reader["Phone"].ToString()
                };
                Organisation_Register orgreg = new Organisation_Register()
                {
                    OrganisationID = Int32.Parse(reader["OrganisationID"].ToString()),
                    RegisterId = Int32.Parse(reader["RegisterId"].ToString()),
                    FromDate = DateTime.Parse(reader["FromDate"].ToString()),
                    UntilDate = DateTime.Parse(reader["UntilDate"].ToString())
                };

                pm.Kassa = reg;
                pm.Organisatie = org;
                pm.Organisatie_Register = orgreg;
                list.Add(pm);
            }
            reader.Close();
            return list;
        }
        private static RegistersManagement CreateKassa(IDataRecord record)
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
        public static int AddRegister(RegistersManagement Register)
        {
            string sql = "INSERT INTO [IT bedrijf].[dbo].[Registers] VALUES(@RegisterName, @Device, @PurchaseDate, @ExpiresDate)";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterName", Register.RegisterName);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Device", Register.Device);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@PurchaseDate", Register.PurchaseDate);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@ExpiresDate", Register.ExpiresDate);
            return Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4);
        }
    }
}