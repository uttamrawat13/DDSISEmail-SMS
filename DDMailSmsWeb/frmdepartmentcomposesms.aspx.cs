using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DDMailSmsWeb.DataAccess;
using DDMailSmsWeb.DynamicAccess;
using Telerik.Web.UI;

namespace DDMailSmsWeb
{
    public partial class TDEPTComposeSMS : System.Web.UI.Page
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
                Session["pagename"] = "Department > Write SMS";
                #endregion
                BindSMSTemplate();
                SMSLongCodeBind();
                RtxtIscode.Text = "+1";
                #region "Search Control"
                    string StudentNo = string.Empty, LeadId = string.Empty;
                    StudentNo = Convert.ToString(Session["StudentNo"]);
                    LeadId = Convert.ToString(Session["leadID"]);
                    if (StudentNo != string.Empty && StudentNo != "0")
                    {
                        #region "Search Control Student Setting"
                        RCBStudentsLeadSearch.WebServiceSettings.Method = "GetStudentsMobileDept";
                        RCBStudentsLeadSearch.HeaderTemplate = new StudentLeadHeaderTemplate();
                        RCBStudentsLeadSearch.EmptyMessage = "Select a Student";
                        #endregion
                    }
                    else if (LeadId != string.Empty && LeadId != "0")
                    {

                        #region "Search Control Lead Setting"
                        RCBStudentsLeadSearch.WebServiceSettings.Method = "GetLeadsMobileDept";
                        RCBStudentsLeadSearch.HeaderTemplate = new StudentLeadHeaderTemplate();
                        RCBStudentsLeadSearch.EmptyMessage = "Select a Lead";
                        #endregion
                    }
                #endregion
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
                    RddlApplySMSTemplateDept.DataSource = dtSMSTemplate;
                    RddlApplySMSTemplateDept.DataTextField = "Title";
                    RddlApplySMSTemplateDept.DataValueField = "ID";
                    RddlApplySMSTemplateDept.DataBind();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void RbtnSMSTempApplyDept_Click(object sender, EventArgs e)
        {
            try
            {
                string templateid = "";
                lblSmSResultDept.Text = string.Empty;
                LRlblSmSResultDept.Visible = false;
                if (RddlApplySMSTemplateDept.SelectedIndex > 0)
                {
                    templateid = Convert.ToString(RddlApplySMSTemplateDept.SelectedValue);


                    DataTable dtshowTemplate = DataAccessManager.getSMSTemplateBodyByID(templateid);
                    if (dtshowTemplate.Rows.Count > 0)
                    {

                        REMessageSMSEditorDept.Text = dtshowTemplate.Rows[0]["Body"].ToString();
                        RLREditorSMSLength.Text = REMessageSMSEditorDept.Text.Length.ToString();

                    }
                }
                else
                {
                    REMessageSMSEditorDept.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                
            }

        }


        protected void RbtmSMSTemplateClearDept_Click(object sender, EventArgs e)
        {
            LRlblSmSResultDept.Visible = false;
            REMessageSMSEditorDept.Text = string.Empty;
            RddlApplySMSTemplateDept.SelectedIndex = 0;
            if (Convert.ToInt32(ViewState["count"]) == 2)
            {
                RddlSMSLongCodeDept.SelectedIndex = 1;
            }
              
            lblSmSResultDept.Text = string.Empty;
        }
        private void SMSLongCodeBind()
        {
            try
            {
                DataTable dtChooseTemplates = new DataTable();
                dtChooseTemplates = DataAccessManager.GetSMSLongCodeList(Convert.ToString(Session["CampusID"]), Convert.ToString(Session["DeptID"]));
                if (dtChooseTemplates.Rows.Count > 0)
                {
         
                    RddlSMSLongCodeDept.DataSource = dtChooseTemplates;
                    RddlSMSLongCodeDept.DataTextField = "LongCode";
                    RddlSMSLongCodeDept.DataValueField = "ID";
                    RddlSMSLongCodeDept.DataBind();
                    int count = 0;
                    count = Convert.ToInt32(dtChooseTemplates.Rows.Count);
                    ViewState["count"] = count;
                    if (Convert.ToInt32(ViewState["count"]) == 2)
                    {
                        RddlSMSLongCodeDept.SelectedIndex = 1;
                    }
              
                }

            }
            catch (Exception ex)
            {
                
            }

        }
        protected void RPbtnSMSSendDept_Click(object sender, EventArgs e)
        {
            try
            {
                #region "Start"
                if (Page.IsValid)
                {
                    LRlblSmSResultDept.Visible = true;
         
                    string SMSLongCode = string.Empty, Mobile = string.Empty, Message = string.Empty, CampusID = string.Empty,
                        StudentNo = string.Empty, LeadNo = string.Empty, DeptId = string.Empty,isdcode=string.Empty;
                    lblSmSResultDept.Text = string.Empty;
                    CampusID = Convert.ToString(Session["CampusID"]);
                    DeptId = Convert.ToString(Session["DeptID"]);

                    StudentNo = "0";
                    LeadNo = "0";

                    SMSLongCode = Convert.ToString(RddlSMSLongCodeDept.SelectedText);
                    isdcode = Convert.ToString(RtxtIscode.Text);
                    Mobile = isdcode + Convert.ToString(RtxtMobileDept.Text); 
                    Message = Convert.ToString(REMessageSMSEditorDept.Text);
                    string SMSLongCodeId = string.Empty;
                    SMSLongCodeId = Convert.ToString(RddlSMSLongCodeDept.SelectedValue);
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
                                            RPbtnSMSSendDept.Enabled = true;
                                            //lblSmSResultDept.Text = "Message has been sent!";
                                            if (Convert.ToInt32(ViewState["count"]) == 2)
                                            {
                                                RddlSMSLongCodeDept.SelectedIndex = 1;
                                            }

                                            RtxtMobileDept.Text = string.Empty;
                                            REMessageSMSEditorDept.Text = string.Empty;
                                            RtxtIscode.Text = "+1";

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
                                            Response.Redirect("~/frmdepartment.aspx?type=Department&Operation=Receive SMS");
                                            /*End*/
                                        }
                                        // DeptGridBind("SMSSent");
                                        //  DeptGridBind("SMSReceived"); ;
                                    }
                                    catch (Exception ex)
                                    {
                                        RPbtnSMSSendDept.Enabled = true;
                                        lblSmSResultDept.Text = "Message Failed!";
                                    }

                                }
                                else
                                {
                                    lblSmSResultDept.Text = "Message failed,Please check the number.";
                                }
                            }
                            else
                            {
                                lblSmSResultDept.Text = "Message Failed!";
                            }
                        }
                        catch (Exception ex)
                        {
                            RPbtnSMSSendDept.Enabled = true;
                            lblSmSResultDept.Text = "Message Failed!";
                        }

                    }

                    else
                    {
                        lblSmSResultDept.Text = "Please make sure update SMS Configration!.";

                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                
            }
        }
        #region "Model Search Mobile No Lead And student"
       
        protected void RCBStudentsSearch_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string searchtext = string.Empty;
            searchtext = e.Text;
            try
            {
                DataTable SwitchToStudetndt = new DataTable();
                string StudentNo = string.Empty, LeadId = string.Empty;
                StudentNo = Convert.ToString(Session["StudentNo"]);
                LeadId = Convert.ToString(Session["leadID"]);
                if (StudentNo != string.Empty && StudentNo != "0")
                { 
                    RCBStudentsSearch.DataSource = DyDataAccessManager.GetStudentMobileNo(searchtext);
                }
                else if (LeadId != string.Empty && LeadId != "0")
                {
                    RCBStudentsSearch.DataSource = DyDataAccessManager.GetLeadMobileNo(searchtext);
                }
                RCBStudentsSearch.DataTextField = "FullName";
                RCBStudentsSearch.DataValueField = "MobilePhone";
                RCBStudentsSearch.DataBind();

            }
            catch (Exception ex)
            {
            }

        }
       
        protected void RCBStudentsSearch_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
           
        }


        #endregion

        #region "Lead Student search controle header template Selection"

        class StudentLeadHeaderTemplate : ITemplate
        {
            public void InstantiateIn(Control container)
            {
                string headertemplate = string.Empty;
                headertemplate += " <ul>";
                headertemplate += " <li class='SMScol1'>Full Name</li>";
                headertemplate += " <li class='SMScol2'>Mobile No</li>";
                headertemplate += " <li class='SMScol3'>Status</li>";
                headertemplate += " </ul>";


                HtmlGenericControl header = new HtmlGenericControl("header");
                header.InnerHtml = headertemplate;

                container.Controls.Add(header);
            }
        }
        protected void RCBStudentsLeadSearch_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                string mobileno = string.Empty;
                mobileno = this.RCBStudentsLeadSearch.SelectedValue;
                RtxtMobileDept.Text = string.Empty;
                if (mobileno != "Not Available")
                {
                    RtxtMobileDept.Text = mobileno;
                }
            }

            catch (Exception ex)
            { }

        }


        #endregion
    }
}