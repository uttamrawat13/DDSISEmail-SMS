using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DDMailSmsWeb.DataAccess;
using DDMailSmsWeb.DynamicAccess;
using Telerik.Web.UI;
using Telerik.Web.UI.Menu;

namespace DDMailSmsWeb
{
    public partial class MainDDMailSMSMaster : System.Web.UI.MasterPage
    {
        private DataTable Menudt = new DataTable();
        DataTable Menuparentdt = new DataTable();
        DataTable MenuChilddt = new DataTable();
        internal class SiteDataItem
        {
            private string _text;
            private string _url;
            private int _id;
            private int? _parentId;
            private string _MobileView;
            public string Text
            {
                get { return _text; }
                set { _text = value; }
            }

            public string Url
            {
                get { return _url; }
                set { _url = value; }
            }

            public int ID
            {
                get { return _id; }
                set { _id = value; }
            }

            public int? ParentID
            {
                get { return _parentId; }
                set { _parentId = value; }
            }
            public string MobileView
            {
                get { return _MobileView; }
                set { _MobileView = value; }
            }
            public SiteDataItem(int id, int? parentId, string text, string url, string MobileView)
            {
                _id = id;
                _parentId = parentId;
                _text = text;
                _url = url;
                _MobileView = MobileView;
            }
        }
        public string smscount = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            RALPSwitchtoanthoruser.Visible = true;
            if (Session["pagename"] != null)
            {
                lbpagename.Text = Convert.ToString(Session["pagename"]);
                lbpagename.CssClass = "additionalColumn";
            }
            if (!IsPostBack)
            {
                LBloginuser.Text = "<b>Current User:- </b>" + Convert.ToString(Session["username"]);

               

                #region Campus Logo
                imgCampusLogo.ImageUrl = "~/Content/clientlogo/" + Convert.ToString(Session["Campuslogo"]);
                #endregion

                if (Session["CampusID"] != null)
                {
                    RCBStudentsLeadSearch.Enabled = true;
                    RBSelectStudent.Enabled = true;
                    RBSelectLead.Enabled = true;
                    RbtnRefreshApplication.Enabled = true;
                    #region "For all User Menu Bind"
                    string username = string.Empty, user_level = string.Empty;
                    username = Convert.ToString(Session["username"]);
                    user_level = Convert.ToString(Session["user_level"]);

                    Menudt = DataAccessManager.UserMenuBind(user_level);
                    if (Menudt.Rows.Count > 0)
                    {


                        List<SiteDataItem> siteData = new List<SiteDataItem>();

                        DataRow[] MenuparentRow = Menudt.Select("ParentID='0'");


                        foreach (DataRow l_parentRow in MenuparentRow)
                        {

                            string text = string.Empty, url = string.Empty, id = string.Empty, parentId = string.Empty, MobileView = string.Empty;

                            text = Convert.ToString(l_parentRow["Menu"]);
                            id = Convert.ToString(l_parentRow["Menu_ID"]);
                            url = Convert.ToString(l_parentRow["link"]);
                            parentId = Convert.ToString(l_parentRow["parentId"]);
                            MobileView = Convert.ToString(l_parentRow["MobileView"]);
                            if (MobileView == "1")
                            {
                                MobileView = "additionalColumn";
                            }
                            else
                            {
                                MobileView = string.Empty;
                            }
                            if (parentId == "0")
                            {
                                siteData.Add(new SiteDataItem(Convert.ToInt32(id), null, text, url, MobileView));
                            }
                            else
                            {
                                siteData.Add(new SiteDataItem(Convert.ToInt32(id), Convert.ToInt32(parentId), text, url, MobileView));

                            }


                            DataRow[] MenuChildRow = Menudt.Select("ParentID='" + id + "' ");
                            if (MenuChildRow.Length > 0)
                            {
                                foreach (DataRow l_ChildRow in MenuChildRow)
                                {
                                    text = string.Empty;
                                    url = string.Empty;
                                    id = string.Empty;
                                    parentId = string.Empty;
                                    MobileView = string.Empty;
                                    text = Convert.ToString(l_ChildRow["Menu"]);
                                    id = Convert.ToString(l_ChildRow["Menu_ID"]);
                                    url = Convert.ToString(l_ChildRow["link"]);
                                    parentId = Convert.ToString(l_ChildRow["parentId"]);
                                    MobileView = Convert.ToString(l_ChildRow["MobileView"]);
                                    if (MobileView == "1")
                                    {
                                        MobileView = "additionalColumn";
                                    }
                                    else
                                    {
                                        MobileView = string.Empty;
                                    }
                                    if (parentId == "0")
                                    {
                                        siteData.Add(new SiteDataItem(Convert.ToInt32(id), null, text, url, MobileView));
                                    }
                                    else
                                    {
                                        siteData.Add(new SiteDataItem(Convert.ToInt32(id), Convert.ToInt32(parentId), text, url, MobileView));

                                    }
                                }
                            }


                        }
                        Session["menudata"] = siteData;
                        if (Session["menudata"] != null)
                        {
                            RMenuMain.DataTextField = "Text";
                            RMenuMain.DataNavigateUrlField = "Url";
                            RMenuMain.DataFieldID = "ID";
                            RMenuMain.DataValueField = "ID";
                            RMenuMain.DataFieldParentID = "ParentID";
                            RMenuMain.DataSource = (List<SiteDataItem>)Session["menudata"];
                            RMenuMain.DataBind();


                        }


                        foreach (RadMenuItem item in RMenuMain.GetAllItems())
                        {
                            string text = item.Text;
                            string id = Convert.ToString(item.Value);

                            DataRow[] MenuItemRow = Menudt.Select(" Menu_ID= '" + id + "' And MobileView=1");
                            if (MenuItemRow.Length > 0)
                            {
                                item.CssClass = "additionalColumn";
                            }




                            // 
                        }

                    }
                    # endregion
                }
                  if (Session["CampusID"] == null || Session["user_level"] == "99")
                  {
                      RCBStudentsLeadSearch.Enabled = false;
                      RBSelectStudent.Enabled = false;
                      RBSelectLead.Enabled = false;
                      RbtnRefreshApplication.Enabled = false;
                      #region "Menu Bind without Process by Admin"
                      string username = string.Empty, user_level = string.Empty;
                      username = Convert.ToString(Session["username"]);
                      user_level = Convert.ToString(Session["user_level"]);

                      Menudt = DataAccessManager.UserMenuBind(user_level, "1");
                      if (Menudt.Rows.Count > 0)
                      {


                          List<SiteDataItem> siteData = new List<SiteDataItem>();

                          DataRow[] MenuparentRow = Menudt.Select("ParentID='0'");


                          foreach (DataRow l_parentRow in MenuparentRow)
                          {

                              string text = string.Empty, url = string.Empty, id = string.Empty, parentId = string.Empty, MobileView = string.Empty;

                              text = Convert.ToString(l_parentRow["Menu"]);
                              id = Convert.ToString(l_parentRow["Menu_ID"]);
                              url = Convert.ToString(l_parentRow["link"]);
                              parentId = Convert.ToString(l_parentRow["parentId"]);
                              MobileView = Convert.ToString(l_parentRow["MobileView"]);
                              if (MobileView == "1")
                              {
                                  MobileView = "additionalColumn";
                              }
                              else
                              {
                                  MobileView = string.Empty;
                              }
                              if (parentId == "0")
                              {
                                  siteData.Add(new SiteDataItem(Convert.ToInt32(id), null, text, url, MobileView));
                              }
                              else
                              {
                                  siteData.Add(new SiteDataItem(Convert.ToInt32(id), Convert.ToInt32(parentId), text, url, MobileView));

                              }


                              DataRow[] MenuChildRow = Menudt.Select("ParentID='" + id + "' ");
                              if (MenuChildRow.Length > 0)
                              {
                                  foreach (DataRow l_ChildRow in MenuChildRow)
                                  {
                                      text = string.Empty;
                                      url = string.Empty;
                                      id = string.Empty;
                                      parentId = string.Empty;
                                      MobileView = string.Empty;
                                      text = Convert.ToString(l_ChildRow["Menu"]);
                                      id = Convert.ToString(l_ChildRow["Menu_ID"]);
                                      url = Convert.ToString(l_ChildRow["link"]);
                                      parentId = Convert.ToString(l_ChildRow["parentId"]);
                                      MobileView = Convert.ToString(l_ChildRow["MobileView"]);
                                      if (MobileView == "1")
                                      {
                                          MobileView = "additionalColumn";
                                      }
                                      else
                                      {
                                          MobileView = string.Empty;
                                      }
                                      if (parentId == "0")
                                      {
                                          siteData.Add(new SiteDataItem(Convert.ToInt32(id), null, text, url, MobileView));
                                      }
                                      else
                                      {
                                          siteData.Add(new SiteDataItem(Convert.ToInt32(id), Convert.ToInt32(parentId), text, url, MobileView));

                                      }
                                  }
                              }


                          }
                          Session["menudata"] = siteData;
                          if (Session["menudata"] != null)
                          {
                              RMenuMain.DataTextField = "Text";
                              RMenuMain.DataNavigateUrlField = "Url";
                              RMenuMain.DataFieldID = "ID";
                              RMenuMain.DataValueField = "ID";
                              RMenuMain.DataFieldParentID = "ParentID";
                              RMenuMain.DataSource = (List<SiteDataItem>)Session["menudata"];
                              RMenuMain.DataBind();


                          }


                          foreach (RadMenuItem item in RMenuMain.GetAllItems())
                          {
                              string text = item.Text;
                              string id = Convert.ToString(item.Value);

                              DataRow[] MenuItemRow = Menudt.Select(" Menu_ID= '" + id + "' And MobileView=1");
                              if (MenuItemRow.Length > 0)
                              {
                                  item.CssClass = "additionalColumn";
                              }




                              // 
                          }

                      }
                      # endregion
                    
                  }
                       #region "Search Control"
            
              string SearchControlLeadId = string.Empty, SearchControlStudentNo = string.Empty;
              SearchControlLeadId = Convert.ToString(Session["LeadId"]);
              SearchControlStudentNo = Convert.ToString(Session["StudentNo"]);
              if (SearchControlLeadId != string.Empty && SearchControlLeadId != null && SearchControlLeadId != "0")
              {
                  #region "Search Control Lead Setting"
                  RCBStudentsLeadSearch.WebServiceSettings.Method = "GetLeads";
                  RCBStudentsLeadSearch.HeaderTemplate = new LeadHeaderTemplate();
                  RCBStudentsLeadSearch.Width = 267;
                  RCBStudentsLeadSearch.DropDownWidth = 880;
                  RCBStudentsLeadSearch.EmptyMessage = "Select a Lead";
                  RBSelectLead.Checked = true;
                  #endregion
              }
              if (SearchControlStudentNo != string.Empty && SearchControlStudentNo != null && SearchControlStudentNo != "0")
              {
                  #region "Search Control Student Setting"
                  RCBStudentsLeadSearch.WebServiceSettings.Method = "GetStudents";
                  RCBStudentsLeadSearch.HeaderTemplate = new StudentHeaderTemplate();
                  RCBStudentsLeadSearch.Width = 280;
                  RCBStudentsLeadSearch.DropDownWidth = 500;
                  RCBStudentsLeadSearch.EmptyMessage = "Select a Student";
                  RBSelectStudent.Checked = true;
                  #endregion
              }
              #endregion

              #region "departmentnamebind"

                  /* Swtich Department */
                  /* DataTable dtdept = DyDataAccessManager.GetListDepartment(DeptID);
                   if (dtdept.Rows.Count > 0)
                   {
                       LBDepartment.Text = "<b>Department:- </b>" + Convert.ToString(dtdept.Rows[0]["DeptDescription"]);
              
                   }
                   else
                   {
                       LBDepartment.Text = "<b>Department:- </b>" + "Not Available";
                   }*/

                  int DeptID = 0;
                  DeptID = Convert.ToInt32(Session["DeptID"]);
                  DataTable dtDepartment = new DataTable();
                  dtDepartment = DyDataAccessManager.GetListDepartmentforswitch();
                  RddlDept.DataSource = dtDepartment;
                  RddlDept.DataTextField = "DeptDescription";
                  RddlDept.DataValueField = "DeptID";
                  RddlDept.DataBind();
                  //RddlDept.Items[0].Text = "Select Departments";
                  //RddlDept.SelectedIndex = 0;

                  RddlDept.SelectedValue = Convert.ToString(DeptID);
                  RddlDept.Enabled = false;
                  if (Convert.ToInt32(Session["AllowSwitchdept"]) == 1)
                  {
                      RddlDept.Enabled = true;
                  }
              #endregion
                  #region "SMS Count"
                  CountSMS();
                  #endregion 
            }
            string LeadId = string.Empty, StudentNo = string.Empty;
            LeadId = Convert.ToString(Session["LeadId"]);
            StudentNo = Convert.ToString(Session["StudentNo"]);
            string type = string.Empty;
            if (StudentNo != string.Empty && StudentNo != null && StudentNo != "0")
            {
                StudentDetailBind();              
            }
            if (LeadId != string.Empty && LeadId != null && LeadId != "0")
            {
                LeadDetailBind();

            }


         
        }

