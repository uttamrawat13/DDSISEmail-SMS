using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDMailSmsWeb.DataAccess;
using Telerik.Web.UI;
using System.IO;


namespace DDMailSmsWeb
{
    public partial class frmCampusLogoMaster : System.Web.UI.Page
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
                Session["pagename"] = "Manage Campus Logo";
                #endregion
                HandleMasterPageItem();
                RgvCampusMasterGridBind();
                CapusLisBind();
             }
        }
        private void HandleMasterPageItem()
        {
            RadMenu RMenuMain = (RadMenu)Master.FindControl("RMenuMain");
            RadPageLayout RPLayoutSubMenu = (RadPageLayout)Master.FindControl("RPLayoutSubMenu");
            RPLayoutSubMenu.Visible = false;
         
        }
        private void CapusLisBind()
        {
            DataTable dtCampusList = new DataTable();
            dtCampusList = DataAccessManager.GetCampusList();
            if (dtCampusList.Rows.Count > 0)
            {
                RddlCampus.DataSource = dtCampusList;
                RddlCampus.DataTextField = "CampusName";
                RddlCampus.DataValueField = "CampusID";
                RddlCampus.DataBind();
            }
        }
        #region "RgvCampusMaster"

        private void RgvCampusMasterGridBind()
        {
            DataTable dtCampusLogoMasterList = new DataTable();
            dtCampusLogoMasterList = DataAccessManager.GetCampusLogoMasterList();
            Session["dtCampusLogoMasterList"] = dtCampusLogoMasterList;
            RgvCampusMaster.DataSource = (DataTable)Session["dtCampusLogoMasterList"];
        }

        protected void RgvCampusMaster_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RgvCampusMaster.DataSource = (DataTable)Session["dtCampusLogoMasterList"];
        }

        protected void RgvCampusMaster_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (e.CommandName == "imgbtnedit")
            {
                e.Item.Selected = true;
                GridDataItem ditem = (GridDataItem)e.Item;
                Label lbCampusID = (Label)ditem.FindControl("lbCampusID");
                string campusid = string.Empty;
                campusid = Convert.ToString(lbCampusID.Text);
                RddlCampus.SelectedValue = campusid;
            }
        }

        #endregion
       
        

        protected void RPbtnCancel_Click(object sender, EventArgs e)
        {
            LRRlblCreateCampusLogoResult.Visible = false;
            RlblCreateCampusLogoResult.Text = string.Empty;
            RddlCampus.SelectedValue="0";
        } 

        protected void RPbtnUpdate_Click(object sender, System.EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    LRRlblCreateCampusLogoResult.Visible = true;
                    RlblCreateCampusLogoResult.Text = string.Empty;
                    String Clientlogo = string.Empty;


                    int i = 0;
                    foreach (UploadedFile file in FUCampusLogo.UploadedFiles)
                    {

                        Clientlogo = file.FileName;
                        file.SaveAs(Server.MapPath("~/Content/clientlogo/") + Clientlogo);
                        i = 1;
                    }
                    #region "start"
                    if (i == 0)
                    {
                        RlblCreateCampusLogoResult.Text = "Please select a logo!";
                        return;
                    }
                    else
                    {
                        Boolean Result = false;
                        string CampusID = string.Empty;
                        CampusID = Convert.ToString(RddlCampus.SelectedValue);
                        Result = DataAccessManager.UpdateCampuslogoMaster(CampusID, Clientlogo);
                        if (Result == true)
                        {
                            RlblCreateCampusLogoResult.Text = "Logo Updated!";
                            RddlCampus.SelectedValue = "0";
                            CapusLisBind();

                        }
                        else
                        {
                            RlblCreateCampusLogoResult.Text = "Operation Failed,Try Again!";
                        }


                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    RlblCreateCampusLogoResult.Text = "Operation Failed,Try Again!";
                }
            }
        }

        protected void RgvCampusMaster_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            this.RgvCampusMaster.MasterTableView.AllowNaturalSort = true;
            this.RgvCampusMaster.MasterTableView.Rebind();
        }
    }
}