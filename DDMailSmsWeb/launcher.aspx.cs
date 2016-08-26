using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDMailSmsWeb.DataAccess;

using DDMailSmsWeb.DynamicAccess;

namespace DDMailSmsWeb
{
    public partial class launcher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                string ApplaunchURL = string.Empty;
                ApplaunchURL = HttpContext.Current.Request.Url.AbsoluteUri;
                Session["ApplaunchURL"] = ApplaunchURL;
                if (Request.QueryString["Campus"] != null && Request.QueryString["Campus"] != string.Empty)
                {
                    DataTable dtConfig = DataAccessManager.GetConfigration(Convert.ToString(Request.QueryString["Campus"]));
                    if (dtConfig.Rows.Count > 0)
                    {
                        Session["GlobalValueKey"] = dtConfig.Rows[0]["CampusConStr"].ToString();
                        Session["CampusID"] = dtConfig.Rows[0]["CampusID"].ToString();
                        Session["Campuslogo"] = dtConfig.Rows[0]["Clientlogo"].ToString();
                        Session["CampusName"]=Convert.ToString(Request.QueryString["Campus"]);
                    }
                    else
                    {
                        Session["GlobalValueKey"] = string.Empty;
                        Session["CampusID"] = string.Empty;
                        Session["globalERROR"] = "Some query string are missing or wrong! Check with your network administrator";
                        Response.Redirect("~/Error.aspx");
                        
                    }
                 
                


                }
                else
                {
                    Session["globalERROR"] = "Campus Code Null";
                    Response.Redirect("~/Error.aspx");
                    
                }
                if (Request.QueryString["StudentNo"] != null && Request.QueryString["StudentNo"] != string.Empty && Request.QueryString["StudentNo"] != "0")
                {
                    DataTable dtCheckValueStudent = DyDataAccessManager.QueryStringValueCheck("dbo.Students", "studentNo", Request.QueryString["StudentNo"]);
                    if (dtCheckValueStudent.Rows.Count == 0)
                    {
                        Session["globalERROR"] = "Student No is not exist!";
                        Response.Redirect("~/Error.aspx");

                    }
                    Session["StudentNo"] = Convert.ToString(Request.QueryString["StudentNo"]);
                }

                if (Request.QueryString["LeadId"] != null && Request.QueryString["LeadId"] != string.Empty && Request.QueryString["LeadId"] != "0")

                {

                    DataTable dtCheckValueLead = DyDataAccessManager.QueryStringValueCheck("dbo.Lead", "LeadsID", Request.QueryString["LeadId"]);
                    if (dtCheckValueLead.Rows.Count == 0)
                    {
                        Session["globalERROR"] = "LeadsId is not exist!";
                        Response.Redirect("~/Error.aspx");

                    }
                  Session["LeadId"] = Convert.ToString(Request.QueryString["LeadId"]);
                }

                if (Session["LeadId"]=="" &&  Session["StudentNo"]=="") 
                {
                     Session["globalERROR"] = "Student No and Lead Id   Null";
                     Response.Redirect("~/Error.aspx");
                }

                if (Session["LeadId"] == "0" && Session["StudentNo"] == "0")
                {
                    Session["globalERROR"] = "Student No and Lead Id   Null";
                    Response.Redirect("~/Error.aspx");
                }
                if (Request.QueryString["DeptID"] != null && Request.QueryString["DeptID"] != string.Empty && Request.QueryString["DeptID"] != "0")
                {
                    DataTable dtCheckValueDeptId = DyDataAccessManager.QueryStringValueCheck("dbo.Departments", "deptid", Request.QueryString["DeptID"]);
                    if (dtCheckValueDeptId.Rows.Count == 0)
                    {
                        Session["globalERROR"] = "Department ID is not exist!";
                        Response.Redirect("~/Error.aspx");

                    }
                    Session["DeptID"] = Convert.ToString(Request.QueryString["DeptID"]);
                   
                
                }
                else
                {
                    Session["globalERROR"] = "Deptment Id  is  Null";
                    Response.Redirect("~/Error.aspx");
                    
                }

                string Starturl = string.Empty;

                Starturl = "~/frmlogin.aspx";


                Response.Redirect(Starturl, false);
               Context.ApplicationInstance.CompleteRequest();
                
            }
            catch(Exception ex)
            {
                throw ex; //
            }
        }
    }
}