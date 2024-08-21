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
using System.Configuration;

namespace JichangeApi.Utilities
{
    public class EmailUtils 
    {
        private readonly Payment pay = new Payment();
        public static void SendActivationEmail(string email, string fullname, string pwd, string username)
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
                    var data = em.GetLatestEmailTextsListByFlow("1");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Subject;
                    string drt = data.Email_Text;
                    /*var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Loginnew", "Loginnew"),
                       Query = null,
                   };

                    Uri uri = urlBuilder.Uri;*/
                    //string url = "web_url";
                    string weburl = ConfigurationManager.AppSettings["MyWebUrl"];
                    string url = "<a href='" + weburl + "' target='_blank'>" + weburl + "</a>";
                    //location.href = '/Loginnew/Loginnew';

                    //String body = data.Email_Text.Replace("}+cName+{", uname).Replace("}+uname+{", auname).Replace("}+pwd+{", pwd).Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");

                    //Welcome to Jichange Portal, login name: }+uname+{   Password: }+pwd+{  Join through the link below.  }+actLink+{  Change the password immediately after logging into the system.  All the best,  Jichange Team.}


                    string body = data.Email_Text.Replace("}+uname+{", username).Replace(" }+pwd+{", pwd + "\n").Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");

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
                Payment pay = new Payment
                {
                    Message = Ex.ToString()
                };
                pay.AddErrorLogs(pay);
            }

        }

        public static void SendSuccessEmail(string email, string company)
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
                    var data = em.GetLatestEmailTextsListByFlow("1");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Subject;
                    string drt = data.Email_Text;
                    /*var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Loginnew", "Loginnew"),
                       Query = null,
                   };

                    Uri uri = urlBuilder.Uri;*/
                    //string url = "web_url";
                    string weburl = ConfigurationManager.AppSettings["MyWebUrl"];
                    string url = "<a href='" + weburl + "' target='_blank'>" + weburl + "</a>";
                   
                    string body = string.Format("{0},You have Successfully registered on JICHANGE Portal, {1} Your account is pending for approval. ", company, email);
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
                //throw new Exception(Ex.ToString());
                Payment pay = new Payment
                {
                    Message = Ex.ToString()
                };
                pay.AddErrorLogs(pay);
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                // Utilites.logfile("lcituion user", drt, Ex.ToString());
            }

        }


        public static void SendCustomerDeliveryCodeEmail(string email, string otp, string mobile)
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
                    var data = em.GetLatestEmailTextsListByFlow("1");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Subject;
                    string drt = data.Email_Text;
                    /*var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Loginnew", "Loginnew"),
                       Query = null,
                   };

                    Uri uri = urlBuilder.Uri;*/
                    //string url = "web_url";
                    string weburl = ConfigurationManager.AppSettings["MyWebUrl"];
                    string url = "<a href='" + weburl + "' target='_blank'>" + weburl + "</a>";
                    string encrypt = PasswordGeneratorUtil.GetEncryptedData(mobile);// MjU1NzUzNjg4ODY3
                    var linkurl = ConfigurationManager.AppSettings["MyCodeUrl"] + encrypt;
                    string body = string.Format("{0},JICHANGE Confirmation code for delivery is {1}, verify through this link: {2}", email, otp, linkurl);
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
                //throw new Exception(Ex.ToString());
                Payment pay = new Payment
                {
                    Message = Ex.ToString()
                };
                pay.AddErrorLogs(pay);
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
                Payment pay = new Payment
                {
                    Message = ex.ToString()
                };
                pay.AddErrorLogs(pay);
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                Payment pay = new Payment
                {
                    Message = ex.ToString()
                };
                pay.AddErrorLogs(pay);
                throw new Exception(ex.Message);
            }
        }


        #region  Invoice Mails Section

        /*
         *  New Invoice:
            Hello (customer name), 
            Kindly pay (currency & amount) for invoice number (invoice number). Payment reference number is (control number).
            Regards,
            (Vendor name)

            ....for email you can attach the invoice....
         */
        public static void SendCustomerNewInvoiceEmail(string email, string customername, string invoiceno, string controlno, string vendor, string amount)
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
                    var data = em.GetLatestEmailTextsListByFlow("2"); // Invoice Generation

                    mm.Body = string.Format("Hello {0}, Kindly pay {1} for invoice number {2}.Payment reference number is {3}. Regards,{4} ", customername, amount, invoiceno, controlno, vendor);
                    mm.Subject = "INVOICE";

                    if (data != null)
                    {
                        mm.Subject = data.Subject;
                        
                        /*  Hello "}+customername+{", Kindly pay "}+amount+{" for invoice number "}+invno+{". Payment reference number is "}+controlno+{". Regards, "}+vendor+{" */

                       string content = data.Email_Text.Replace("}+customername+{", customername + "\n").Replace("}+amount+{", amount).Replace("}+invno+{", invoiceno).Replace("}+controlno+{", controlno + "\n").Replace("}+vendor+{", vendor);

                        mm.Body = content;
                    }
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    
                    //string body = string.Format("Hello {0}, Kindly pay {1} for invoice number {2}.Payment reference number is {3}. Regards,{4} ", customername, amount, invoiceno, controlno, vendor);
                    
                    
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
            {Payment pay = new Payment { Message = Ex.ToString() };
                pay.AddErrorLogs(pay);
            }

        }

        /*
            Invoice Ammendment:
            Hello (vendor name),
            Invoice number (invoice number) has been amended. New invoice amount is (currency & amount), reference number for payment is (control number).
            Regards,
            (Vendor name)
         */
        public static void SendCustomerAmmendedInvoiceEmail(string email, string customername, string invoiceno, string controlno, string vendor, string amount)
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
                    var data = em.GetLatestEmailTextsListByFlow("5"); // Invoice Amendment
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = "Invoice Amendment";
                    mm.Body = string.Format("Hello {0}, Invoice number {1} has been amended, New invoice amount is {2}, reference number for payment is {3}. Regards,{4} ", customername, invoiceno, amount, controlno, vendor);

                    if (data != null)
                    {
                        mm.Subject = data.Subject;

                        /*  Hello "}+customername+{", Invoice number "}+invno+{" has been amended, New invoice amount is "}+amount+{". reference number for payment is "}+controlno+{". Regards, "}+vendor+{" */

                        string content = data.Email_Text.Replace("}+customername+{", customername + "\n").Replace("}+invno+{", invoiceno).Replace("}+amount+{", amount).Replace("}+controlno+{", controlno + "\n").Replace("}+vendor+{", vendor);

                        mm.Body = content;
                    }
                   
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
            {Payment pay = new Payment { Message = Ex.ToString() };
                pay.AddErrorLogs(pay);
            }

        }

        /*
         *  Cancel Invoice:
            Hello (customer name),
            Invoice number (invoice number) with reference number (control number) has been cancelled. Reach us for new order and invoice.
            Regards,
            (Vendor Name)

            ...... For email you can attach cancelled Invoice with reason descriptions....
         */
        public static void SendCustomerCancelledInvoiceEmail(string email, string customername, string invoiceno, string controlno, string vendor, string amount)
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
                    var data = em.GetLatestEmailTextsListByFlow("4");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Subject;
                    mm.Subject = "Invoice Cancellation";
                    mm.Body = string.Format("Hello {0}, invoice number {1} with reference number {2} has been cancelled. Reach out for new order and invoice. Regards,{3} ", customername, invoiceno, controlno, vendor);

                    if (data != null)
                    {
                        mm.Subject = data.Subject;

                        /*  Hello "}+customername+{", Invoice number "}+invno+{" with reference number "}+controlno+{" has been cancelled. Reach out for new order and invoice. Regards, "}+vendor+{" */

                        string content = data.Email_Text.Replace("}+customername+{", customername + "\n").Replace("}+invno+{", invoiceno).Replace("}+controlno+{", controlno + "\n").Replace("}+vendor+{", vendor);

                        mm.Body = content;
                    }



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
            {Payment pay = new Payment { Message = Ex.ToString() };
                pay.AddErrorLogs(pay);
            }

        }


        #endregion
    }
}