        private void CountSMS()
        {
            string smscount = "0";
            DataTable dtDeptReceiveSMScount = new DataTable();
            lblSMSCount.Visible = false;
            dtDeptReceiveSMScount = DataAccessManager.GetSMSReceivedCount(Convert.ToString(Session["DeptID"]), Convert.ToInt32(Session["CampusID"]));
            if (dtDeptReceiveSMScount.Rows.Count > 0)
            {
                lblSMSCount.Visible = true;
                smscount = dtDeptReceiveSMScount.Rows[0]["Rowcounts"].ToString();
            }
            lblSMSCount.Text = "(" + smscount + ")";
            if (Session["PageClick"] == "0")
            {
                lblSMSCount.Visible = false;
            }
        }

        #region "RLBSwitchtoStudent Bind"
       

      
        #endregion
        protected void RMenuMain_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
        {
            string clickeditem = string.Empty;
            clickeditem = Convert.ToString(e.Item.Index);
            switch (clickeditem)
            {
                case "0":
                    Session["PageClick"]="0";
                    BindStudentLeadDepartmentInbox();
                    lblSMSCount.Visible = false;
                    break;
                case "1":
                    Session["PageClick"] = "1";
                    lblSMSCount.Visible = true;
                    BindStudentLeadDepartmentInbox();
                    #region "SMS Count"
                    CountSMS();
                    #endregion
                    break;
               
            }
        }

