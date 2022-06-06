using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Text;

namespace MVVM_SocialContractProject.Models.Database
{
    public class sqlDB
    {
        public sqlDB()
        {
        }

        public SqlConnection SqlQuery()
        {
            //Connects to the DB
            string Server = MVVM_SocialContractProject.Properties.Settings.Default.Server;
            string Database = MVVM_SocialContractProject.Properties.Settings.Default.Database;
            string Username = MVVM_SocialContractProject.Properties.Settings.Default.Username;
            string Password = MVVM_SocialContractProject.Properties.Settings.Default.Password;
            string TCPIP = Properties.Settings.Default.TCPIP;
            try
            {
                string connectionQuery = "Data source="+ Server + ","+ TCPIP + ";Initial catalog="+Database+";User id="+Username+";password="+Password;
                SqlConnection databaseConnection = new SqlConnection(connectionQuery);
                return databaseConnection;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error" + e);
                return null;
            }

        }

    }
}


