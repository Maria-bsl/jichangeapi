using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Text;
using BL.BIZINVOICING.BusinessEntities.Masters;
using BL.BIZINVOICING.BusinessEntities.ConstantFile;

namespace BIZINVOICING.Controllers
{
    public class ForgotController : Controller
    {
        private readonly dynamic returnNull = null;
        // GET: Forgot
        EMP_DET emp = new EMP_DET();
        S_SMTP stp = new S_SMTP();
        EMAIL em = new EMAIL();
        string drt;
        public ActionResult Forgot()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Getemail(String Sno)
        {
            try
            {
                var result = emp.FPassword(Sno);
                if (result == null)
                {
                    result = emp.FPasswordE(Sno);
                    
                }
                if (result != null)
                {
                    SendActivationEmail(result.Email_Address, result.Full_Name, DecodeFrom64(result.Password), result.User_name);
                    /*ch.User_SNO = result.User_SNO;
                    ch.Email = Sno;
                    ch.UpdateUsers(ch);*/
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }

        private void SendActivationEmail(String email, String auname, String pwd, String uname)
        {
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();

                using (MailMessage mm = new MailMessage())
                {
                    var m = stp.getSMTPText();
                    if (m != null)
                    {
                        //var data = em.getEMAILst("2");
                        mm.To.Add(email);
                        mm.From = new MailAddress(m.From_Address);
                        //mm.Subject = data.Subject;
                        mm.Subject = "Forgot Password Details";
                        //drt = data.Subject;
                        var urlBuilder =
                       new System.UriBuilder(Request.Url.AbsoluteUri)
                       {
                           Path = Url.Action("Loginnew", "Loginnew"),
                           Query = null,
                       };

                        Uri uri = urlBuilder.Uri;
                        string url = urlBuilder.ToString();
                        //String body = data.Email_Text.Replace("}+cName+{", uname).Replace("}+uname+{", auname).Replace("}+pwd+{", pwd).Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");
                        String body = "Dear " + auname + "<br>";
                        body += "User Name:" + uname + "<br>";
                        body += "Password:" + pwd + "<br><br>";
                        body += "Admin" + "<br>";
                        body += "Invoicing Portal" + "<br>";
                        mm.Body = body;
                        mm.IsBodyHtml = true;

                        smtp.Host = m.SMTP_Address;
                        smtp.EnableSsl = Convert.ToBoolean(m.SSL_Enable);
                        NetworkCredential NetworkCred = new NetworkCredential(m.From_Address, m.SMTP_Password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.Send(mm);
                    }
                }
            }
            catch (Exception Ex)
            {
                Utilites.logfile("Forgot", drt, Ex.ToString());
            }

        }
        public static string DecodeFrom64(string password)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(password);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
    }
}