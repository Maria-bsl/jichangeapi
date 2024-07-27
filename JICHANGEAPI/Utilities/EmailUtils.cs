using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BL.BIZINVOICING.BusinessEntities.Masters;
using System.Runtime.Remoting.Messaging;

namespace JichangeApi.Utilities
{
    public class EmailUtils
    {
        public static void SendActivationEmail(String email, String auname, String pwd, String uname)
        {
            EMAIL em = new EMAIL();
            S_SMTP ss = new S_SMTP();
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();

                using (MailMessage mm = new MailMessage())
                {
                    var m = ss.getSMTPText();
                    var data = em.getEMAILst("1");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Local_subject;
                    string drt = data.Local_subject;
                    /*var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Loginnew", "Loginnew"),
                       Query = null,
                   };

                    Uri uri = urlBuilder.Uri;*/
                    //string url = "web_url";
                    // string weburl = System.Web.Configuration.WebConfigurationManager.AppSettings["web_url"].ToString();
                    //  string url = "<a href='" + weburl + "' target='_blank'>" + weburl + "</a>";
                    //location.href = '/Loginnew/Loginnew';
                    String body = data.Local_Text.Replace("}+cName+{", uname).Replace("}+uname+{", auname).Replace("}+pwd+{", pwd).Replace("}+ +{", "").Replace("{", "").Replace("}", "");
                    //m1(weburl);
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    if (string.IsNullOrEmpty(m.SMTP_UName))
                    {
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.Host = m.SMTP_Address;
                    }
                    else
                    {
                        smtp.Host = m.SMTP_Address;
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.EnableSsl = Convert.ToBoolean(m.SSL_Enable);
                        NetworkCredential NetworkCred = new NetworkCredential(m.SMTP_UName, Utilites.DecodeFrom64(m.SMTP_Password));
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                    }
                    smtp.Send(mm);
                }
            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                // Utilites.logfile("lcituion user", drt, Ex.ToString());
            }

        }
        public static void SendSubjectTextBodyEmail(string email,string subject,string text,string body)
        {
            try
            {
                S_SMTP smtp = new S_SMTP();
                SmtpClient smtpClient = new SmtpClient();
                MailMessage mailMessage = new MailMessage();
                var esmtp = smtp.getSMTPText();
                if (esmtp == null) throw new ArgumentException("Error occured with the smtp");
                int port = Int32.Parse(esmtp.SMTP_Port);
                if (string.IsNullOrEmpty(esmtp.SMTP_UName))
                {
                    smtpClient = new SmtpClient(esmtp.SMTP_Address, port);
                    mailMessage = new MailMessage(esmtp.From_Address, email, subject, body);
                    mailMessage.IsBodyHtml = true;
                }
                else
                {
                    mailMessage = new MailMessage(esmtp.From_Address, email, subject, body);
                    mailMessage.IsBodyHtml = true;
                    smtpClient = new SmtpClient(esmtp.SMTP_Address, port);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = Convert.ToBoolean(esmtp.SSL_Enable);
                    smtpClient.Credentials = new NetworkCredential(esmtp.SMTP_UName, Utilites.DecodeFrom64(esmtp.SMTP_Password));
                }
                smtpClient.Send(mailMessage);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
