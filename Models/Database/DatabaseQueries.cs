using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVM_SocialContractProject.Models.Database
{
    public class DatabaseQueries
    {
        private readonly sqlDB dbConnect;
        private MySqlConnection conn { get; set; }
        public DatabaseQueries()
        {
            dbConnect = new sqlDB();
        }

        public bool RunConnectionCheck()
        {
            RunSystemCheck();
            try
            {
                conn.Open();
                MySqlCommand cmdDb = new MySqlCommand("SELECT * FROM tbl_recordtbl WHERE 1", conn);
                cmdDb.ExecuteReader();
                conn.Close();
                return true;
            }
            catch (MySqlException e)
            {
                conn.Close();
                MessageBox.Show("Error code:" + e.Number + "\nPlease contact the administrator for more details", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public void RunSystemCheck()
        {
            conn = dbConnect.SqlQuery();
            if (conn != null && conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public void LoadStudentInfo(List<StudentInfo> _studentInfo, string SearchQuery, int page)
        {
            RunSystemCheck();
            string query = "SELECT stu.s_ID, stu.s_fn, stu.s_mn,stu.s_ln,stu.s_batchNo,stu.s_Course ";
            if(SearchQuery == null)
            {
                query += "FROM tbl_studentinfo stu LIMIT @page,5;";
            }
            else
            {
                query += "FROM tbl_studentinfo stu " +
                    "" +
                    "WHERE stu.s_ID LIKE @SID LIMIT @page,5;";
            }
            MySqlCommand cmdDb = new MySqlCommand(query, conn);
            if(SearchQuery != null)
            {
                cmdDb.Parameters.AddWithValue("@SID", "%" + SearchQuery + "%");
            }
            cmdDb.Parameters.AddWithValue("@page", page);
            try
            {
                //---open DB---
                conn.Open();
                MySqlDataReader reader = cmdDb.ExecuteReader();
                while (reader.Read())
                {
                    string sID = reader[0].ToString();
                    string fName = reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString();
                    int b_no = Convert.ToInt32(reader[4]);
                    string Course = reader[5].ToString();
                    _studentInfo.Add(new StudentInfo(sID, reader[1].ToString(), reader[2].ToString(), reader[3].ToString() , b_no, Course));
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Error: " + e);
            }
        }

        public void GetStudentInfo(object studentID, List<StudentInfo> _studentInfo)
        {
            RunSystemCheck();
            string query = "SELECT s_ID, s_fn, s_mn, s_ln, s_batchNo, s_Course from tbl_studentinfo WHERE s_ID = @SID";
            MySqlCommand cmdDB = new MySqlCommand(query, conn);
            cmdDB.Parameters.AddWithValue("@SID", studentID);
            try
            {
                conn.Open();
                MySqlDataReader reader = cmdDB.ExecuteReader();
                while (reader.Read())
                {
                    _studentInfo.Add(new StudentInfo(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), Convert.ToInt32(reader[4]), reader[5].ToString()));
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Error Message" + e);
            }
        }

        public int GetStudentCount(string SearchQuery)
        {
            RunSystemCheck();
            string query = "SELECT COUNT(*) FROM tbl_studentinfo";
            if (SearchQuery != null)
            { 
                query += " WHERE s_ID LIKE @SID";
            }
            MySqlCommand cmdDb = new MySqlCommand(query, conn);
            if (SearchQuery != null)
            {
                cmdDb.Parameters.AddWithValue("@SID", "%" + SearchQuery + "%");
            }
            try
            {
                conn.Open();
                MySqlDataReader reader = cmdDb.ExecuteReader();
                while (reader.Read())
                {
                    return Convert.ToInt32(reader[0]);
                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Error Message" + e);
                return 0;
            }
        }
        public void LoadSocialContractInfo(StudentInfo student , List<SocialContract> _socialContract)
        {
            RunSystemCheck();
            string query = "SELECT record_FirstSemester, record_SecondSemester , record_Summer ,  record_SchoolYear, record_SocialContract, record_ID FROM  tbl_recordtbl  " +
                "WHERE  record_s_ID  = @studentID AND record_IsRemoved = 0 ORDER BY record_SchoolYear ";
            MySqlCommand cmdDb = new MySqlCommand(query, conn);
            cmdDb.Parameters.AddWithValue("@studentID", student.StudentID.ToString());
            try
            {
                //---open DB---
                conn.Open();
                MySqlDataReader reader = cmdDb.ExecuteReader();
                while (reader.Read())
                {
                    int SocialContractID = Convert.ToInt32(reader[5]);
                    int FirstSemester = Convert.ToInt32(reader[0]);
                    int SecondSemester = Convert.ToInt32(reader[1]);
                    int Summer = Convert.ToInt32(reader[2]);
                    int SchoolYear = Convert.ToInt32(reader[3]);
                    string ImageSource = reader[4].ToString();
                    _socialContract.Add(new SocialContract(SocialContractID, student, FirstSemester, SecondSemester, Summer, SchoolYear, ImageSource));
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Error: " + e);
            }
        }

        public void InsertStudentRecords(StudentInfo student)
        {
                RunSystemCheck();
                string addOnQuery = "INSERT INTO tbl_studentinfo ( `s_ID`, `s_fn`, `s_mn`, `s_ln`, `s_batchNo`, `s_Course`)";
                addOnQuery += "VALUES (@sID, @sfn, @smn,@sln,@sbNo,@sc)";
                MySqlCommand customCM = new MySqlCommand(addOnQuery, conn);
                customCM.Parameters.AddWithValue("@sID", student.StudentID);
                customCM.Parameters.AddWithValue("@sfn", student.FirstName);
                customCM.Parameters.AddWithValue("@smn", student.MiddleName);
                customCM.Parameters.AddWithValue("@sln", student.LastName);
                customCM.Parameters.AddWithValue("@sbNo", student.BatchNo);
                customCM.Parameters.AddWithValue("@sc", student.Course);
                try
                {
                    conn.Open();
                    MySqlDataReader reader = customCM.ExecuteReader();
                    conn.Close();
                }
                catch (Exception a)
                {
                conn.Close();
                MessageBox.Show("Error Message" + a);
                }
        }

        public void InsertSocialContract(SocialContract contract)
        {
            RunSystemCheck();
            string defaultQuery = "INSERT INTO tbl_recordtbl (`record_s_ID`, `record_SchoolYear`, `record_FirstSemester`, `record_SecondSemester`, `record_Summer`,`record_SocialContract`)" +
               "VALUES (@sID,@rSy,@rFs,@rSs,@rS,@rSC)";
            MySqlCommand defaultCM = new MySqlCommand(defaultQuery, conn);
            defaultCM.Parameters.AddWithValue("@sID", contract.StudentID.StudentID);
            defaultCM.Parameters.AddWithValue("@rSy", contract.SchoolYear);
            defaultCM.Parameters.AddWithValue("@rFs", contract.FirstSemester);
            defaultCM.Parameters.AddWithValue("@rSs", contract.SecondSemester);
            defaultCM.Parameters.AddWithValue("@rS", contract.Summer);
            defaultCM.Parameters.AddWithValue("@rSC", contract.SocialContractimage);
            try
            {
                conn.Open();
                MySqlDataReader reader = defaultCM.ExecuteReader();
                conn.Close();
            }
            catch (Exception c)
            {
                conn.Close();
                MessageBox.Show("Error Message" + c);
            }
        }

        public void GetUserInfo(List<UserInfo> UserInfo)
        {
            RunSystemCheck();
            //---get stored password---
            string query = "SELECT admin_user, admin_pass,admin_salt FROM tbl_adminacc";
            MySqlCommand commandDatabase = new MySqlCommand(query, conn);

            //---Open Connection--
            try
            {
                conn.Open();
                //---ExecuteQuery---
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                while (myReader.Read())
                {
                    string username = myReader["admin_user"].ToString();
                    string encodedsalt = myReader["admin_salt"].ToString();
                    string encodedpass = myReader["admin_pass"].ToString();
                    UserInfo.Add(new UserInfo(username, encodedpass, encodedsalt));
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error. Error message:" + ex);

            }
        }

        public void CreateUserInfo(UserInfo user)
        {
            RunSystemCheck();
            string query = "INSERT INTO tbl_adminacc (admin_user,admin_pass,admin_salt) VALUES (@user,@pass,@salt)";
            MySqlCommand commandDatabase = new MySqlCommand(query, conn);
            commandDatabase.Parameters.AddWithValue("@user", user.Username);
            commandDatabase.Parameters.AddWithValue("@pass", user.Password);
            commandDatabase.Parameters.AddWithValue("@salt", user.Salt);
            try
            {
                //Open connection
                conn.Open();
                //Execute Query
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Error. Error message:" + e);
            }
        }

        public byte[] DeriveKey(SecureString password, byte[] salt)
        {
            IntPtr ptr = Marshal.SecureStringToBSTR(password);
            byte[] passwordByteArray = null;
            try
            {
                int length = Marshal.ReadInt32(ptr, -4);
                passwordByteArray = new byte[length];
                GCHandle handle = GCHandle.Alloc(passwordByteArray, GCHandleType.Pinned);
                try
                {
                    for (int i = 0; i < length; i++)
                    {
                        passwordByteArray[i] = Marshal.ReadByte(ptr, i);
                    }
                    using (var rfc2898 = new Rfc2898DeriveBytes(passwordByteArray, salt, 1000))
                    {
                        return rfc2898.GetBytes(24);
                    }
                }
                finally
                {
                    Array.Clear(passwordByteArray, 0, passwordByteArray.Length);
                    handle.Free();
                }
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
        }

        public void SearchQueryStudent(List<StudentInfo> studentInfo, string s_ID)
        {
            RunSystemCheck();
            string query = "SELECT stu.s_ID, stu.s_fn, stu.s_mn,stu.s_ln,stu.s_batchNo,stu.s_Course ";
            query += "FROM tbl_studentinfo stu WHERE stu.s_ID LIKE @SID";
            MySqlCommand cmdDB = new MySqlCommand(query, conn);
            cmdDB.Parameters.AddWithValue("SID", "%" + s_ID + "%");
            try
            {
                int count = 0;
                conn.Open();
                MySqlDataReader reader = cmdDB.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if(count < 5)
                        {
                            count++;
                            studentInfo.Add(new StudentInfo(reader[0].ToString(),reader[1].ToString(),reader[2].ToString(),reader[3].ToString(),Convert.ToInt32(reader[4]), reader[5].ToString()));
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Error Message:" + e);
            }
        }


        public void AddPdf(PDFInfo pdf)
        {
            RunSystemCheck();
            //Still needs form validation btw
            //Textbox values
            string activity = pdf.EventName;
            string venue = pdf.EventVenue;
            DateTime date = pdf.EventDate;
            string supervisor = pdf.EventSupervisor;
            //Image upload and save
            var fileNameToSave = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(pdf.EventPDFSource);
            var imagepath = Path.Combine("D:\\VSCODE\\WpfApp1\\Sample Uploads\\" + fileNameToSave);
                             
            //Save to DB part here (copy paste ez) 
            string query = "INSERT INTO " +
                "`tbl_events`( `event_name`, `event_date`, `event_supervisor`, " +
                "`event_PDF`, `event_venue`) VALUES (@e_Name,@e_date,@e_supervisor,@e_filePath,@e_venue)";

            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.Parameters.AddWithValue("@e_Name", activity);
            cmDB.Parameters.AddWithValue("@e_date", date);
            cmDB.Parameters.AddWithValue("@e_supervisor", supervisor);
            cmDB.Parameters.AddWithValue("@e_filePath", imagepath);
            cmDB.Parameters.AddWithValue("@e_venue", venue);
            try
            {
                conn.Open();
                MySqlDataReader reader = cmDB.ExecuteReader();
                conn.Close();
                File.Copy(pdf.EventPDFSource, imagepath);
                MessageBox.Show("Event Saved");
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("DB related Error, please contact the administratior " +
                    "\n Error Message:" + ex);
            }
        }

        public void RemoveSocialContract(int recordID)
        {
            RunSystemCheck();
            string query = "UPDATE tbl_recordtbl SET record_IsRemoved = 1 WHERE record_ID = @recordID";
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.Parameters.AddWithValue("@recordID", recordID);
            try
            {
                conn.Open();
                MySqlDataReader reader = cmDB.ExecuteReader();
                conn.Close();

            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("DB related Error, please contact the administratior " +
                  "\n Error Message:" + e);
            }

        }

        public void GetAllPDF(List<PDFInfo> pdf, string SearchQuery)
        {
            RunSystemCheck();
            string query = "SELECT event_name, event_date, event_supervisor, event_PDF, event_venue ";
            if (SearchQuery == null)
            {
                query += "FROM tbl_events LIMIT 0,50;";
            }
            else
            {
                query += "FROM tbl_events WHERE event_name LIKE @SID LIMIT 0,50;";
            }
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            if (SearchQuery != null)
            {
                cmDB.Parameters.AddWithValue("@SID", "%" + SearchQuery + "%");
            }
            try
            {
                conn.Open();
                MySqlDataReader reader = cmDB.ExecuteReader();
                while (reader.Read())
                {
                    DateTime date = reader.GetDateTime(1);
                    pdf.Add(new PDFInfo(reader[0].ToString(), reader[2].ToString(),
                        reader[3].ToString(), reader[4].ToString(), date));
                }
                conn.Close();
            }catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("DB related Error, please contact the administratior " +
                  "\n Error Message:" + e);
            }
        }
    }
}
