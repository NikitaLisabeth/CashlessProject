using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Data;
using nmct.ba.cashlessproject.Models;
using System.Configuration;
using System.Security.Claims;
using nmct.ba.cashlessproject.apicall;

namespace nmct.ba.cashlessproject.api.Helper
{
    public class DAMedewerker
    {
        private const string CONNECTIONSTRING = "KlantConnection";
        private static ConnectionStringSettings CreateConnectionString(IEnumerable<Claim> claims)
        {
            string dblogin = claims.FirstOrDefault(c => c.Type == "dblogin").Value;
            string dbpass = claims.FirstOrDefault(c => c.Type == "dbpass").Value;
            string dbname = claims.FirstOrDefault(c => c.Type == "dbname").Value;
            return Database.CreateConnectionString("System.Data.SqlClient", @"NIKITAPC", dbname, dblogin, dbpass);

            //return Database.CreateConnectionString("System.Data.SqlClient", @"NIKITAPC", Cryptography.Decrypt(dbname), Cryptography.Decrypt(dblogin), Cryptography.Decrypt(dbpass));
        }
        public static List<Employee> GetEmployee(IEnumerable<Claim> claims)
        {
            List<Employee> list = new List<Employee>();
            string sql = "SELECT [Id],[EmployeeName],[Address],[Email],[Phone],[LoginCode] FROM [Employee]";
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql);
            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;

        }
        public static Employee GetEmployee(int id, IEnumerable<Claim> claims)
        {
            Employee emp = new Employee();
            string sql = "SELECT [Id],[EmployeeName],[Address],[Email],[Phone],[LoginCode] FROM [Employee] where Id=@id";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@id", id);
            DbDataReader reader = Database.GetData(Database.GetConnection(CreateConnectionString(claims)), sql, par1);
            while (reader.Read())
            {
                emp = Create(reader);
            }
            reader.Close();
            return emp;

        }

        private static Employee Create(IDataRecord record)
        {
            return new Employee()
            {
                Id = Int32.Parse(record["Id"].ToString()),
                EmployeeName = record["EmployeeName"].ToString(),
                Address = record["Address"].ToString(),
                Email = record["Email"].ToString(),
                Phone = record["Phone"].ToString(),
                LoginCode = Int32.Parse( record["LoginCode"].ToString())
            };
        }

        public static int UpdateEmployee(Employee emp, IEnumerable<Claim> claims)
        {
            int rowsaffected = 0;
            string address = emp.Address;
            string email = emp.Email;
            string name = emp.EmployeeName;
            int id = emp.Id;
            string phone = emp.Phone;
            int logincode = emp.LoginCode;

            string sql = "UPDATE [Employee] SET LoginCode = @LoginCode, EmployeeName = @Name, Email = @Email, Address = @Address, Phone = @Phone WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@Name", name);
            DbParameter par2 = Database.AddParameter(CreateConnectionString(claims), "@Email", email);
            DbParameter par3 = Database.AddParameter(CreateConnectionString(claims), "@Phone", phone);
            DbParameter par4 = Database.AddParameter(CreateConnectionString(claims), "@Address", address);
            DbParameter par5 = Database.AddParameter(CreateConnectionString(claims), "@ID", id);
            DbParameter par6 = Database.AddParameter(CreateConnectionString(claims), "@LoginCode", logincode);

            rowsaffected += Database.ModifyData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par5, par6);

            return rowsaffected;
        }

        public static void AddNewEmployee(Employee emp, IEnumerable<Claim> claims)
        {
            string address = emp.Address;
            string email = emp.Email;
            string name = emp.EmployeeName;
            int id = emp.Id;
            string phone = emp.Phone;
            int logincode = emp.LoginCode;

            string sql = "INSERT INTO [Employee] ([EmployeeName],[Address],[Email],[Phone],[LoginCode] ) VALUES(@EmployeeName, @Address, @Email, @Phone, @LoginCode)";

            DbParameter par1 = Database.AddParameter(CreateConnectionString(claims), "@EmployeeName", name);
            DbParameter par2 = Database.AddParameter(CreateConnectionString(claims), "@Email", email);
            DbParameter par3 = Database.AddParameter(CreateConnectionString(claims), "@Phone", phone);
            DbParameter par4 = Database.AddParameter(CreateConnectionString(claims), "@Address", address);
            DbParameter par6 = Database.AddParameter(CreateConnectionString(claims), "@LoginCode", logincode);

           // DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql, par1, par2, par3, par4, par6);
        }

        public static void DeleteEmployee(int idMedewerker, IEnumerable<Claim> claims)
        {
            string sql2 = "DELETE FROM [Employee] WHERE ID =@ID";
            DbParameter par21 = Database.AddParameter(CreateConnectionString(claims), "@ID", idMedewerker);
            Database.InsertData(Database.GetConnection(CreateConnectionString(claims)), sql2, par21);
        }

        
    }
}