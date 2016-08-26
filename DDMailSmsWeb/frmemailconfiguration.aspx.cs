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
    public partial class frmemailconfiguration : System.Web.UI.Page
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
                Session["pagename"] = "Email Configuration";
                #endregion
                HandleMasterPageItem();
                RgvEmailConfigGridBind();
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
        #region "RgvEmailConfiguration"
            private void RgvEmailConfigGridBind()
            {
                DataTable dtEmailConfig = new DataTable();
                dtEmailConfig = DataAccessManager.GetEmailconfiguration();
                Session["dtEmailConfig"] = dtEmailConfig;
                RgvEmailConfig.DataSource = (DataTable)Session["dtEmailConfig"];
            }
            protected void RgvEmailConfig_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
            {
                RgvEmailConfigGridBind();
            }

            protected void RgvEmailConfig_ItemCommand(object sender, GridCommandEventArgs e)
            {
                if (e.CommandName == "imgbtndelete")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    e.Item.Selected = true;
                    string DeleteID = e.CommandArgument.ToString();
                    Boolean result = false;
                    result = DataAccessManager.dELETEEmailConfig(DeleteID);
                    if (result == true)
                    {
                        RgvEmailConfig.Rebind();
                        Reset();
                        RlblEmailconfigResulut.Text = string.Empty;
                        LRRlblEmailconfigResulut.Visible = false;
                        RPbtnSave.Text = "Save";
                    }
                }

                if (e.CommandName == "imgbtnedit")
                {

                    RlblEmailconfigResulut.Text = string.Empty;
                    LRRlblEmailconfigResulut.Visible = false;
                    e.Item.Selected = true;
                    GridDataItem ditem = (GridDataItem)e.Item;
                    Label lbID = (Label)ditem.FindControl("lbID");
                    Label lbCampusID = (Label)ditem.FindControl("lbCampusID");
                    Label lbDeptId = (Label)ditem.FindControl("lbDeptId");
                    Label lbDeptEmail = (Label)ditem.FindControl("lbDeptEmail");
                    Label lbPwd = (Label)ditem.FindControl("lbPwd");
                    Label lbSMTP = (Label)ditem.FindControl("lbSMTP");
                    Label lbPortOut = (Label)ditem.FindControl("lbPortOut");
                    Label lbPop3 = (Label)ditem.FindControl("lbPop3");
                    Label lbPortIn = (Label)ditem.FindControl("lbPortIn");
                    Label lbSSL = (Label)ditem.FindControl("lbSSL");
                    CheckBox ItemChkboxActiveuser = (CheckBox)ditem.FindControl("ItemChkboxActiveuser");
                    RPbtnSave.Text = "Update";
                    ViewState["emailconfigid"] = lbID.Text;
                    Rddlcampus.SelectedValue = Convert.ToString(lbCampusID.Text);
                    //==============================================================
                    string campusname = string.Empty;
                    campusname = Convert.ToString(Rddlcampus.SelectedText);
                    DepartmentBind(campusname);
                    RddlDept.SelectedValue = Convert.ToString(lbDeptId.Text);
                    //==============================================================
                    RtxtDeptEmail.Text = Convert.ToString(lbDeptEmail.Text);
                    RtxtPassword.Text = Convert.ToString(lbPwd.Text);
                    RtxtSMTP.Text = Convert.ToString(lbSMTP.Text);
                    RtxtPort.Text = Convert.ToString(lbPortOut.Text);
                    RtxtPOP3.Text = Convert.ToString(lbPop3.Text);
                    RtxtPortIn.Text = Convert.ToString(lbPortIn.Text);

                    string Checked = Convert.ToString(lbSSL.Text);
                    if (Checked == "True")
                    {
                        RbtnSSL.Checked = true;
                    }
                    else
                    {
                        RbtnSSL.Checked = false;
                    }
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
            foreach (GridDataItem dataItem in RgvEmailConfig.MasterTableView.Items)
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
                    DataAccessManager.GetUpdateEmailConfigStatus(EmailConfigId, strchk);


                }
            }
        }
        #endregion
        protected void RPbtnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                
               LRRlblEmailconfigResulut.Visible = true;
                try
                {
                        string campuseid = string.Empty, deptid = string.Empty, deptemail = string.Empty,departmentname=string.Empty, password = string.Empty, smtp = string.Empty, port = string.Empty
                            , pop3 = string.Empty, portin = string.Empty, ssl = string.Empty, Status=string.Empty;
                        campuseid = Convert.ToString(Rddlcampus.SelectedValue);
                        deptid = Convert.ToString(RddlDept.SelectedValue);
                        departmentname = Convert.ToString(RddlDept.SelectedText);
                        departmentname = Regex.Replace(departmentname, "[0-9]", "");
                        departmentname = departmentname.Replace('(', ' ');
                        departmentname = departmentname.Replace(')', ' ');
                        deptemail = Convert.ToString(RtxtDeptEmail.Text);
                        password = Convert.ToString(RtxtPassword.Text);
                        smtp = Convert.ToString(RtxtSMTP.Text);
                        port = Convert.ToString(RtxtPort.Text);
                        pop3 = Convert.ToString(RtxtPOP3.Text);
                        portin = Convert.ToString(RtxtPortIn.Text);
                       

                        Boolean SSLActive;
                        if (RbtnSSL.Checked == true)
                            SSLActive = true;
                        else
                            SSLActive = false;

                        if (RbtnStatus.Checked == true)
                            Status = "1";
                        else
                            Status = "0";
                       
                       if (RPbtnSave.Text == "Save")
                        {
                            Boolean Result = false;
                            Result = DataAccessManager.InsertEmailConfig(campuseid, deptid, deptemail, password, smtp, port, pop3, portin, SSLActive, Status, departmentname);
                            if(Result==true)
                            {
                                RlblEmailconfigResulut.Text = "Email Configuration Setting Add Successfully!";
                        
                                Reset();
                            }
                            else
                            {
                                RlblEmailconfigResulut.Text = "Operation Failed,Try Again!";
                            }
                        }
                        else if(  RPbtnSave.Text == "Update")
                        {
                            Boolean Result = false;
                            string id=string.Empty;
                            id=Convert.ToString(ViewState["emailconfigid"]);
                            Result = DataAccessManager.UpdateEmailConfig(id, campuseid, deptid, deptemail, password, smtp, port, pop3, portin, SSLActive, Status, departmentname);
                            if(Result==true)
                            {
                                RlblEmailconfigResulut.Text = "Email Configuration Setting Update Successfully!";
                                RPbtnSave.Text = "Save";
                                Reset();
                            }
                            else
                            {
                                RlblEmailconfigResulut.Text = "Operation Failed,Try Again!";
                            }
                            
                        }
                }
                catch(Exception ex)
                {
                    RlblEmailconfigResulut.Text = "Error:"+ex.ToString();
                 
                }
            }
        }

        protected void RPbtnCancel_Click(object sender, EventArgs e)
        {
            //Rddlcampus,RddlDept,RtxtDeptEmail,RtxtPassword,RtxtSMTP,RtxtPort,RtxtPOP3,RtxtPortIn,RtxtSSL
            Reset();
            RlblEmailconfigResulut.Text = string.Empty;
            LRRlblEmailconfigResulut.Visible = false;
            RPbtnSave.Text = "Save";
        }

        private void Reset()
        {
            Rddlcampus.SelectedIndex = 0;
            RddlDept.SelectedIndex = 0;
            RtxtDeptEmail.Text = string.Empty;
            RtxtPassword.Text = string.Empty;
            RtxtSMTP.Text = string.Empty;
            RtxtPort.Text = string.Empty;
            RtxtPOP3.Text = string.Empty;
            RtxtPortIn.Text = string.Empty;
            RbtnSSL.Checked =false;
            RbtnStatus.Checked = false;
            ViewState["emailconfigid"] = string.Empty;
           
           // "Select Departments";
            RgvEmailConfig.Rebind();
        }

        protected void RgvEmailConfig_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            /*if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = GridSortOrder.Ascending;

                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }*/

            this.RgvEmailConfig.MasterTableView.AllowNaturalSort =true;
            this.RgvEmailConfig.MasterTableView.Rebind();
        }
        
        
    }
}