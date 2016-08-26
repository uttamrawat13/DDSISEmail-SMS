using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDMailSmsWeb.DataAccess;
using Telerik.Web.UI;


namespace DDMailSmsWeb
{
    public partial class frmManageUser : System.Web.UI.Page
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
                Session["pagename"] = "Manage Users";
                #endregion
                HandleMasterPageItem();
               
                BindCampus();
                string CampusID = string.Empty;
                CampusID = Convert.ToString(Rddlcampus.SelectedValue);
                BindRole(CampusID);

                RgvManageUserGridBind();
            }
          
        }
        private void HandleMasterPageItem()
        {
            RadMenu RMenuMain = (RadMenu)Master.FindControl("RMenuMain");
            //RMenuMain.Items.FindItemByText("SMS Templates").Selected = true;
            RadPageLayout RPLayoutSubMenu = (RadPageLayout)Master.FindControl("RPLayoutSubMenu");
            RadPageLayout RPLayoutRNav = (RadPageLayout)Master.FindControl("RPLayoutRNav");
            RPLayoutSubMenu.Visible = false;
        }
        private void BindRole(string CampusID )
        {
            try
            {
                DataTable dtRole = new DataTable();
                dtRole = DataAccessManager.FillRoleCreateUser(CampusID);
                RddlRole.DataSource = dtRole;
                RddlRole.DataTextField = "Role";
                RddlRole.DataValueField = "Role_Id";
                RddlRole.DataBind();
                RddlRole.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                
            }
        }
        private void BindCampus()
        {
            try
            {
                string user_level = string.Empty,CampusID=string.Empty;
                user_level = Convert.ToString(Session["user_level"]);
                CampusID = Convert.ToString(Session["CampusID"]);
                DataTable dtCampus = new DataTable();
                dtCampus = DataAccessManager.FillCampusCreateUser();
                Rddlcampus.DataSource = dtCampus;
                Rddlcampus.DataTextField = "CampusName";
                Rddlcampus.DataValueField = "CampusID";
                Rddlcampus.DataBind();
                Rddlcampus.SelectedIndex = 0;

                if (user_level == "99")
                {
                    Rddlcampus.Enabled = true;
                }
                else
                {
                    Rddlcampus.SelectedValue = CampusID;
                    Rddlcampus.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void Rddlcampus_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            string CampusID = string.Empty,user_level = string.Empty;
            CampusID = Convert.ToString(Rddlcampus.SelectedValue);
            RddlRole.Enabled = true;
            user_level = Convert.ToString(Session["user_level"]);
            BindRole(CampusID);
            /*if (CampusName == "DSIS")
            {
                BindRole("DSIS");
                RddlRole.SelectedValue = "99";
                RddlRole.Enabled = false;
            }
            else
            {
                 BindRole("");
            }*/

        }
        #region "RgvManageUser"
                private void RgvManageUserGridBind()
                {
                    string CampusID=string.Empty,user_level=string.Empty;
                    CampusID=Convert.ToString(Session["CampusID"]);
                    user_level = Convert.ToString(Session["user_level"]);

                    DataTable dtUserList = new DataTable();
                    if (user_level == "99")
                    {
                        dtUserList = DataAccessManager.GetUserList("");
                    }
                    else
                    {

                        dtUserList = DataAccessManager.GetUserList(CampusID);
                    }
                 
                   
                    Session["dtUserList"] = dtUserList;
                    RgvManageUser.DataSource = (DataTable)Session["dtUserList"];
                }

                protected void RgvManageUser_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
                {
                        RgvManageUserGridBind();
                }

                protected void RgvManageUser_ItemCommand(object sender, GridCommandEventArgs e)
                {
                    if (e.CommandName == "imgbtndelete")
                    {
                        GridDataItem ditem = (GridDataItem)e.Item;
                        e.Item.Selected = true;
                        string DeleteID = e.CommandArgument.ToString();
                        Boolean result = false;
                        result = DataAccessManager.GetDeleteUser(DeleteID);
                        if (result == true)
                        {
                            LRRlblCreateNewUserResult.Visible = false;
                            RlblCreateNewUserResult.Text = string.Empty;
                            RPbtnSave.Text = "Save";
                            Reset();
                        }    
                    }

                    if (e.CommandName == "imgbtnedit")
                    {
                        LRRlblCreateNewUserResult.Visible = false;
                        RlblCreateNewUserResult.Text = string.Empty;
                        e.Item.Selected = true;
                        GridDataItem ditem = (GridDataItem)e.Item;
                        Label lbUserID = (Label)ditem.FindControl("lbUserID");
                        Label lbCampusId = (Label)ditem.FindControl("lbCampusId");
                        Label lbCampusName = (Label)ditem.FindControl("lbCampusName");
                        Label lbRoleId = (Label)ditem.FindControl("lbRoleId");
                        Label lbSwitchDept = (Label)ditem.FindControl("lbSwitchDept");
                        Label lblpassword = (Label)ditem.FindControl("lblpassword");

                        
                        
                        string lbUsername = Convert.ToString(ditem["UNUsername"].Text);
                        CheckBox ItemChkboxActiveuser = (CheckBox)ditem.FindControl("ItemChkboxActiveuser");
                        RPbtnSave.Text = "Update";
                        ViewState["userid"] = lbUserID.Text;
                        RFVtxtConfirmPassword.Enabled=false;
                        RFVtxtPassword.Enabled = false;


                        Rddlcampus.SelectedValue = Convert.ToString(lbCampusId.Text);

                        string CampusID = string.Empty;
                        CampusID = Convert.ToString(Rddlcampus.SelectedValue);

                      
                        BindRole(CampusID);
                        /*if (lbCampusName.Text == "DSIS")
                        {
                            BindRole("DSIS");
                            RddlRole.SelectedValue = "99";
                            RddlRole.Enabled = false;
                        }
                        else
                        {
                            BindRole("");
                        }*/

                        RddlRole.SelectedValue = Convert.ToString(lbRoleId.Text);
                        txtUserName.Text = lbUsername;
                        //txtPassword.Text = lblpassword.Text;
                        if (ItemChkboxActiveuser.Checked == true)
                        {
                            RbtnUserStatus.Checked = true;
                        }
                        else
                        {
                            RbtnUserStatus.Checked = false;
                        }
                        Boolean allowswitchuser = false;
                        allowswitchuser = Convert.ToBoolean(lbSwitchDept.Text);

                        RbtnAllSwitchdept.Checked = false;
                        if (allowswitchuser == true)
                        {
                            RbtnAllSwitchdept.Checked = true;                 
                        }
                    }   

                }

                protected void ItemChkboxActiveuser_CheckedChanged(object sender, EventArgs e)
                {
                    ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
                    bool checkHeader = true;
                    foreach (GridDataItem dataItem in RgvManageUser.MasterTableView.Items)
                    {
                        if ((dataItem.FindControl("ItemChkboxActiveuser") as CheckBox).ClientID == (sender as CheckBox).ClientID)
                        {
                            string UserID = Convert.ToString((dataItem.Cells[0].FindControl("lbUserID") as Label).Text);
                            string strchk = "";
                            if ((sender as CheckBox).Checked == true)
                            {
                                strchk = "true";
                            }
                            else
                            {
                                strchk = "false";
                            }
                            DataAccessManager.GetUpdateUserStatus(UserID, strchk);
                        }
                    }
                }

       #endregion

        protected void RPbtnSave_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                LRRlblCreateNewUserResult.Visible = true;

                try
                {
                    string campusid = string.Empty, Roleid = string.Empty, username = string.Empty, password = string.Empty, confrimpassword = string.Empty,allowswitch=string.Empty;
                    campusid = Convert.ToString(Rddlcampus.SelectedValue);
                    Roleid = Convert.ToString(RddlRole.SelectedValue);
                    username = txtUserName.Text;
                    password = txtPassword.Text;
                    confrimpassword = txtConfirmPassword.Text;
                    

                    string userstatus = string.Empty;
                    if (RbtnUserStatus.Checked == true)
                    { userstatus = "1"; }
                    else
                    { userstatus = "0"; }

                    if (RbtnAllSwitchdept.Checked == true)
                    { allowswitch = "1";}
                    else
                    { allowswitch = "0"; }

                    if (RPbtnSave.Text == "Save")
                    {
                        Boolean result = DataAccessManager.InsertUser(username, password, Roleid, userstatus, campusid, allowswitch);
                        if (result == true)
                        {
                            LRRlblCreateNewUserResult.Visible = true;
                            RlblCreateNewUserResult.Text = "User added successfully!";
                            Reset();
                        }
                        else
                        {
                            LRRlblCreateNewUserResult.Visible = true;
                            RlblCreateNewUserResult.Text = "Operation Failed,Please try again!";
                        }
                    }
                    else if (RPbtnSave.Text == "Update")
                    {
                        Boolean Result = false;
                        string UserID = string.Empty;
                        UserID = Convert.ToString(ViewState["userid"]);

                        Result = DataAccessManager.UpdateUser(UserID, username, password, Roleid, userstatus, campusid, allowswitch);
                        
                        
                        if (Result == true)
                        {
                            RlblCreateNewUserResult.Text = "User update successfully!";
                            RPbtnSave.Text = "Save";

                            Reset();
                        }
                        else
                        {
                            RlblCreateNewUserResult.Text = "Operation Failed,Try Again!";
                        }

                    }
                }
                catch (Exception ex)
                {
                    RlblCreateNewUserResult.Text = "Error:" + ex.ToString();
                }
            }
        }
        protected void RPbtnCancel_Click(object sender, EventArgs e)
        {
            LRRlblCreateNewUserResult.Visible = false;
            RlblCreateNewUserResult.Text = string.Empty;
            RPbtnSave.Text = "Save";
            Reset();
        }

        private void Reset()
        {
            
            Rddlcampus.SelectedIndex = 0;
            RddlRole.SelectedIndex = 0;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            RbtnUserStatus.Checked = false;
            RbtnAllSwitchdept.Checked = false;
            RddlRole.Enabled = true;
            string CampusID = string.Empty, user_level = string.Empty;
            CampusID = Convert.ToString(Rddlcampus.SelectedValue);
            RddlRole.Enabled = true;
            user_level = Convert.ToString(Session["user_level"]);
            BindRole(CampusID);
            ViewState["userid"] = string.Empty;
            RgvManageUser.Rebind();
            RFVtxtConfirmPassword.Enabled = true;
            RFVtxtPassword.Enabled = true;
        }

        protected void RgvManageUser_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            /*if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = GridSortOrder.Ascending;
                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }*/
            this.RgvManageUser.MasterTableView.AllowNaturalSort = true;
            this.RgvManageUser.MasterTableView.Rebind();
        }

       
    }
}