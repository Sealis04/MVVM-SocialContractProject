using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Text;
public class sqlDB
    {
    private DataTable dataTable = new DataTable();
    public sqlDB()
        {
        }

        public static MySqlConnection SqlQuery()
        {
        //Connects to the DB
        try
        {
            string connectionQuery = "SERVER=127.0.0.1;DATABASE=socialcontractdb;UID=root;PASSWORD=;Convert Zero Datetime=True";
            MySqlConnection databaseConnection = new MySqlConnection(connectionQuery);
            return databaseConnection;
        }catch (Exception e)
        {
            MessageBox.Show("Error" + e);
            return null;
        }   
         
        }

}