        protected void RbtnComposeEmail_Click(object sender, EventArgs e)
        {
            Session["ReplyDeptEmailTo"] = string.Empty;
            Session["ReplyDeptEmailSubject"] = string.Empty;
            Session["ReplyDeptEmailComposeBody"] = string.Empty;
            Session["ReplyStdLeadEmailTo"] = string.Empty;
            Session["ReplyStdLeadEmailSubject"] = string.Empty;
            Session["ReplyStdLeadEmailComposeBody"] = string.Empty;
            string LeadId = string.Empty, StudentNo = string.Empty;
            LeadId = Convert.ToString(Session["LeadId"]);
            StudentNo = Convert.ToString(Session["StudentNo"]);
            
            if (LeadId != string.Empty && LeadId != null && LeadId != "0")
            {
                Page.Title = "Lead Compose Email"; 
            }
            if (StudentNo != string.Empty && StudentNo != null && StudentNo != "0")
            {
                Page.Title = "Student Compose Email";   
            }


            if (Convert.ToString(Session["PageClick"]) == "1")
            {
                Response.Redirect("~/frmdepartmentcomposeemail.aspx");
            }
            else if (Convert.ToString(Session["PageClick"]) == "0")  
            {
                Response.Redirect("~/frmstudentleadcomposeemaill.aspx");
            }
        }

