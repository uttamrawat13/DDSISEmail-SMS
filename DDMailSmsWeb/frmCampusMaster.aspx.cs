using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDMailSmsWeb.DataAccess;
using Telerik.Web.UI;


namespace DDMailSmsWeb
{
    public partial class frmCampusMaster : System.Web.UI.Page
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
                Session["pagename"] = "Manage Campus";
                #endregion
                HandleMasterPageItem();
                RgvCampusMasterGridBind();
               
            }
        }
        private void HandleMasterPageItem()
        {
            RadMenu RMenuMain = (RadMenu)Master.FindControl("RMenuMain");
             
            RadPageLayout RPLayoutSubMenu = (RadPageLayout)Master.FindControl("RPLayoutSubMenu");
            RadPageLayout RPLayoutRNav = (RadPageLayout)Master.FindControl("RPLayoutRNav");
            RPLayoutSubMenu.Visible = false;
       
        }
        #region "RgvCampusMaster"

            private void RgvCampusMasterGridBind()
            {
                DataTable dtUserList = new DataTable();
                dtUserList = DataAccessManager.GetCampusMasterList();
                Session["dtCampusMasterList"] = dtUserList;
                RgvCampusMaster.DataSource = (DataTable)Session["dtCampusMasterList"];
            }

            protected void RgvCampusMaster_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
            {
                RgvCampusMasterGridBind();
            }

            protected void RgvCampusMaster_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
            { 
              
                    if (e.CommandName == "imgbtnedit")
                    {
                        LRRlblCreateCampusResult.Visible = false;
                        RlblCreateCampusResult.Text = string.Empty;
                        e.Item.Selected = true;
                        GridDataItem ditem = (GridDataItem)e.Item;
                        Label lbCampusID = (Label)ditem.FindControl("lbCampusID");
                        Label lbCampusCode = (Label)ditem.FindControl("lbCampusCode");
                        Label lbCampusName = (Label)ditem.FindControl("lbCampusName");
                        Label lbCampusConStr = (Label)ditem.FindControl("lbCampusConStr");
                        CheckBox ItemChkboxActiveuser = (CheckBox)ditem.FindControl("ItemChkboxActiveuser");
                        RPbtnSave.Text = "Update";
                        ViewState["lbCampusID"] = lbCampusID.Text;
                        RtxtCampusCode.Text=lbCampusCode.Text;
                        RtxtCampusName.Text=lbCampusName.Text;
                        RtxtDatabaseConnection.Text = lbCampusConStr.Text;
                        RLChkconstr.Text = string.Empty;
                        if (ItemChkboxActiveuser.Checked == true)
                        {
                            RbtnCamousStatus.Checked = true;
                        }
                        else
                        {
                            RbtnCamousStatus.Checked = false;
                        }
                        
                    }   


            }

            protected void ItemChkboxActiveuser_CheckedChanged(object sender, EventArgs e)
            {
                ((sender as CheckBox).NamingContainer as GridItem).Selected = (sender as CheckBox).Checked;
                bool checkHeader = true;
                foreach (GridDataItem dataItem in RgvCampusMaster.MasterTableView.Items)
                {
                    if ((dataItem.FindControl("ItemChkboxActiveuser") as CheckBox).ClientID == (sender as CheckBox).ClientID)
                    {
                        string lbCampusID = Convert.ToString((dataItem.Cells[0].FindControl("lbCampusID") as Label).Text);
                        string strchk = "";
                        if ((sender as CheckBox).Checked == true)
                        {
                            strchk = "true";
                        }
                        else
                        {
                            strchk = "false";
                        }
                        DataAccessManager.ActiveDeactiveCampusMaster(lbCampusID, strchk);
                        RLChkconstr.Text = string.Empty;
                    }
                }
            }

        #endregion
        protected void RPbtnSave_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                
                bool ConnectionResult = false;
                string constr = string.Empty;
                constr = Convert.ToString(RtxtDatabaseConnection.Text);
                ConnectionResult = DataAccessManager.CheckConnectionstring(constr);
                LRRlblCreateCampusResult.Visible = true;

                try
                {
                    string CampusCode = string.Empty, CampusName = string.Empty, DatabaseConnection = string.Empty;

                    CampusCode = RtxtCampusCode.Text;
                    CampusName = RtxtCampusName.Text;
                    DatabaseConnection = RtxtDatabaseConnection.Text;
                    string campusstatus = string.Empty;
                    if (RbtnCamousStatus.Checked == true)
                        campusstatus = "1";
                    else
                        campusstatus = "0";

                    if (RPbtnSave.Text == "Save")
                    {
                        Boolean result = DataAccessManager.InsertCampusMaster(CampusCode, CampusName, DatabaseConnection, string.Empty, campusstatus);
                        if (result == true)
                        {
                            RlblCreateCampusResult.ForeColor = System.Drawing.Color.Green;
                            RlblCreateCampusResult.Text = "Campus added successfully!";
                            Reset();
                            if (ConnectionResult == false)
                            {
                                RLChkconstr.ForeColor = System.Drawing.Color.Red;
                                RLChkconstr.Text = "Invalid connection string!";
                            }
               
                        }
                        else
                        {
                            RlblCreateCampusResult.ForeColor = System.Drawing.Color.Red;
                            RlblCreateCampusResult.Text = "Operation Failed,Please try again!";
                            if (ConnectionResult == false)
                            {

                                RLChkconstr.ForeColor = System.Drawing.Color.Red;
                                RLChkconstr.Text = "Invalid connection string!";
                            }
               
                        }
                    }
                    else if (RPbtnSave.Text == "Update")
                    {
                        Boolean Result = false;
                        string CampusID = string.Empty;
                        CampusID = Convert.ToString(ViewState["lbCampusID"]);
                        Result = DataAccessManager.UpdateCampusMaster(CampusID, CampusCode, CampusName, DatabaseConnection, string.Empty, campusstatus);
                        if (Result == true)
                        {
                            RlblCreateCampusResult.ForeColor = System.Drawing.Color.Green;
                            RlblCreateCampusResult.Text = "Campus update successfully!";
                            RPbtnSave.Text = "Save";
                            Reset();
                            if (ConnectionResult == false)
                            {
                                RLChkconstr.ForeColor = System.Drawing.Color.Red;
                                RLChkconstr.Text = "Invalid connection string!";
                            }
               
                        }
                        else
                        {
                            RlblCreateCampusResult.ForeColor = System.Drawing.Color.Red;
                            RlblCreateCampusResult.Text = "Operation Failed,Try Again!";
                            if (ConnectionResult == false)
                            {

                                RLChkconstr.ForeColor = System.Drawing.Color.Red;
                                RLChkconstr.Text = "Invalid connection string!";
                            }
               
                        }

                    }
                }
                catch (Exception ex)
                {
                    RlblCreateCampusResult.ForeColor = System.Drawing.Color.Red;
                    RlblCreateCampusResult.Text = "Error:" + ex.ToString();
                }
            }
        }

        protected void RPbtnCancel_Click(object sender, EventArgs e)
        {
            LRRlblCreateCampusResult.Visible = false;
            RlblCreateCampusResult.Text = string.Empty;
            RPbtnSave.Text = "Save";
            Reset();
        }
        private void Reset()
        {
            
            ViewState["lbCampusID"] = string.Empty;
            RtxtCampusCode.Text = string.Empty;
            RtxtCampusName.Text = string.Empty;
            RtxtDatabaseConnection.Text = string.Empty;
            RbtnCamousStatus.Checked = false;
            RgvCampusMaster.Rebind();
            RLChkconstr.Text = string.Empty;
        }

        protected void RgvCampusMaster_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            this.RgvCampusMaster.MasterTableView.AllowNaturalSort = true;
            this.RgvCampusMaster.MasterTableView.Rebind();
        }

        protected void RBtnCheckConstr_Click(object sender, EventArgs e)
        {
            bool ConnectionResult = false;
            string constr = string.Empty;
            constr = Convert.ToString(RtxtDatabaseConnection.Text);
            ConnectionResult = DataAccessManager.CheckConnectionstring(constr);
            if (ConnectionResult == false)
            {
                RLChkconstr.ForeColor = System.Drawing.Color.Red;
                RLChkconstr.Text = "TEST NOT COMPLETED!";
            }
            else
            {
                RLChkconstr.ForeColor = System.Drawing.Color.Green;
                RLChkconstr.Text = "TEST COMPLETED SUCCESSFULLY!";
            }

        }

      

       

  

        
    }
}