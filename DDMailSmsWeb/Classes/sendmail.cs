using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Net;

namespace DDMailSmsWeb.Classes
{
    public class sendmail
    {
        private static sendmail instance;

        private sendmail() { }

        public static sendmail Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new sendmail();
                }
                return instance;
            }
        }




        public bool MailGo(string strHTML, string From, string Too, string CC, string Bcc, string Subject, string SMTP, string pass, Int32 port, string AttachedFile, Boolean ssl)
        {
            bool sucess = true;
            try
            {

                System.Net.Mail.MailMessage mailUserAdmin = new System.Net.Mail.MailMessage();
                mailUserAdmin.From = new MailAddress(From);
                mailUserAdmin.To.Add(Too);
                if (CC != string.Empty)
                {
                    mailUserAdmin.CC.Add(CC);
                }
                if (Bcc != string.Empty)
                {
                    mailUserAdmin.Bcc.Add(Bcc);
                }
                mailUserAdmin.Subject = Subject;
                mailUserAdmin.IsBodyHtml = true;
                if (AttachedFile.Length > 0)
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(AttachedFile);
                    mailUserAdmin.Attachments.Add(attachment);
                }

                mailUserAdmin.Body = strHTML;


                SmtpClient smtp = new SmtpClient();
                smtp.Host = SMTP;//"smtp.gmail.com";
                smtp.EnableSsl = ssl;// true;

                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                NetworkCredential NetworkCred = new NetworkCredential(From, pass);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = port;
                smtp.Send(mailUserAdmin);
            }
            catch (Exception ex)
            {
                sucess = false;
                throw ex;
            }
            return sucess;

        }

    }
}