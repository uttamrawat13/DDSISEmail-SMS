using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDMailSmsWeb.DataAccess;
using DDMailSmsWeb.DynamicAccess;
using Telerik.Web.UI;


namespace DDMailSmsWeb
{
    public partial class frmSMSconfiguration : System.Web.UI.Page
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
                Session["pagename"] = "SMS Configuration";
                #endregion
                HandleMasterPageItem();
                RgvSMSConfigGridBind();
                BindCampus();
                 
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
        #region "Department Bind"

        protected void Rddlcampus_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            string campusname = string.Empty;
            campusname=Convert.ToString(Rddlcampus.SelectedText);
            DepartmentBind(campusname);

        }

        private void DepartmentBind(string campusname)
        {
            DataTable dtConfig = DataAccessManager.GetConfigration(campusname);
            if (dtConfig.Rows.Count > 0)
            {
                string connectionstring = string.Empty;
                connectionstring = dtConfig.Rows[0]["CampusConStr"].ToString();
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
                RddlDept.DataSource = null;
                RddlDept.DataBind();
            }
        }

        #endregion
        #region "RgvEmailConfiguration"
        private void RgvSMSConfigGridBind()
            {
                DataTable dtSMSConfig = new DataTable();
                dtSMSConfig = DataAccessManager.GetSMSconfiguration();
                Session["dtSMSConfig"] = dtSMSConfig;
                RgvSMSConfig.DataSource = (DataTable)Session["dtSMSConfig"];
            }
            protected void RgvSMSConfig_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
            {
                RgvSMSConfigGridBind();
            }
            protected void RgvSMSConfig_ItemCommand(object sender, GridCommandEventArgs e)
            {
                if (e.CommandName == "imgbtndelete")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    e.Item.Selected = true;
                    string DeleteID = e.CommandArgument.ToString();
                    Boolean result = false;
                    result = DataAccessManager.dELETESMSConfig(DeleteID);
                    if (result == true)
                    {
                        RgvSMSConfig.Rebind();
                        Reset();
                        RlblSMSconfigResulut.Text = string.Empty;
                        LRRlblSMSconfigResulut.Visible = false;
                        RPbtnSave.Text = "Save";
                    }
                }

                if (e.CommandName == "imgbtnedit")
                {

                    RlblSMSconfigResulut.Text = string.Empty;
                    LRRlblSMSconfigResulut.Visible = false;
                    e.Item.Selected = true;
                    GridDataItem ditem = (GridDataItem)e.Item;
                   
                    Label lbID = (Label)ditem.FindControl("lbID");
                    Label lbCampusID = (Label)ditem.FindControl("lbCampusID");
                    Label lbDeptId = (Label)ditem.FindControl("lbDeptId");
                    Label lbLongCode = (Label)ditem.FindControl("lbLongCode");
                    Label lbAuthToken = (Label)ditem.FindControl("lbAuthToken");
                    Label lbAccountSID = (Label)ditem.FindControl("lbAccountSID");
                    CheckBox ItemChkboxActiveuser = (CheckBox)ditem.FindControl("ItemChkboxActiveuser");
                    RPbtnSave.Text = "Update";
                    ViewState["SMSconfigid"] = lbID.Text;
                    Rddlcampus.SelectedValue = Convert.ToString(lbCampusID.Text);

                    string campusname = string.Empty;
                    campusname = Convert.ToString(Rddlcampus.SelectedText);
                    DepartmentBind(campusname);

                    RddlDept.SelectedValue = Convert.ToString(lbDeptId.Text);
                    RtxtLongCode.Text = Convert.ToString(lbLongCode.Text);
                    RtxtAuthToken.Text = Convert.ToString(lbAuthToken.Text);
                     RtxtAccountSID.Text = Convert.ToString(lbAccountSID.Text);
                    

    
                    if (ItemChkboxActiveuser.Checked == true)
                    {
                        RbtnStatus.Checked = true;
                    }
                    else
                    {
                        RbtnStatus.Checked = false;
                    }
                }

            }
            protected void ItemChkboxActiveuser_CheckedChanged(object sender, EventArgs e)
            {
                ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
                bool checkHeader = true;
                foreach (GridDataItem dataItem in RgvSMSConfig.MasterTableView.Items)
                {
                    if ((dataItem.FindControl("ItemChkboxActiveuser") as CheckBox).ClientID == (sender as CheckBox).ClientID)
                    {
                        string EmailConfigId = Convert.ToString((dataItem.Cells[0].FindControl("lbID") as Label).Text);
                        string strchk = "";
                        if ((sender as CheckBox).Checked == true)
                        {
                            strchk = "true";
                        }
                        else
                        {
                            strchk = "false";
                        }
                        DataAccessManager.GetUpdateSMSConfigStatus(EmailConfigId, strchk);


                    }
                }
            }
            protected void RgvSMSConfig_SortCommand(object sender, GridSortCommandEventArgs e)
            {
                this.RgvSMSConfig.MasterTableView.AllowNaturalSort = true;
                this.RgvSMSConfig.MasterTableView.Rebind();
            }
        #endregion
        protected void RPbtnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                LRRlblSMSconfigResulut.Visible = true;
                try
                {
                    string campuseid = string.Empty, deptid = string.Empty, DeptName = string.Empty, longcode = string.Empty, Status = string.Empty, AuthToken = string.Empty, AccountSID = string.Empty;
                    campuseid = Convert.ToString(Rddlcampus.SelectedValue);
                    deptid = Convert.ToString(RddlDept.SelectedValue);
                    DeptName = Convert.ToString(RddlDept.SelectedText);

                    DeptName = Regex.Replace(DeptName, "[0-9]", "");
                    DeptName = DeptName.Replace('(', ' ');
                    DeptName = DeptName.Replace(')', ' ');

                    longcode = Convert.ToString(RtxtLongCode.Text);
                    AccountSID = Convert.ToString(RtxtAccountSID.Text);
                    AuthToken = Convert.ToString(RtxtAuthToken.Text);
                    if (RbtnStatus.Checked == true)
                        Status = "1";
                    else
                        Status = "0";

                    if (RPbtnSave.Text == "Save")
                    {
                        Boolean Result = false;
                        Result = DataAccessManager.InsertSMSConfig(campuseid, deptid, longcode, Status, AccountSID, AuthToken,DeptName);
                        if (Result == true)
                        {
                            RlblSMSconfigResulut.Text = "SMS Configuration Setting Add Successfully!";

                            Reset();
                        }
                        else
                        {
                            RlblSMSconfigResulut.Text = "Operation Failed,Try Again!";
                        }
                    }
                    else if (RPbtnSave.Text == "Update")
                    {
                        Boolean Result = false;
                        string id = string.Empty;
                        id = Convert.ToString(ViewState["SMSconfigid"]);
                        Result = DataAccessManager.UpdateSMSConfig(id, campuseid, deptid, longcode, Status, AccountSID, AuthToken, DeptName);
                        if (Result == true)
                        {
                            RlblSMSconfigResulut.Text = "SMS Configuration Setting Update Successfully!";
                            RPbtnSave.Text = "Save";
                            Reset();
                        }
                        else
                        {
                            RlblSMSconfigResulut.Text = "Operation Failed,Try Again!";
                        }

                    }
                }
                catch (Exception ex)
                {
                    RlblSMSconfigResulut.Text = "Error:" + ex.ToString();

                }
            }
        }

        protected void RPbtnCancel_Click(object sender, EventArgs e)
        {
            Reset();
            RlblSMSconfigResulut.Text = string.Empty;
            LRRlblSMSconfigResulut.Visible = false;
            RPbtnSave.Text = "Save";
        }
        private void Reset()
        {
            Rddlcampus.SelectedIndex = 0;
            RddlDept.SelectedIndex = 0;
            RtxtLongCode.Text = string.Empty;
            RtxtAccountSID.Text = string.Empty;
            RtxtAuthToken.Text = string.Empty;
            RbtnStatus.Checked = false;
            ViewState["SMSconfigid"] = string.Empty;
          
            RgvSMSConfig.Rebind();
        }

       

         
    }
}