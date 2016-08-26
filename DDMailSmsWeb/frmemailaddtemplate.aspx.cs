using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDMailSmsWeb.DataAccess;
using Telerik.Web.UI;

namespace DDMailSmsWeb
{
    public partial class TEMAILADDTemplate : System.Web.UI.Page
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
                Session["pagename"] = "Email Templates";
                #endregion
                HandleMasterPageItem();
                EmailTemplateBind();
            }
        }
        private void HandleMasterPageItem()
        {
            RadMenu RMenuMain = (RadMenu)Master.FindControl("RMenuMain");
            // RMenuMain.Items.FindItemByText("Email Templates").Selected = true;
            RadPageLayout RPLayoutSubMenu = (RadPageLayout)Master.FindControl("RPLayoutSubMenu");
            RadPageLayout RPLayoutRNav = (RadPageLayout)Master.FindControl("RPLayoutRNav");
            RPLayoutSubMenu.Visible = false;
        }
        private void EmailTemplateBind()
        {
            try
            {
                DataTable dtEmailTemplates = new DataTable();
                dtEmailTemplates = DataAccessManager.GetTemplateList(Convert.ToString(Session["CampusID"]));
                Session["dtEmailTemplates"] = dtEmailTemplates;
                RgvEmailTemplate.DataSource = (DataTable)Session["dtEmailTemplates"];
                RgvEmailTemplate.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void RgvEmailTemplate_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ViewEmailTemplate")
                {
                    e.Item.Selected = true;
                    string ID = e.CommandArgument.ToString();
                    ViewState["ID"] = string.Empty;
                    RlblTempResult.Text = "";
                    DataTable dtshowTemplate = DataAccessManager.GetShowTemplateBody(ID, Convert.ToString(Session["CampusID"]));
                    if (dtshowTemplate.Rows.Count > 0)
                    {
                        if (dtshowTemplate.Rows[0]["Body"].ToString() != string.Empty)
                        {
                            REditorTempEmail.Content = dtshowTemplate.Rows[0]["Body"].ToString();

                            btnSaveEmailTemp.Text = "Update";
                            txtemailtempTitle.Text = dtshowTemplate.Rows[0]["Title"].ToString();
                            ViewState["ID"] = ID;
                            //  RbtntempActive.Enabled = false;
                            string Checked = dtshowTemplate.Rows[0]["Active"].ToString();
                            if (Checked == "True")
                                RbtntempActive.Checked = true;
                            else
                                RbtntempActive.Checked = false;
                        }
                    }
                }
                if (e.CommandName == "DeleteEmailTemp")
                {

                    e.Item.Selected = true;
                    string DeleteID = e.CommandArgument.ToString();
                    ViewState["ID"] = string.Empty;
                    RlblTempResult.Text = "";
                    Boolean result = false;
                    result = DataAccessManager.DeleteEmailTemplate(DeleteID, Convert.ToString(Session["CampusID"]));
                    if (result == true)
                    {
                        RlblTempResult.Text = "";
                        REditorTempEmail.Content = string.Empty;
                        btnSaveEmailTemp.Text = "Save";
                        txtemailtempTitle.Text = string.Empty;
                        ViewState["ID"] = string.Empty;
                        EmailTemplateBind();
                        RbtntempActive.Checked = false;
                    }
                }
                LRRlblTempResult.Visible = false;
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnSaveEmailTemp_Click(object sender, EventArgs e)
        {
            try
            {
                RlblTempResult.Text = string.Empty;

                if (Page.IsValid)
                {
                    RlblTempResult.Text = "";
                    LRRlblTempResult.Visible = true;
                    String fullName = string.Empty;
                    string html = string.Empty;

                    int i = 1;
                    foreach (UploadedFile file in fileuploadTemp.UploadedFiles)
                    {
                        if (i == 1)
                        {
                            fullName = file.FileName;
                            file.SaveAs(Server.MapPath("~/Upload/") + fullName);
                            html = File.ReadAllText(Server.MapPath("~/Upload/") + fullName);
                            if (File.Exists(Server.MapPath("~/Upload/") + fullName))
                            {
                                File.Delete(Server.MapPath("~/Upload/") + fullName);
                            }
                        }
                        i = i + 1;
                    }

                    if (fullName == string.Empty && REditorTempEmail.Content == string.Empty)
                    {
                        RlblTempResult.Text = "Please Upload vaild html,htm  Template  or Enter Content!";
                        return;
                    }

                    //===========================================================================================

                    if (btnSaveEmailTemp.Text == "Save")
                    {
                        #region "Start"
                        if (txtemailtempTitle.Text == string.Empty)
                        {
                            RlblTempResult.Text = "Please enter a template title!";
                            return;
                        }


                        if (fullName != string.Empty)
                        {
                        }
                        else
                        {
                            html = REditorTempEmail.Content.Replace("'", " ");
                        }
                        Boolean Active;
                        if (RbtntempActive.Checked == true)
                            Active = true;
                        else
                            Active = false;
                        Boolean status = DataAccessManager.SaveTemplateInDB(txtemailtempTitle.Text, html.Replace("'", " "), Active, Convert.ToString(Session["CampusID"]));
                        if (status == true)
                        {
                            REditorTempEmail.Content = string.Empty;
                            btnSaveEmailTemp.Text = "Save";
                            txtemailtempTitle.Text = string.Empty;
                            ViewState["ID"] = string.Empty;
                            RbtntempActive.Checked = false;
                            EmailTemplateBind();
                            RlblTempResult.Text = "Template save successfully!";
                        }
                        else
                        {
                            RlblTempResult.Text = "Execution failed try again!";
                        }
                        #endregion
                    }

                    if (btnSaveEmailTemp.Text == "Update")
                    {
                        #region "start"
                        Boolean Active;
                        if (RbtntempActive.Checked == true)
                            Active = true;
                        else
                            Active = false;

                        if (fullName != string.Empty)
                        {
                        }
                        else
                        {
                            html = REditorTempEmail.Content.Replace("'", " ");
                        }
                        Boolean status = DataAccessManager.UpdateTemplateInDB(txtemailtempTitle.Text, html.Replace("'", " "), Active, Convert.ToString(ViewState["ID"]));
                        if (status == true)
                        {
                            REditorTempEmail.Content = string.Empty;
                            btnSaveEmailTemp.Text = "Save";
                            txtemailtempTitle.Text = string.Empty;
                            ViewState["ID"] = string.Empty;
                            RbtntempActive.Checked = false;
                            EmailTemplateBind();
                            RlblTempResult.Text = "Template updated successfully!";
                        }
                        else
                        {
                            RlblTempResult.Text = "Execution failed try again!";
                        }
                        #endregion
                    }
                    //===========================================================================================

                }
                btnSaveEmailTemp.Enabled = true;
            }
            catch (Exception ex)
            {

            }
        }
        protected void btnAddNewEmailTemp_Click(object sender, EventArgs e)
        {
            try
            {
                RlblTempResult.Text = "";
                REditorTempEmail.Content = string.Empty;
                btnSaveEmailTemp.Text = "Save";
                txtemailtempTitle.Text = string.Empty;
                ViewState["ID"] = string.Empty;
                EmailTemplateBind();
                RbtntempActive.Checked = false;
                LRRlblTempResult.Visible = false;
            }
            catch (Exception ex)
            {

            }
        }

        protected void RgvEmailTemplate_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            this.RgvEmailTemplate.MasterTableView.AllowNaturalSort = true;
            this.RgvEmailTemplate.MasterTableView.Rebind();
        }

        protected void RgvEmailTemplate_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                RgvEmailTemplate.DataSource = (DataTable)Session["dtEmailTemplates"];
                RgvEmailTemplate.DataBind();
            }
            catch (Exception ex)
            { }
        }
    }
}