﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.database
{
    public class Database
    {
        public static DbConnection GetConnection(string ConnectionString)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[ConnectionString];
            return GetConnection(settings);
        }

        public static DbConnection GetConnection(ConnectionStringSettings Settings)
        {
            DbConnection con = DbProviderFactories.GetFactory(Settings.ProviderName).CreateConnection();
            con.ConnectionString = Settings.ConnectionString;
            con.Open();

            return con;
        }

        public static void ReleaseConnection(DbConnection con)
        {
            if (con != null)
            {
                con.Close();
                con = null;
            }
        }

        private static DbCommand BuildCommand(string constring, string sql, params DbParameter[] parameters)
        {
            DbCommand command = GetConnection(constring).CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = sql;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            return command;
        }

        public static DbDataReader GetData(string constring, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            DbDataReader reader = null;
            try
            {
                command = BuildCommand(constring, sql, parameters);
                reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (reader != null)
                {
                    reader.Close();
                }
                if (command != null)
                {
                    ReleaseConnection(command.Connection);
                }
                throw;
            }
        }

        public static int ModifyData(string constring, string sql, params DbParameter[] parameters)
        {
            //hetzelfde maar je hebt geen reader nodig.
            DbCommand command = null;
            try
            {
                command = BuildCommand(constring, sql, parameters);
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                if (command != null)
                {
                    ReleaseConnection(command.Connection);
                }
                throw;
            }
        }
        public static int InsertData(string constring, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            try
            {
                command = BuildCommand(constring, sql, parameters);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                command.CommandText = "SELECT @@IDENTITY";
                int id = Convert.ToInt32(command.ExecuteScalar());
                command.Connection.Close();
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                if (command != null)
                {
                    ReleaseConnection(command.Connection);
                }
                return 0;
            }
        }

        public static DbParameter AddParameter(string constring, string name, object value)
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[constring];
            DbParameter par = DbProviderFactories.GetFactory(settings.ProviderName).CreateParameter();
            par.ParameterName = name;
            par.Value = value;
            return par;
        }

        public static int ModifyData(DbConnection con, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            try
            {
                command = BuildCommand(con, sql, parameters);
                int affected = command.ExecuteNonQuery();
                command.Connection.Close();

                return affected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (command != null)
                    ReleaseConnection(command.Connection);
                return 0;
            }
        }


        private static DbCommand BuildCommand(DbConnection con, string sql, params DbParameter[] parameters)
        {
            DbCommand command = con.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = sql;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }

        public static DbTransaction BeginTransaction(string ConnectionString)
        {
            DbConnection con = null;
            try
            {
                con = GetConnection(ConnectionString);
                return con.BeginTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ReleaseConnection(con);
                throw;
            }
        }
        public static int ModifyData(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = null;
            try
            {
                command = BuildCommand(trans, sql, parameters);
                command.Transaction = trans;
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        private static DbCommand BuildCommand(DbTransaction trans, string sql, params DbParameter[] parameters)
        {
            DbCommand command = trans.Connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = sql;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }
    }
}
