using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using DDMailSmsWeb.DataAccess;
using Telerik.Web.UI;
using DDMailSmsWeb.DynamicAccess;
using OpenPop.Pop3;
using OpenPop.Mime;
using DDMailSmsWeb.Classes;
using System.IO;
using System;
using System.Collections.Generic;


namespace DDMailSmsWeb
{
    public partial class smsreply : System.Web.UI.Page
    {
     static public  string CampusConStr;
     Boolean YesNo;
     static string dbConnectionLocal = ConfigurationManager.ConnectionStrings["DDSuperConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            string AccountSid =Request["AccountSid"];
            string sid = Request["MessageSid"];
            string date_sent = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//Request["date_sent"].ToString("yyyy-MM-dd HH:mm:ss");
            string to = Request["to"];
            string from = Request["from"];
            string body = Request["body"];
            string status = "received";//Request["CallStatus"];
            string direction = "inbound";// Request["Direction"];
            string DateCreated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//Request["DateCreated"].ToString("yyyy-MM-dd HH:mm:ss");
            string DateUpdated = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//Request["DateUpdated"]ToString("yyyy-MM-dd HH:mm:ss");
            string ErrorCode = Request["ErrorCode"];
            string ErrorMessage = Request["ErrorMessage"];
            string DeptID = string.Empty, StudentNo = string.Empty, LeadId = string.Empty; string campusid=string.Empty;
            DataTable dtSMSDetails;
          /* Testing Pupose
           

           
             dtSMSDetails = DataAccessManager.GetSMSConfigDetail(AccountSid);
             if (dtSMSDetails.Rows.Count > 0)
             {
                 DeptID = dtSMSDetails.Rows[0]["DeptID"].ToString();
                 //campusid = dtSMSDetails.Rows[0]["CampusID"].ToString();
                 //******Create Connection for Gloabl******
                 CampusConStr = dtSMSDetails.Rows[0]["CampusConStr"].ToString();
             }

             YesNo = Insertresponse(AccountSid, sid, date_sent, to, from, body, status, direction, DateCreated, DateUpdated, ErrorCode, ErrorMessage, DeptID, StudentNo, LeadId);
         */

            if (from != null)
            {
              
            //*****************Get Dept ID**************************************
            //DataTable dtSMSDetails;
            dtSMSDetails = DataAccessManager.GetSMSConfigDetail(AccountSid);
            if (dtSMSDetails.Rows.Count > 0)
            {
                DeptID = dtSMSDetails.Rows[0]["DeptID"].ToString();
                //campusid = dtSMSDetails.Rows[0]["CampusID"].ToString();
                //******Create Connection for Gloabl******
                CampusConStr = dtSMSDetails.Rows[0]["CampusConStr"].ToString();
            }

           


           


            //*****************Get Student No or LeadsID**************************************
            DataTable dtSnoandLID =new DataTable();
           
                if (from == "+919999890039")
                {


                    from = from.Substring(3, from.Length - 3);
                    dtSnoandLID =GetStudentNo(from);
                    if (dtSnoandLID.Rows.Count > 0)
                    {
                        StudentNo = dtSnoandLID.Rows[0]["StudentNo"].ToString();
                    }
                    else
                    {
                        StudentNo = "0";
                        dtSnoandLID = GetLeadsID(from);
                        if (dtSnoandLID.Rows.Count > 0)
                        {
                            LeadId = dtSnoandLID.Rows[0]["LeadsID"].ToString();
                        }
                        else
                        {
                            LeadId = "0";
                        }


                    }
                }
                else
                {
                    from = from.Substring(2, from.Length - 2);
                    dtSnoandLID = GetStudentNo(from);
                    if (dtSnoandLID.Rows.Count > 0)
                    {
                        StudentNo = dtSnoandLID.Rows[0]["StudentNo"].ToString();
                    }
                    else
                    {
                        StudentNo = "0";
                        dtSnoandLID =GetLeadsID(from);
                        if (dtSnoandLID.Rows.Count > 0)
                        {
                            LeadId = dtSnoandLID.Rows[0]["LeadsID"].ToString();
                        }
                        else
                        {
                            LeadId = "0";
                        }


                    }
                }
            
                if (AccountSid != null)
                {
                    try
                    {

                        YesNo =Insertresponse(AccountSid, sid, date_sent, to, from, body, status, direction, DateCreated, DateUpdated, ErrorCode, ErrorMessage, DeptID, StudentNo, LeadId);
                    }

                    catch (Exception ex)
                    {
                        YesNo = false;
                    }

                }
            }      
           
           

        }

        static public DataTable GetStudentNo(string mobileno)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(CampusConStr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = "select StudentNo from Students where MobilePhone='" + mobileno + "'";

                        CMD.Prepare();
                        SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                        DataAdapter.Fill(_ds);

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }

            return _ds;
        }

        static public DataTable GetLeadsID(string mobileno)
        {

            DataTable _ds = new DataTable();
            try
            {
                var connection = new SqlConnection(CampusConStr);

                using (SqlCommand CMD = new SqlCommand())
                {
                    connection.Open();
                    CMD.Connection = connection;
                    CMD.CommandType = System.Data.CommandType.Text;
                    CMD.CommandText = "select LeadsID from Lead where PhoneMobile='" + mobileno + "'";
                    CMD.Prepare();
                    SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                    DataAdapter.Fill(_ds);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {

            }

            return _ds;
        }

        public static int ExecuteNonQueryLocal(string strSQL)
        {
            int retval = 0;
            using (SqlConnection conn = new SqlConnection(dbConnectionLocal))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                }



                SqlCommand dbOleCommand = new SqlCommand();
                dbOleCommand.Connection = conn;
                dbOleCommand.CommandText = strSQL;

                try
                {
                    retval = dbOleCommand.ExecuteNonQuery();
                }
                catch (Exception ex1)
                {

                    throw ex1;

                }
                finally
                {
                }
                return retval;
            }
            return retval;
        }

        static public Boolean Insertresponse(string AccountSid, string sid, string date_sent, string to, string from, string body, string status, string direction, string DateCreated, string DateUpdated, string ErrorCode, string ErrorMessage, string DeptID, string StudentNo, string LeadID)
        {
            Boolean YesNo;
            try
            {
                string strQuery = "";
                strQuery = "INSERT INTO EmailSMSLocalData([AccountSid],[sid],[date_sent],[to],[from],[body],[status],[direction],[DateCreated],[DateUpdated],[ErrorCode],[ErrorMessage],DeptID,StudentNo,LeadNo) VALUES('" + AccountSid + "','" + sid + "','" + date_sent + "','" + to + "','" + from + "','" + body + "','" + status + "','" + direction + "','" + DateCreated + "','" + DateUpdated + "','" + ErrorCode + "','" + ErrorMessage + "','" + DeptID + "','" + StudentNo + "','" + LeadID + "')";

                ExecuteNonQueryLocal(strQuery);
                YesNo = true;
            }

            catch (Exception ex)
            {
                YesNo = false;
            }
            return YesNo;
        }

    }
}