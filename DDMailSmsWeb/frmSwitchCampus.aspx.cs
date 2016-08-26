using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DDMailSmsWeb.DataAccess;
using Telerik.Web.UI;


namespace DDMailSmsWeb
{
    public partial class frmSwitchCampus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack == false)
            {

                #region "Url Hit Check"
                if (Request.UrlReferrer != null)
                { }
                else
                {
                    Response.Redirect("~/frmlogin.aspx");
                }
                #endregion
                #region "Session Check"

                if (Session["username"] != null)
                {

                }
                else
                {
                    Response.Redirect("~/frmlogin.aspx");
                }
                #endregion

                #region "set page name by session"
                Session["pagename"] = "Switch Campus";
                #endregion
                HandleMasterPageItem();
                BindCampus();
                #region "Search student lead Control"
                RCBStudentsLeadSearch.Enabled = false;
                string SearchControlLeadId = string.Empty, SearchControlStudentNo = string.Empty;
                SearchControlLeadId = Convert.ToString(Session["LeadId"]);
                SearchControlStudentNo = Convert.ToString(Session["StudentNo"]);
                if (SearchControlLeadId != string.Empty && SearchControlLeadId != null && SearchControlLeadId != "0")
                {
                    #region "Search Control Lead Setting"
                    RCBStudentsLeadSearch.WebServiceSettings.Method = "GetSwitchLeads";
                    RCBStudentsLeadSearch.HeaderTemplate = new LeadHeaderTemplate();
                    RCBStudentsLeadSearch.DropDownWidth = 880;
                    RCBStudentsLeadSearch.EmptyMessage = "Select a Lead";
                    RBSelectLead.Checked = true;
                    #endregion
                }
                else if (SearchControlStudentNo != string.Empty && SearchControlStudentNo != null && SearchControlStudentNo != "0")
                {
                    #region "Search Control Student Setting"
                    RCBStudentsLeadSearch.WebServiceSettings.Method = "GetSwitchStudents";
                    RCBStudentsLeadSearch.HeaderTemplate = new StudentHeaderTemplate();
                    RCBStudentsLeadSearch.DropDownWidth = 500;
                    RCBStudentsLeadSearch.EmptyMessage = "Select a Student";
                    RBSelectStudent.Checked = true;
                    #endregion
                }
                else
                {
                    #region "Search Control Student Setting"
                    RCBStudentsLeadSearch.WebServiceSettings.Method = "GetSwitchStudents";
                    RCBStudentsLeadSearch.HeaderTemplate = new StudentHeaderTemplate();
                    RCBStudentsLeadSearch.DropDownWidth = 500;
                    RCBStudentsLeadSearch.EmptyMessage = "Select a Student";
                    RBSelectStudent.Checked = true;
                    #endregion
                }
                #endregion
            }

        }

        private void HandleMasterPageItem()
        {
            RadMenu RMenuMain = (RadMenu)Master.FindControl("RMenuMain");
            RadPageLayout RPLayoutSubMenu = (RadPageLayout)Master.FindControl("RPLayoutSubMenu");
            RadPageLayout RPLayoutRNav = (RadPageLayout)Master.FindControl("RPLayoutRNav");
            RPLayoutSubMenu.Visible = false;
        }
        private void BindCampus()
        {
            try
            {
                DataTable dtCampus = new DataTable();
                dtCampus = DataAccessManager.FillCampusCreateUserFilter();
                Rddlcampus.DataSource = dtCampus;
                Rddlcampus.DataTextField = "CampusName";
                Rddlcampus.DataValueField = "CampusID";
                Rddlcampus.DataBind();
                Rddlcampus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

            }
        }
        protected void Rddlcampus_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            string campusname = string.Empty;
            campusname = Convert.ToString(Rddlcampus.SelectedText);
            DepartmentBind(campusname);

        }
        private void DepartmentBind(string campusname)
        {
            DataTable dtConfig = DataAccessManager.GetConfigration(campusname);
            if (dtConfig.Rows.Count > 0)
            {
                string connectionstring = string.Empty;
                connectionstring = dtConfig.Rows[0]["CampusConStr"].ToString();
                Session["Switchcampusconstring"] = connectionstring;
                RCBStudentsLeadSearch.Enabled = true;
                try
                {  
                    DataTable dtCampus = new DataTable();
                    dtCampus = DataAccessManager.Getdynamicdepartment(connectionstring);
                    RddlDept.DataSource = dtCampus;
                    RddlDept.DataTextField = "DeptDescription";
                    RddlDept.DataValueField = "DeptID";
                    RddlDept.DataBind();
                    RddlDept.Items[0].Text = "Select Departments";
                    RddlDept.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    RddlDept.DataSource = null;
                    RddlDept.DataBind();
                }
            }
            else
            {
                RCBStudentsLeadSearch.Enabled = false;
                Session["Switchcampusconstring"] = null;
                RddlDept.DataSource = null;
                RddlDept.DataBind();
            }

            StudentLeadSeachSetup();
        }

        private void StudentLeadSeachSetup()
        {
            if (RBSelectLead.Checked == true)
            {
                RCBStudentsLeadSearch.Text = string.Empty;
                RCBStudentsLeadSearch.WebServiceSettings.Method = "GetSwitchLeads";
                RCBStudentsLeadSearch.HeaderTemplate = new LeadHeaderTemplate();
                RCBStudentsLeadSearch.DropDownWidth = 880;
                RCBStudentsLeadSearch.EmptyMessage = "Select a Lead";
            }
            else if (RBSelectStudent.Checked == true)
            {
                RCBStudentsLeadSearch.Text = string.Empty;
                RCBStudentsLeadSearch.WebServiceSettings.Method = "GetSwitchStudents";
                RCBStudentsLeadSearch.HeaderTemplate = new StudentHeaderTemplate();
                RCBStudentsLeadSearch.DropDownWidth = 500;
                RCBStudentsLeadSearch.EmptyMessage = "Select a Student";
            }
        }
        #region "student lead selection redio button"
            #region "student selection"
                protected void RBSelectStudent_Click(object sender, EventArgs e)
                {
                    StudentLeadSeachSetup();
                }
            #endregion  GetSwitchLeads, GetSwitchStudents, Session["Switchcampusconstring"]
                #region "Lead Selection"
                protected void RBSelectLead_Click(object sender, EventArgs e)
                {
                    StudentLeadSeachSetup();
                }
            #endregion
        #endregion
        #region "Lead Student search controle header template Selection"

        class StudentHeaderTemplate : ITemplate
        {
            public void InstantiateIn(Control container)
            {
                HtmlGenericControl header = new HtmlGenericControl("header");
                header.InnerHtml
                    = "<ul style='z-index:100'><li class='col1'>Full Name</li><li class='col2'>Student No</li><li class='col3'>Status</li><li class='col4'>EMail</li></ul>";
                container.Controls.Add(header);
            }
        }
        class LeadHeaderTemplate : ITemplate
        {
            public void InstantiateIn(Control container)
            {
                string headertemplate = string.Empty;
                headertemplate += "<ul style='z-index:100'>";
                headertemplate += "    <li class='Lcol1'>Full Name</li>";
                headertemplate += "    <li class='Lcol2'>Lead ID</li>";
                headertemplate += "    <li class='Lcol3'>Status</li>";
                headertemplate += "    <li class='Lcol4'>AdRep</li>";
                headertemplate += "    <li class='Lcol5'>City</li>";
                headertemplate += "    <li class='Lcol6'>State</li>";
                headertemplate += "    <li class='Lcol7'>Zip</li>";
                headertemplate += "</ul>";
                HtmlGenericControl header = new HtmlGenericControl("header");
                header.InnerHtml = headertemplate;
                container.Controls.Add(header);
            }
        }

        #endregion

        protected void btnSwitchCampus_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string campusname = string.Empty, StudentNo = string.Empty, LeadId = string.Empty, DeptID = string.Empty;
                campusname = Convert.ToString(Rddlcampus.SelectedText);
                DeptID = Convert.ToString(RddlDept.SelectedValue);
                StudentNo = "0";
                LeadId = "0";
                if (RBSelectLead.Checked)
                {
                    LeadId = Convert.ToString(RCBStudentsLeadSearch.SelectedValue);
                    Session["LeadId"] = LeadId;
                    Session["StudentNo"] = StudentNo;
                }
                else if (RBSelectStudent.Checked)
                {
                    StudentNo = Convert.ToString(RCBStudentsLeadSearch.SelectedValue);
                    Session["LeadId"] = LeadId;
                    Session["StudentNo"] = StudentNo;
                }

                DataTable dtConfig = DataAccessManager.GetConfigration(Convert.ToString(campusname));
                if (dtConfig.Rows.Count > 0)
                {
                    Session["GlobalValueKey"] = dtConfig.Rows[0]["CampusConStr"].ToString();
                    Session["CampusID"] = dtConfig.Rows[0]["CampusID"].ToString();
                    Session["Campuslogo"] = dtConfig.Rows[0]["Clientlogo"].ToString();
                }
                 
                if (DeptID != string.Empty)
                {
                    Session["DeptID"] = DeptID;
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
                string Starturl = string.Empty;

                Starturl = "~/frmstudentlead.aspx?type=" + type + "&Operation=Inbox";
                Response.Redirect(Starturl, false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}