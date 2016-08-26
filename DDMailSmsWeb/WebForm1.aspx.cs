using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DDMailSmsWeb.Classes;
using DDMailSmsWeb.DataAccess;
using DDMailSmsWeb.DynamicAccess;
using Telerik.Web.UI;
using System.Net.Mail;
using System.Net;

namespace DDMailSmsWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


         





            System.Net.Mail.MailMessage mailUserAdmin = new System.Net.Mail.MailMessage();
            mailUserAdmin.From = new MailAddress("registrar@abtu.edu");
            mailUserAdmin.To.Add("uttamrawat13@gmail.com");
           
            mailUserAdmin.Subject = "Subject";
            mailUserAdmin.IsBodyHtml = true;
           

            mailUserAdmin.Body = "Body";


            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;

            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = true;
            NetworkCredential NetworkCred = new NetworkCredential("registrar@abtu.edu", "Millie12", "smtp.gmail.com");
         
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
         
            smtp.Send(mailUserAdmin);
        }
    }
}