using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DDMailSmsWeb.Classes;
using DDMailSmsWeb.DataAccess;
using DDMailSmsWeb.DynamicAccess;
using Telerik.Web.UI;

namespace DDMailSmsWeb
{
    public partial class TDEPTComposeEmail : System.Web.UI.Page
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
                    #region "set page name by session"
                    Session["pagename"] = "Department > Write Email ";
                    #endregion
                    EmailTemplateBind();
                    DataTable dtConfigMail = DataAccessManager.GetEmailConfigDetail(Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
                    if (dtConfigMail.Rows.Count > 0)
                    {
                        RtxtEmailFromdept.Text = Convert.ToString(dtConfigMail.Rows[0]["DeptEmail"]);
                    }

                    //                  Session["ReplyDeptEmailTo"],Session["ReplyDeptEmailSubject"],Session["ReplyDeptEmailComposeBody"]


                    if (Session["ReplyDeptEmailTo"] != string.Empty && Session["ReplyDeptEmailTo"] != null)
                    {
                        Rtxtemailtodept.Text = Convert.ToString(Session["ReplyDeptEmailTo"]);
                    }
                    if (Session["ReplyDeptEmailSubject"] != string.Empty && Session["ReplyDeptEmailSubject"] != null)
                    {
                        RtxtemailSubjectdept.Text = Convert.ToString(Session["ReplyDeptEmailSubject"]);
                    }
                    if (Session["ReplyDeptEmailComposeBody"] != string.Empty && Session["ReplyDeptEmailComposeBody"] != null)
                    {
                        REditComposeBodydept.Content = Convert.ToString(Session["ReplyDeptEmailComposeBody"]);
                    }
                    #region "Search Control"
                        string StudentNo = string.Empty, LeadId = string.Empty;
                        StudentNo = Convert.ToString(Session["StudentNo"]);
                        LeadId = Convert.ToString(Session["leadID"]);
                        if (StudentNo != string.Empty && StudentNo != "0")
                        {
                            #region "Search Control Student Setting"
                            RCBStudentsLeadSearch.WebServiceSettings.Method = "GetStudentsEmailDept";
                            RCBStudentsLeadSearch.HeaderTemplate = new StudentLeadHeaderTemplate();
                            RCBStudentsLeadSearch.EmptyMessage = "Select a Student";
                            #endregion
                        }
                        else if (LeadId != string.Empty && LeadId != "0")
                        {
                            
                            #region "Search Control Lead Setting"
                            RCBStudentsLeadSearch.WebServiceSettings.Method = "GetLeadsEmailDept";
                            RCBStudentsLeadSearch.HeaderTemplate = new StudentLeadHeaderTemplate();
                            RCBStudentsLeadSearch.EmptyMessage = "Select a Lead";
                            #endregion
                        }
                    #endregion
                }
        }
        private void EmailTemplateBind()
        {
            try
            {
                DataTable dtChooseTemplates = new DataTable();
                dtChooseTemplates = DataAccessManager.GetTemplateddlList("2");
                if (dtChooseTemplates.Rows.Count > 0)
                {
                    RddlChooseTemplatesdept.DataSource = dtChooseTemplates;
                    RddlChooseTemplatesdept.DataTextField = "Title";
                    RddlChooseTemplatesdept.DataValueField = "ID";
                    RddlChooseTemplatesdept.DataBind();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void btnApplyTemplateDept_Click(object sender, EventArgs e)
        {
            try
            {
                RlblComposeMaildeptMsg.Text = string.Empty;
                LRRlblComposeMaildeptMsg.Visible = false;
                string templateid = "";
                if (RddlChooseTemplatesdept.SelectedIndex > 0)
                {
                    templateid = Convert.ToString(RddlChooseTemplatesdept.SelectedValue);
                    DataTable dtshowTemplate = DataAccessManager.GetShowTemplateBodyByID(templateid);
                    if (dtshowTemplate.Rows.Count > 0)
                    {
                        REditComposeBodydept.Content = dtshowTemplate.Rows[0]["Body"].ToString();
                    }
                }
                else
                {
                    REditComposeBodydept.Content = string.Empty;
                }
            }
            catch (Exception ex)
            {
                
            }

        }
        /// <summary>
        /// Clear Template on compose mail in student or Mail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearTemplateDeptEmail_Click(object sender, EventArgs e)
        {
            RlblComposeMaildeptMsg.Text = string.Empty;
            REditComposeBodydept.Content = string.Empty;
            RddlChooseTemplatesdept.SelectedIndex = 0;
            LRRlblComposeMaildeptMsg.Visible = false;
        }
        protected void RpushBtnComposeMaildept_Click(object sender, EventArgs e)
        {
            try
            {
                RlblComposeMaildeptMsg.Text = string.Empty;
                if (Page.IsValid)
                {
                    try
                    {
                        LRRlblComposeMaildeptMsg.Visible = true;
                        bool success = false;
                        string filname = string.Empty;
                        string StudentNo = string.Empty, LeadId = string.Empty, DeptID = string.Empty;
                        int CampusID = 0;
                        StudentNo = Convert.ToString("0");
                        LeadId = Convert.ToString("0");
                        DeptID = Convert.ToString(Session["DeptID"]);
                        CampusID = Convert.ToInt32(Session["CampusID"]);
                        string EmailFrom = string.Empty, temailto = string.Empty, EmailCC = string.Empty, EmailBCC = string.Empty, Subject = string.Empty, composemailbody = string.Empty;
                        EmailFrom = RtxtEmailFromdept.Text.Trim();
                        temailto = Rtxtemailtodept.Text.Trim();
                        EmailCC = RtxtEmailCCdept.Text;
                        EmailBCC = RtxtEmailBCCdept.Text;
                        Subject = RtxtemailSubjectdept.Text;
                        composemailbody = REditComposeBodydept.Content;
                        DataTable dtemaildetails = DataAccessManager.GetEmailConfigDetail(DeptID, CampusID);
                        if (dtemaildetails.Rows.Count > 0)
                        {
                            string fileName = string.Empty;
                            string filepath = string.Empty;
                            //======================================================================
                            int i = 1;
                            foreach (UploadedFile file in fileuploadComposeTempdept.UploadedFiles)
                            {
                                if (i == 1)
                                {
                                    fileName = file.FileName;
                                    string[] words = fileName.Split('.');
                                    fileName = words[0] + DateTime.Now.ToString("MMddyyyyhmmsstt") + '.' + words[1];
                                    string FolderName = string.Empty;
                                    FolderName = Convert.ToString(Session["CampusName"]);
                                    String path = Server.MapPath("~/SentAttachment/" + FolderName);
                                    if (!Directory.Exists(path))
                                    {
                                        // Try to create the directory.
                                        DirectoryInfo di = Directory.CreateDirectory(path);
                                    }
                                    //file.SaveAs(Server.MapPath("~/SentAttachment/") + fileName);
                                    //fileName = Server.MapPath("~/SentAttachment/") + fileName;
                                    file.SaveAs(Server.MapPath("~/SentAttachment/" + FolderName+"/") + fileName);
                                    filepath = Server.MapPath("~/SentAttachment/" + FolderName + "/") + fileName;
                                }
                                i = i + 1;
                            }
                            //======================================================================
                            string sentmail = string.Empty;
                            if (fileName != string.Empty)
                            {
                                sentmail = "1";
                                success = sendmail.Instance.MailGo(composemailbody, EmailFrom, temailto, EmailCC, EmailBCC, Subject, dtemaildetails.Rows[0]["SMTP"].ToString(), dtemaildetails.Rows[0]["Pass"].ToString(), Convert.ToInt32(dtemaildetails.Rows[0]["PortOut"]), filepath, Convert.ToBoolean(dtemaildetails.Rows[0]["SSL"]));
                            }
                            else
                            {
                                sentmail = "0";
                                success = sendmail.Instance.MailGo(composemailbody, EmailFrom, temailto, EmailCC, EmailBCC, Subject, dtemaildetails.Rows[0]["SMTP"].ToString(), dtemaildetails.Rows[0]["Pass"].ToString(), Convert.ToInt32(dtemaildetails.Rows[0]["PortOut"]), "", Convert.ToBoolean(dtemaildetails.Rows[0]["SSL"]));
                            }
                            if (success == true)
                            {
                                if (Source.SOrL(StudentNo, LeadId))
                                {
                                    DataAccessManager.SetSentEmailrecords(StudentNo, DeptID, temailto, EmailCC, EmailBCC, EmailFrom, Subject, composemailbody.Replace("'", " "), sentmail, CampusID, fileName);
                                }
                                else
                                { 
                                    DataAccessManager.SetSentEmailrecordsLead(LeadId, DeptID, temailto, EmailCC, EmailBCC, EmailFrom, Subject, composemailbody.Replace("'", " "), sentmail, CampusID, fileName);
                                }
                                Rtxtemailtodept.Text = "";
                                RtxtEmailBCCdept.Text = "";
                                RtxtEmailCCdept.Text = string.Empty;
                                RtxtemailSubjectdept.Text = "";
                                REditComposeBodydept.Content = string.Empty;
                               // RlblComposeMaildeptMsg.Text = "Send Successfully!";
                                Response.Redirect("~/frmdepartment.aspx?type=Department&Operation=Sent Email");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
        #region "Model Search Email id Lead And student"
        protected void RCBStudentsLeadSearch_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string email = string.Empty;
                email = Convert.ToString(RCBStudentsLeadSearch.SelectedValue);
                if (email == null)
                {
                    email = string.Empty;
                }
                Rtxtemailtodept.Text = string.Empty;
                if (email != string.Empty)
                {
                    Rtxtemailtodept.Text = email;
                }
            }

            catch (Exception ex)
            { }
        }
        #region "Lead Student search controle header template Selection"

        class StudentLeadHeaderTemplate : ITemplate
        {
            public void InstantiateIn(Control container)
            {
                string headertemplate = string.Empty;
                headertemplate += " <ul>";
                headertemplate += " <li class='dpemailcol1'>Full Name</li>";
                headertemplate += " <li class='dpemailcol2'>Status</li>";
                headertemplate += " <li class='dpemailcol3'>Email</li>";
                headertemplate += " </ul>";
                HtmlGenericControl header = new HtmlGenericControl("header");
                header.InnerHtml = headertemplate;

                container.Controls.Add(header);
            }
        }


        #endregion
 
        #endregion

        
    }
}