        protected void RbtnComposeSMS_Click(object sender, EventArgs e)
        {
            string LeadId = string.Empty, StudentNo = string.Empty;
            LeadId = Convert.ToString(Session["LeadId"]);
            StudentNo = Convert.ToString(Session["StudentNo"]);

            if (LeadId != string.Empty && LeadId != null && LeadId != "0")
            {
                Page.Title = "Lead Compose SMS";
            }
            if (StudentNo != string.Empty && StudentNo != null && StudentNo != "0")
            {
                Page.Title = "Student Compose SMS";
            }

            if (Convert.ToString(Session["PageClick"]) == "1")
            {
                Response.Redirect("~/frmdepartmentcomposesms.aspx");
            }
            else if (Convert.ToString(Session["PageClick"]) == "0")
            {
                Response.Redirect("~/frmstudentleadcomposesms.aspx");
            }
        }

        protected void RbtnInboxEamil_Click(object sender, EventArgs e)
        {
            string LeadId = string.Empty, StudentNo = string.Empty;
            LeadId = Convert.ToString(Session["LeadId"]);
            StudentNo = Convert.ToString(Session["StudentNo"]);
            string type = string.Empty;
            if (LeadId != string.Empty && LeadId != null && LeadId != "0")
            {
                type = "Lead";
            }
            if (StudentNo != string.Empty && StudentNo != null && StudentNo != "0")
            {
                type = "Student";
            }
            if (Convert.ToString(Session["PageClick"]) == "1")
             {
                 Response.Redirect("~/frmdepartment.aspx?type=Department&Operation=Inbox");
             }
            else if (Convert.ToString(Session["PageClick"]) == "0")
             {
                 Response.Redirect("~/frmstudentlead.aspx?type=" + type + "&Operation=Inbox");
             }
        }

