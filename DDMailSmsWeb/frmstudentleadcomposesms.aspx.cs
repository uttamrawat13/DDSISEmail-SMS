using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class TSTDLEADComposeSMS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
            RadButton RbtnComposeSMS = (RadButton)Master.FindControl("RbtnComposeSMS");
            RbtnComposeSMS.Focus();
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
                    Session["pagename"] = "Student > Write SMS";
                }
                else
                {
                    Session["pagename"] = "Lead > Write SMS";
                }
                #endregion
                StudentDetailBind();
                lblSmSResult.Text = string.Empty;
                LRlblSmSResult.Visible = false;
                LeadDetailBind();
                SMSLongCodeBind();
                BindSMSTemplate();
                RtxtIscode.Text = "+1";
            
            }

             
        }
        //=============================================================================================================
        // bind textbox composeemail in student   id and department
        private void StudentDetailBind()
        {
            try
            {
                string StudentNo = string.Empty, LeadId = string.Empty;
                StudentNo = Convert.ToString(Session["StudentNo"]);
                LeadId = Convert.ToString(Session["leadID"]);
                DataTable dtStudentEmail = new DataTable();
                dtStudentEmail = DyDataAccessManager.GetStudentEmail(StudentNo);
                if (dtStudentEmail.Rows.Count > 0)
                {
                    RtxtMobile.Text = Convert.ToString(dtStudentEmail.Rows[0]["MobilePhone"]);
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
                StudentNo = Convert.ToString(Session["StudentNo"]);
                LeadId = Convert.ToString(Session["leadID"]);

                DataTable dtStudentEmail = new DataTable();
                dtStudentEmail = DyDataAccessManager.GetLeadEmail(LeadId);
                if (dtStudentEmail.Rows.Count > 0)
                {
                    RtxtMobile.Text = Convert.ToString(dtStudentEmail.Rows[0]["PhoneMobile"]);
                }
               
            }
            catch (Exception ex)
            {
                
            }
        }

        //=============================================================================================================
        private void SMSLongCodeBind()
        {
            try
            {
                DataTable dtChooseTemplates = new DataTable();
                dtChooseTemplates = DataAccessManager.GetSMSLongCodeList(Convert.ToString(Session["CampusID"]), Convert.ToString(Session["DeptID"]));
                if (dtChooseTemplates.Rows.Count > 0)
                {
                    RddlSMSLongCode.DataSource = dtChooseTemplates;
                    RddlSMSLongCode.DataTextField = "LongCode";
                    RddlSMSLongCode.DataValueField = "ID";
                    RddlSMSLongCode.DataBind();
                    int count = 0;
                    count = Convert.ToInt32(dtChooseTemplates.Rows.Count);
                    ViewState["STDcount"] = count;
                    if (Convert.ToInt32(ViewState["STDcount"]) == 2)
                    {
                        RddlSMSLongCode.SelectedIndex = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

        }
        
        protected void RbtnSMSTempApply_Click(object sender, EventArgs e)
        {
            try
            {
                string templateid = "";
                LRlblSmSResult.Visible = false;
                lblSmSResult.Text = string.Empty;
                if (RddlApplySMSTemplate.SelectedIndex > 0)
                {
                    templateid = Convert.ToString(RddlApplySMSTemplate.SelectedValue);
                    DataTable dtshowTemplate = DataAccessManager.getSMSTemplateBodyByID(templateid);
                    if (dtshowTemplate.Rows.Count > 0)
                    {
                        REMessageSMSEditor.Text = dtshowTemplate.Rows[0]["Body"].ToString();
                        RLREditorSMSLength.Text = REMessageSMSEditor.Text.Length.ToString();
                    }
                }
                else
                {
                    REMessageSMSEditor.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                
            }

        }

        protected void RbtmSMSTemplateClear_Click(object sender, EventArgs e)
        {
            try
            {
                LRlblSmSResult.Visible = false;
                lblSmSResult.Text = string.Empty;
                REMessageSMSEditor.Text = string.Empty;
                RddlApplySMSTemplate.SelectedIndex = 0;
                if (Convert.ToInt32(ViewState["STDcount"]) == 2)
                {
                    RddlSMSLongCode.SelectedIndex = 1;
                }
            }
            catch (Exception ex)
            {
                
            }

        }

        private void BindSMSTemplate()
        {
            try
            {
                DataTable dtSMSTemplate = new DataTable();
                dtSMSTemplate = DataAccessManager.getSMSTemplateddlList(Convert.ToString(Session["CampusID"]));
                if (dtSMSTemplate.Rows.Count > 0)
                {
                    RddlApplySMSTemplate.DataSource = dtSMSTemplate;
                    RddlApplySMSTemplate.DataTextField = "Title";
                    RddlApplySMSTemplate.DataValueField = "ID";
                    RddlApplySMSTemplate.DataBind();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void btnSMSSend_Click(object sender, EventArgs e)
        {
            try
            {
                #region "Start"
                if (Page.IsValid)
                {
                    LRlblSmSResult.Visible = true;
                    string SMSLongCode = string.Empty, Mobile = string.Empty, Message = string.Empty, CampusID = string.Empty,
                    StudentNo = string.Empty, LeadNo = string.Empty, DeptId = string.Empty, isdcode=string.Empty;
                    lblSmSResult.Text = string.Empty;
                    CampusID = Convert.ToString(Session["CampusID"]);
                    StudentNo = Convert.ToString(Session["StudentNo"]);
                    LeadNo = Convert.ToString(Session["leadID"]);
                    DeptId = Convert.ToString(Session["DeptID"]);

                    if (StudentNo == string.Empty)
                    {
                        StudentNo = "0";
                    }
                    if (LeadNo == string.Empty)
                    {
                        LeadNo = "0";
                    }

                    SMSLongCode = Convert.ToString(RddlSMSLongCode.SelectedText);
                    isdcode = Convert.ToString(RtxtIscode.Text);
                    Mobile = isdcode + Convert.ToString(RtxtMobile.Text); 
                    Message = Convert.ToString(REMessageSMSEditor.Text);

                    Boolean mobilechk = true;
                    if (StudentNo != "0")
                    {
                        mobilechk = DyDataAccessManager.CheckStdMobleSMS(Mobile);
                    }
                    if (LeadNo != "0")
                    {
                        mobilechk = DyDataAccessManager.CheckStdMobleSMS(Mobile);
                    }

                    string SMSLongCodeId = string.Empty;
                    SMSLongCodeId = Convert.ToString(RddlSMSLongCode.SelectedValue);
                    DataTable dtSMSSetup = DataAccessManager.GetSMSLongCodeFullList(Convert.ToString(Session["CampusID"]), SMSLongCodeId);
                    if (dtSMSSetup.Rows.Count > 0)
                    {
                        try
                        {
                            var twilio = new Twilio.TwilioRestClient(dtSMSSetup.Rows[0]["AccountSID"].ToString(), dtSMSSetup.Rows[0]["AuthToken"].ToString());
                            var message = twilio.SendMessage(SMSLongCode, Mobile, Message);
                            if (message != null)
                            {
                                if (message.Sid != null)
                                {
                                    try
                                    {
                                        var twilioGet = new Twilio.TwilioRestClient(dtSMSSetup.Rows[0]["AccountSID"].ToString(), dtSMSSetup.Rows[0]["AuthToken"].ToString());
                                        var msg = twilioGet.GetMessage(message.Sid);
                                        Boolean result = DataAccessManager.ReadSMSAndSaveDatabase(msg.AccountSid, msg.DateSent.ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToString(msg.Sid), Convert.ToString(msg.To), Convert.ToString(msg.From), Convert.ToString(msg.Body), Convert.ToString(msg.Status), Convert.ToString(msg.Direction), Convert.ToString(msg.ErrorCode), Convert.ToString(msg.ErrorMessage), msg.DateCreated.ToString("yyyy-MM-dd HH:mm:ss"), msg.DateUpdated.ToString("yyyy-MM-dd HH:mm:ss"), Convert.ToString(StudentNo), Convert.ToString(LeadNo), Convert.ToString(DeptId));
                                        if (result == true)
                                        {
                                            lblSmSResult.Text = "Message has been sent!";
                                            RddlSMSLongCode.SelectedIndex = 0;
                                            if (Convert.ToInt32(ViewState["STDcount"]) == 2)
                                            {
                                                RddlSMSLongCode.SelectedIndex = 1;
                                            }
                                            RtxtIscode.Text = "+1";
                                            RtxtMobile.Text = string.Empty;
                                            REMessageSMSEditor.Text = string.Empty;
                                            SMSLongCodeBind();
                                            BindSMSTemplate();
                                            StudentDetailBind();
                                            LeadDetailBind();
                                            /*27 july change After SMS Message send, return to SMS page*/
                                            /*Start*/
                                                string LeadId = string.Empty;
                                                LeadId = Convert.ToString(Session["LeadId"]);
                                                StudentNo = Convert.ToString(Session["StudentNo"]);

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
                                                Response.Redirect("frmstudentlead.aspx?type=" + type + "&Operation=Receive SMS");
                                            /*End*/
             
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        btnSMSSend.Enabled = true;
                                        lblSmSResult.Text = "Message Failed!";
                                    }
                                }
                                else
                                {
                                    lblSmSResult.Text = "Message failed,Please check the number.";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            btnSMSSend.Enabled = true;
                            lblSmSResult.Text = "Message Failed!";
                        }
                    }

                    else
                    {
                        btnSMSSend.Enabled = true;
                        lblSmSResult.Text = "Please make sure update SMS Configration!.";

                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                
            }

        }
        
    }
}