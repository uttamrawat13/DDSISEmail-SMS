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
    public partial class frmaddrole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {

                #region "Session Check"

                if (Session["username"] != null) 
                { 
                
                }
                else
                {
                    Response.Redirect("~/frmlogin.aspx");
                }
                #endregion

                #region "Url Hit Check"
                if (Request.UrlReferrer != null)
                    {
                    
                    }
                    else
                    {
                        Response.Redirect("~/frmlogin.aspx");
                    }
                #endregion
               
                 #region "set page name by session"
                    Session["pagename"] = "User Roles";
                    #endregion
                    HandleMasterPageItem();
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
                dtCampus = DataAccessManager.GetCampusUserRole();
                Rddlcampus.DataSource = dtCampus;
                Rddlcampus.DataTextField = "CampusName";
                Rddlcampus.DataValueField = "CampusID";
                Rddlcampus.DataBind();

                if (Convert.ToString(Session["user_level"]) != "99")
                {
                    string CampusID=string.Empty;
                    CampusID=Convert.ToString(Session["CampusID"]);
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
                DataTable dtRole = new DataTable();
                dtRole = DataAccessManager.GetRole(CampusID);
                Session["dtRole"] = dtRole;
                RgvAddRole.DataSource = (DataTable)Session["dtRole"];
                RgvAddRole.DataBind();
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void RPbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    LRRRlblRoleResult.Visible = true;
                    RlblRoleResult.Text = string.Empty;
                    string CampusID = string.Empty;
                    CampusID = Convert.ToString(Rddlcampus.SelectedValue);
                    if (RPbtnSave.Text == "Save")
                    {
                        if (DataAccessManager.CheckRole(RtxtRoleCode.Text, CampusID, "", "Role_Id") == false) 
                        {
                            RlblRoleResult.Text = "Role code  exist!";
                            return;
                        }

                        Boolean status = DataAccessManager.SaveAddRoleDB(RtxtRoleCode.Text, txtRoleName.Text, CampusID);
                        if (status == true)
                        {
                            txtRoleName.Text = string.Empty;
                            RtxtRoleCode.Text = string.Empty;
                            RlblRoleResult.Text = "Role save successfully!";
                            RtxtRoleCode.Enabled = true;
                        }
                        else
                        {
                             RlblRoleResult.Text = "Execute Failed Please Try Again!";
                            RtxtRoleCode.Enabled = true;

                        }
                    }

                    if (RPbtnSave.Text == "Update")
                    {
                      

                        string updateCampuID = string.Empty;
                        updateCampuID=Convert.ToString(ViewState["updateCampuID"]);
                        Boolean status = DataAccessManager.UpdateAddRoleDB(RtxtRoleCode.Text, txtRoleName.Text, CampusID, updateCampuID);
                        if (status == true)
                        {
                            txtRoleName.Text = string.Empty;
                            RtxtRoleCode.Text = string.Empty;
                            RPbtnSave.Text = "Save";
                            RtxtRoleCode.Enabled = true;
                            RlblRoleResult.Text = "Role update successfully!";
                        }
                        else
                        {
                            RlblRoleResult.Text = "Execute Failed Please Try Again!";
                            RtxtRoleCode.Enabled = false;     
                        }
                    }
                    if (Convert.ToString(Session["user_level"]) != "99")
                    {

                        BindRole(Convert.ToString(Session["CampusID"]));
                    }
                    else
                    {
                        BindRole("");
                        Rddlcampus.SelectedIndex = 0;
                    }
                     
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        protected void RPbtnADD_Click(object sender, EventArgs e)
        {
            txtRoleName.Text = string.Empty;
            RtxtRoleCode.Text = string.Empty;
            RPbtnSave.Text = "Save";
            RtxtRoleCode.Enabled = true;
            RtxtRoleCode.Focus();
            LRRRlblRoleResult.Visible = false;
            RlblRoleResult.Text = string.Empty;
        }

        protected void RgvAddRole_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                LRRRlblRoleResult.Visible = false;
                RlblRoleResult.Text = string.Empty;
                if (e.CommandName == "ViewSMSTemp")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    e.Item.Selected = true;
                    string ID = e.CommandArgument.ToString();
                    ViewState["IDAddRoleTemp"] = string.Empty;
                    Label lbCampusID = (Label)ditem.FindControl("lbCampusID");

                    string campusid = string.Empty;
                    campusid = Convert.ToString(lbCampusID.Text);

                    DataTable dtshowTemplate = DataAccessManager.GetRoleById(ID, campusid);
                    if (dtshowTemplate.Rows.Count > 0)
                    {
                        RtxtRoleCode.Text =Convert.ToString(dtshowTemplate.Rows[0]["Role_Id"]);
                        txtRoleName.Text = Convert.ToString(dtshowTemplate.Rows[0]["Role"]);
                        Rddlcampus.SelectedValue = Convert.ToString(dtshowTemplate.Rows[0]["CampusID"]);
                        RPbtnSave.Text = "Update";
                        ViewState["IDAddRoleTemp"] = ID;
                        ViewState["updateCampuID"] = Convert.ToString(dtshowTemplate.Rows[0]["CampusID"]);
                        
                        RtxtRoleCode.Enabled = false;
                    }
                }


                if (e.CommandName == "DeleteSMSTemp")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    e.Item.Selected = true;
                    string DeleteID = e.CommandArgument.ToString();
                    ViewState["IDSMSTemp"] = string.Empty;
                    Boolean result = false;
                    result = DataAccessManager.DeleteAddRoleDB(DeleteID);
                    if (result == true)
                    {
                        RtxtRoleCode.Text = string.Empty;
                        RPbtnSave.Text = "Save";
                        txtRoleName.Text = string.Empty;
                     //   BindRole();
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "Login Result", "alert('Delete successfully!');", true);
                        RtxtRoleCode.Enabled = true;
                    }


                }
            }
            catch (Exception ex)
            {
                
            }

        }

        
        protected void RgvAddRole_SortCommand(object sender, GridSortCommandEventArgs e)
        {
           /* if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = GridSortOrder.Ascending;
                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }*/
            this.RgvAddRole.MasterTableView.AllowNaturalSort = true;
            this.RgvAddRole.MasterTableView.Rebind();
        }

        protected void RgvAddRole_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                RgvAddRole.DataSource = (DataTable)Session["dtRole"];
                RgvAddRole.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void btngotologin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmlogin.aspx");
        }
       
    }
}