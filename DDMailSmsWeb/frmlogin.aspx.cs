using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DDMailSmsWeb.DataAccess;

namespace DDMailSmsWeb
{
    public partial class frmlogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        protected void RbtnLogin_Click(object sender, EventArgs e)
        {
            if (Session["CampusID"] != null)
            {
                if (Page.IsValid)
                {
                    string username = string.Empty, password = string.Empty;
                    username = RTxtUsername.Text;
                    password = RTxtPassword.Text;
                    DataTable dtloginuser = new DataTable();
                    dtloginuser = DataAccessManager.GetUserLogin(username, password, Session["CampusID"].ToString());
                    if (dtloginuser.Rows.Count > 0)
                    {
                        
                        Session["username"] = dtloginuser.Rows[0]["username"];
                        Session["user_level"] = dtloginuser.Rows[0]["User_level"];
                        Session["AllowSwitchdept"] = Convert.ToInt32(dtloginuser.Rows[0]["SwitchDept"]);
                        string LeadId = string.Empty, StudentNo = string.Empty;
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

                        Session["PageClick"] = "0";
                        Response.Redirect("frmstudentlead.aspx?type=" + type + "&Operation=Inbox");

                    }
                    else
                    {
                        RlblResult.Text = "Invalid credentials!";


                    }
                }
            }
            else 
            {

              string username = string.Empty, password = string.Empty;
                    username = RTxtUsername.Text;
                    password = RTxtPassword.Text;
                    DataTable dtloginuser = new DataTable();
                    dtloginuser = DataAccessManager.GetUserLogin(username, password,"4");
                    if (dtloginuser.Rows.Count > 0)
                    {
                        Session["username"] = dtloginuser.Rows[0]["username"];
                        Session["user_level"] = dtloginuser.Rows[0]["User_level"];
                        if (Session["user_level"].ToString() == "99")
                        {
                            Response.Redirect("frmCampusMaster.aspx");
                        }
                        else 
                        {
                            RlblResult.Text = "You are not authorised to login!";
                        }

                    }
                    else
                    {
                        RlblResult.Text = "Invalid credentials!";


                    }
            }

        }
    }
}