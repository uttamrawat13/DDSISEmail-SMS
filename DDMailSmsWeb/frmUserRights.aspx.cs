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
    public partial class frmUserRights : System.Web.UI.Page
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
                Session["pagename"] = "User Rights"; 
                #endregion
                HandleMasterPageItem();
                BindCampus();

                string user_level = string.Empty;
                user_level = Convert.ToString(Session["user_level"]);
                string Roleid = string.Empty;
                DataTable dtRoleManage = (DataTable)Session["dtRoleManage"];
                if (dtRoleManage.Rows.Count > 0)
                {
                    Roleid = Convert.ToString(dtRoleManage.Rows[0]["Role_Id"]);
                }
                BindUserMenu(Roleid, user_level);

         
               
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
                dtCampus = DataAccessManager.GetCampusUserRole();
                Rddlcampus.DataSource = dtCampus;
                Rddlcampus.DataTextField = "CampusName";
                Rddlcampus.DataValueField = "CampusID";
                Rddlcampus.DataBind();

                if (Convert.ToString(Session["user_level"]) != "99")
                {
                    string CampusID = string.Empty;
                    CampusID = Convert.ToString(Session["CampusID"]);
                    Rddlcampus.SelectedValue = CampusID;
                    Rddlcampus.Enabled = false;
                    BindRole(CampusID);
                }
                else
                {
                    BindRole("");
                    Rddlcampus.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void BindRole(string CampusID)
        {
            try
            {
                DataTable dtRoleManage = new DataTable();
                dtRoleManage = DataAccessManager.GetRoleUserRight(CampusID);
                Session["dtRoleManage"] = dtRoleManage;
                
              

                RGRole.DataSource = (DataTable)Session["dtRoleManage"];
                RGRole.DataBind();

              
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void Rddlcampus_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            if (Rddlcampus.SelectedIndex > 0)
            {
                string CampusID = string.Empty;
                CampusID = Convert.ToString(Rddlcampus.SelectedValue);
                BindRole(CampusID);
            }
            else
            {
                    BindRole("");
                    Rddlcampus.SelectedIndex = 0;
            }
        }
        private void BindUserMenu(string roleid, string user_level)
        {
            try
            {
                DataTable dtUserMenuManage = new DataTable();

                dtUserMenuManage = DataAccessManager.GetUserMenu(roleid, user_level);
                 
                Session["dtUserMenuManage"] = dtUserMenuManage;
                RgvAddRole.DataSource = (DataTable)Session["dtUserMenuManage"];
                RgvAddRole.DataBind();

                //

             

                  
                //=======================================================================================================================
                             //=======================================================================================================================
                
            }
            catch (Exception ex)
            {
                
            }
        }



      
        protected void RgvAddRole_ItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                   
                 
                }
            }
            catch (Exception ex)
            {

            }
        }

      

        protected void RPbtnSave_Click(object sender, EventArgs e)
        {
            string roleid = string.Empty;
            roleid = Convert.ToString(ViewState["userrightroleid"]);

            bool result = false;
                
            for (int i = 0; i < RgvAddRole.Items.Count; i++)
            {
                CheckBox chkShowMenu = (CheckBox)RgvAddRole.Items[i].FindControl("chkShowMenu");
                Label Rmenu_id = (Label)RgvAddRole.Items[i].FindControl("Rmenu_id");
                string menu_id = Convert.ToString(Rmenu_id.Text);

                string showmenu = string.Empty;
                showmenu = "0";
                if (chkShowMenu.Checked == true)
                {
                    showmenu = "1";
                }

                result = DataAccessManager.SaveUserRight(roleid,menu_id,showmenu);
            }
            if (result == true)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "User Rights Add  Result", "alert('User rights added successfully!');", true);
                string user_level = string.Empty;
                user_level = Convert.ToString(Session["user_level"]);
                DataTable dtUserMenuManage = new DataTable();
                dtUserMenuManage = DataAccessManager.GetUserMenu(roleid, user_level);              
                Session["dtUserMenuManage"] = dtUserMenuManage;
                RgvAddRole.DataSource = (DataTable)Session["dtUserMenuManage"];
                RgvAddRole.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "User Rights Add  Result", "alert('Operation Failed,Please try again!');", true);
            }
        }

        protected void RgvAddRole_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            this.RgvAddRole.MasterTableView.AllowNaturalSort = true;
            this.RgvAddRole.MasterTableView.Rebind();
        }
        protected void RgvAddRole_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ViewSMSTemp")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    e.Item.Selected = true;
                  string user_level=string.Empty;
                  user_level = Convert.ToString(Session["user_level"]);
                    string roleid = e.CommandArgument.ToString();
                    ViewState["userrightroleid"] = roleid;
                    BindUserMenu(roleid,user_level);
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected void RgvAddRole_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                RgvAddRole.DataSource = (DataTable)Session["dtUserMenuManage"];
                RgvAddRole.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void RGRole_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            
            try
            {
                RGRole.DataSource = (DataTable)Session["dtRoleManage"];
                RGRole.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        

   }
}  
 