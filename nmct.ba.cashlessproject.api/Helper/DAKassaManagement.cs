using nmct.ba.cashlessproject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.api.Helper
{
    public class DAKassaManagement
    {
        private const string CONNECTIONSTRING = "KlantConnection";
        public static List<RegistersKlant> GetKassasManagement()
        {
            List<RegistersKlant> list = new List<RegistersKlant>();
            string sql = "SELECT RegisterId,EmployeeId, EmployeeName, FromDate, UntilDate, RegisterName, Device FROM [Klant].[dbo].Register_Employee Inner join [Klant].[dbo].[Employee]	on Employee.Id = Register_Employee.EmployeeId INNER JOIN Klant.dbo.Registers ON Registers.Id = Register_Employee.RegisterId";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);
            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;

        }

        private static RegistersKlant Create(IDataRecord record)
        {
            return new RegistersKlant()
            {
                Id = Int32.Parse(record["RegisterId"].ToString()),
                Device = record["Device"].ToString(),
                RegisterName = record["RegisterName"].ToString(),
                From = Convert.ToDateTime(record["FromDate"].ToString()),
                Until = Convert.ToDateTime(record["UntilDate"].ToString()), 
                EmployeeName =  record["EmployeeName"].ToString(),
                EmployeeId = Int32.Parse(record["EmployeeId"].ToString())

                
            };
        }
        public static int UpdateAccount(RegistersKlant kassa)
        {
            int rowsaffected = 0;

            int id = kassa.Id;
            string medewerkerName = kassa.EmployeeName;
            string device = kassa.Device;
            DateTime from = kassa.From;
            string kassaName = kassa.RegisterName;
            DateTime until = kassa.Until;
            int medewerkerid = kassa.EmployeeId;


            string sql = "UPDATE Registers SET CustomerName = @Name, Balance = @Balance, Address = @Address, Picture = @Picture WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@RegisterName", kassaName);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Device", device);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

            rowsaffected += Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3);

            string sql1 = "UPDATE [Klant].[dbo].[Register_Employee] SET EmployeeId = @EmployeeId, FromDate = @FromDate, UntilDate = @UntilDate WHERE RegisterId=@RegisterId";
            DbParameter par11 = Database.AddParameter(CONNECTIONSTRING, "@RegisterId", id);
            DbParameter par12 = Database.AddParameter(CONNECTIONSTRING, "@EmployeeId", medewerkerid);
            DbParameter par13 = Database.AddParameter(CONNECTIONSTRING, "@FromDate", from);
            DbParameter par14 = Database.AddParameter(CONNECTIONSTRING, "@UntilDate", until);



            rowsaffected += Database.ModifyData(CONNECTIONSTRING, sql1, par1, par2, par3);

            return rowsaffected;
        }
    }
}