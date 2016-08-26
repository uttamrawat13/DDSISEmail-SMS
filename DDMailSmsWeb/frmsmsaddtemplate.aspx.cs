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
    public partial class TSMSADDTemplate : System.Web.UI.Page
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
                Session["pagename"] = "SMS Templates";
                #endregion
                HandleMasterPageItem();
                BindSMSTemplate();
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
        protected void RPbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                RlblSMSTempResult.Text = string.Empty;
                if (Page.IsValid)
                {
                    if (RPbtnSave.Text == "Save")
                    {
                        if (txtSMStitle.Text == string.Empty)
                        {
                            RlblSMSTempResult.Text = "Please enter a template title!";
                            return;
                        }
                        Boolean Active;
                        if (RbtntempSMSActive.Checked == true)
                            Active = true;
                        else
                            Active = false;
                      
                        Boolean status = DataAccessManager.SaveSMSTemplateInDB(txtSMStitle.Text, REditorSMS.Text.Replace("'","''"), Active, Convert.ToString(Session["CampusID"]));
                        if (status == true)
                        {
                            RlblSMSTempResult.Text = "Template save successfully!";
                            txtSMStitle.Text = "";
                            RbtntempSMSActive.Checked = false;
                            REditorSMS.Text = string.Empty;
                            RLREditorSMSLength.Text = "Characters Count:0";
                        }
                        else
                        {
                            RlblSMSTempResult.Text = "Execution failed try again!";
                        }
                    }

                    if (RPbtnSave.Text == "Update")
                    {
                        Boolean Active;
                        if (RbtntempSMSActive.Checked == true)
                            Active = true;
                        else
                            Active = false;
                        Boolean status = DataAccessManager.UpdateSMSTemplateInDB(txtSMStitle.Text, REditorSMS.Text.Replace("'", "''"), Active, Convert.ToString(ViewState["IDSMSTemp"]));
                        if (status == true)
                        {
                            RlblSMSTempResult.Text = "Template updated successfully!";
                            txtSMStitle.Text = "";
                            REditorSMS.Text = string.Empty;
                            RbtntempSMSActive.Checked = false;
                            RPbtnSave.Text = "Save";
                            RLREditorSMSLength.Text = "Characters Count:0";
                        }
                        else
                        {
                            RlblSMSTempResult.Text = "Execution failed try again!";
                        }
                    }
                    BindSMSTemplate();
                    LRRlblSMSTempResult.Visible = true;
                }
            }
            catch (Exception ex)
            {
                
            }

        }
        protected void RPbtnADD_Click(object sender, EventArgs e)
        {
            try
            {
                REditorSMS.Text = string.Empty;
                RPbtnSave.Text = "Save";
                txtSMStitle.Text = string.Empty;
                ViewState["IDSMSTemp"] = string.Empty;
                RbtntempSMSActive.Checked = false;
                RlblSMSTempResult.Text = string.Empty;
                RLREditorSMSLength.Text = "Characters Count:0";
                BindSMSTemplate();
                LRRlblSMSTempResult.Visible = false;
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
                dtSMSTemplate = DataAccessManager.getSMSTemplateList(Convert.ToString(Session["CampusID"]));
                Session["dtSMSTemplate"] = dtSMSTemplate;
                RgvSMSTemplate.DataSource =(DataTable)Session["dtSMSTemplate"];
                RgvSMSTemplate.DataBind();
            }
            catch (Exception ex)
            {
                
            }
        }
        protected void RgvSMSTemplate_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                RLREditorSMSLength.Text = "0";
                if (e.CommandName == "ViewSMSTemp")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    e.Item.Selected = true;
                    string ID = e.CommandArgument.ToString();
                    ViewState["IDSMSTemp"] = string.Empty;
                    RlblSMSTempResult.Text = "";
                    RLREditorSMSLength.Text = "Characters Count:0";
                    DataTable dtshowTemplate = DataAccessManager.GetShowSMSTemplateBody(ID, Convert.ToString(Session["CampusID"]));
                    if (dtshowTemplate.Rows.Count > 0)
                    {
                        REditorSMS.Text = dtshowTemplate.Rows[0]["Body"].ToString();
                        RLREditorSMSLength.Text = Convert.ToString(REditorSMS.Text.Length);
                        RPbtnSave.Text = "Update";
                        txtSMStitle.Text = dtshowTemplate.Rows[0]["Title"].ToString();
                        ViewState["IDSMSTemp"] = ID;
                        string Checked = dtshowTemplate.Rows[0]["Active"].ToString();
                        if (Checked == "True")
                            RbtntempSMSActive.Checked = true;
                        else
                            RbtntempSMSActive.Checked = false;

                    }
                }


                if (e.CommandName == "DeleteSMSTemp")
                {
                    GridDataItem ditem = (GridDataItem)e.Item;
                    e.Item.Selected = true;
                    string DeleteID = e.CommandArgument.ToString();
                    ViewState["IDSMSTemp"] = string.Empty;
                    RlblSMSTempResult.Text = "";
                    Boolean result = false;
                    result = DataAccessManager.DeleteTemplateInDB(DeleteID, Convert.ToString(Session["CampusID"]));
                    if (result == true)
                    {
                        REditorSMS.Text = string.Empty;
                        RPbtnSave.Text = "Save";
                        txtSMStitle.Text = string.Empty;
                        ViewState["IDSMSTemp"] = string.Empty;
                        RbtntempSMSActive.Checked = false;
                        RlblSMSTempResult.Text = string.Empty;

                        BindSMSTemplate();

                    }


                }
                LRRlblSMSTempResult.Visible = false;
            }
            catch (Exception ex)
            {
                
            }

        }
        protected void RgvSMSTemplate_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            this.RgvSMSTemplate.MasterTableView.AllowNaturalSort = true;
            this.RgvSMSTemplate.MasterTableView.Rebind();
        }

        protected void RgvSMSTemplate_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                RgvSMSTemplate.DataSource = (DataTable)Session["dtSMSTemplate"];
                RgvSMSTemplate.DataBind();
                LRRlblSMSTempResult.Visible = false;
            }
            catch (Exception ex)
            {
            }
        }
      
    }
}