using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
 
using System.Configuration;
using DDMailSmsWeb.Classes;
using DDMailSmsWeb.DynamicAccess;

namespace DDMailSmsWeb.DataAccess
{
    static class DataAccessManager
    {
        static string constr = ConfigurationManager.ConnectionStrings["DDSuperConnectionString"].ToString();

        static public Boolean Insertresponse(string AccountSid, string sid, string date_sent, string to, string from, string body, string status, string direction, string DateCreated, string DateUpdated, string ErrorCode, string ErrorMessage,string DeptID,string StudentNo,string LeadID)
        {
            Boolean YesNo;
            try
            {
                string strQuery = "";
                strQuery = "INSERT INTO EmailSMSLocalData([AccountSid],[sid],[date_sent],[to],[from],[body],[status],[direction],[DateCreated],[DateUpdated],[ErrorCode],[ErrorMessage],DeptID,StudentNo,LeadNo) VALUES('" + AccountSid + "','" + sid + "','" + date_sent + "','" + to + "','" + from + "','" + body + "','" + status + "','" + direction + "','" + DateCreated + "','" + DateUpdated + "','" + ErrorCode + "','" + ErrorMessage + "','" + DeptID + "','" + StudentNo + "','" + LeadID + "')";
               
                DataLogicLayer.ExecuteNonQueryLocal(strQuery);
                YesNo = true;
            }

            catch (Exception ex)
            {
                YesNo = false;
            }
            return YesNo;
        }
        #region "Method"

