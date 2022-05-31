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

        public void LoadStudentInfo(List<StudentInfo> _studentInfo, string SearchQuery, int page, int studentQuery, bool direction)
        {
            RunSystemCheck();
            string query = "SELECT s_ID, s_fn, s_mn,s_ln,s_batchNo,s_Course " +
                "FROM tbl_studentinfo INNER JOIN tbl_recordtbl ON tbl_studentinfo.s_ID = tbl_recordtbl.record_s_ID  ";
            if (SearchQuery != null)
            {
                query += " WHERE s_ID LIKE @SID ";
            }
            query += "GROUP BY s_ID ";
            if (studentQuery != 0)
            {
                if (direction)
                {
                    query += "ORDER BY " +
                        "CASE @sort " +
                        "WHEN 1 THEN s_ID " +
                        "END ASC, " +
                        "CASE @sort " +
                        "WHEN 2 THEN s_fn " +
                        "END ASC, " +
                        "CASE @sort " +
                        "WHEN 3 THEN s_batchNo " +
                        "END ASC, " +
                        "CASE @sort " +
                        "WHEN 4 THEN s_Course " +
                        "END ASC, " +
                        "CASE @sort " +
                        "WHEN 5 THEN SUM(tbl_recordtbl.record_FirstSemester +tbl_recordtbl.record_SecondSemester + tbl_recordtbl.record_Summer) " +
                        "END ASC, " +
                        "CASE @sort " +
                        "WHEN 6 THEN 160 - SUM(tbl_recordtbl.record_FirstSemester + tbl_recordtbl.record_SecondSemester + tbl_recordtbl.record_Summer) " +
                        "END ASC ";
                }
                else
                {
                    query += "ORDER BY " +
                     "CASE @sort " +
                     "WHEN 1 THEN s_ID " +
                     "END DESC, " +
                     "CASE @sort " +
                     "WHEN 2 THEN s_fn " +
                     "END DESC, " +
                     "CASE @sort " +
                     "WHEN 3 THEN s_batchNo " +
                     "END DESC, " +
                     "CASE @sort " +
                     "WHEN 4 THEN s_Course " +
                     "END DESC, " +
                     "CASE @sort " +
                     "WHEN 5 THEN SUM(tbl_recordtbl.record_FirstSemester +tbl_recordtbl.record_SecondSemester + tbl_recordtbl.record_Summer) " +
                     "END DESC, " +
                     "CASE @sort " +
                     "WHEN 6 THEN 160 - SUM(tbl_recordtbl.record_FirstSemester + tbl_recordtbl.record_SecondSemester + tbl_recordtbl.record_Summer) " +
                     "END DESC ";
                }
            }
            query += " LIMIT @page,20;";
            MySqlCommand CommandDB = new MySqlCommand(query, conn);
            CommandDB.Parameters.AddWithValue("@SID", "%" + SearchQuery + "%");
            CommandDB.Parameters.AddWithValue("@sort", studentQuery);
            CommandDB.Parameters.AddWithValue("@page", page);
            try
            {
                //---open DB---
                conn.Open();
                MySqlDataReader reader = CommandDB.ExecuteReader();
                while (reader.Read())
                {
                    string sID = reader[0].ToString();
                    string fName = reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString();
                    int b_no = Convert.ToInt32(reader[4]);
                    string Course = reader[5].ToString();
                    _studentInfo.Add(new StudentInfo(sID, reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), b_no, Course));
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
        public void LoadSocialContractInfo(StudentInfo student, List<SocialContract> _socialContract, int intQuery, bool direction)
        {
            RunSystemCheck();
            string query = "SELECT record_FirstSemester, record_SecondSemester , record_Summer ,  record_SchoolYear, record_SocialContract, record_ID FROM  tbl_recordtbl  " +
                "WHERE  record_s_ID  = @studentID AND record_IsRemoved = 0 ";
            if(intQuery != 0)
            {
                if (direction)
                {
                    query += "ORDER BY " +
                            "CASE @sort " +
                            "WHEN 1 THEN record_FirstSemester " +
                            "END ASC, " +
                            "CASE @sort " +
                            "WHEN 2 THEN record_SecondSemester " +
                            "END ASC, " +
                            "CASE @sort " +
                            "WHEN 3 THEN record_SchoolYear " +
                            "END ASC, " + 
                            "CASE @sort " +
                            "WHEN 4 THEN record_Summer " +
                            "END ASC ";
                }
                else
                {
                    query += "ORDER BY " +
                            "CASE @sort " +
                            "WHEN 1 THEN record_FirstSemester " +
                            "END DESC, " +
                            "CASE @sort " +
                            "WHEN 2 THEN record_SecondSemester " +
                            "END DESC, " +
                            "CASE @sort " +
                            "WHEN 3 THEN record_SchoolYear " +
                            "END DESC, " +
                            "CASE @sort " +
                            "WHEN 4 THEN record_Summer " +
                            "END DESC ";
                }
            }
            else
            {
                query += "ORDER BY record_SchoolYear";
            }
            
            MySqlCommand cmdDb = new MySqlCommand(query, conn);
            cmdDb.Parameters.AddWithValue("@studentID", student.StudentID.ToString());
            cmdDb.Parameters.AddWithValue("@sort", intQuery);
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

        public void GetSCInfo(StudentInfo student, List<SocialContract> SCList)
        {
            RunSystemCheck();
            string query = "SELECT record_ID,record_FirstSemester, record_SecondSemester , record_Summer ,  record_SchoolYear, record_SocialContract FROM  tbl_recordtbl  " +
                "WHERE  record_s_ID  = @sID AND record_IsRemoved = 0";
            MySqlCommand cmdDB = new MySqlCommand(query, conn);
            cmdDB.Parameters.AddWithValue("@sID", student.StudentID);
            try
            {
                conn.Open();
                MySqlDataReader reader = cmdDB.ExecuteReader();
                while (reader.Read())
                {
                    SCList.Add(new SocialContract(Convert.ToInt32(reader[0]),student, Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]), Convert.ToInt32(reader[4]),reader[5].ToString()));
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("Error Message" + e);
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

        public void InsertSocialContract(StudentInfo student,SocialContract contract)
        {
            RunSystemCheck();
            string defaultQuery = "INSERT INTO tbl_recordtbl (`record_s_ID`, `record_SchoolYear`, `record_FirstSemester`, `record_SecondSemester`, `record_Summer`,`record_SocialContract`)" +
               "VALUES (@sID,@rSy,@rFs,@rSs,@rS,@rSC)";
            var fileNameToSave = DateTime.Now.ToString("yyyyMMddHHmmssfff") + student.StudentID + Path.GetExtension(contract.SocialContractimage);
            var imagepath = Path.Combine("\\\\" + Properties.Settings.Default.Server + "\\SocialContractsFolder\\" + fileNameToSave);
            MySqlCommand defaultCM = new MySqlCommand(defaultQuery, conn);
            defaultCM.Parameters.AddWithValue("@sID", contract.StudentID.StudentID);
            defaultCM.Parameters.AddWithValue("@rSy", contract.SchoolYear);
            defaultCM.Parameters.AddWithValue("@rFs", contract.FirstSemester);
            defaultCM.Parameters.AddWithValue("@rSs", contract.SecondSemester);
            defaultCM.Parameters.AddWithValue("@rS", contract.Summer);
            defaultCM.Parameters.AddWithValue("@rSC", imagepath);
            
            try
            {
                conn.Open();
                MySqlDataReader reader = defaultCM.ExecuteReader();
                conn.Close();
                File.Copy(contract.SocialContractimage, imagepath);
                MessageBox.Show("Successfuly Updated", "Success",
                         MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception c)
            {
                conn.Close();
                MessageBox.Show("Error Message" + c);
            }
        }

        public void UpdateSocialContract(SocialContract ConflictSC, StudentInfo student, SocialContract contract)
        {
            RunSystemCheck();
            string defaultQuery = "UPDATE tbl_recordtbl SET record_FirstSemester=@FS, " +
                "record_SecondSemester=@SS, record_Summer=@S ,record_SocialContract= @SC  WHERE record_s_ID = @sID AND record_SchoolYear = @SY";
            var ConflictImage = ConflictSC.SocialContractimage;
            var fileNameToSave = DateTime.Now.ToString("yyyyMMddHHmmssfff")+ student.StudentID + Path.GetExtension(contract.SocialContractimage);
            var imagepath = Path.Combine("\\\\" + Properties.Settings.Default.Server + "\\SocialContractsFolder\\" + fileNameToSave);
            MySqlCommand defaultCM = new MySqlCommand(defaultQuery, conn);
            defaultCM.Parameters.AddWithValue("@SY", contract.SchoolYear);
            defaultCM.Parameters.AddWithValue("@FS", contract.FirstSemester);
            defaultCM.Parameters.AddWithValue("@SS", contract.SecondSemester);
            defaultCM.Parameters.AddWithValue("@S", contract.Summer);
            defaultCM.Parameters.AddWithValue("@SC", imagepath);
            defaultCM.Parameters.AddWithValue("@sID", student.StudentID);
            try
            {
                conn.Open();
                if (File.Exists(ConflictImage))
                {
                    File.Delete(ConflictImage);
                    File.Copy(contract.SocialContractimage, imagepath);
                    MessageBox.Show("Successfuly Updated", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                MySqlDataReader reader = defaultCM.ExecuteReader();
                conn.Close();
              
            }
            catch (IOException s)
            {
                conn.Close();
                MessageBox.Show("Network Connection Error:" + s);
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
            string query = "SELECT admin_user, admin_pass,admin_salt,admin_type FROM tbl_adminacc";
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
                    UserInfo.Add(new UserInfo(username, encodedpass, encodedsalt, Convert.ToInt32(myReader["admin_type"])));
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
            commandDatabase.Parameters.AddWithValue("@user", user.Username.ToLower());
            commandDatabase.Parameters.AddWithValue("@pass", user.Password);
            commandDatabase.Parameters.AddWithValue("@salt", user.Salt);
            try
            {
                //Open connection
                conn.Open();
                //Execute Query
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                conn.Close();
                MessageBox.Show("UserCreated!", "Success!",
             MessageBoxButton.OK);
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
                        if (count < 5)
                        {
                            count++;
                            studentInfo.Add(new StudentInfo(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), Convert.ToInt32(reader[4]), reader[5].ToString()));
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
            var imagepath = Path.Combine("\\\\" + Properties.Settings.Default.Server + "\\PDFFolder\\" + fileNameToSave);

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

        public void GetAllPDF(List<PDFInfo> pdf, string SearchQuery, int page, int intQuery, bool direction)
        {
            RunSystemCheck();
            string query = "SELECT event_name, event_date, event_supervisor, event_PDF, event_venue FROM tbl_events ";
            if (SearchQuery != null)
            {
                query += "WHERE event_name LIKE @SearchQuery ";
            }
            if (intQuery != 0)
            {
                if (direction)
                {
                    query += "ORDER BY " +
                            "CASE @sort " +
                            "WHEN 1 THEN event_name " +
                            "END ASC, " +
                            "CASE @sort " +
                            "WHEN 2 THEN event_date " +
                            "END ASC, " +
                             "CASE @sort " +
                            "WHEN 3 THEN event_supervisor " +
                            "END ASC, " +
                            "CASE @sort " +
                            "WHEN 4 THEN event_venue " +
                            "END ASC ";
                }
                else
                {
                    query += "ORDER BY " +
                           "CASE @sort " +
                           "WHEN 1 THEN event_name " +
                           "END DESC, " +
                            "CASE @sort " +
                           "WHEN 2 THEN event_date " +
                           "END DESC, " +
                            "CASE @sort " +
                           "WHEN 3 THEN event_supervisor " +
                           "END DESC, " +
                            "CASE @sort " +
                           "WHEN 4 THEN event_venue " +
                           "END DESC ";
                }
            }
            query += "LIMIT @page,20;";
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            if (SearchQuery != null)
            {
                cmDB.Parameters.AddWithValue("@SearchQuery", "%" + SearchQuery + "%");
            }
            cmDB.Parameters.AddWithValue("@sort", intQuery);
            cmDB.Parameters.AddWithValue("@page", page);
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
            }
            catch (Exception e)
            {
                conn.Close();
                MessageBox.Show("DB related Error, please contact the administratior " +
                  "\n Error Message:" + e);
            }
        }

        public int GetAllPDFCount(string SearchQuery)
        {
            RunSystemCheck();
            //---get stored password---
            string query = "SELECT COUNT(*) ";
            if (SearchQuery == null)
            {
                query += " FROM tbl_events";
            }
            else
            {
                query += " FROM tbl_events WHERE event_name LIKE @SearchQuery";
            }
            MySqlCommand cmdDb = new MySqlCommand(query, conn);
            if (SearchQuery != null)
            {
                cmdDb.Parameters.AddWithValue("@SearchQuery", "%" + SearchQuery + "%");
            }
            //---Open Connection--
            try
            {
                conn.Open();
                //---ExecuteQuery---
                MySqlDataReader myReader = cmdDb.ExecuteReader();
                while (myReader.Read())
                {
                    return Convert.ToInt32(myReader[0]);
                }
                conn.Close();
                return 0;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error. Error message:" + ex);
                return 0;

            }
        }
        public void GetAllUserInfo(List<UserInfo> userInfo, string SearchQuery, int page, int intQuery, bool direction)
        {
            RunSystemCheck();
            //---get stored password---
            string query = "SELECT admin_user, admin_pass,admin_salt, admin_type FROM tbl_adminacc WHERE admin_IsRemoved = 0 AND admin_type = 0 ";
            if (SearchQuery != null)
            {
                query += "AND admin_user LIKE @SearchQuery ";
            }
            if (intQuery != 0)
            {
                if (direction)
                {
                    query +="ORDER BY " +
                            "CASE @sort " +
                            "WHEN 1 THEN admin_user " +
                            "END ASC ";
                }
                else
                {
                    query += "ORDER BY " +
                            "CASE @sort " +
                            "WHEN 1 THEN admin_user " +
                            "END DESC ";
                }
            }
            query += "LIMIT @page,20;";
            MySqlCommand cmdDb = new MySqlCommand(query, conn);
            if (SearchQuery != null)
            {
                cmdDb.Parameters.AddWithValue("@SearchQuery", "%" + SearchQuery + "%");
            }
            cmdDb.Parameters.AddWithValue("@sort", intQuery);
            cmdDb.Parameters.AddWithValue("@page", page);
            //---Open Connection--
            try
            {
                conn.Open();
                //---ExecuteQuery---
                MySqlDataReader myReader = cmdDb.ExecuteReader();
                while (myReader.Read())
                {
                    string username = myReader["admin_user"].ToString();
                    string encodedsalt = myReader["admin_salt"].ToString();
                    string encodedpass = myReader["admin_pass"].ToString();
                    userInfo.Add(new UserInfo(username, encodedpass, encodedsalt, Convert.ToInt32(myReader["admin_type"])));
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error. Error message:" + ex);

            }
        }

        public int GetAllUserCount(string SearchQuery)
        {
            RunSystemCheck();
            //---get stored password---
            string query = "SELECT COUNT(*) ";
            if (SearchQuery == null)
            {
                query += " FROM tbl_adminacc WHERE admin_IsRemoved = 0";
            }
            else
            {
                query += " FROM tbl_adminacc WHERE admin_IsRemoved = 0 AND admin_user LIKE @SearchQuery";
            }
            MySqlCommand cmdDb = new MySqlCommand(query, conn);
            if (SearchQuery != null)
            {
                cmdDb.Parameters.AddWithValue("@SearchQuery", "%" + SearchQuery + "%");
            }
            //---Open Connection--
            try
            {
                conn.Open();
                //---ExecuteQuery---
                MySqlDataReader myReader = cmdDb.ExecuteReader();
                while (myReader.Read())
                {
                    return Convert.ToInt32(myReader[0]);
                }
                conn.Close();
                return 0;
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error. Error message:" + ex);
                return 0;

            }
        }

        public void RemoveUser(string username)
        {
            RunSystemCheck();
            string query = "UPDATE tbl_adminacc SET admin_IsRemoved = 1 WHERE admin_user = @recordID";
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.Parameters.AddWithValue("@recordID", username.ToLower());
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

        public void UpdateUser(UserInfo user)
        {
            RunSystemCheck();
            string query = "UPDATE tbl_adminacc SET admin_pass = @pass,  admin_salt = @salt WHERE admin_user = @user";
            MySqlCommand cmDB = new MySqlCommand(query, conn);
            cmDB.Parameters.AddWithValue("@pass", user.Password);
            cmDB.Parameters.AddWithValue("@salt", user.Salt);
            cmDB.Parameters.AddWithValue("@user", user.Username.ToLower());
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
    }
}
