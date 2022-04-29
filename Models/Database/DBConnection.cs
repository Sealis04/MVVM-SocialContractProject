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

        public MySqlConnection SqlQuery()
        {
            //Connects to the DB
            string Server = MVVM_SocialContractProject.Properties.Settings.Default.Server;
            string Database = MVVM_SocialContractProject.Properties.Settings.Default.Database;
            string Username = MVVM_SocialContractProject.Properties.Settings.Default.Username;
            string Password = MVVM_SocialContractProject.Properties.Settings.Default.Password;
            try
            {
                string connect = "SERVER=127.0.0.1;DATABASE=socialcontractdb;UID=root;PASSWORD=;Convert Zero Datetime = True";
                string connectionQuery = "SERVER=" + Server + ";DATABASE=" + Database + ";UID=" + Username + ";PASSWORD=" + Password + ";Convert Zero Datetime=True";
                if (connect == connectionQuery)
                {
                    MessageBox.Show("ASDASD");
                }
                MySqlConnection databaseConnection = new MySqlConnection(connectionQuery);
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


