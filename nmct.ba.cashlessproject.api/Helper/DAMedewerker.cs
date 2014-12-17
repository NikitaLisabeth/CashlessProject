using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using nmct.ba.cashlessproject.Models;
using System.Data;

namespace nmct.ba.cashlessproject.api.Helper
{
    public class DAMedewerker
    {
        private const string CONNECTIONSTRING = "KlantConnection";
        public static List<Employee> GetEmployee()
        {
            List<Employee> list = new List<Employee>();
            string sql = "SELECT [Id],[EmployeeName],[Address],[Email],[Phone] FROM [Klant].[dbo].[Employee]";
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);
            while (reader.Read())
            {
                list.Add(Create(reader));
            }
            reader.Close();
            return list;

        }
        public static Employee GetEmployee(int id)
        {
            Employee emp = new Employee();
            string sql = "SELECT [Id],[EmployeeName],[Address],[Email],[Phone] FROM [Klant].[dbo].[Employee] where Id=@id";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@id", id);
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql,par1);
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
                Phone = record["Phone"].ToString()
            };
        }

        public static int UpdateEmployee(Employee emp)
        {
            int rowsaffected = 0;
            string address = emp.Address;
            string email = emp.Email;
            string name = emp.EmployeeName;
            int id = emp.Id;
            string phone = emp.Phone;


            string sql = "UPDATE [Klant].[dbo].[Employee] SET EmployeeName = @Name, Email = @Email, Address = @Address, Phone = @Phone WHERE ID=@ID";
            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@Name", name);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Email", email);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Phone", phone);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Address", address);
            DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);

            rowsaffected += Database.ModifyData(CONNECTIONSTRING, sql, par1, par2, par3, par4, par5);

            return rowsaffected;
        }

        public static void AddNewEmployee(Employee emp)
        {
            string address = emp.Address;
            string email = emp.Email;
            string name = emp.EmployeeName;
            int id = emp.Id;
            string phone = emp.Phone;

            string sql = "INSERT INTO [Klant].[dbo].[Employee] VALUES(@EmployeeName, @Address, @Email, @Phone)";

            DbParameter par1 = Database.AddParameter(CONNECTIONSTRING, "@EmployeeName", name);
            DbParameter par2 = Database.AddParameter(CONNECTIONSTRING, "@Email", email);
            DbParameter par3 = Database.AddParameter(CONNECTIONSTRING, "@Phone", phone);
            DbParameter par4 = Database.AddParameter(CONNECTIONSTRING, "@Address", address);
           // DbParameter par5 = Database.AddParameter(CONNECTIONSTRING, "@ID", id);
            Database.InsertData(CONNECTIONSTRING, sql, par1, par2, par3, par4);
        }

        public static void DeleteEmployee(int idMedewerker)
        {
            string sql2 = "DELETE FROM [Klant].[dbo].[Employee] WHERE ID =@ID";
            DbParameter par21 = Database.AddParameter(CONNECTIONSTRING, "@ID", idMedewerker);
            Database.InsertData(CONNECTIONSTRING, sql2, par21);
        }

        
    }
}