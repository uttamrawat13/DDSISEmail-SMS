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
using Telerik.Web.UI;

namespace DDMailSmsWeb
{
    public partial class TSTDLEADComposeEmail : System.Web.UI.Page
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
                    string StudentNo = string.Empty, LeadId = string.Empty;
                    StudentNo = Convert.ToString(Session["StudentNo"]);
                    LeadId = Convert.ToString(Session["leadID"]);

                    if (Source.SOrL(StudentNo, LeadId))
                    {
                        Session["pagename"] = "Student > Write Email";
                    }
                    else
                    {
                        Session["pagename"]="Lead > Write Email";
                    }
                #endregion
                EmailTemplateBind();
                DataTable dtConfigMail = DataAccessManager.GetEmailConfigDetail(Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
                if (dtConfigMail.Rows.Count > 0)
                {
                    RtxtEmailFrom.Text = Convert.ToString(dtConfigMail.Rows[0]["DeptEmail"]);
                }

                if(Session["ReplyStdLeadEmailTo"] !=string.Empty && Session["ReplyStdLeadEmailTo"] !=null)
                {
                    Rtxtemailto.Text=Convert.ToString(Session["ReplyStdLeadEmailTo"]);  
                }
                if(Session["ReplyStdLeadEmailSubject"] !=string.Empty && Session["ReplyStdLeadEmailSubject"] !=null)
                {
                    RtxtemailSubject.Text = Convert.ToString(Session["ReplyStdLeadEmailSubject"]);  
                }
                if(Session["ReplyStdLeadEmailComposeBody"] !=string.Empty && Session["ReplyStdLeadEmailComposeBody"] !=null)
                {
                    REditComposeBody.Content = Convert.ToString(Session["ReplyStdLeadEmailComposeBody"]);  
                }
                StudentDetailBind();
                LeadDetailBind();
               
            }
            
        }
        // bind textbox composeemail in student   id and department
        private void StudentDetailBind()
        {
            try
            {
                string StudentNo = string.Empty, LeadId = string.Empty;
                StudentNo = Convert.ToString(Session["StudentNo"]);
                DataTable dtStudentEmail = new DataTable();
                dtStudentEmail = DyDataAccessManager.GetStudentEmail(StudentNo);
                if (dtStudentEmail.Rows.Count > 0)
                {
                    if (dtStudentEmail.Rows[0]["Email"].ToString() != string.Empty && dtStudentEmail.Rows[0]["Email2"].ToString() != string.Empty)
                    {
                        Rtxtemailto.Text = dtStudentEmail.Rows[0]["Email"].ToString() + ',' + dtStudentEmail.Rows[0]["Email2"].ToString();

                    }
                    if (dtStudentEmail.Rows[0]["Email"].ToString() == string.Empty)
                    {
                        Rtxtemailto.Text = dtStudentEmail.Rows[0]["Email2"].ToString();

                    }
                    if (dtStudentEmail.Rows[0]["Email2"].ToString() == string.Empty)
                    {
                        Rtxtemailto.Text = dtStudentEmail.Rows[0]["Email"].ToString();

                    }
            
                }
                else
                {
                   // tabstudentlead.Text = "No Available";
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        // bind textbox composeemail in Lead   id and department
        private void LeadDetailBind()
        {
            try 
            {
                string StudentNo = string.Empty, LeadId = string.Empty;
                LeadId = Convert.ToString(Session["leadID"]);
                DataTable dtStudentEmail = new DataTable();
                dtStudentEmail = DyDataAccessManager.GetLeadEmail(LeadId);
                if (dtStudentEmail.Rows.Count > 0)
                {
                    if (dtStudentEmail.Rows[0]["Email"].ToString() != string.Empty && dtStudentEmail.Rows[0]["Email2"].ToString() != string.Empty)
                    {
                        Rtxtemailto.Text = dtStudentEmail.Rows[0]["Email"].ToString() + ',' + dtStudentEmail.Rows[0]["Email2"].ToString();
                    }

                    if (dtStudentEmail.Rows[0]["Email"].ToString() == string.Empty)
                    {
                        Rtxtemailto.Text = dtStudentEmail.Rows[0]["Email2"].ToString();
                    }

                    if (dtStudentEmail.Rows[0]["Email2"].ToString() == string.Empty)
                    {
                        Rtxtemailto.Text = dtStudentEmail.Rows[0]["Email"].ToString();
                    }
                }
                else
                {
                    //tabstudentlead.Text = "No Available";
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        private void EmailTemplateBind()
        {
            try
            {  
                DataTable dtChooseTemplates = new DataTable();
                dtChooseTemplates = DataAccessManager.GetTemplateddlList(Convert.ToString(Session["CampusID"]));
                if (dtChooseTemplates.Rows.Count > 0)
                {
                    RddlChooseTemplates.DataSource = dtChooseTemplates;
                    RddlChooseTemplates.DataTextField = "Title";
                    RddlChooseTemplates.DataValueField = "ID";
                    RddlChooseTemplates.DataBind();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btmApplyTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                string templateid = "";
                RlblComposeMailResult.Text = string.Empty;
                LRRlblComposeMailResult.Visible = false;
                if (RddlChooseTemplates.SelectedIndex > 0)
                {
                    templateid = Convert.ToString(RddlChooseTemplates.SelectedValue);


                    DataTable dtshowTemplate = DataAccessManager.GetShowTemplateBodyByID(templateid);
                    if (dtshowTemplate.Rows.Count > 0)
                    {

                        REditComposeBody.Content = dtshowTemplate.Rows[0]["Body"].ToString();

                    }
                }
                else
                {
                    REditComposeBody.Content = string.Empty;
                }
            }
            catch (Exception ex)
            {
                
            }

        }
        /// <summary>
        /// Apply Template on compose mail in student or Mail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearTemplatestdEmail_Click(object sender, EventArgs e)
        {
            REditComposeBody.Content = string.Empty;
            RlblComposeMailResult.Text = string.Empty;
            LRRlblComposeMailResult.Visible = false;
        }

        protected void RpushBtnComposeMail_Click(object sender, EventArgs e)
        {
            try
            {
                RlblComposeMailResult.Text = string.Empty;
                if (Page.IsValid)
                {

                    try
                    {
                        LRRlblComposeMailResult.Visible = true;
                        bool success = false;
                        string StudentNo = string.Empty, LeadId = string.Empty, DeptID = string.Empty;
                        int CampusID = 0;
                        StudentNo = Convert.ToString(Session["StudentNo"]);
                        LeadId = Convert.ToString(Session["leadID"]);
                        DeptID = Convert.ToString(Session["DeptID"]);
                        CampusID = Convert.ToInt32(Session["CampusID"]);



                        string EmailFrom = string.Empty, temailto = string.Empty, EmailCC = string.Empty, EmailBCC = string.Empty, Subject = string.Empty, composemailbody = string.Empty;
                        EmailFrom = RtxtEmailFrom.Text.Trim();
                        temailto = Rtxtemailto.Text.Trim();
                        EmailCC = RtxtEmailCC.Text;
                        EmailBCC = RtxtEmailBCC.Text;
                        Subject = RtxtemailSubject.Text;
                        composemailbody = REditComposeBody.Content;

                        DataTable dtemaildetails = DataAccessManager.GetEmailConfigDetail(DeptID, CampusID);
                        if (dtemaildetails.Rows.Count > 0)
                        {

                            string fileName = string.Empty;
                            string filepath = string.Empty;
                            //======================================================================
                            int i = 1;
                            foreach (UploadedFile file in fileuploadComposeTemp.UploadedFiles)
                            {
                                if (i == 1)
                                {
                                    /*fileName = file.FileName;
                                    file.SaveAs(Server.MapPath("~/SentAttachment/") + fileName);
                                    fileName = Server.MapPath("~/SentAttachment/") + fileName;*/

                                    //filname
                             
                                   
                                    fileName = file.FileName;
                                    string[] words = fileName.Split('.');
                                    fileName = words[0] + DateTime.Now.ToString("MMddyyyyhmmsstt") +'.'+ words[1];
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
                                    file.SaveAs(Server.MapPath("~/SentAttachment/" + FolderName + "/") + fileName);
                                    filepath = Server.MapPath("~/SentAttachment/" + FolderName + "/") + fileName;

                                }
                                i = i + 1;
                            }
                            //======================================================================
                            string sentmail = string.Empty;
                            if (fileName != string.Empty)
                            {
                                //fileName = fileuploadComposeTemp.PostedFile.FileName;
                                //fileuploadComposeTemp.SaveAs(Server.MapPath("~/SentAttachment/") + fileuploadComposeTemp.PostedFile.FileName);
                                //fileName = Server.MapPath("~/SentAttachment/") + fileuploadComposeTemp.PostedFile.FileName;
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
                                    Rtxtemailto.Text = "";
                                    RtxtEmailBCC.Text = "";
                                    RtxtEmailCC.Text = string.Empty;
                                    RtxtemailSubject.Text = "";
                                    REditComposeBody.Content = string.Empty;
                                }

                                else
                                {
                                    DataAccessManager.SetSentEmailrecordsLead(LeadId, DeptID, temailto, EmailCC, EmailBCC, EmailFrom, Subject, composemailbody.Replace("'", " "), sentmail, CampusID, fileName);
                                    Rtxtemailto.Text = "";
                                    RtxtEmailBCC.Text = "";
                                    REditComposeBody.Content = string.Empty;
                                    RtxtemailSubject.Text = "";
                                    RtxtEmailCC.Text = string.Empty;
                                }

                          

                               

                                string type = string.Empty;
                                if (StudentNo != string.Empty && StudentNo != null && StudentNo != "0")
                                {
                                    type = "Student";
                                }
                                if (LeadId != string.Empty && LeadId != null && LeadId != "0")
                                {
                                    type = "Lead";
                                }

                                Session["PageClick"] = "0";
                                Response.Redirect("frmstudentlead.aspx?type=" + type + "&Operation=Sent Email");


                                //RlblComposeMailResult.Text = "Send Email Successfully!";
                            }
                            StudentDetailBind();
                            LeadDetailBind();
                        }
                        else
                        {
                            RlblComposeMailResult.Text = "Department Not Configured";
                        }


                    }
                    catch (Exception ex)
                    {

                        //   MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}