        private void BindStudentLeadDepartmentInbox()
        { 
            string LeadId = string.Empty, StudentNo = string.Empty;
            LeadId = Convert.ToString(Session["LeadId"]);
            StudentNo = Convert.ToString(Session["StudentNo"]);
            string type = string.Empty;
            if (LeadId != string.Empty && LeadId != null && LeadId != "0")
            {
                type = "Lead";
                #region "Search Control"
                    RCBStudentsLeadSearch.WebServiceSettings.Method = "GetLeads";
                    RCBStudentsLeadSearch.HeaderTemplate = new LeadHeaderTemplate();
                    RCBStudentsLeadSearch.Width = 267;
                    RCBStudentsLeadSearch.DropDownWidth = 880;
                    RCBStudentsLeadSearch.EmptyMessage = "Select a Lead";
                    RBSelectLead.Checked = true;
                #endregion
            }
            if (StudentNo != string.Empty && StudentNo != null && StudentNo != "0")
            {
                type = "Student";
                #region "Search Control"
                    RCBStudentsLeadSearch.WebServiceSettings.Method = "GetStudents";
                    RCBStudentsLeadSearch.HeaderTemplate = new StudentHeaderTemplate();
                    RCBStudentsLeadSearch.Width = 280;
                    RCBStudentsLeadSearch.DropDownWidth = 500;
                    RCBStudentsLeadSearch.EmptyMessage = "Select a Student";
                    RBSelectStudent.Checked = true;
                #endregion
            }

            if (Convert.ToString(Session["PageClick"]) == "1")
            {
                Response.Redirect("~/frmdepartment.aspx?type=Department&Operation=Inbox");
            }
            else if (Convert.ToString(Session["PageClick"]) == "0")
            {
                Response.Redirect("~/frmstudentlead.aspx?type=" + type + "&Operation=Inbox");
            }
            
        }

        protected void RbtnFetchEmail_Click(object sender, EventArgs e)
        {

            string LeadId=string.Empty,StudentNo=string.Empty;
            LeadId=Convert.ToString(Session["LeadId"]);
            StudentNo=Convert.ToString(Session["StudentNo"]);

            string type=string.Empty;

            if(LeadId!=string.Empty && LeadId !=null && LeadId !="0")
            {
              type="Lead";
            }

            if(StudentNo!=string.Empty && StudentNo !=null && StudentNo !="0")
            {
              type="Student";
            }

            if (Convert.ToString(Session["PageClick"]) == "1")
             {
                 Response.Redirect("~/frmdepartment.aspx?type=Department&Operation=download");
             }
            else if (Convert.ToString(Session["PageClick"]) == "0")
             {
                 Response.Redirect("~/frmstudentlead.aspx?type=" + type + "&Operation=download");
             }
            
        }

