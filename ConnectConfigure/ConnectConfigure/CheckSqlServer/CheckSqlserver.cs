using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Data.SqlClient;

namespace ConnectConfigure.CheckSqlServer
{
    public class CheckSqlserver
    {


        public static bool IsSqlServerInstalled()
        {
            const string registryKey = @"SOFTWARE\Microsoft\Microsoft SQL Server";

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                if (key != null)
                {
                    string[] instances = key.GetSubKeyNames();

                    foreach (string instanceName in instances)
                    {
                        if (instanceName.StartsWith("MSSQL"))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public static string GetDbName(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return connection.Database;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to connect to SQL Server. Error: " + ex.Message);
                }
            }

            return null;
        }

        public static bool CheckSqlConnectivity(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to connect to SQL Server. Error: " + ex.Message);
                }
            }

            return false;
        }

    }
}
