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
    public partial class TSTDLEADInbox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
               

                #region "Session Check"
                    if (Request.UrlReferrer != null)
                    {
                        if (Session["CampusID"] != null) { }
                        else
                        {
                            Response.Redirect("~/frmlogin.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/frmlogin.aspx");
                    }
                #endregion
                string type = string.Empty;
                type = Request.QueryString["type"];
                if (type == "Student")
                { Page.Title = "Student"; }
                else if (type == "Lead")
                { Page.Title = "Lead"; }
                GlobalGridBind();
            }
          
        }
       
        private void GlobalGridBind()
        {
            RgvInbox.Visible = false;
            RgvSent.Visible = false;
            RgvDelete.Visible = false;
            RgvSMSReceived.Visible = false;
            //RGVSMSSent.Visible = false;
            string type = string.Empty, Operation = string.Empty;

            type = Request.QueryString["type"];
            Operation = Request.QueryString["Operation"];

            #region "set page name by session"
                string pageOperation=string.Empty;

                if (Operation == "download")
                {
                    pageOperation = "Check email";
                }
                else if (Operation == "Inbox")
                {
                    pageOperation = "inbox";
                }
                else if (Operation == "Sent Email")
                {
                    pageOperation = "Sent mail";
                }

                else if (Operation == "Unread Email")
                {
                    pageOperation = "Unread mail";
                }

                else if (Operation == "Sent SMS")
                {
                    pageOperation = "Sent SMS";
                }
                else if (Operation == "Receive SMS")
                {
                    pageOperation = "Receive SMS";
                }

                Session["pagename"] = type + " > " + pageOperation;
            #endregion
            if (type == "Student")
            {
                if (Operation == "download")
                {
                    RgvInbox.Visible = true;
                    fetchmail(Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
                    StudentInboxEmail();

                    RadButton RbtnFetchEmail = (RadButton)Master.FindControl("RbtnFetchEmail");
                    RbtnFetchEmail.Focus();
                }
                else if (Operation == "Inbox")
                {
                    RgvInbox.Visible = true;
                    StudentInboxEmail();
                    RadButton RbtnInbox = (RadButton)Master.FindControl("RbtnInbox");
                    RbtnInbox.Focus();
                }
                else if (Operation == "Sent Email")
                {
                    RgvSent.Visible = true;
                    StudentSentEmail();
                    RadButton RbtnSentEmail = (RadButton)Master.FindControl("RbtnSentEmail");
                    RbtnSentEmail.Focus();

                }
                else if (Operation == "Unread Email")
                {
                    RgvInbox.Visible = true;
                    StudentUnreadEmail();
                    RadButton RbtnUnreadEmail = (RadButton)Master.FindControl("RbtnUnreadEmail");
                    RbtnUnreadEmail.Focus();
                }
                else if (Operation == "Remove Email")
                {
                    RgvDelete.Visible = true;
                    StudentDeleteEmail();
                    RadButton RbtnRemoveEmail = (RadButton)Master.FindControl("RbtnRemoveEmail");
                    RbtnRemoveEmail.Focus();
                }
                else if (Operation == "Receive SMS")
                {
                    RgvSMSReceived.Visible = true;
                    StudentSMSReceived();
                    RadButton RbtnReceiveSMS = (RadButton)Master.FindControl("RbtnReceiveSMS");
                    RbtnReceiveSMS.Focus();
                }
                //else if (Operation == "Sent SMS")
                //{
                //    RGVSMSSent.Visible = true;
                //    StudentSent_SMS();
                //    RadButton RbtnReceiveSMS = (RadButton)Master.FindControl("RbtnReceiveSMS");
                //    RbtnReceiveSMS.Focus();
                //}

            }
            else if (type == "Lead")
            {
                if (Operation == "download")
                {
                    RgvInbox.Visible = true;
                   fetchmail(Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
                    LeadInboxEmail();
                    RadButton RbtnFetchEmail = (RadButton)Master.FindControl("RbtnFetchEmail");
                    RbtnFetchEmail.Focus();
                }
                else if (Operation == "Inbox")
                {
                    RgvInbox.Visible = true;
                    LeadInboxEmail();
                    RadButton RbtnInbox = (RadButton)Master.FindControl("RbtnInbox");
                    RbtnInbox.Focus();
                }
                else if (Operation == "Sent Email")
                {
                    RgvSent.Visible = true;
                    LeadSentEmail();
                    RadButton RbtnSentEmail = (RadButton)Master.FindControl("RbtnSentEmail");
                    RbtnSentEmail.Focus();
                }
                else if (Operation == "Unread Email")
                {
                    RgvInbox.Visible = true;
                    LeadUnreadEmail();
                    RadButton RbtnUnreadEmail = (RadButton)Master.FindControl("RbtnUnreadEmail");
                    RbtnUnreadEmail.Focus();
                }
                else if (Operation == "Remove Email")
                {
                    RgvDelete.Visible = true;
                    LeadDeleteEmail();
                    RadButton RbtnRemoveEmail = (RadButton)Master.FindControl("RbtnRemoveEmail");
                    RbtnRemoveEmail.Focus();
                }
                else if (Operation == "Receive SMS")
                {
                    RgvSMSReceived.Visible = true;
                    LeadSMSReceived();
                    RadButton RbtnReceiveSMS = (RadButton)Master.FindControl("RbtnReceiveSMS");
                    RbtnReceiveSMS.Focus();
                }
                //else if (Operation == "Sent SMS")
                //{
                //    RGVSMSSent.Visible = true;
                //    LeadSent_SMS();
                //    RadButton RbtnReceiveSMS = (RadButton)Master.FindControl("RbtnReceiveSMS");
                //    RbtnReceiveSMS.Focus();

                //}

            }
        }

        #region "Sutdent Inbox Email,Email download,Email Unread Gridview Function"

        protected void RgvInbox_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            this.RgvInbox.MasterTableView.AllowNaturalSort = true;
            this.RgvInbox.MasterTableView.Rebind();
        }

       
        protected void RgvInbox_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GlobalGridBind();
        }
        private void StudentUnreadEmail()
        {
            DataTable dtStudentLeadInboxEmail = new DataTable();
            dtStudentLeadInboxEmail = DataAccessManager.GetListStudentUnreadEmail(Convert.ToString(Session["StudentNo"]), Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtStudentLeadInboxEmail"] = dtStudentLeadInboxEmail;
            RgvInbox.DataSource = (DataTable)Session["dtStudentLeadInboxEmail"];
            // RgvInbox_NeedDataSource(object sender, GridNeedDataSourceEventArgs e);

        }
        private void LeadUnreadEmail()
        {
            DataTable dtStudentLeadInboxEmail = new DataTable();
            dtStudentLeadInboxEmail = DataAccessManager.GetListStudentUnreadEmailLead(Convert.ToString(Session["LeadId"]), Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtStudentLeadInboxEmail"] = dtStudentLeadInboxEmail;
            RgvInbox.DataSource = (DataTable)Session["dtStudentLeadInboxEmail"];
        }
        private void StudentInboxEmail()
        {
            DataTable dtStudentLeadInboxEmail = new DataTable();
            dtStudentLeadInboxEmail = DataAccessManager.GetListStudent(Convert.ToString(Session["StudentNo"]), Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtStudentLeadInboxEmail"] = dtStudentLeadInboxEmail;
            RgvInbox.DataSource = (DataTable)Session["dtStudentLeadInboxEmail"];
        }
        private void LeadInboxEmail()
        {
            DataTable dtStudentLeadInboxEmail = new DataTable();
            dtStudentLeadInboxEmail = DataAccessManager.GetListLead(Convert.ToString(Session["LeadID"]), Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtStudentLeadInboxEmail"] = dtStudentLeadInboxEmail;
            RgvInbox.DataSource = (DataTable)Session["dtStudentLeadInboxEmail"];
        }
        protected void ItemChkboxEmailIsRead_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
                bool checkHeader = true;
                foreach (GridDataItem dataItem in RgvInbox.MasterTableView.Items)
                {
                    if ((dataItem.FindControl("ItemChkboxEmailIsRead") as CheckBox).ClientID == (sender as CheckBox).ClientID)
                    {
                        string InboxID = Convert.ToString((dataItem.Cells[0].FindControl("ItemLblInboxID") as Label).Text);
                        string strchk = "";
                        if ((sender as CheckBox).Checked == true)
                        {
                            strchk = "true";
                        }
                        else
                        {
                            strchk = "false";
                        }
                        DataAccessManager.GetUpdateUnread(InboxID, strchk);

                        if (Source.SOrL(Convert.ToString(Session["StudentNo"]), Convert.ToString(Session["leadID"])))
                        {
                            RgvInbox.Rebind();
                            //StudentInboxEmail();

                            checkHeader = false;
                            break;
                        }
                        else
                        {
                            RgvInbox.Rebind();
                            //StudentInboxEmail();

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void RgvInbox_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    Label EmailReceived = item.FindControl("EmailReceived") as Label;
                    Label EmailSubject = item.FindControl("EmailSubject") as Label;
                    Label EmailReceivedDatetime = item.FindControl("EmailReceivedDatetime") as Label;
                    Label Attachment = item.FindControl("Attachment") as Label;
                    CheckBox chkBoolean = item.FindControl("ItemChkboxEmailIsRead") as CheckBox;
                    EmailSubject.ForeColor = System.Drawing.Color.Blue;
                    EmailReceived.Font.Bold = true;
                    EmailSubject.Font.Bold = true;
                    Attachment.Font.Bold = true;
                    EmailReceivedDatetime.Font.Bold = true;

                    if (chkBoolean.Checked == true)
                    {
                        EmailReceived.Font.Bold = false;
                        EmailSubject.Font.Bold = false;
                        Attachment.Font.Bold = false;
                        EmailReceivedDatetime.Font.Bold = false;

                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void RgvInbox_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {
                RgvInbox.DataSource = (DataTable)Session["dtStudentLeadInboxEmail"];
               
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void RgvInbox_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {



                if (e.CommandName == "download_fileInbox")
                {
                    GridNestedViewItem ditem = (GridNestedViewItem)e.Item;
                    string filename = e.CommandArgument.ToString();
                    if (filename != "No")
                    {
                        if (filename != "Not Available")
                        {
                            string FolderName = string.Empty;
                            FolderName = Convert.ToString(Session["CampusName"]);
                            string path = MapPath("~/InboxAttachment/" + FolderName +"/" +filename);
                            byte[] bts = System.IO.File.ReadAllBytes(path);
                            Response.Clear();
                            Response.ClearHeaders();
                            Response.AddHeader("Content-Type", "Session/octet-stream");
                            Response.AddHeader("Content-Length", bts.Length.ToString());
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                            Response.BinaryWrite(bts);
                            Response.Flush();
                            Response.End();
                            return;
                        }
                    }
                }


                if (e.CommandName == RadGrid.ExpandCollapseCommandName && !e.Item.Expanded)
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    HiddenField hdn = (HiddenField)ditem.ChildItem.FindControl("hdnInboxID");
                    //  string s = ditem.ChildItem.OwnerTableView.ParentItem.GetDataKeyValue("InboxID").ToString();
                    string inboxid = hdn.Value;
                    Boolean result = false;
                    result = DataAccessManager.GetUpdateUnread(inboxid, "true");
                    if (result == true)
                    {
                        GridDataItem item = (GridDataItem)e.Item;
                        Label EmailReceived = item.FindControl("EmailReceived") as Label;
                        Label EmailSubject = item.FindControl("EmailSubject") as Label;
                        Label EmailReceivedDatetime = item.FindControl("EmailReceivedDatetime") as Label;
                        Label Attachment = item.FindControl("Attachment") as Label;
                        CheckBox chkBoolean = item.FindControl("ItemChkboxEmailIsRead") as CheckBox;
                        EmailSubject.ForeColor = System.Drawing.Color.Blue;
                        chkBoolean.Checked = true;
                        EmailReceived.Font.Bold = false;
                        EmailSubject.Font.Bold = false;
                        Attachment.Font.Bold = false;
                        EmailReceivedDatetime.Font.Bold = false;
                    }


                }


                if (e.CommandName == "DeleteInboxstdleademail")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    e.Item.Selected = true;
                    string DeleteID = e.CommandArgument.ToString();
                    Boolean result = false;
                    result = DataAccessManager.GetDeleteStdLeadInbox(DeleteID);
                    if (result == true)
                    {
                        if (Source.SOrL(Convert.ToString(Session["StudentNo"]), Convert.ToString(Session["leadID"])))
                        {
                            StudentInboxEmail();
                        }
                        else
                        {
                            LeadInboxEmail();
                        }
                        RgvInbox.Rebind();
                    }
                }

                if (e.CommandName == "imgbtnreplystd")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    Label EmailReceived = (Label)ditem.FindControl("EmailReceived");
                    Label EmailSubject = (Label)ditem.FindControl("EmailSubject");
                    Label lbdeptEmailBody = (Label)ditem.FindControl("lblstdEmailBody");
                    //Rtxtemailto.Text = EmailReceived.Text;
                    //RtxtemailSubject.Text = "Re:" + EmailSubject.Text;
                    //REditComposeBody.Content = "<br/><br/>-----------------------------------------------------<br/>" + lbdeptEmailBody.Text;

                    Session["ReplyStdLeadEmailTo"] = EmailReceived.Text;
                    Session["ReplyStdLeadEmailSubject"] = "Re:" + EmailSubject.Text;
                    Session["ReplyStdLeadEmailComposeBody"] = "<br/><br/>-----------------------------------------------------<br/>" + lbdeptEmailBody.Text;
                    Response.Redirect("~/frmstudentleadcomposeemaill.aspx", false);
                }

            }
            catch (Exception ex)
            {
                // 
            }
        }
        #endregion

        #region "Sutdent Sent Email Gridview Function"
        protected void RgvSent_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            this.RgvSent.MasterTableView.AllowNaturalSort = true;
            this.RgvSent.MasterTableView.Rebind();
        }
        protected void RgvSent_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GlobalGridBind();
        }
        private void StudentSentEmail()
        {
            DataTable dtStudentLeadSentEmail = new DataTable();
            dtStudentLeadSentEmail = DataAccessManager.GetEmailSentList(Convert.ToString(Session["StudentNo"]), Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtStudentLeadSentEmail"] = dtStudentLeadSentEmail;
            RgvSent.DataSource = (DataTable)Session["dtStudentLeadSentEmail"];
            
        }
        private void LeadSentEmail()
        {
            DataTable dtStudentLeadSentEmail = new DataTable();
            dtStudentLeadSentEmail = DataAccessManager.GetEmailSentListLead(Convert.ToString(Session["LeadId"]), Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtStudentLeadSentEmail"] = dtStudentLeadSentEmail;
            RgvSent.DataSource = (DataTable)Session["dtStudentLeadSentEmail"];
        }
        protected void RgvSent_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "download_fileInbox")
            {
                GridNestedViewItem ditem = (GridNestedViewItem)e.Item;
                string filename = e.CommandArgument.ToString();
                if (filename != "No")
                {
                    if (filename != "Not Available")
                    {
                        string FolderName = string.Empty;
                        FolderName = Convert.ToString(Session["CampusName"]);
                        String path = Server.MapPath("~/SentAttachment/" + FolderName +"/"+ filename);

                       // string path = MapPath("~/InboxAttachment/" + filename);
                        byte[] bts = System.IO.File.ReadAllBytes(path);
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.AddHeader("Content-Type", "Session/octet-stream");
                        Response.AddHeader("Content-Length", bts.Length.ToString());
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                        Response.BinaryWrite(bts);
                        Response.Flush();
                        Response.End();
                        return;
                    }
                }
            }
        }
        protected void RgvSent_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {
                RgvSent.DataSource = (DataTable)Session["dtStudentLeadSentEmail"];
                
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion

        #region "Sutdent Delete Email Gridview Function"
        protected void RgvDelete_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            this.RgvDelete.MasterTableView.AllowNaturalSort = true;
            this.RgvDelete.MasterTableView.Rebind();
        }
        protected void RgvDelete_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GlobalGridBind();
        }

        private void StudentDeleteEmail()
        {
            DataTable dtStudentLeadDeleteEmail = new DataTable();
            dtStudentLeadDeleteEmail = DataAccessManager.GetDeleteEmailList(Convert.ToString(Session["StudentNo"]), Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtStudentLeadDeleteEmail"] = dtStudentLeadDeleteEmail;
            RgvDelete.DataSource = (DataTable)Session["dtStudentLeadDeleteEmail"];
        }
        private void LeadDeleteEmail()
        {
            DataTable dtStudentLeadDeleteEmail = new DataTable();
            dtStudentLeadDeleteEmail = DataAccessManager.GetDeleteEmailListLead(Convert.ToString(Session["LeadId"]), Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtStudentLeadDeleteEmail"] = dtStudentLeadDeleteEmail;
            RgvDelete.DataSource = (DataTable)Session["dtStudentLeadDeleteEmail"];
        }
        protected void RgvDelete_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {
                RgvDelete.DataSource = (DataTable)Session["dtStudentLeadDeleteEmail"];
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion

        #region "Sutdent Receive SMS Gridview Function"
        protected void RgvSMSReceived_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            this.RgvSMSReceived.MasterTableView.AllowNaturalSort = true;
            this.RgvSMSReceived.MasterTableView.Rebind();
      
        }
        protected void RgvSMSReceived_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    Label lbldirection = item.FindControl("lbldirection") as Label;
                    Image imgdriection = item.FindControl("imgdriection") as Image;
                    string direction = string.Empty;
                    direction = Convert.ToString(lbldirection.Text);
                    if (direction == "outbound-api")
                    {
                        imgdriection.ImageUrl = "~/Content/img/outgoingSMS.png";
                    } 
                    else
                    { 
                      imgdriection.ImageUrl="~/Content/img/incomingSMS.png";
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void RgvSMSReceived_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GlobalGridBind();
        }
        private void StudentSMSReceived()
        {
            DataTable dtStudentLeadReceiveSMS = new DataTable();
            dtStudentLeadReceiveSMS = DataAccessManager.GetSMSReceivedStd(Convert.ToString(Session["StudentNo"]), Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtStudentLeadReceiveSMS"] = dtStudentLeadReceiveSMS;
            RgvSMSReceived.DataSource = (DataTable)Session["dtStudentLeadReceiveSMS"];
        }
        private void LeadSMSReceived()
        {
            DataTable dtStudentLeadReceiveSMS = new DataTable();
            dtStudentLeadReceiveSMS = DataAccessManager.GetSMSReceivedLead(Convert.ToString(Session["LeadId"]), Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtStudentLeadReceiveSMS"] = dtStudentLeadReceiveSMS;
            RgvSMSReceived.DataSource = (DataTable)Session["dtStudentLeadReceiveSMS"];
           
        }
        protected void RgvSMSReceived_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {
                RgvSMSReceived.DataSource = (DataTable)Session["dtStudentLeadReceiveSMS"];
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion

        #region "Student Sent SMS Gridview Function"
        //protected void RGVSMSSent_SortCommand(object sender, GridSortCommandEventArgs e)
        //{
        //    this.RGVSMSSent.MasterTableView.AllowNaturalSort = true;
        //    this.RGVSMSSent.MasterTableView.Rebind();
        //}
      
        //protected void RGVSMSSent_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    GlobalGridBind();
        //}
        //private void StudentSent_SMS()
        //{
        //    DataTable dtStudentLeadSMSSent = new DataTable();
        //    dtStudentLeadSMSSent = DataAccessManager.GetSMSSentStd(Convert.ToString(Session["StudentNo"]), Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
        //    Session["dtStudentLeadSMSSent"] = dtStudentLeadSMSSent;
        //    RGVSMSSent.DataSource = (DataTable)Session["dtStudentLeadSMSSent"];
        //}
        //private void LeadSent_SMS()
        //{
        //    DataTable dtStudentLeadSMSSent = new DataTable();
        //    dtStudentLeadSMSSent = DataAccessManager.GetSMSSentLead(Convert.ToString(Session["LeadId"]), Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
        //    Session["dtStudentLeadSMSSent"] = dtStudentLeadSMSSent;
        //    RGVSMSSent.DataSource = (DataTable)Session["dtStudentLeadSMSSent"];
        //}
        //protected void RGVSMSSent_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        //{
        //    try
        //    {
        //        RGVSMSSent.DataSource = (DataTable)Session["dtStudentLeadSMSSent"];
        //    }
        //    catch (Exception ex)
        //    {
                
        //    }
        //}
        #endregion
        public void fetchmail(string DeptID, int CampusID)
        {

            try
            {
                DataTable dtEmailConfig = DataAccessManager.GetEmailConfigDetail(DeptID, CampusID);
                if (dtEmailConfig.Rows.Count > 0)
                {
                    Pop3Client pop3Client;
                    pop3Client = new Pop3Client();
                    pop3Client.Connect(dtEmailConfig.Rows[0]["Pop3"].ToString(), Convert.ToInt32(dtEmailConfig.Rows[0]["PortIn"]), Convert.ToBoolean(dtEmailConfig.Rows[0]["SSL"]));
                    pop3Client.Authenticate(dtEmailConfig.Rows[0]["DeptEmail"].ToString(), dtEmailConfig.Rows[0]["Pass"].ToString(), AuthenticationMethod.UsernameAndPassword);
                    if (pop3Client.Connected)
                    {
                        int count = pop3Client.GetMessageCount();
                        int progressstepno;
                        if (count == 0)
                        {
                        }
                        else
                        {
                            progressstepno = 100 - count;
                            this.Emails = new List<Email>();
                            for (int i = 1; i <= count; i++)
                            {
                                OpenPop.Mime.Message message = pop3Client.GetMessage(i);
                                Email email = new Email()
                                {
                                    MessageNumber = i,
                                    messageId = message.Headers.MessageId,
                                    Subject = message.Headers.Subject,
                                    DateSent = message.Headers.DateSent,
                                    From = message.Headers.From.Address
                                };
                                MessagePart body = message.FindFirstHtmlVersion();
                                if (body != null)
                                {
                                    email.Body = body.GetBodyAsText();
                                }
                                else
                                {
                                    body = message.FindFirstHtmlVersion();
                                    if (body != null)
                                    {
                                        email.Body = body.GetBodyAsText();
                                    }
                                }
                                email.IsAttached = false;
                                this.Emails.Add(email);
                                //Attachment Process
                                List<MessagePart> attachments = message.FindAllAttachments();
                                foreach (MessagePart attachment in attachments)
                                {
                                    email.IsAttached = true;
                                    string FolderName = string.Empty;
                                    FolderName = Convert.ToString(Session["CampusName"]);
                                    String path = Server.MapPath("~/InboxAttachment/" + FolderName);
                                    if (!Directory.Exists(path))
                                    {
                                        // Try to create the directory.
                                        DirectoryInfo di = Directory.CreateDirectory(path);
                                    }
                                    string ext = attachment.FileName.Split('.')[1];
                                   // FileInfo file = new FileInfo(Server.MapPath("InboxAttachment\\") + attachment.FileName.ToString());
                                    FileInfo file = new FileInfo(Server.MapPath("InboxAttachment\\" + FolderName + "\\") + attachment.FileName.ToString());
                                    attachment.SaveToFile(file);
                                    Attachment att = new Attachment();
                                    att.messageId = message.Headers.MessageId;
                                    att.FileName = attachment.FileName;
                                    attItem.Add(att);
                                }
                                //System.Threading.Thread.Sleep(500);

                            }

                            //Insert into database Inbox table
                            DataTable dtStudentNo = new DataTable();
                            bool IsReadAndSave = false;
                            foreach (var ReadItem in Emails)
                            {

                                string from = string.Empty, subj = string.Empty, messId = string.Empty, Ebody = string.Empty;
                                from = Convert.ToString(ReadItem.From);
                                subj = Convert.ToString(ReadItem.Subject);
                                messId = Convert.ToString(ReadItem.messageId);
                                Ebody = Convert.ToString(ReadItem.Body);
                                if (Ebody != string.Empty && Ebody != null)
                                {
                                    Ebody = Ebody.Replace("'", " ");
                                }
                                DateTime date = ReadItem.DateSent;
                                bool IsAtta = ReadItem.IsAttached;
                                //Student Email

                                if (Source.SOrL(Convert.ToString(Session["StudentNo"]), Convert.ToString(Session["leadID"])))
                                {
                                    dtStudentNo = DyDataAccessManager.GetStudentNo(from, from);

                                    if (dtStudentNo.Rows.Count == 0)
                                    {
                                        IsReadAndSave = DataAccessManager.ReadEmailAndSaveDatabase("0", Convert.ToString(Session["DeptID"]), messId, dtEmailConfig.Rows[0]["DeptEmail"].ToString(),
                                            from, subj, Ebody, IsAtta, date, Convert.ToInt32(Session["CampusID"]));
                                    }
                                    else
                                    {
                                        IsReadAndSave = DataAccessManager.ReadEmailAndSaveDatabase(dtStudentNo.Rows[0]["StudentNo"].ToString(), Convert.ToString(Session["DeptID"]),
                                            messId, dtEmailConfig.Rows[0]["DeptEmail"].ToString(), from, subj, Ebody, IsAtta, date, Convert.ToInt32(Session["CampusID"]));

                                    }
                                }
                                //Leads Email
                                if (Source.SOrL(Convert.ToString(Session["ParamStudentNo"]), Convert.ToString(Session["ParamleadID"])) == false)
                                {
                                    dtStudentNo = DyDataAccessManager.GetLeadsID(from, from);

                                    if (dtStudentNo.Rows.Count == 0)
                                    {
                                        IsReadAndSave = DataAccessManager.ReadEmailAndSaveDatabaseLead("0", Convert.ToString(Session["DeptID"]), messId,
                                            dtEmailConfig.Rows[0]["DeptEmail"].ToString(), from, subj, Ebody, IsAtta, date, Convert.ToInt32(Session["CampusID"]));
                                    }
                                    else
                                    {
                                        IsReadAndSave = DataAccessManager.ReadEmailAndSaveDatabaseLead(dtStudentNo.Rows[0]["LeadsID"].ToString(),
                                            Convert.ToString(Session["DeptID"]), messId, dtEmailConfig.Rows[0]["DeptEmail"].ToString(), from, subj, Ebody, IsAtta, date, Convert.ToInt32(Session["CampusID"]));

                                    }
                                }
                                //
                            }
                            //Insert into database Attachment table
                            foreach (var attachItem in attItem)
                            {
                                bool success;
                                string Filname = attachItem.FileName;
                                string MssID = attachItem.messageId;
                                success = DataAccessManager.ReadEmailAttachmentAndSaveDatabase(MssID, Filname);

                            }
                            Emails.Clear();
                            // attItem.Clear();
                            pop3Client.DeleteAllMessages();
                            //StartNotification(count);
                        }

                    }

                    pop3Client.Disconnect();
                }
            }
            catch (Exception ex)
            {

            }
        }

        object EmailObj;
        List<Attachment> attItem = new List<Attachment>();
        protected List<Email> Emails
        {
            get { return (List<Email>)EmailObj; }
            set { EmailObj = value; }
        }
        [Serializable]
        public class Email
        {

            public int MessageNumber { get; set; }
            public string messageId { get; set; }
            public string From { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public DateTime DateSent { get; set; }
            public bool IsAttached { get; set; }

        }
        [Serializable]
        public class Attachment
        {
            public string FileName { get; set; }
            public string messageId { get; set; }
        }





        protected void rbTnSearchGvInbox_Click(object sender, EventArgs e)
        {
            RadTextBox RtxtSearchGvInbox;
            foreach (GridFilteringItem filterItem in RgvInbox.MasterTableView.GetItems(GridItemType.FilteringItem))
            {
                RtxtSearchGvInbox = (RadTextBox)filterItem.FindControl("RtxtSearchGvInbox");
                string searchvalue = Convert.ToString(RtxtSearchGvInbox.Text);
                DataTable dtStudentLeadInboxEmail = new DataTable();
                dtStudentLeadInboxEmail = (DataTable)Session["dtStudentLeadInboxEmail"];
                dtStudentLeadInboxEmail = dtStudentLeadInboxEmail.Select("EmailSubject LIKE \'%" + searchvalue + "%\' ").CopyToDataTable();
                RgvInbox.DataSource = dtStudentLeadInboxEmail;
                RgvInbox.DataBind();
            }
        }

      

       








        
    }

}