        protected void RbtnInbox_Click(object sender, EventArgs e)
        {
            string LeadId = string.Empty, StudentNo = string.Empty;
            LeadId = Convert.ToString(Session["LeadId"]);
            StudentNo = Convert.ToString(Session["StudentNo"]);

            string type = string.Empty;

            if (LeadId != string.Empty && LeadId != null && LeadId != "0")
            {
                type = "Lead";
            }

            if (StudentNo != string.Empty && StudentNo != null && StudentNo != "0")
            {
                type = "Student";
            }

            if (Convert.ToString(Session["PageClick"]) == "1")
            {
                Response.Redirect("~/frmdepartment.aspx?type=Department&Operation=Inbox");
            }
            else if (Convert.ToString(Session["PageClick"]) == "0")
            {
                Response.Redirect("~/frmstudentlead.aspx?type=" + type + "&Operation=Inbox");
            }
            

        }

        protected void RbtnSentEmail_Click(object sender, EventArgs e)
        {
            string LeadId = string.Empty, StudentNo = string.Empty;
            LeadId = Convert.ToString(Session["LeadId"]);
            StudentNo = Convert.ToString(Session["StudentNo"]);

            string type = string.Empty;

            if (LeadId != string.Empty && LeadId != null && LeadId != "0")
            {
                type = "Lead";
            }

            if (StudentNo != string.Empty && StudentNo != null && StudentNo != "0")
            {
                type = "Student";
            }
            if (Convert.ToString(Session["PageClick"]) == "1")
            {
                Response.Redirect("~/frmdepartment.aspx?type=Department&Operation=Sent Email");
            }
            else if (Convert.ToString(Session["PageClick"]) == "0")
            {
                Response.Redirect("~/frmstudentlead.aspx?type=" + type + "&Operation=Sent Email");
            }
             
        }

        protected void RbtnUnreadEmail_Click(object sender, EventArgs e)
        {
            string LeadId = string.Empty, StudentNo = string.Empty;
            LeadId = Convert.ToString(Session["LeadId"]);
            StudentNo = Convert.ToString(Session["StudentNo"]);

            string type = string.Empty;

            if (LeadId != string.Empty && LeadId != null && LeadId != "0")
            {
                type = "Lead";
            }

            if (StudentNo != string.Empty && StudentNo != null && StudentNo != "0")
            {
                type = "Student";
            }
            if (Convert.ToString(Session["PageClick"]) == "1")
            {
                Response.Redirect("~/frmdepartment.aspx?type=Department&Operation=Unread Email");
            }
            else if (Convert.ToString(Session["PageClick"]) == "0")
            {
                Response.Redirect("~/frmstudentlead.aspx?type=" + type + "&Operation=Unread Email");
            }
            

        }

        protected void RbtnRemoveEmail_Click(object sender, EventArgs e)
        {
            string LeadId = string.Empty, StudentNo = string.Empty;
            LeadId = Convert.ToString(Session["LeadId"]);
            StudentNo = Convert.ToString(Session["StudentNo"]);

            string type = string.Empty;

            if (LeadId != string.Empty && LeadId != null && LeadId != "0")
            {
                type = "Lead";
            }

            if (StudentNo != string.Empty && StudentNo != null && StudentNo != "0")
            {
                type = "Student";
            }
            if (Convert.ToString(Session["PageClick"]) == "1")
            {
                Response.Redirect("~/frmdepartment.aspx?type=Department&Operation=Remove Email");
            }
            else if (Convert.ToString(Session["PageClick"]) == "0")
            {
                Response.Redirect("~/frmstudentlead.aspx?type=" + type + "&Operation=Remove Email");
            }
            
        }

        //protected void RbtnSentSMS_Click(object sender, EventArgs e)
        //{
        //    string LeadId = string.Empty, StudentNo = string.Empty;
        //    LeadId = Convert.ToString(Session["LeadId"]);
        //    StudentNo = Convert.ToString(Session["StudentNo"]);
            
        //    string type = string.Empty;

        //    if (LeadId != string.Empty && LeadId != null && LeadId != "0")
        //    {
        //        type = "Lead";
        //    }