        static public DataTable GetConfigration(string CampusName)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = "SELECT * FROM EmailCampusMaster  WHERE Active='TRUE' AND CampusName = '" + CampusName + "' and  Active='1' ";
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
        static public DataTable GetEmailConfigDetail(string DeptID, string CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = "select * from EmailConfig where DeptName='" + DeptID + "' and CampusID=" + CampusID;

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
        static public DataTable GetListStudent(string studentno, string DeptID,int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {
                        string strqry = string.Empty;
                        strqry = strqry + "  select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject,   ";
                        strqry = strqry + "   case EmailAttachment when 'True' then (isnull((select EmailAttachment.FileName from EmailAttachment    ";
                        strqry = strqry + "   where EmailInbox.messageId=EmailAttachment.messageId),'Not Available')) else 'No' end as EmailAttachment,    ";
                        strqry = strqry + "   case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachmentStatus,   ";
                        strqry = strqry + "   EmailReceivedDatetime,EmailIsRead,messageId      ";
                        strqry = strqry + "   from EmailInbox      ";
                        strqry = strqry + "   where EmailIsDelete='False'    ";
                        strqry = strqry + "  and DeptID='" + DeptID + "'and  StudentNo='" + studentno + "' and CampusID=" + CampusID + "    order by InboxID desc ";

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        //CMD.CommandText = "select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='False' and DeptID='" + DeptID + "' and (StudentNo='0' or StudentNo='" + studentno + "') and CampusID=" + CampusID + "  order by InboxID desc";
                        CMD.CommandText = strqry;
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
        /*Overloading Method*/
        static public DataTable GetListStudent(string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        string strqry = string.Empty;
                        strqry = strqry + "  select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject,   ";
                        strqry = strqry + "   case EmailAttachment when 'True' then (isnull((select EmailAttachment.FileName from EmailAttachment    ";
                        strqry = strqry + "   where EmailInbox.messageId=EmailAttachment.messageId),'Not Available')) else 'No' end as EmailAttachment,    ";
                        strqry = strqry + "   case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachmentStatus,   ";
                        strqry = strqry + "   EmailReceivedDatetime,EmailIsRead,messageId      ";
                        strqry = strqry + "   from EmailInbox      ";
                        strqry = strqry + "   where EmailIsDelete='False'    ";
                        strqry = strqry + "  and DeptID='" + DeptID + "'and  ( StudentNo='0' or LeadsID='0')  and CampusID=" + CampusID + "    order by InboxID desc ";

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        //CMD.CommandText = "select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='False' and DeptID='" + DeptID + "' and (StudentNo='0' or StudentNo='" + studentno + "') and CampusID=" + CampusID + "  order by InboxID desc";
                        CMD.CommandText = strqry;
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
        static public DataTable GetListStudentUnreadEmail(string studentno, string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {
                        string strqry = string.Empty;
                        strqry = strqry + "  select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject,   ";
                        strqry = strqry + "   case EmailAttachment when 'True' then (isnull((select EmailAttachment.FileName from EmailAttachment    ";
                        strqry = strqry + "   where EmailInbox.messageId=EmailAttachment.messageId),'Not Available')) else 'No' end as EmailAttachment,    ";
                        strqry = strqry + "   case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachmentStatus,   ";
                        strqry = strqry + "   EmailReceivedDatetime,EmailIsRead,messageId      ";
                        strqry = strqry + "   from EmailInbox      ";
                        strqry = strqry + "   where EmailIsDelete='False'    ";
                        strqry = strqry + "  and DeptID='" + DeptID + "'and  StudentNo='" + studentno + "' and CampusID=" + CampusID + "  and EmailIsRead=0   order by InboxID desc ";
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = strqry; 
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
        static public DataTable GetListStudentUnreadEmail(string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {
                        string strqry=string.Empty;
                        strqry = strqry + "   select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject, ";
                        strqry = strqry + "   case EmailAttachment when 'True' then ((select EmailAttachment.FileName from EmailAttachment  ";
                        strqry = strqry + "   where EmailInbox.messageId=EmailAttachment.messageId)) else 'No' end as EmailAttachment, ";
                        strqry = strqry + "   case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachmentStatus, ";
                        strqry = strqry + "   EmailReceivedDatetime,EmailIsRead,messageId    ";
                        strqry = strqry + "   from EmailInbox   ";
                        strqry = strqry + " where EmailIsDelete='False' and DeptID='" + DeptID + "' and (StudentNo='0' or LeadsID='0' )   and EmailIsRead=0 and CampusID=" + CampusID + "  order by InboxID desc";
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText =strqry;                    
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
        static public DataTable GetEmailSentList(string studentno, string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {
                        string strqry = string.Empty;
                        strqry=strqry+ " select SentID,EmailsentFrom,EmailSentTo,EmailSentSubject,EmailSentBody, ";
                        strqry=strqry+ " case EmailSentAttachment when '1' then (isnull((select EmailSentAttachment.FileName from EmailSentAttachment ";   
                        strqry=strqry+ " where EmailSent.SentID=EmailSentAttachment.SentID),'Not Available')) else 'No' end as EmailAttachment, ";
                        strqry=strqry+ " case EmailSentAttachment when '1' then 'Yes' else 'No' end as EmailAttachmentStatus, ";
                        strqry=strqry+ " EmailSentAttachment,EmailSentDatetime ";
                        strqry=strqry+ " from  EmailSent where ";
                        strqry=strqry+ " EmailSentIsDelete='False' ";
                        strqry = strqry + " and DeptID='" + DeptID + "' and StudentNo='" + studentno + "'   and CampusID=" + CampusID + " order by SentID desc";
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = strqry;// "select SentID,EmailsentFrom,EmailSentTo,EmailSentSubject,EmailSentBody,EmailSentAttachment,EmailSentDatetime from  EmailSent where EmailSentIsDelete='False' and DeptID='" + DeptID + "' and StudentNo='" + studentno + "'   and CampusID=" + CampusID + " order by SentID desc";
                   
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
        static public DataTable GetEmailSentList(  string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {
                        string strqry = string.Empty;
                        strqry = strqry + " select SentID,EmailsentFrom,EmailSentTo,EmailSentSubject,EmailSentBody, ";
                        strqry = strqry + " case EmailSentAttachment when '1' then (isnull((select EmailSentAttachment.FileName from EmailSentAttachment ";
                        strqry = strqry + " where EmailSent.SentID=EmailSentAttachment.SentID),'Not Available')) else 'No' end as EmailAttachment, ";
                        strqry = strqry + " case EmailSentAttachment when '1' then 'Yes' else 'No' end as EmailAttachmentStatus, ";
                        strqry = strqry + " EmailSentAttachment,EmailSentDatetime ";
                        strqry = strqry + " from  EmailSent where ";
                        strqry = strqry + " EmailSentIsDelete='False' ";
                        strqry = strqry + "  and DeptID='" + DeptID + "' and (Isnull(StudentNo,'0')='0' and Isnull(LeadsID,'0')='0') and CampusID=" + CampusID + " order by SentID desc";
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = strqry;// "select SentID,EmailsentFrom,EmailSentTo,EmailSentSubject,EmailSentBody,EmailSentDatetime from  EmailSent where EmailSentIsDelete='False'  and DeptID='" + DeptID + "' and ( StudentNo='0' or LeadsID='0')  and CampusID=" + CampusID + " order by SentID desc";

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
        static public DataTable GetDeleteEmailList(string studentno, string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = "select InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='True' and DeptID='" + DeptID + "' and StudentNo='" + studentno + "'   and CampusID=" + CampusID + "   order by InboxID desc";
                       
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
        static public DataTable GetDeleteEmailList(string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = "select InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='True' and DeptID='" + DeptID + "' and ( StudentNo='0' or LeadsID='0')  and CampusID=" + CampusID + "   order by InboxID desc";

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
        static public DataTable GetEmailConfigDetail(  string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = "select * from EmailConfig where Status=1 and DeptName='" + DeptID + "' and CampusID=" + CampusID;
                  
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
        static public DataTable GetSMSConfigDetail(string sid)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = "select SC.DeptID,CM.CampusID,CM.CampusName,CampusConStr from EmailSMSConfig SC INNER JOIN dbo.EmailCampusMaster CM ON SC.CampusID=CM.CampusID WHERE CM.Active='1' and SC.AccountSid='" + sid + "'";
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
        static public Boolean ReadEmailAndSaveDatabase(string StudentNo, string DeptID, string messageId, string EmailFrom, string EmailReceived, string EmailSubject, string EmailBody, bool EmailAttachment, DateTime EmailReceivedDatetime, int CampusID)
        {
            SqlConnection connection = new SqlConnection(constr);
            Boolean status;
            try
            {
                using (SqlCommand CMD = new SqlCommand())
                {

                    connection.Open();
                    CMD.Connection = connection;
                    CMD.CommandType = System.Data.CommandType.Text;
                    CMD.CommandText = "Insert into EmailInbox(StudentNo,DeptID,messageId,EmailFrom,EmailReceived,EmailSubject,EmailBody,EmailAttachment,EmailReceivedDatetime,CampusID) values('" + StudentNo + "','" + DeptID + "','" + messageId + "','" + EmailFrom + "','" + EmailReceived + "','" + EmailSubject + "','" + EmailBody + "','" + EmailAttachment + "','" + EmailReceivedDatetime.ToString("MM/dd/yyyy hh:mm tt") + "'," + CampusID + ")";
                    CMD.Prepare();
                    CMD.ExecuteNonQuery();
                    status = true;
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        static public Boolean ReadEmailAndSaveDatabaseLead(string LeadsID, string DeptID, string messageId, string EmailFrom, string EmailReceived, string EmailSubject, string EmailBody, bool EmailAttachment, DateTime EmailReceivedDatetime, int CampusID)
        {
            SqlConnection connection = new SqlConnection(constr);
            Boolean status;
            try
            {
                using (SqlCommand CMD = new SqlCommand())
                {

                    connection.Open();
                    CMD.Connection = connection;
                    CMD.CommandType = System.Data.CommandType.Text;
                    CMD.CommandText = "Insert into EmailInbox(LeadsID,DeptID,messageId,EmailFrom,EmailReceived,EmailSubject,EmailBody,EmailAttachment,EmailReceivedDatetime,CampusID) values('" + LeadsID + "','" + DeptID + "','" + messageId + "','" + EmailFrom + "','" + EmailReceived + "','" + EmailSubject + "','" + EmailBody + "','" + EmailAttachment + "','" + EmailReceivedDatetime.ToString("MM/dd/yyyy hh:mm tt") + "'," + CampusID + ")";
                    CMD.Prepare();
                    CMD.ExecuteNonQuery();
                    status = true;
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        static public Boolean ReadEmailAttachmentAndSaveDatabase(string messageId, string FileName)
        {
            SqlConnection connection = new SqlConnection(constr);
            Boolean status;
            try
            {
                using (SqlCommand CMD = new SqlCommand())
                {
                    connection.Open();
                    CMD.Connection = connection;
                    CMD.CommandType = System.Data.CommandType.Text;
                    CMD.CommandText = "Insert into EmailAttachment(messageId,FileName) values('" + messageId + "','" + FileName + "')";
                    CMD.Prepare();
                    CMD.ExecuteNonQuery();
                    status = true;
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        static public DataTable GetListLead(string LeadsID, string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        string strqry = string.Empty;
                        strqry=strqry+"  select  '' as StudentNo,InboxID,EmailBody, EmailReceived,EmailSubject, ";
                        strqry=strqry+"  case EmailAttachment when 'True' then (IsNull((select EmailAttachment.FileName from EmailAttachment  ";
                        strqry=strqry+"  where EmailInbox.messageId=EmailAttachment.messageId),'Not Available')) else 'No' end as EmailAttachment, ";
                        strqry=strqry+"  case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachmentStatus ";
                        strqry = strqry + "  ,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox   ";
                        strqry = strqry + "   where EmailIsDelete='False' and DeptID='" + DeptID + "' and ( LeadsID='" + LeadsID + "')  and CampusID=" + CampusID + "   order by InboxID desc";
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = strqry;
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
        static public DataTable GetListLead( string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        string strqry = string.Empty;
                        strqry = strqry + "  select  '' as StudentNo,InboxID,EmailBody, EmailReceived,EmailSubject, ";
                        strqry = strqry + "  case EmailAttachment when 'True' then (IsNull((select EmailAttachment.FileName from EmailAttachment  ";
                        strqry = strqry + "  where EmailInbox.messageId=EmailAttachment.messageId),'Not Available')) else 'No' end as EmailAttachment, ";
                        strqry = strqry + "  case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachmentStatus ";
                        strqry = strqry + "  ,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox   ";
                        strqry = strqry + "   where EmailIsDelete='False' and DeptID='" + DeptID + "' and (  ( LeadsID='0') )  and CampusID=" + CampusID + "   order by InboxID desc";
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = strqry;
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
         //------------------------------------------------------------------
        static public DataTable GetEmailSentListLead(string LeadsID, string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        string strqry = string.Empty;
                        strqry = strqry + " select SentID,EmailsentFrom,EmailSentTo,EmailSentSubject,EmailSentBody, ";
                        strqry = strqry + " case EmailSentAttachment when '1' then (isnull((select EmailSentAttachment.FileName from EmailSentAttachment ";
                        strqry = strqry + " where EmailSent.SentID=EmailSentAttachment.SentID),'Not Available')) else 'No' end as EmailAttachment, ";
                        strqry = strqry + " case EmailSentAttachment when '1' then 'Yes' else 'No' end as EmailAttachmentStatus, ";
                        strqry = strqry + " EmailSentAttachment,EmailSentDatetime ";
                        strqry = strqry + " from  EmailSent where ";
                        strqry = strqry + " EmailSentIsDelete='False' ";
                        strqry = strqry + "  and DeptID='" + DeptID + "' and LeadsID='" + LeadsID + "'   and CampusID=" + CampusID + "    order by SentID desc";

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = strqry;// "select SentID,EmailsentFrom,EmailSentTo,EmailSentSubject,EmailSentBody,EmailSentDatetime from  EmailSent where EmailSentIsDelete='False' and DeptID='" + DeptID + "' and LeadsID='" + LeadsID + "'   and CampusID=" + CampusID + "    order by SentID desc";
                      
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
        static public DataTable GetEmailSentListLead( string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = "select SentID,EmailsentFrom,EmailSentTo,EmailSentSubject,EmailSentBody,EmailSentDatetime from  EmailSent where EmailSentIsDelete='False' and DeptID='" + DeptID + "' and LeadsID='0'   and CampusID=" + CampusID + "    order by SentID desc";

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
        //===============================================================================
        static public DataTable GetDeleteEmailListLead(string LeadsID, string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = " select InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='True' and DeptID='" + DeptID + "' and LeadsID='" + LeadsID + "'  and CampusID=" + CampusID + "    order by InboxID desc";
                       
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
        static public DataTable GetDeleteEmailListLead( string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = " select InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='True' and DeptID='" + DeptID + "' and LeadsID='0'  and CampusID=" + CampusID + "    order by InboxID desc";

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
        static public DataTable GetListStudentUnreadEmailLead(string LeadsID, string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {
                        string strqry = string.Empty;
                        strqry = strqry + "  select  '' as StudentNo,InboxID,EmailBody, EmailReceived,EmailSubject, ";
                        strqry = strqry + "  case EmailAttachment when 'True' then (IsNull((select EmailAttachment.FileName from EmailAttachment  ";
                        strqry = strqry + "  where EmailInbox.messageId=EmailAttachment.messageId),'Not Available')) else 'No' end as EmailAttachment, ";
                        strqry = strqry + "  case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachmentStatus ";
                        strqry = strqry + "  ,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox   ";
                        strqry = strqry + "   where EmailIsDelete='False' and DeptID='" + DeptID + "' and (  LeadsID='" + LeadsID + "')  and EmailIsRead=0  and CampusID=" + CampusID + "   order by InboxID desc";
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = strqry;
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
        static public DataTable GetListStudentUnreadEmailLead(  string DeptID, int CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {
                        string strqry=string.Empty;
                        strqry = strqry + "  select  '' as StudentNo,InboxID,EmailBody, EmailReceived,EmailSubject, ";
                        strqry = strqry + "  case EmailAttachment when 'True' then (IsNull((select EmailAttachment.FileName from EmailAttachment  ";
                        strqry = strqry + "  where EmailInbox.messageId=EmailAttachment.messageId),'Not Available')) else 'No' end as EmailAttachment, ";
                        strqry = strqry + "  case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachmentStatus ";
                        strqry = strqry + "  ,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox   ";
                        strqry = strqry + "   where EmailIsDelete='False' and DeptID='" + DeptID + "' and (  ( LeadsID='0') )   and EmailIsRead=0  and CampusID=" + CampusID + "   order by InboxID desc";

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = strqry;
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
        static public DataTable GetViewMail(string messageId)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = "select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='False' and  messageId='" + messageId + "'   order by InboxID desc";

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
        static public DataTable GetTemplateList(string CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = " select ID,Title,(case Active when 'true' then 'Active' else 'Inactive' end) as ActiveStatus,Active from  EmailTemplates  where    CampusID='" + CampusID + "' order by ID desc";

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
        static public DataTable GetTemplateddlList(string CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = " select '0' as ID,'Choose Template' as Title  from  EmailTemplates union select ID,Title  from  EmailTemplates  where Active='true'   and  CampusID='" + CampusID + "'  ";

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
        static public DataTable getSMSTemplateList(string CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = " select ID,Title,(case Active when 'true' then 'Active' else 'Inactive' end) as ActiveStatus,Active   from EmailSMSTemplates   where CampusID='" + CampusID + "' order by ID desc";

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
        static public DataTable getSMSTemplateBodyByID(string ID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {

                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = " select  Body  from EmailSMSTemplates   where ID='" + ID + "'  ";

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
        static public DataTable getSMSTemplateddlList(string CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    using (SqlCommand CMD = new SqlCommand())
                    {
                        
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = " select '0' as ID,'Choose Template' as Title  from  EmailSMSTemplates union  select ID,Title   from EmailSMSTemplates   where CampusID='" + CampusID + "' and Active='true'  ";

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
        static public Boolean GetUpdateUnread(string InboxID,string status)
        {
            SqlConnection connection = new SqlConnection(constr);
            Boolean sstatus;
            try
            {
                using (SqlCommand CMD = new SqlCommand())
                {
                    connection.Open();
                    CMD.Connection = connection;
                    CMD.CommandType = System.Data.CommandType.Text;
                    CMD.CommandText = "update EmailInbox set EmailIsRead='" + status + "' where InboxID='" + InboxID + "'";
                    CMD.Prepare();
                    CMD.ExecuteNonQuery();
                    sstatus = true;
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                sstatus = false;
            }
            return sstatus;
        }
        static public Boolean GetDeleteStdLeadInbox(string InboxID)
        {
 
            SqlConnection connection = new SqlConnection(constr);
            Boolean sstatus;
            try
            {
                using (SqlCommand CMD = new SqlCommand())
                {
                    connection.Open();
                    CMD.Connection = connection;
                    CMD.CommandType = System.Data.CommandType.Text;
                    CMD.CommandText = " update EmailInbox set EmailIsDelete='True'  where  InboxID='" + InboxID + "'";
                    CMD.Prepare();
                    CMD.ExecuteNonQuery();
                    sstatus = true;
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                sstatus = false;
            }
            return sstatus;
        }                    
        static public Boolean SetSentEmailrecords(string StudentNo, string DeptID, string EmailSenTo, string EmailSentCC, string EmailSentBcc, string EmailSentFrom, string EmailSentSubject, string EmailSentBody, string EmailAttachment, int CampusID,string fileName)
        {
            SqlConnection connection = new SqlConnection(constr);
            Boolean status;
            try
            {
                using (SqlCommand CMD = new SqlCommand())
                {
                    connection.Open();
                    CMD.Connection = connection;
                    CMD.CommandType = System.Data.CommandType.Text;
                    CMD.CommandText = "insert into EmailSent(StudentNo,DeptID,EmailSentTo,EmailSentCC,EmailSentBcc,EmailSentFrom,EmailSentSubject,EmailSentBody,EmailSentDatetime,EmailSentAttachment,CampusID) output INSERTED.SentID values('" + StudentNo + "','" + DeptID + "','" + EmailSenTo + "','" + EmailSentCC + "','" + EmailSentBcc + "','" + EmailSentFrom + "','" + EmailSentSubject + "','" + EmailSentBody.Replace("'", " ") + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "','" + EmailAttachment + "'," + CampusID + ")";
                    CMD.Prepare();
                    //CMD.ExecuteNonQuery();

                    string SentID = Convert.ToString(CMD.ExecuteScalar());
                    status = true;
                    connection.Close();
                    if (EmailAttachment == "1")
                    {
                        connection.Open();
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = "insert into EmailSentAttachment(SentID,FileName) values('" + SentID + "','" + fileName + "')";
                        CMD.Prepare();
                        CMD.ExecuteNonQuery();
                        status = true;
                        connection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        static public Boolean SetSentEmailrecordsLead(string LeadsID, string DeptID, string EmailSenTo, string EmailSentCC, string EmailSentBcc, string EmailSentFrom, string EmailSentSubject, string EmailSentBody, string EmailAttachment, int CampusID, string fileName)
        {
            SqlConnection connection = new SqlConnection(constr);
            Boolean status;
            try
            {
                using (SqlCommand CMD = new SqlCommand())
                {
                    connection.Open();
                    CMD.Connection = connection;
                    CMD.CommandType = System.Data.CommandType.Text;
                    CMD.CommandText = "insert into EmailSent(LeadsID,DeptID,EmailSentTo,EmailSentCC,EmailSentBcc,EmailSentFrom,EmailSentSubject,EmailSentBody,EmailSentDatetime,EmailSentAttachment,CampusID) output INSERTED.SentID values('" + LeadsID + "','" + DeptID + "','" + EmailSenTo + "','" + EmailSentCC + "','" + EmailSentBcc + "','" + EmailSentFrom + "','" + EmailSentSubject + "','" + EmailSentBody.Replace("'", " ") + "','" + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "','" + EmailAttachment + "'," + CampusID + ")";
                    CMD.Prepare();
                    string SentID = Convert.ToString(CMD.ExecuteScalar());
                    status = true;
                    connection.Close();
                    if (EmailAttachment == "1")
                    {
                        connection.Open();
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = "insert into EmailSentAttachment(SentID,FileName) values('" + SentID + "','" + fileName + "')";
                        CMD.Prepare();
                        CMD.ExecuteNonQuery();
                        status = true;
                        connection.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        static public DataTable GetShowTemplateBody(string ID,string CampusID)
        {
            //string BatchDateTime = GetDateTimeStamp("spCanvasEnrollment");
            DataTable _ds = new DataTable();
            try
            {
                var connection = new SqlConnection(constr);

                //string sql = string.Format(@"SELECT  course_id,root_account,user_id,role,role_id,section_id,associated_user_id,status FROM dbo.CanvasEnrollments where ExportDateTime='"+ BatchDateTime+"'");


                using (SqlCommand CMD = new SqlCommand())
                {
                    connection.Open();
                    CMD.Connection = connection;
                    CMD.CommandType = System.Data.CommandType.Text;
                    CMD.CommandText = "select Title,Body,Active from  EmailTemplates where ID='" + ID + "' and CampusID='" + CampusID + "'";
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
        static public Boolean DeleteEmailTemplate(string ID, string CampusID)
        {
            SqlConnection connection = new SqlConnection(constr);
            Boolean status;
            try
            {
                using (SqlCommand CMD = new SqlCommand())
                {
                    connection.Open();
                    CMD.Connection = connection;
                    CMD.CommandType = System.Data.CommandType.Text;
                    CMD.CommandText = "Delete from EmailTemplates   where ID='" + ID + "' and CampusID='" + CampusID + "'";
                 
                    CMD.Prepare();
                    CMD.ExecuteNonQuery();
                    status = true;
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        static public Boolean SaveTemplateInDB(string Title, string Body, Boolean Active, string CampusID)
        {
            SqlConnection connection = new SqlConnection(constr);
            Boolean status;
            try
            {
                using (SqlCommand CMD = new SqlCommand())
                {
                    connection.Open();
                    CMD.Connection = connection;
                    CMD.CommandType = System.Data.CommandType.Text;
                    CMD.CommandText = "insert into EmailTemplates(Title,Body,Active,CampusID) values('" + Title + "','" + Body + "','" + Active + "','" + CampusID + "')";
                    CMD.Prepare();
                    CMD.ExecuteNonQuery();
                    status = true;
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        static public Boolean UpdateTemplateInDB(string NewTitle, string Body, Boolean Active, string ID)
        {
            SqlConnection connection = new SqlConnection(constr);
            Boolean status;
            try
            {
                using (SqlCommand CMD = new SqlCommand())
                {
                    connection.Open();
                    CMD.Connection = connection;
                    CMD.CommandType = System.Data.CommandType.Text;
                    CMD.CommandText = "Update EmailTemplates Set Title='" + NewTitle + "',Body='" + Body + "',Active='" + Active + "' where ID='" + ID + "'";
                    CMD.Prepare();
                    CMD.ExecuteNonQuery();
                    status = true;
                    connection.Close();
                }

            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        static public DataTable GetShowSMSTemplateBody(string ID, string CampusID)
        {
            DataTable _ds = new DataTable();
            try
            {
                var connection = new SqlConnection(constr);

                //string sql = string.Format(@"SELECT  course_id,root_account,user_id,role,role_id,section_id,associated_user_id,status FROM dbo.CanvasEnrollments where ExportDateTime='"+ BatchDateTime+"'");


                using (SqlCommand CMD = new SqlCommand())
                {
                    connection.Open();
                    CMD.Connection = connection;
                    CMD.CommandType = System.Data.CommandType.Text;
                    CMD.CommandText = "select Title,Body,Active from EmailSMSTemplates where ID='" + ID + "' and CampusID='" + CampusID + "' ";
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
        static public Boolean DeleteTemplateInDB(string ID, string CampusID)
       {
           SqlConnection connection = new SqlConnection(constr);
           Boolean status;
           try
           {
               using (SqlCommand CMD = new SqlCommand())
               {
                   connection.Open();
                   CMD.Connection = connection;
                   CMD.CommandType = System.Data.CommandType.Text;
                   CMD.CommandText = "Delete from EmailSMSTemplates   where ID='" + ID + "'  and CampusID='" + CampusID + "' ";
                   CMD.Prepare();
                   CMD.ExecuteNonQuery();
                   status = true;
                   connection.Close();
               }

           }
           catch (Exception ex)
           {
               status = false;
           }
           return status;
       }
       static public Boolean SaveSMSTemplateInDB(string Title, string Body, Boolean Active, string CampusID)
       {
           SqlConnection connection = new SqlConnection(constr);
           Boolean status;
           try
           {
               using (SqlCommand CMD = new SqlCommand())
               {
                   connection.Open();
                   CMD.Connection = connection;
                   CMD.CommandType = System.Data.CommandType.Text;
                   CMD.CommandText = "insert into EmailSMSTemplates(Title,Body,Active,CampusID) values('" + Title + "','" + Body + "','" + Active + "','" + CampusID + "')";
                   CMD.Prepare();
                   CMD.ExecuteNonQuery();
                   status = true;
                   connection.Close();
               }

           }
           catch (Exception ex)
           {
               status = false;
           }
           return status;
       }
       static public Boolean UpdateSMSTemplateInDB(string NewTitle, string Body, Boolean Active, string ID)
       {
           SqlConnection connection = new SqlConnection(constr);
           Boolean status;
           try
           {
               using (SqlCommand CMD = new SqlCommand())
               {
                   connection.Open();
                   CMD.Connection = connection;
                   CMD.CommandType = System.Data.CommandType.Text;
                   CMD.CommandText = "Update EmailSMSTemplates Set Title='" + NewTitle + "',Body='" + Body + "',Active='" + Active + "' where ID='" + ID + "'";
                   CMD.Prepare();
                   CMD.ExecuteNonQuery();
                   status = true;
                   connection.Close();
               }

           }
           catch (Exception ex)
           {
               status = false;
           }
           return status;
       }
       static public DataTable GetShowTemplateBodyByID(string ID)
       {
           //string BatchDateTime = GetDateTimeStamp("spCanvasEnrollment");
           DataTable _ds = new DataTable();
           try
           {
               var connection = new SqlConnection(constr);

               //string sql = string.Format(@"SELECT  course_id,root_account,user_id,role,role_id,section_id,associated_user_id,status FROM dbo.CanvasEnrollments where ExportDateTime='"+ BatchDateTime+"'");


               using (SqlCommand CMD = new SqlCommand())
               {
                   connection.Open();
                   CMD.Connection = connection;
                   CMD.CommandType = System.Data.CommandType.Text;
                   CMD.CommandText = "select Title,Body,Active from dbo.EmailTemplates where ID='" + ID + "'";
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
       static public DataTable GetSMSReceivedStd(string StudentNo, string DeptID, int CampusID)
       {
           DataTable _ds = new DataTable();
           try
           {
               using (SqlConnection connection = new SqlConnection(constr))
               {
                   using (SqlCommand CMD = new SqlCommand())
                   {

                       string strqry = string.Empty;

                       strqry = strqry + "  select [From],[To],Body,  ";
                       strqry = strqry + "  Status,Direction,  ";
                       strqry = strqry + "   date_sent  as date_sent,ErrorMessage   ";
                       strqry = strqry + "  from  EmailSMSLocalData   ";
                       strqry = strqry + "  where  deptid='" + DeptID + "'and (StudentNo='" + StudentNo + "' and LeadNo='0')  ";
                       strqry = strqry + "  and AccountSID  in (SELECT AccountSID FROM EmailSMSConfig WHERE CampusID='" + CampusID + "')   order by EmailSMSId desc";

                       CMD.Connection = connection;
                       CMD.CommandType = System.Data.CommandType.Text;
                       //CMD.CommandText = "select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='False' and DeptID='" + DeptID + "' and (StudentNo='0' or StudentNo='" + studentno + "') and CampusID=" + CampusID + "  order by InboxID desc";
                       CMD.CommandText = strqry;
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
       static public DataTable GetSMSReceivedLead(string LeadNo, string DeptID, int CampusID)
       {
           DataTable _ds = new DataTable();
           try
           {
               using (SqlConnection connection = new SqlConnection(constr))
               {
                   using (SqlCommand CMD = new SqlCommand())
                   {

                       string strqry = string.Empty;

                       strqry = strqry + "  select [From],[To],Body,  ";
                       strqry = strqry + "  Status,Direction,  ";
                       strqry = strqry + "   date_sent  as date_sent,ErrorMessage   ";
                       strqry = strqry + "  from  EmailSMSLocalData   ";
                       strqry = strqry + "  where  deptid='" + DeptID + "'and (StudentNo='0' and LeadNo='" + LeadNo + "')  ";
                       strqry = strqry + "  and AccountSID  in (SELECT AccountSID FROM EmailSMSConfig WHERE CampusID='" + CampusID + "') order by EmailSMSId desc  ";

                       CMD.Connection = connection;
                       CMD.CommandType = System.Data.CommandType.Text;
                       //CMD.CommandText = "select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='False' and DeptID='" + DeptID + "' and (StudentNo='0' or StudentNo='" + studentno + "') and CampusID=" + CampusID + "  order by InboxID desc";
                       CMD.CommandText = strqry;
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
       static public DataTable GetSMSReceivedCount(string DeptID, int CampusID)
       {
           DataTable _ds = new DataTable();
           try
           {
               using (SqlConnection connection = new SqlConnection(constr))
               {
                   using (SqlCommand CMD = new SqlCommand())
                   {

                       string strqry = string.Empty;

                       strqry = strqry + "  select count(*) as Rowcounts ";
                       strqry = strqry + "  from  EmailSMSLocalData   ";
                       strqry = strqry + "  where  deptid='" + DeptID + "'and (StudentNo='0' and LeadNo='0')   and Isread='0'  ";
                       strqry = strqry + "  and AccountSID  in (SELECT AccountSID FROM EmailSMSConfig WHERE CampusID='" + CampusID + "')   ";

                       CMD.Connection = connection;
                       CMD.CommandType = System.Data.CommandType.Text;
                       //CMD.CommandText = "select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='False' and DeptID='" + DeptID + "' and (StudentNo='0' or StudentNo='" + studentno + "') and CampusID=" + CampusID + "  order by InboxID desc";
                       CMD.CommandText = strqry;
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
       static public DataTable GetSMSReceived(string DeptID, int CampusID)
       {
           DataTable _ds = new DataTable();
           try
           {
               using (SqlConnection connection = new SqlConnection(constr))
               {
                   using (SqlCommand CMD = new SqlCommand())
                   {

                       string strqry = string.Empty;

                       strqry = strqry + "  select EmailSMSId,[From],[To],Body,  ";
                       strqry = strqry + "  Status,Direction,  ";
                       strqry = strqry + "  date_sent as date_sent,ErrorMessage,IsRead   ";
                       strqry = strqry + "  from  EmailSMSLocalData   ";
                       strqry = strqry + "  where  deptid='" + DeptID + "'and (StudentNo='0' and LeadNo='0')  ";
                       strqry = strqry + "  and AccountSID  in (SELECT AccountSID FROM EmailSMSConfig WHERE CampusID='" + CampusID + "')  order by EmailSMSId desc ";

                       CMD.Connection = connection;
                       CMD.CommandType = System.Data.CommandType.Text;
                       //CMD.CommandText = "select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='False' and DeptID='" + DeptID + "' and (StudentNo='0' or StudentNo='" + studentno + "') and CampusID=" + CampusID + "  order by InboxID desc";
                       CMD.CommandText = strqry;
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
       static public DataTable GetSMSSent(string DeptID, int CampusID)
       {
           DataTable _ds = new DataTable();
           try
           {
               using (SqlConnection connection = new SqlConnection(constr))
               {
                   using (SqlCommand CMD = new SqlCommand())
                   {

                       string strqry = string.Empty;

                       strqry = strqry + "  select [From],[To],Body,  ";
                       strqry = strqry + "  Status,Direction,  ";
                       strqry = strqry + "  date_sent as date_sent,ErrorMessage   ";
                       strqry = strqry + "  from  EmailSMSLocalData   ";
                       strqry = strqry + "  where (Status='delivered' or Status='Sent') and deptid='" + DeptID + "'and (StudentNo='0' and LeadNo='0')  ";
                       strqry = strqry + "  and AccountSID  in (SELECT AccountSID FROM EmailSMSConfig WHERE CampusID='" + CampusID + "') order by EmailSMSId desc  ";

                       CMD.Connection = connection;
                       CMD.CommandType = System.Data.CommandType.Text;
                       //CMD.CommandText = "select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='False' and DeptID='" + DeptID + "' and (StudentNo='0' or StudentNo='" + studentno + "') and CampusID=" + CampusID + "  order by InboxID desc";
                       CMD.CommandText = strqry;
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
       static public DataTable GetSMSSentStd(string StudentNo, string DeptID, int CampusID)
       {
           DataTable _ds = new DataTable();
           try
           {
               using (SqlConnection connection = new SqlConnection(constr))
               {
                   using (SqlCommand CMD = new SqlCommand())
                   {

                       string strqry = string.Empty;

                       strqry = strqry + "  select [From],[To],Body,  ";
                       strqry = strqry + "  Status,Direction ";
                   
                       strqry = strqry + "  ,  date_sent  as date_sent,ErrorMessage   ";
                       strqry = strqry + "  from  EmailSMSLocalData   ";
                       strqry = strqry + "  where (Status='delivered' or Status='Sent') and deptid='" + DeptID + "'and (StudentNo='" + StudentNo + "' and LeadNo='0')  ";
                       strqry = strqry + "  and AccountSID  in (SELECT AccountSID FROM EmailSMSConfig WHERE CampusID='" + CampusID + "') order by EmailSMSId desc  ";

                       CMD.Connection = connection;
                       CMD.CommandType = System.Data.CommandType.Text;
                       //CMD.CommandText = "select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='False' and DeptID='" + DeptID + "' and (StudentNo='0' or StudentNo='" + studentno + "') and CampusID=" + CampusID + "  order by InboxID desc";
                       CMD.CommandText = strqry;
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
       static public DataTable GetSMSSentLead(string LeadNo, string DeptID, int CampusID)
       {
           DataTable _ds = new DataTable();
           try
           {
               using (SqlConnection connection = new SqlConnection(constr))
               {
                   using (SqlCommand CMD = new SqlCommand())
                   {

                       string strqry = string.Empty;

                       strqry = strqry + "  select [From],[To],Body,  ";
                       strqry = strqry + "  Status,Direction,  ";
                       strqry = strqry + "   date_sent  as date_sent,ErrorMessage   ";
                       strqry = strqry + "  from  EmailSMSLocalData   ";
                       strqry = strqry + "  where (Status='delivered' or Status='Sent') and deptid='" + DeptID + "'and (StudentNo='0' and LeadNo='" + LeadNo + "')  ";
                       strqry = strqry + "  and AccountSID  in (SELECT AccountSID FROM EmailSMSConfig WHERE CampusID='" + CampusID + "')  order by EmailSMSId desc ";

                       CMD.Connection = connection;
                       CMD.CommandType = System.Data.CommandType.Text;
                       //CMD.CommandText = "select   StudentNo, InboxID,EmailBody, EmailReceived,EmailSubject,case EmailAttachment when 'True' then 'Yes' else 'No' end as EmailAttachment,EmailReceivedDatetime,EmailIsRead,messageId from EmailInbox  where EmailIsDelete='False' and DeptID='" + DeptID + "' and (StudentNo='0' or StudentNo='" + studentno + "') and CampusID=" + CampusID + "  order by InboxID desc";
                       CMD.CommandText = strqry;
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
       static public DataTable GetSMSLongCodeList(string CampusID, string DeptID)
       {

           DataTable _ds = new DataTable();
           try
           {
               var connection = new SqlConnection(constr);

               using (SqlCommand CMD = new SqlCommand())
               {
                   string strqry=string.Empty;
                   strqry=strqry+ " select '0' As ID,'Choose Sender' As LongCode from EmailSMSConfig  union   select ID,LongCode from EmailSMSConfig where ";
                   strqry = strqry + " Active='True' and (deleteFlag is null or  deleteFlag<>'1') and CampusID='" + CampusID + "' and DeptID='" + DeptID + "'  ";
                   connection.Open();
                   CMD.Connection = connection;
                   CMD.CommandType = System.Data.CommandType.Text;
                   CMD.CommandText = strqry;
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
      static public DataTable GetSMSLongCodeFullList(string CampusID, String ID)
      {

          DataTable _ds = new DataTable();
          try
          {
              var connection = new SqlConnection(constr);

              using (SqlCommand CMD = new SqlCommand())
              {
                  connection.Open();
                  CMD.Connection = connection;
                  CMD.CommandType = System.Data.CommandType.Text;
                  CMD.CommandText = "select * from  EmailSMSConfig where Active='True' and CampusID='" + CampusID + "' and ID='"+ ID +"'   order by ID desc";
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
        //Update 8 aug 2016
      static public Boolean ReadSMSAndSaveDatabase(string AccountSid ,string  DateSent ,string  Sid ,string  To ,string  From ,string Body ,string  Status ,string  Direction ,string  ErrorCode ,string  ErrorMessage ,string  DateCreated ,string  DateUpdated ,string  StudentNo ,string  LeadNo ,string  DeptId )
      {
          SqlConnection connection = new SqlConnection(constr);
          Boolean status;
          try
          {
              using (SqlCommand CMD = new SqlCommand())
              {

                  connection.Open();
                  CMD.Connection = connection;
                  CMD.CommandType = System.Data.CommandType.Text;
                  CMD.CommandText = "INSERT INTO EmailSMSLocalData([AccountSid],Date_Sent,[sid],[to],[from],[body],[status],[direction],[ErrorCode],[ErrorMessage],DateCreated,DateUpdated,StudentNo,LeadNo,DeptId,IsRead) VALUES('" + AccountSid + "','" + DateSent + "','" + Sid + "','" + To + "','" + From + "','" + Body + "','" + Status + "','" + Direction + "','" + ErrorCode + "','" + ErrorMessage + "','" + DateCreated + "','" + DateUpdated + "','" + StudentNo + "','" + LeadNo + "','" + DeptId + "','1')";
                  CMD.Prepare();
                  CMD.ExecuteNonQuery();
                  status = true;
                  connection.Close();
              }

          }
          catch (Exception ex)
          {
              status = false;
          }
          return status;
      }
      static public Boolean GetUpdateSMSStatus(string EmailSMSId, string status)
      {
          SqlConnection connection = new SqlConnection(constr);
          Boolean sstatus;
          try
          {
              using (SqlCommand CMD = new SqlCommand())
              {
                  connection.Open();
                  CMD.Connection = connection;
                  CMD.CommandType = System.Data.CommandType.Text;
                  CMD.CommandText = "update EmailSMSLocalData set IsRead='" + status + "' where EmailSMSId='" + EmailSMSId + "'";
                  CMD.Prepare();
                  CMD.ExecuteNonQuery();
                  sstatus = true;
                  connection.Close();
              }

          }
          catch (Exception ex)
          {
              sstatus = false;
          }
          return sstatus;
      }

    #endregion

        #region "Login"
      static public DataTable GetUserLogin(string Username, string Password,string CampusID)
      {
          DataTable _ds = new DataTable();
          try
          {
              using (SqlConnection connection = new SqlConnection(constr))
              {
                  using (SqlCommand CMD = new SqlCommand())
                  {

                      CMD.Connection = connection;
                      CMD.CommandType = System.Data.CommandType.Text;
                      CMD.CommandText = "SELECT * FROM MSTUSER WHERE Username='" + Username + "' AND Password='" + Password + "' and (CampusId='" + CampusID + "' OR user_level='99')";

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
      static public DataTable GetUserLogin(string Username, string Password)
      {
          DataTable _ds = new DataTable();
          try
          {
              using (SqlConnection connection = new SqlConnection(constr))
              {
                  using (SqlCommand CMD = new SqlCommand())
                  {

                      CMD.Connection = connection;
                      CMD.CommandType = System.Data.CommandType.Text;
                      CMD.CommandText = "SELECT * FROM MSTUSER WHERE Username='" + Username + "' AND Password='" + Password + "'";

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
        #endregion

      #region "menubind for All"
      static public DataTable UserMenuBind(string Role_Id)
      {
          DataTable _ds = new DataTable();
          try
          {
              using (SqlConnection connection = new SqlConnection(constr))
              {
                  using (SqlCommand CMD = new SqlCommand())
                  {

                      string sqlQRY = string.Empty;
                      sqlQRY += " select M.Menu_ID,M.Menu,M.ParentID,M.link,M.MobileView from mstmenu M inner join mstuserright UR ";
                      sqlQRY += " on m.menu_id=UR.menu_id and UR.Role_Id='" + Role_Id + "'   where Active='True' AND  UR.show_menu='TRUE'  ";
                      sqlQRY += " order by Sno asc ";
                      CMD.Connection = connection;
                      CMD.CommandType = System.Data.CommandType.Text;
                      CMD.CommandText = sqlQRY;

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

      static public DataTable UserMenuBind(string Role_Id, string IsAdmin)
      {
          DataTable _ds = new DataTable();
          try
          {
              using (SqlConnection connection = new SqlConnection(constr))
              {
                  using (SqlCommand CMD = new SqlCommand())
                  {

                      string sqlQRY = string.Empty;
                      sqlQRY += " select M.Menu_ID,M.Menu,M.ParentID,M.link,M.MobileView from mstmenu M inner join mstuserright UR ";
                      sqlQRY += " on m.menu_id=UR.menu_id and UR.Role_Id='" + Role_Id + "'   where Active='True' AND  UR.show_menu='TRUE' AND (M.IsAdmin is null OR  M.IsAdmin='" + IsAdmin + "' ) ";
                      sqlQRY += " order by Sno asc ";
                      CMD.Connection = connection;
                      CMD.CommandType = System.Data.CommandType.Text;
                      CMD.CommandText = sqlQRY;

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

      #endregion

        #region "Add Role"
      static public DataTable GetRole(string CampusID)
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(constr))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {

                          string strqry = string.Empty;
                          strqry=strqry+" select MS.Role_Id,MS.Role,MS.CampusID,ECM.CampusName from mstuserrole MS inner join EmailCampusMaster  ECM on ";
                          strqry = strqry + " ECM.CampusID=MS.CampusID where ECM.Active='1' ";
                          if (CampusID != "")
                          {
                              strqry = strqry + " and MS.CampusID='" + CampusID + "' and MS.Role_Id<>'99'";
                          }
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
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
          static public DataTable GetCampusUserRole()
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(constr))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {

                          string strqry = string.Empty;
                          strqry = strqry + "  select '0' As CampusID,'Select Campus' As CampusName from  EmailCampusMaster  union ";
                          strqry = strqry + "  select CampusID,CampusName from  EmailCampusMaster where  Active='1'  order by CampusID ";
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
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
          static public Boolean CheckRole(string Role_Id, string CampusID, string Role, string chktype)
          {
              Boolean result = true;
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(constr))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {
                          string strqry = string.Empty;

                          if (chktype == "Role_Id")
                          {
                              strqry += " select count(Role) from  mstuserrole where Role_Id='" + Role_Id + "' ";
                          }
                           
                          CMD.Connection = connection;
                          CMD.Connection.Open();
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
                          CMD.Prepare();
                          int Rcount = 0;
                          Rcount=Convert.ToInt32(CMD.ExecuteScalar());
                          CMD.Connection.Close();
                          if (Rcount > 0)
                          {
                              result = false;     
                          }
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

              return result;
          }
          static public DataTable GetRoleById(string Role_Id, string CampusID)
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(constr))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {

                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = " select Role_Id,Role,CampusID from  mstuserrole where Role_Id='" + Role_Id + "' and CampusID='" + CampusID + "' ";
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
          static public Boolean SaveAddRoleDB(string Role_Id, string Role, string CampusID)
          {
              SqlConnection connection = new SqlConnection(constr);
              Boolean status;
              try
              {
                  using (SqlCommand CMD = new SqlCommand())
                  {
                      connection.Open();
                      CMD.Connection = connection;
                      CMD.CommandType = System.Data.CommandType.Text;
                      CMD.CommandText = "insert into mstuserrole(Role_Id,Role,CampusID) values('" + Role_Id + "','" + Role + "','" + CampusID + "')";
                      CMD.Prepare();
                      CMD.ExecuteNonQuery();
                      status = true;
                      connection.Close();

                   
                  }

              }
              catch (Exception ex)
              {
                  status = false;
              }
              return status;
          }
          static public Boolean UpdateAddRoleDB(string Role_Id, string Role, string CampusID,string updateCampuID)
          {
              SqlConnection connection = new SqlConnection(constr);
              Boolean status;
              try
              {
                  using (SqlCommand CMD = new SqlCommand())
                  {
                      connection.Open();
                      CMD.Connection = connection;
                      CMD.CommandType = System.Data.CommandType.Text;
                      CMD.CommandText = " update  mstuserrole set  Role='" + Role + "' ,CampusID='" + CampusID + "' where Role_Id='" + Role_Id + "' ";
                      CMD.Prepare();
                      CMD.ExecuteNonQuery();
                      status = true;
                      connection.Close();


                  }

              }
              catch (Exception ex)
              {
                  status = false;
              }
              return status;
          }
          static public Boolean DeleteAddRoleDB(string Role_Id)
          {
              SqlConnection connection = new SqlConnection(constr);
              Boolean status;
              try
              {
                  using (SqlCommand CMD = new SqlCommand())
                  {
                      connection.Open();
                      CMD.Connection = connection;
                      CMD.CommandType = System.Data.CommandType.Text;
                      CMD.CommandText = "Delete from mstuserrole   where Role_Id='" + Role_Id + "' ";

                      CMD.Prepare();
                      CMD.ExecuteNonQuery();
                      status = true;
                      connection.Close();
                  }

              }
              catch (Exception ex)
              {
                  status = false;
              }
              return status;
          }
        #endregion
        #region "Manage User Rights"
          static public DataTable GetRoleUserRight(string CampusID)
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(constr))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {

                          string strqry = string.Empty;
                          strqry = strqry + " select MS.Role_Id,MS.Role  As Role,MS.CampusID,ECM.CampusName from mstuserrole MS inner join EmailCampusMaster  ECM on ";
                          strqry = strqry + " ECM.CampusID=MS.CampusID where ECM.Active='1' ";
                          if (CampusID != "")
                          {
                              strqry = strqry + " and MS.CampusID='" + CampusID + "' and MS.Role_Id<>'99'";
                          }
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
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
          static public DataTable GetUserMenu(string Role_Id, string user_level)
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(constr))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {

                          string strqry = string.Empty;
                          if (user_level == "99")
                          {
                              strqry = strqry + " select MM.menu_id,MM.menu, ";
                              strqry = strqry + " isnull((select  MUR.show_menu from mstuserright As MUR  where MUR.menu_id=MM.menu_id and MUR.Role_Id='" + Role_Id + "'   ),'0') as show_menu  ";
                              strqry = strqry + " from  mstmenu As MM ";
                          }
                          else
                          {
                              strqry = strqry + " select MM.menu_id,MM.menu, ";
                              strqry = strqry + " isnull((select  MUR.show_menu from mstuserright As MUR  where MUR.menu_id=MM.menu_id and MUR.Role_Id='" + Role_Id + "'   ),'0') as show_menu  ";
                              strqry = strqry + " from  mstmenu As MM where (MM.menu_id<>'10' and MM.parentid<>'10') ";
                          }
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
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

          static public Boolean SaveUserRight(string Role_Id, string menu_id, string show_menu)
          {
              SqlConnection connection = new SqlConnection(constr);
              Boolean status;
              try
              {
                  using (SqlCommand CMD = new SqlCommand())
                  {
                        string strqry=string.Empty;
                        strqry = strqry + " IF not exists(select distinct * from  dbo.mstuserright  where  Role_id='" + Role_Id + "' and menu_id='" + menu_id + "')  ";
                        strqry=strqry+" BEGIN  ";
                        strqry=strqry+"   insert into mstuserright (Role_Id ,menu_id,show_menu )   ";
                        strqry = strqry + "   values('" + Role_Id + "' ,'" + menu_id + "','" + show_menu + "' )  ";
                        strqry=strqry+" END  ";
                        strqry=strqry+" ELSE  ";
                        strqry=strqry+" BEGIN  ";
                        strqry = strqry + "  Update mstuserright set menu_id='" + menu_id + "',show_menu='" + show_menu + "'  ";
                        strqry = strqry + "  where  Role_id='" + Role_Id + "' and menu_id='" + menu_id + "'  ";
                        strqry = strqry + " END  ";


                        connection.Open();
                        CMD.Connection = connection;
                        CMD.CommandType = System.Data.CommandType.Text;
                        CMD.CommandText = strqry;
                        CMD.Prepare();
                        CMD.ExecuteNonQuery();
                        status = true;
                        connection.Close();


                  }

              }
              catch (Exception ex)
              {
                  status = false;
              }
              return status;
          }
         
        #endregion

        #region "Create New User"
          static public DataTable FillRoleCreateUser(string CampusID)
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(constr))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {

                          string strqry=string.Empty;
                          
                          strqry = strqry + "  select '0' As Role_Id,'Select level' As Role from  mstuserrole  union  ";
                          strqry=strqry+"   select Role_Id,Role from  mstuserrole ";
                              strqry = strqry + "  where CampusID='" + CampusID + "'";
                          
                          /* if (CampusName == "0") { } else if (CampusName != "DSIS") { strqry = strqry + " where Role_Id !=99 ";}strqry = strqry + " where Role_Id !=99 ";*/
                          strqry = strqry + "order by Role_Id ";
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
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
          static public DataTable FillCampusCreateUser()
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(constr))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {

                          string strqry = string.Empty;
                          strqry = strqry + "  select '0' As CampusID,'Select Campus' As CampusName  from  EmailCampusMaster  union ";
                          strqry = strqry + "  select CampusID,CampusName from  EmailCampusMaster  where Active='1' order by CampusID ";
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
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
          static public DataTable FillCampusCreateUserFilter()
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(constr))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {

                          string strqry = string.Empty;
                          strqry = strqry + "  select '0' As CampusID,'Select Campus' As CampusName from  EmailCampusMaster  union ";
                          strqry = strqry + "  select CampusID,CampusName from  EmailCampusMaster where CampusName!='DSIS' and   Active='1'  order by CampusID ";
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
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

          static public Boolean InsertUser(string Username, string Password, string User_level, string UserStatus, string CampusId, string SwitchDept)

          {
              SqlConnection connection = new SqlConnection(constr);
              Boolean status;
              try
              {
                  using (SqlCommand CMD = new SqlCommand())
                  {
                      string strQuery = "";
                      strQuery = " INSERT INTO mstuser(Username,Password,User_level,CreatedOn,UserStatus,CampusId,SwitchDept) ";
                      strQuery = strQuery + "VALUES('" + Username + "','" + Password + "','" + User_level + "',getdate(),'" + UserStatus + "','" + CampusId + "','" + SwitchDept + "') ";
                      connection.Open();
                      CMD.Connection = connection;
                      CMD.CommandType = System.Data.CommandType.Text;
                      CMD.CommandText = strQuery;
                      CMD.Prepare();
                      CMD.ExecuteNonQuery();
                      status = true;
                      connection.Close();
                  }

              }
              catch (Exception ex)
              {
                  status = false;
              }
              return status;
          }

          static public Boolean UpdateUser(string UserID, string Username, string Password, string User_level, string UserStatus, string CampusId, string SwitchDept)
          {
              SqlConnection connection = new SqlConnection(constr);
              Boolean status;
              try
              {
                  using (SqlCommand CMD = new SqlCommand())
                  {
                      string strQuery = "";
                      strQuery = " UPDATE  mstuser SET Username='" + Username + "' , User_level='" + User_level + "' ,";
                      if (Password != string.Empty)
                      {
                          strQuery = strQuery + " Password='" + Password + "',";
                      }
                      
                      strQuery = strQuery + "   UserStatus='" + UserStatus + "' , CampusId='" + CampusId + "' , SwitchDept='" + SwitchDept + "' ";
                      strQuery = strQuery + "  where UserID='" + UserID + "'";
                      connection.Open();
                      CMD.Connection = connection;
                      CMD.CommandType = System.Data.CommandType.Text;
                      CMD.CommandText = strQuery;
                      CMD.Prepare();
                      CMD.ExecuteNonQuery();
                      status = true;
                      connection.Close();
                  }

              }
              catch (Exception ex)
              {
                  status = false;
              }
              return status;
          }

        #endregion
        #region "Manage User"
          static public DataTable GetUserList(string CampusId)
          {
              DataTable _ds = new DataTable();
              try
              {
                  using (SqlConnection connection = new SqlConnection(constr))
                  {
                      using (SqlCommand CMD = new SqlCommand())
                      {
                          string strqry = string.Empty;
                          strqry = strqry + "  SELECT MU.UserID,MU.Username,MU.UserStatus,MU.SwitchDept,MU.Password,MU.User_level,MU.CreatedOn,MU.UserStatus,MU.CampusId,ECM.CampusName,MUR.Role  FROM mstuser as MU ";
                          strqry = strqry + "  inner join mstUserrole MUR on  MUR.role_id=MU.User_level  ";
                          strqry = strqry + "  inner join EmailCampusMaster ECM on MU.CampusId=ECM.CampusID where   ECM.Active='1' and MU.DeleteFlag is null ";
                          if(CampusId!="")
                          {
                              strqry = strqry + "  and MU.CampusId='" + CampusId + "'";
                          }
                          strqry = strqry + "  order by MU.UserID desc ";
                          CMD.Connection = connection;
                          CMD.CommandType = System.Data.CommandType.Text;
                          CMD.CommandText = strqry;
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
          static public Boolean GetDeleteUser(string UserID)
          {

              SqlConnection connection = new SqlConnection(constr);
              Boolean sstatus;
              try
              {
                  using (SqlCommand CMD = new SqlCommand())
                  {
                      connection.Open();
                      CMD.Connection = connection;
                      CMD.CommandType = System.Data.CommandType.Text;
                      CMD.CommandText = " update mstuser set DeleteFlag='D'  where  UserID='" + UserID + "'";
                      CMD.Prepare();
                      CMD.ExecuteNonQuery();
                      sstatus = true;
                      connection.Close();
                  }

              }
              catch (Exception ex)
              {
                  sstatus = false;
              }
              return sstatus;
          }
          static public Boolean GetUpdateUserStatus(string UserID, string UserStatus)
          {
              SqlConnection connection = new SqlConnection(constr);
              Boolean sstatus;
              try
              {
                  using (SqlCommand CMD = new SqlCommand())
                  {
                      string status=string.Empty;
                      status="0";
                      if(UserStatus=="true")
                      {
                        status="1";
                      }
                      connection.Open();
                      CMD.Connection = connection;
                      CMD.CommandType = System.Data.CommandType.Text;

                      CMD.CommandText = " update mstuser set UserStatus='" + status + "'  where  UserID='" + UserID + "'";
                    
                      CMD.Prepare();
                      CMD.ExecuteNonQuery();
                      sstatus = true;
                      connection.Close();
                  }

              }
              catch (Exception ex)
              {
                  sstatus = false;
              }
              return sstatus;
          }
        #endregion

        #region "Email Configuration"
                 static public DataTable GetEmailconfiguration( )
                  {
                      DataTable _ds = new DataTable();
                      try
                      {
                          using (SqlConnection connection = new SqlConnection(constr))
                          {
                              using (SqlCommand CMD = new SqlCommand())
                              {
                                  string strqry = string.Empty;
                                  strqry = strqry + "  SELECT EC.ID,EC.CampusID,EC.DeptName,EC.Status,EC.Department,EC.DeptEmail,EC.Pass,EC.SMTP,EC.PortOut,EC.Pop3,EC.PortIn,EC.SSL, ";
                                  strqry = strqry + "  ECM.CampusName ";
                                  strqry = strqry + "   FROM EmailConfig as EC ";
                                  strqry = strqry + "  inner join EmailCampusMaster ECM on EC.CampusID=ECM.CampusID  where  ECM.Active='1' and  EC.DeleteFlag is null order by EC.ID desc  ";
                                 
                                  CMD.Connection = connection;
                                  CMD.CommandType = System.Data.CommandType.Text;
                                  CMD.CommandText = strqry;
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
                 static public Boolean GetUpdateEmailConfigStatus(string EmailConfigId, string Status)
                 {
                     SqlConnection connection = new SqlConnection(constr);
                     Boolean sstatus;
                     try
                     {
                         using (SqlCommand CMD = new SqlCommand())
                         {
                             string status = string.Empty;
                             status = "0";
                             if (Status == "true")
                             {
                                 status = "1";
                             }
                             connection.Open();
                             CMD.Connection = connection;
                             CMD.CommandType = System.Data.CommandType.Text;
                             CMD.CommandText = " update EmailConfig set Status='" + status + "'  where  ID='" + EmailConfigId + "'";
                             CMD.Prepare();
                             CMD.ExecuteNonQuery();
                             sstatus = true;
                             connection.Close();
                         }
                     }
                     catch (Exception ex)
                     {
                         sstatus = false;
                     }
                     return sstatus;
                 }
                 static public Boolean dELETEEmailConfig(string UserID)
                 {

                     SqlConnection connection = new SqlConnection(constr);
                     Boolean sstatus;
                     try
                     {
                         using (SqlCommand CMD = new SqlCommand())
                         {
                             connection.Open();
                             CMD.Connection = connection;
                             CMD.CommandType = System.Data.CommandType.Text;
                             CMD.CommandText = " update EmailConfig set DeleteFlag='D'  where  ID='" + UserID + "'";
                             CMD.Prepare();
                             CMD.ExecuteNonQuery();
                             sstatus = true;
                             connection.Close();
                         }

                     }
                     catch (Exception ex)
                     {
                         sstatus = false;
                     }
                     return sstatus;
                 }
                 static public Boolean InsertEmailConfig(string CampusID, string DeptNameId, string DeptEmail, string Pass, string SMTP, string PortOut, string Pop3, string PortIn, Boolean SSL, string Status, string departmentname)
                 {
                     SqlConnection connection = new SqlConnection(constr);
                     Boolean status;
                     try
                     {
                         using (SqlCommand CMD = new SqlCommand())
                         {
                             string strQuery = "";
                             strQuery = " INSERT INTO EmailConfig(CampusID , DeptName , DeptEmail , Pass , SMTP , PortOut , Pop3 , PortIn , SSL ,Status ,Department ) ";
                             strQuery = strQuery + "VALUES('" + CampusID + "','" + DeptNameId + "','" + DeptEmail + "','" + Pass + "','" + SMTP + "','" + PortOut + "','" + Pop3 + "','" + PortIn + "','" + SSL + "','" + Status + "','" + departmentname + "') ";
                             connection.Open();
                             CMD.Connection = connection;
                             CMD.CommandType = System.Data.CommandType.Text;
                             CMD.CommandText = strQuery;
                             CMD.Prepare();
                             CMD.ExecuteNonQuery();
                             status = true;
                             connection.Close();
                         }

                     }
                     catch (Exception ex)
                     {
                         status = false;
                     }
                     return status;
                 }
                 static public Boolean UpdateEmailConfig(string ID, string CampusID, string DeptNameid, string DeptEmail, string Pass, string SMTP, string PortOut, string Pop3, string PortIn, Boolean SSL, string Status, string Department)
                 {
                     SqlConnection connection = new SqlConnection(constr);
                     Boolean status;
                     try
                     {
                         using (SqlCommand CMD = new SqlCommand())
                         {
                             string strQuery = "";
                             strQuery = " UPDATE  EmailConfig SET CampusID='" + CampusID + "' , DeptName='" + DeptNameid + "' , DeptEmail='" + DeptEmail + "' ,";
                             strQuery = strQuery + "   Pass='" + Pass + "' , SMTP='" + SMTP + "' , PortOut='" + PortOut + "' , Pop3='" + Pop3 + "' , PortIn='" + PortIn + "' , SSL='" + SSL + "', ";
                             strQuery = strQuery + " Status='" + Status + "' , Department='" + Department + "' where ID='" + ID + "'";
                             connection.Open();
                             CMD.Connection = connection;
                             CMD.CommandType = System.Data.CommandType.Text;
                             CMD.CommandText = strQuery;
                             CMD.Prepare();
                             CMD.ExecuteNonQuery();
                             status = true;
                             connection.Close();
                         }

                     }
                     catch (Exception ex)
                     {
                         status = false;
                     }
                     return status;
                 }
       #endregion

        #region "Campus Master"
           static public DataTable GetCampusMasterList( )
                  {
                      DataTable _ds = new DataTable();
                      try
                      {
                          using (SqlConnection connection = new SqlConnection(constr))
                          {
                              using (SqlCommand CMD = new SqlCommand())
                              {
                                  string strqry = string.Empty;
                                  strqry = strqry + "  SELECT CampusID , CampusCode , CampusName , CampusConStr , Clientlogo , Active   FROM   EmailCampusMaster ";
                                  strqry = strqry + "  where CampusName!='DSIS' ";
                                  CMD.Connection = connection;
                                  CMD.CommandType = System.Data.CommandType.Text;
                                  CMD.CommandText = strqry;
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
           static public Boolean ActiveDeactiveCampusMaster(string CampusID, string Status)
                 {
                     SqlConnection connection = new SqlConnection(constr);
                     Boolean sstatus;
                     try
                     {
                         using (SqlCommand CMD = new SqlCommand())
                         {
                             string status = string.Empty;
                             status = "0";
                             if (Status == "true")
                             {
                                 status = "1";
                             }
                             connection.Open();
                             CMD.Connection = connection;
                             
                                 
                             CMD.CommandType = System.Data.CommandType.Text;
                             CMD.CommandText = " update EmailCampusMaster set Active='" + status + "'  where  CampusID='" + CampusID + "'";
                             CMD.Prepare();
                             CMD.ExecuteNonQuery();
                             sstatus = true;
                             connection.Close();
                         }
                     }
                     catch (Exception ex)
                     {
                         sstatus = false;
                     }
                     return sstatus;
                 }


           static public Boolean InsertCampusMaster(string CampusCode, string CampusName, string CampusConStr, string Clientlogo, string Active)
                 {
                     SqlConnection connection = new SqlConnection(constr);
                     Boolean status;
                     try
                     {
                         using (SqlCommand CMD = new SqlCommand())
                         {
                             string strQuery = "";
                             strQuery = " INSERT INTO EmailCampusMaster( CampusCode , CampusName , CampusConStr , Clientlogo , Active ) ";
                             strQuery = strQuery + "VALUES('" + CampusCode + "','" + CampusName + "','" + CampusConStr + "','" + Clientlogo + "','" + Active + "') ";
                             connection.Open();
                             CMD.Connection = connection;
                             CMD.CommandType = System.Data.CommandType.Text;
                             CMD.CommandText = strQuery;
                             CMD.Prepare();
                             CMD.ExecuteNonQuery();
                             status = true;
                             connection.Close();
                         }

                     }
                     catch (Exception ex)
                     {
                         status = false;
                     }
                     return status;
                 }
           static public Boolean UpdateCampusMaster(string CampusID, string CampusCode, string CampusName, string CampusConStr, string Clientlogo, string Active)
                 {
                     SqlConnection connection = new SqlConnection(constr);
                     Boolean status;
                     try
                     {
                        
                         using (SqlCommand CMD = new SqlCommand())
                         {
                             string strQuery = "";
                             strQuery = " UPDATE  EmailCampusMaster SET CampusCode='" + CampusCode + "' , CampusName='" + CampusName + "' , CampusConStr='" + CampusConStr + "' ,";
                             strQuery = strQuery + " Active='" + Active + "' where CampusID='" + CampusID + "'";
                             connection.Open();
                             CMD.Connection = connection;
                             CMD.CommandType = System.Data.CommandType.Text;
                             CMD.CommandText = strQuery;
                             CMD.Prepare();
                             CMD.ExecuteNonQuery();
                             status = true;
                             connection.Close();
                         }

                     }
                     catch (Exception ex)
                     {
                         status = false;
                     }
                     return status;
                 }
   
        
        #endregion

        #region "Campus Logo Master"
           static public DataTable GetCampusLogoMasterList()
           {
               DataTable _ds = new DataTable();
               try
               {
                   using (SqlConnection connection = new SqlConnection(constr))
                   {
                       using (SqlCommand CMD = new SqlCommand())
                       {
                           string strqry = string.Empty;
                           strqry = strqry + "  SELECT CampusID , CampusCode , CampusName , ";
                           strqry = strqry + "    Clientlogo   FROM   EmailCampusMaster ";
                           strqry = strqry + "  where CampusName!='DSIS' ";
                           CMD.Connection = connection;
                           CMD.CommandType = System.Data.CommandType.Text;
                           CMD.CommandText = strqry;
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

           static public Boolean UpdateCampuslogoMaster(string CampusID,string Clientlogo)
           {
               SqlConnection connection = new SqlConnection(constr);
               Boolean status;
               try
               {

                   using (SqlCommand CMD = new SqlCommand())
                   {
                       string strQuery = "";
                       strQuery = " UPDATE  EmailCampusMaster SET ";
                       strQuery = strQuery + " Clientlogo='" + Clientlogo + "' where CampusID='" + CampusID + "'";
                       connection.Open();
                       CMD.Connection = connection;
                       CMD.CommandType = System.Data.CommandType.Text;
                       CMD.CommandText = strQuery;
                       CMD.Prepare();
                       CMD.ExecuteNonQuery();
                       status = true;
                       connection.Close();
                   }

               }
               catch (Exception ex)
               {
                   status = false;
               }
               return status;
           }

           static public DataTable GetCampusList()
           {
               DataTable _ds = new DataTable();
               try
               {
                   using (SqlConnection connection = new SqlConnection(constr))
                   {
                       using (SqlCommand CMD = new SqlCommand())
                       {
                           string strqry = string.Empty;
                           strqry = strqry + "  select '0' As CampusID,'Select a Campus' As CampusName  from EmailCampusMaster  union  ";
                           strqry = strqry + "  select CampusID,CampusName from EmailCampusMaster  ";
                           strqry = strqry + "  where CampusName!='DSIS' ";
                           CMD.Connection = connection;
                           CMD.CommandType = System.Data.CommandType.Text;
                           CMD.CommandText = strqry;
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


           #endregion

        //13 july 2016
           #region "SMS Configuration"
           static public DataTable GetSMSconfiguration()
           {
               DataTable _ds = new DataTable();
               try
               {
                   using (SqlConnection connection = new SqlConnection(constr))
                   {
                       using (SqlCommand CMD = new SqlCommand())
                       {
                           string strqry = string.Empty;
                           strqry = strqry + "  SELECT ESC.ID,ESC.CampusID,ESC.DeptID,ESC.Active,ESC.LongCode,DeptName, ";
                           strqry = strqry + "  ECM.CampusName,ESC.AccountSID ,ESC.AuthToken FROM EmailSMSConfig as ESC  ";
                           strqry = strqry + "  inner join EmailCampusMaster ECM on ESC.CampusID=ECM.CampusID ";
                           strqry = strqry + "  where  ECM.Active='1' and  ESC.DeleteFlag is null  order by ESC.ID desc ";  
                           CMD.Connection = connection;
                           CMD.CommandType = System.Data.CommandType.Text;
                           CMD.CommandText = strqry;
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
           static public Boolean GetUpdateSMSConfigStatus(string SMSConfigId, string Status)
           {
               SqlConnection connection = new SqlConnection(constr);
               Boolean sstatus;
               try
               {
                   using (SqlCommand CMD = new SqlCommand())
                   {
                       string status = string.Empty;
                       status = "0";
                       if (Status == "true")
                       {
                           status = "1";
                       }
                       connection.Open();
                       CMD.Connection = connection;
                       CMD.CommandType = System.Data.CommandType.Text;
                       CMD.CommandText = " update EmailSMSConfig set Active='" + status + "'  where  ID='" + SMSConfigId + "'";
                       CMD.Prepare();
                       CMD.ExecuteNonQuery();
                       sstatus = true;
                       connection.Close();
                   }
               }
               catch (Exception ex)
               {
                   sstatus = false;
               }
               return sstatus;
           }
           static public Boolean dELETESMSConfig(string ID)
           {

               SqlConnection connection = new SqlConnection(constr);
               Boolean sstatus;
               try
               {
                   using (SqlCommand CMD = new SqlCommand())
                   {
                       connection.Open();
                       CMD.Connection = connection;
                       CMD.CommandType = System.Data.CommandType.Text;
                       CMD.CommandText = " update EmailSMSConfig set DeleteFlag='1'  where  ID='" + ID + "'";
                       CMD.Prepare();
                       CMD.ExecuteNonQuery();
                       sstatus = true;
                       connection.Close();
                   }

               }
               catch (Exception ex)
               {
                   sstatus = false;
               }
               return sstatus;
           }
           static public Boolean InsertSMSConfig(string CampusID, string DeptID, string LongCode, string Active, string AccountSID, string AuthToken, string DeptName)
           {
               SqlConnection connection = new SqlConnection(constr);
               Boolean status;
               try
               {
                   using (SqlCommand CMD = new SqlCommand())
                   {
                       string strQuery = "";
                       strQuery = " INSERT INTO EmailSMSConfig(CampusID,DeptID,LongCode,Active,AccountSID, AuthToken,DeptName ) ";
                       strQuery = strQuery + "VALUES('" + CampusID + "','" + DeptID + "','" + LongCode + "','" + Active + "','" + AccountSID + "','" + AuthToken + "','" + DeptName + "') ";
                       connection.Open();
                       CMD.Connection = connection;
                       CMD.CommandType = System.Data.CommandType.Text;
                       CMD.CommandText = strQuery;
                       CMD.Prepare();
                       CMD.ExecuteNonQuery();
                       status = true;
                       connection.Close();
                   }

               }
               catch (Exception ex)
               {
                   status = false;
               }
               return status;
           }
           static public Boolean UpdateSMSConfig(string ID, string CampusID, string DeptID, string LongCode, string Active, string AccountSID, string AuthToken, string DeptName)
           {
               SqlConnection connection = new SqlConnection(constr);
               Boolean status;
               try
               {
                   using (SqlCommand CMD = new SqlCommand())
                   {
                       string strQuery = "";
                       strQuery = " update EmailSMSConfig set CampusID='" + CampusID + "',DeptID='" + DeptID + "' ";
                       strQuery = strQuery + " , LongCode='" + LongCode + "',Active='" + Active + "',AccountSID='" + AccountSID + "' ,AuthToken='" + AuthToken + "' ";
                       strQuery = strQuery + " ,DeptName='" + DeptName + "' where  ID='" + ID + "'";
                       CMD.CommandText = strQuery;
                       connection.Open();
                       CMD.Connection = connection;
                       CMD.CommandType = System.Data.CommandType.Text;
                       CMD.CommandText = strQuery;
                       CMD.Prepare();
                       CMD.ExecuteNonQuery();
                       status = true;
                       connection.Close();
                   }

               }
               catch (Exception ex)
               {
                   status = false;
               }
               return status;
           }
           #endregion

       #region Check Database Connection string
           static public bool CheckConnectionstring(string dbconstr)
           {
               bool result = false;
               DataTable _dtChkdbconnection = new DataTable();
               try
               {
                   using (SqlConnection connection = new SqlConnection(dbconstr))
                   {
                       using (SqlCommand CMD = new SqlCommand())
                       {

                           CMD.Connection = connection;
                           CMD.CommandType = System.Data.CommandType.Text;
                           CMD.CommandText = "select Count(*)  as count from Students";
                           CMD.Prepare();
                           SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                           DataAdapter.Fill(_dtChkdbconnection);
                       }
                   }
                   int count = 0;
                   count = Convert.ToInt32(_dtChkdbconnection.Rows[0]["count"]);
                   if (count > 0)
                   {
                       result = true;
                   }

               }
               catch (Exception ex)
               {
                   return result;
               }
               finally
               {
               }
               return result;
           }
       #endregion

        #region Department Dynamic Access
           static public DataTable Getdynamicdepartment(string dbconstr)
           {
               
               DataTable _dt = new DataTable();
               try
               {
                   using (SqlConnection connection = new SqlConnection(dbconstr))
                   {
                       using (SqlCommand CMD = new SqlCommand())
                       {

                           CMD.Connection = connection;
                           CMD.CommandType = System.Data.CommandType.Text;
                           CMD.CommandText = " select DeptID,DeptDescription +'('+convert(varchar(5),DeptID)+')' as  DeptDescription from Departments  ";
                           CMD.Prepare();
                           SqlDataAdapter DataAdapter = new SqlDataAdapter(CMD);
                           DataAdapter.Fill(_dt);
                       }
                   }
                    
               }
               catch (Exception ex)
               {
               }
               finally
               {
               }
               return _dt;
           }



        #endregion
    }


   
  
}
