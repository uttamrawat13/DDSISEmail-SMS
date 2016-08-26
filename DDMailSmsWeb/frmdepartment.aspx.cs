using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDMailSmsWeb.Classes;
using DDMailSmsWeb.DataAccess;
using DDMailSmsWeb.DynamicAccess;
using OpenPop.Mime;
using OpenPop.Pop3;
using Telerik.Web.UI;

namespace DDMailSmsWeb
{
    public partial class TDEPTInbox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
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
                GlobalGridBind();
            }

        }

        private void GlobalGridBind()
        {
            string type = string.Empty, Operation = string.Empty;
            type = Request.QueryString["type"];
            Operation = Request.QueryString["Operation"];
            rgDepartment.Visible = false;
            rgDepartmentSent.Visible = false;
            rgDepartmentDelete.Visible = false;
            rgvDepartmentReceived.Visible = false;
            //rgvDepartmentSent.Visible = false;
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
            if (type == "Department")
            {
                if (Operation == "download")
                {
                    rgDepartment.Visible = true;
                    fetchmail(Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
                    DepartmentInboxEmail();
                    RadButton RbtnFetchEmail = (RadButton)Master.FindControl("RbtnFetchEmail");
                    RbtnFetchEmail.Focus();
                }
                else if (Operation == "Inbox")
                {
                    rgDepartment.Visible = true;
                    DepartmentInboxEmail();
                    RadButton RbtnInbox = (RadButton)Master.FindControl("RbtnInbox");
                    RbtnInbox.Focus();
                }
                else if (Operation == "Sent Email")
                {
                    rgDepartmentSent.Visible = true;
                    DepartmentSentEmail();
                    RadButton RbtnSentEmail = (RadButton)Master.FindControl("RbtnSentEmail");
                    RbtnSentEmail.Focus();

                }
                else if (Operation == "Unread Email")
                {
                    rgDepartment.Visible = true;
                    DepartmentUnreadEmail();
                    RadButton RbtnUnreadEmail = (RadButton)Master.FindControl("RbtnUnreadEmail");
                    RbtnUnreadEmail.Focus();
                }
                else if (Operation == "Remove Email")
                {
                    rgDepartmentDelete.Visible = true;
                    DepartmentDeleteEmail();
                    RadButton RbtnRemoveEmail = (RadButton)Master.FindControl("RbtnRemoveEmail");
                    RbtnRemoveEmail.Focus();
                }
                else if (Operation == "Receive SMS")
                {
                    rgvDepartmentReceived.Visible = true;
                    DepartmentSMSReceived();
                    RadButton RbtnReceiveSMS = (RadButton)Master.FindControl("RbtnReceiveSMS");
                    RbtnReceiveSMS.Focus();
                }
                //else if (Operation == "Sent SMS")
                //{
                //    rgvDepartmentSent.Visible = true;
                //    DepartSent_SMS();
                //    RadButton RbtnReceiveSMS = (RadButton)Master.FindControl("RbtnReceiveSMS");
                //    RbtnReceiveSMS.Focus();
                //}

            }
        }
     #region "Department Inbox Email,Email download,Email Unread Gridview Function"
        protected void rgDepartment_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            this.rgDepartment.MasterTableView.AllowNaturalSort = true;
            this.rgDepartment.MasterTableView.Rebind();
        }

        protected void rgDepartment_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GlobalGridBind();
        }
        private void DepartmentUnreadEmail()
        {
            DataTable dtDepartmentInboxEmail = new DataTable();
            dtDepartmentInboxEmail = DataAccessManager.GetListStudentUnreadEmail(Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtDepartmentInboxEmail"] = dtDepartmentInboxEmail;
            rgDepartment.DataSource = (DataTable)Session["dtDepartmentInboxEmail"];
        }
        private void DepartmentInboxEmail()
        {
            DataTable dtDepartmentInboxEmail = new DataTable();
            dtDepartmentInboxEmail = DataAccessManager.GetListStudent(Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtDepartmentInboxEmail"] = dtDepartmentInboxEmail;
            rgDepartment.DataSource = (DataTable)Session["dtDepartmentInboxEmail"];
        }
        protected void rgDepartment_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                
                if (e.CommandName == "Deptdownload_file")
                {
                    GridNestedViewItem ditem = (GridNestedViewItem)e.Item;
                    string filename = e.CommandArgument.ToString();
                    if (filename != "No")
                    {
                        if (filename != "Not Available")
                        {
                            string FolderName = string.Empty;
                            FolderName = Convert.ToString(Session["CampusName"]);
                            string path = MapPath("~/InboxAttachment/" + FolderName + "/" + filename);
                           // string path = MapPath("~/InboxAttachment/" + filename);

                            byte[] bts = System.IO.File.ReadAllBytes(path);
                            Response.Clear();
                            Response.ClearHeaders();
                            Response.AddHeader("Content-Type", "Application/octet-stream");
                            Response.AddHeader("Content-Length", bts.Length.ToString());
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                            Response.BinaryWrite(bts);
                            Response.Flush();
                            Response.End();
                            return;
                        }
                    }
                }
                if (e.CommandName == "DeleteInboxDeptemail")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    e.Item.Selected = true;
                    string DeleteID = e.CommandArgument.ToString();
                    Boolean result = false;
                    result = DataAccessManager.GetDeleteStdLeadInbox(DeleteID);
                    if (result == true)
                    {
                        DepartmentInboxEmail();
                        rgDepartment.Rebind();
                    }
                }

                if (e.CommandName == "imgbtnreplystd")
                {
                    
                    GridDataItem ditem = (GridDataItem)e.Item;
                    Label EmailReceived = (Label)ditem.FindControl("EmailReceived");
                    Label EmailSubject = (Label)ditem.FindControl("EmailSubject");
                    Label lbdeptEmailBody = (Label)ditem.FindControl("lbdeptEmailBody");
                   // Rtxtemailtodept.Text = EmailReceived.Text;
                    //RtxtemailSubjectdept.Text = "Re:" + EmailSubject.Text;
                    //REditComposeBodydept.Content = "<br/><br/>-----------------------------------------------------<br/>" + lbdeptEmailBody.Text;
                    Session["ReplyDeptEmailTo"] = EmailReceived.Text;
                    Session["ReplyDeptEmailSubject"] = "Re:" + EmailSubject.Text;
                    Session["ReplyDeptEmailComposeBody"] = "<br/><br/>-----------------------------------------------------<br/>" + lbdeptEmailBody.Text;
                    Response.Redirect("~/frmdepartmentcomposeemail.aspx", false);
               
                }
                if (e.CommandName == "ExpandCollapse")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    HiddenField hdn = (HiddenField)ditem.ChildItem.FindControl("hdnInboxID");
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
            }
            catch (Exception ex)
            {
                // Following Code comment beacause download button given error always
                //
            }

        }
        protected void rgDepartment_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {
                rgDepartment.DataSource = (DataTable)Session["dtDepartmentInboxEmail"];
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void rgDepartment_ItemDataBound(object sender, GridItemEventArgs e)
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
        protected void ItemChkboxEmailIsRead_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
                bool checkHeader = true;
                foreach (GridDataItem dataItem in rgDepartment.MasterTableView.Items)
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
                            rgDepartment.Rebind();
                            checkHeader = false;
                            break;
                        }
                        else
                        {
                            rgDepartment.Rebind();
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

     #endregion

     #region "Sutdent Sent Email Gridview Function"
        protected void rgvDepartmentSent_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
        
            GlobalGridBind();
        }
     
        protected void rgDepartmentSent_SortCommand(object sender, GridSortCommandEventArgs e)
        {

            this.rgDepartmentSent.MasterTableView.AllowNaturalSort = true;
            this.rgDepartmentSent.MasterTableView.Rebind();
        }

        protected void rgDepartmentSent_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GlobalGridBind();
        }
        private void DepartmentSentEmail()
        {
            DataTable dtDepartmentSentEmail = new DataTable();
            dtDepartmentSentEmail = DataAccessManager.GetEmailSentList(Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtDepartmentSentEmail"] = dtDepartmentSentEmail;
            rgDepartmentSent.DataSource = (DataTable)Session["dtDepartmentSentEmail"];
        }
        protected void rgDepartmentSent_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try {
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
                                String path = Server.MapPath("~/SentAttachment/" + FolderName + "/" + filename);

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
            catch (Exception ex)
            { }
        }
        protected void rgDepartmentSent_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {
                rgDepartmentSent.DataSource = (DataTable)Session["dtDepartmentSentEmail"];
            }
            catch (Exception ex)
            {
                
            }
        }
      #endregion

     #region "Sutdent Delete Email Gridview Function"
        protected void rgDepartmentDelete_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            this.rgDepartmentDelete.MasterTableView.AllowNaturalSort = true;
            this.rgDepartmentDelete.MasterTableView.Rebind();
        }

        protected void rgDepartmentDelete_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            GlobalGridBind();
        }
        private void DepartmentDeleteEmail()
        {
            DataTable dtDepartmentDeleteEmail = new DataTable();
            dtDepartmentDeleteEmail = DataAccessManager.GetDeleteEmailList(Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtDepartmentDeleteEmail"] = dtDepartmentDeleteEmail;
            rgDepartmentDelete.DataSource = (DataTable)Session["dtDepartmentDeleteEmail"];
        }
        protected void rgDepartmentDelete_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {
                rgDepartmentDelete.DataSource = (DataTable)Session["dtDepartmentDeleteEmail"];
            }
            catch (Exception ex)
            {
                
            }
        }
     #endregion

     #region "Sutdent Receive SMS Gridview Function"
        protected void rgvDepartmentReceived_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            this.rgvDepartmentReceived.MasterTableView.AllowNaturalSort = true;
            this.rgvDepartmentReceived.MasterTableView.Rebind();
        }
        protected void rgvDepartmentReceived_ItemDataBound(object sender, GridItemEventArgs e)
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
                        imgdriection.ImageUrl = "~/Content/img/incomingSMS.png";
                    }
                    Label lblBody = item.FindControl("lblBody") as Label;
                    Label lbStatus = item.FindControl("lbStatus") as Label;
                    CheckBox chkBoolean = item.FindControl("ItemChkboxSMSIsRead") as CheckBox;
                    //lbldirection.ForeColor = System.Drawing.Color.Blue;
                    lbldirection.Font.Bold = true;
                    lblBody.Font.Bold = true;
                    item["SMSFrom"].Font.Bold = true;
                    item["SMSTo"].Font.Bold = true;
                    item["lbStatus"].Font.Bold = true;
                    
                
                    if (chkBoolean.Checked == true)
                    {
                        lbldirection.Font.Bold = false;
                        lblBody.Font.Bold = false;
                        item["SMSFrom"].Font.Bold = false;
                        item["SMSTo"].Font.Bold = false;
                        item["lbStatus"].Font.Bold = false;
                      
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void rgvDepartmentReceived_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            GlobalGridBind();
        }
        private void DepartmentSMSReceived()
        {
            DataTable dtDepartmentReceiveSMS = new DataTable();
            dtDepartmentReceiveSMS = DataAccessManager.GetSMSReceived(Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            Session["dtDepartmentReceiveSMS"] = dtDepartmentReceiveSMS;
            rgvDepartmentReceived.DataSource = (DataTable)Session["dtDepartmentReceiveSMS"];
        }

        protected void rgvDepartmentReceived_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            try
            {
                rgvDepartmentReceived.DataSource = (DataTable)Session["dtDepartmentReceiveSMS"];
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void ItemChkboxSMSIsRead_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
                bool checkHeader = true;
                foreach (GridDataItem dataItem in rgvDepartmentReceived.MasterTableView.Items)
                {
                    if ((dataItem.FindControl("ItemChkboxSMSIsRead") as CheckBox).ClientID == (sender as CheckBox).ClientID)
                    {
                        string EmailSMSId = Convert.ToString((dataItem.Cells[0].FindControl("lbEmailSMSId") as Label).Text);
                        string strchk = "";
                        if ((sender as CheckBox).Checked == true)
                        {
                            strchk = "1";
                        }
                        else
                        {
                            strchk = "0";
                        }
                        DataAccessManager.GetUpdateSMSStatus(EmailSMSId, strchk);
                        DepartmentSMSReceived();
                        rgvDepartmentReceived.Rebind();
                        #region Master page SMS Count
                          CountSMS();
                        #endregion 
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void CountSMS()
        {
            string smscount = "0";
            Label lblSMSCount = (Label)Master.FindControl("lblSMSCount");
            DataTable dtDeptReceiveSMScount = new DataTable();
            lblSMSCount.Visible = false;
            dtDeptReceiveSMScount = DataAccessManager.GetSMSReceivedCount(Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            if (dtDeptReceiveSMScount.Rows.Count > 0)
            {
                lblSMSCount.Visible = true;
                smscount = dtDeptReceiveSMScount.Rows[0]["Rowcounts"].ToString();
            }
            lblSMSCount.Text = "(" + smscount + ")";
            if (Session["PageClick"] == "0")
            {
                lblSMSCount.Visible = false;
            }
        } 
     #endregion

        #region "Student Sent SMS Gridview Function"
        //private void DepartSent_SMS()
        //{
        //    DataTable dtDepartmentSMSSent = new DataTable();
        //    dtDepartmentSMSSent = DataAccessManager.GetSMSSent( Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
        //    Session["dtDepartmentSMSSent"] = dtDepartmentSMSSent;
        //    rgvDepartmentSent.DataSource = (DataTable)Session["dtDepartmentSMSSent"];
        //}
        //protected void rgvDepartmentSent_SortCommand(object sender, GridSortCommandEventArgs e)
        //{
        //    this.rgvDepartmentSent.MasterTableView.AllowNaturalSort = true;
        //    this.rgvDepartmentSent.MasterTableView.Rebind();
        //}

        //protected void rgvDepartmentSent_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        //{
        //    try
        //    {
        //        rgvDepartmentSent.DataSource = (DataTable)Session["dtDepartmentSMSSent"];
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
                                        IsReadAndSave = DataAccessManager.ReadEmailAndSaveDatabase(dtStudentNo.Rows[0]["StudentNo"].ToString(),
                                            Convert.ToString(Session["DeptID"]), messId, dtEmailConfig.Rows[0]["DeptEmail"].ToString(), from, subj, Ebody, IsAtta, date, Convert.ToInt32(Session["CampusID"]));

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


   

       

    }
}