        //    if (StudentNo != string.Empty && StudentNo != null && StudentNo != "0")
        //    {
        //        type = "Student";
        //    }
        //    if (Convert.ToString(Session["PageClick"]) == "1")
        //    {
        //        Response.Redirect("~/frmdepartment.aspx?type=Department&Operation=Sent SMS");
        //    }
        //    else if (Convert.ToString(Session["PageClick"]) == "0")
        //    {
        //        Response.Redirect("~/frmstudentlead.aspx?type=" + type + "&Operation=Sent SMS");
        //    }
            
        //}

        protected void RbtnReceiveSMS_Click(object sender, EventArgs e)
        {
            string LeadId = string.Empty, StudentNo = string.Empty, DeptID=string.Empty;
            LeadId = Convert.ToString(Session["LeadId"]);
            StudentNo = Convert.ToString(Session["StudentNo"]);
            DeptID = Convert.ToString(Session["DeptID"]);
            string type = string.Empty;

            if (LeadId != string.Empty && LeadId != null && LeadId != "0")
            {
                type = "Lead";
            }
            
            if (StudentNo != string.Empty && StudentNo != null && StudentNo != "0")
            {
                type = "Student";
            }
            if (Convert.ToString(Session["PageClick"]) == "1")
            {
                Response.Redirect("~/frmdepartment.aspx?type=Department&Operation=Receive SMS");
            }
            else if (Convert.ToString(Session["PageClick"]) == "0")
            {
                Response.Redirect("~/frmstudentlead.aspx?type=" + type + "&Operation=Receive SMS");
            }
            
        }

        //======================================================================================================================
        private void StudentDetailBind()
        {
            try
            {
                string StudentNo = string.Empty, LeadId = string.Empty;
                StudentNo = Convert.ToString(Session["StudentNo"]);
                LeadId = Convert.ToString(Session["leadID"]);
                DataTable dtStudentEmail = new DataTable();
                dtStudentEmail = DyDataAccessManager.GetStudentEmail(StudentNo);
                if (dtStudentEmail.Rows.Count > 0)
                {
                    lblStdLeadName.Text = "<b>Student:- </b>" + Convert.ToString(dtStudentEmail.Rows[0]["Name"]);
                    LBselectstudentLead.Text = "<b>Student:- </b>" + Convert.ToString(dtStudentEmail.Rows[0]["Name"]);
                    
                }
                else
                {
                    lblStdLeadName.Text = "<b>Student:- </b><"+"No Available";
                    LBselectstudentLead.Text = "<b>Student:- </b>" + Convert.ToString(Session["username"]);

                   
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        // bind textbox composeemail in Lead   id and department
        private void LeadDetailBind()
        {
            try
            {
                 
                string StudentNo = string.Empty, LeadId = string.Empty;
                StudentNo = Convert.ToString(Session["StudentNo"]);
                LeadId = Convert.ToString(Session["leadID"]);
                DataTable dtStudentEmail = new DataTable();
                dtStudentEmail = DyDataAccessManager.GetLeadEmail(LeadId);
                if (dtStudentEmail.Rows.Count > 0)
                {
                    lblStdLeadName.Text = "<b>Lead:- </b>"+Convert.ToString(dtStudentEmail.Rows[0]["Name"]);
                    LBselectstudentLead.Text = "<b>Lead:- </b>" + Convert.ToString(dtStudentEmail.Rows[0]["Name"]);
                }
                else
                {
                    lblStdLeadName.Text = "<b>Lead:- </b>" + "No Available";
                    LBselectstudentLead.Text = "<b>Lead:- </b>" + "No Available";
                }
            }
            catch (Exception ex)
            {
                
            }
        }
       

        protected void RbtLogout_Click(object sender, EventArgs e)
        {
            //string userrole=string.Empty;
            //userrole = Convert.ToString(Session["user_level"]);
            //if (userrole != "99")
            //{
               // Session.Abandon();
               // Session.Clear();
               // Session.RemoveAll();
           // }
                Session.Abandon();
                Session.Clear();
                Session.RemoveAll();
                Response.Redirect("~/frmlogin.aspx");
        }

        #region "Model Switch to another user"
         
            protected void RCBStudentsSearch_ItemDataBound(object sender, RadComboBoxItemEventArgs e)
            {
                DataRowView row = e.Item.DataItem as DataRowView;
                e.Item.Attributes["FullName"] = row["FullName"].ToString();
                e.Item.Attributes["StudentNo"] = row["StudentNo"].ToString();
                e.Item.Text = ((DataRowView)e.Item.DataItem)["FullName"].ToString();
                e.Item.Value = ((DataRowView)e.Item.DataItem)["StudentNo"].ToString();

            }
            
        
        #endregion

    
          
       

            //======================================================================================================================
            #region "Search"
           
            #region "student selection"
                protected void RBSelectStudent_Click(object sender, EventArgs e)
                {
                    RCBStudentsLeadSearch.WebServiceSettings.Method = "GetStudents";
                    RCBStudentsLeadSearch.HeaderTemplate = new StudentHeaderTemplate();
                    RCBStudentsLeadSearch.Width = 280;
                    RCBStudentsLeadSearch.DropDownWidth = 500;

                    RCBStudentsLeadSearch.EmptyMessage = "Select a Student";
                }
            #endregion
            #region "Lead Selection"
                protected void RBSelectLead_Click(object sender, EventArgs e)
                {
                    RCBStudentsLeadSearch.WebServiceSettings.Method = "GetLeads";
                    RCBStudentsLeadSearch.HeaderTemplate = new LeadHeaderTemplate();
                    RCBStudentsLeadSearch.Width = 267;
                    RCBStudentsLeadSearch.DropDownWidth = 880;

                    RCBStudentsLeadSearch.EmptyMessage = "Select a Lead";
                }
            #endregion
            #region "Search Selection"
                protected void RCBStudentsLeadSearch_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
                {
                    string LeadId = string.Empty, StudentNo = string.Empty;
                    if (Convert.ToString(RCBStudentsLeadSearch.SelectedValue) != string.Empty)
                    {
                        if(RBSelectStudent.Checked==true)
                        {
                            StudentNo = Convert.ToString(RCBStudentsLeadSearch.SelectedValue);
                            Session["LeadId"] = string.Empty;
                            Session["StudentNo"] = StudentNo;
                        }
                        else if (RBSelectLead.Checked == true)
                        {
                            LeadId = Convert.ToString(RCBStudentsLeadSearch.SelectedValue);
                            Session["LeadId"] = LeadId;
                            Session["StudentNo"] = string.Empty;
                        }
                        hdselectedstudentlead.Value = string.Empty;
                        LeadId = Convert.ToString(Session["LeadId"]);
                        StudentNo = Convert.ToString(Session["StudentNo"]);

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
                    headertemplate +="    <li class='Lcol2'>Lead ID</li>";
                    headertemplate +="    <li class='Lcol3'>Status</li>";
                    headertemplate +="    <li class='Lcol4'>AdRep</li>";
                    headertemplate +="    <li class='Lcol5'>City</li>";
                    headertemplate +="    <li class='Lcol6'>State</li>";
                    headertemplate +="    <li class='Lcol7'>Zip</li>";
                    headertemplate +="</ul>";
                    HtmlGenericControl header = new HtmlGenericControl("header");
                    header.InnerHtml=headertemplate;
                    container.Controls.Add(header);
                }
            }
           
            #endregion

            

           

            

            #endregion

            #region "Site Refresh"
            protected void RbtnRefreshApplication_Click(object sender, EventArgs e)
            {
                string ApplaunchURL = string.Empty;
                ApplaunchURL = Convert.ToString(Session["ApplaunchURL"]);

                Response.Redirect(ApplaunchURL);
            }
            #endregion
            #region "swtich to department"
                protected void RddlDept_SelectedIndexChanged(object sender, DropDownListEventArgs e)
                {
                    if (RddlDept.SelectedIndex > 0)
                    {
                        string LeadId = string.Empty, StudentNo = string.Empty,DeptID=string.Empty;
                        LeadId=Convert.ToString(Session["LeadId"]);
                        StudentNo=Convert.ToString( Session["StudentNo"]);
                        DeptID=Convert.ToString(RddlDept.SelectedValue);
                        Session["DeptID"]=DeptID;
                        string type=string.Empty;

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
            #endregion

    }
}