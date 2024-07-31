using BL.BIZINVOICING.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.IO;
using System.Xml;
using BL.BIZINVOICING.BusinessEntities.ConstantFile;

namespace BIZINVOICING.Controllers
{
    public class CompanyUsersController : LangcoController
    {
        // GET: CompanyUsers
        CompanyUsers cu = new CompanyUsers();
        EMAIL em = new EMAIL();
        S_SMTP ss = new S_SMTP();
        private readonly dynamic returnNull = null;
        String pwd, drt;
        Auditlog ad = new Auditlog();
        

        String[] list = new String[8] { "comp_users_sno", "comp_mas_sno", "username",  "user_type", "created_date", "expiry_date",
             "posted_by", "posted_date"};
        public ActionResult CompanyUsers()
        {
            if (Session["sessComp"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetCompanyUserss()
        {
            try
            {
                cu.Compmassno = long.Parse(Session["CompID"].ToString());
                var result = cu.GetCompanyUsers1(long.Parse(Session["CompID"].ToString()));
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var d = 0;
                    return Json(d, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public ActionResult EditCompanyUserss(long Sno)
        {
            try
            {
                //var result = cu.EditCompanyUsers(long.Parse(Session["UserID"].ToString()));
                var result = cu.EditCompanyUsers(Sno);
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var d = 0;
                    return Json(d, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        private static string CreateRandomPassword(int length)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        public static string GetEncryptedData(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
        [HttpPost]
        public ActionResult AddCompanyUser(String chname, string auname, String mob, String mail, String uname, string pos, /*long dsno,*/ long sno)
        {
            try
            {

                cu.Compmassno = long.Parse(Session["CompID"].ToString());
                cu.Username = auname;
                cu.Mobile = mob;
               // cu.Re_Ssno = dsno;
                cu.Userpos = pos;
                cu.Email = mail;
                cu.Fullname = uname;
                //cu.User_SNO = sno;
                cu.Flogin = "false";
                cu.CreatedDate = System.DateTime.Now;
                cu.ExpiryDate = System.DateTime.Now.AddMonths(3);
                cu.Usertype = chname;
                pwd = CreateRandomPassword(8);
                cu.Password = GetEncryptedData(pwd);
                cu.PostedBy = Session["UserID"].ToString();
                long ssno = 0;
                if (sno == 0)
                {
                    if (cu.ValidateduplicateEmail(mail))
                    {
                        return Json("Exist", JsonRequestBehavior.AllowGet);
                    }
                    else if (cu.Validateduplicateuser(auname))
                    {
                        return Json("UExist", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ssno = cu.AddCompanyUsers(cu);
                        if (ssno > 0)
                        {

                            SendActivationEmail(mail, auname, pwd, uname);
                            //SendSms(mob, auname, pwd, uname);
                            String[] list1   = new String[8] { ssno.ToString(), Session["CompID"].ToString(), auname, chname, System.DateTime.Now.ToString(), System.DateTime.Now.AddMonths(3).ToString(),
              Session["UserID"].ToString(), DateTime.Now.ToString() };
                            //String[] list1 = new String[11] { ssno.ToString(), auname, chname, DateTime.Now.ToString(), DateTime.Now.AddMonths(3).ToString(), Session["UserID"].ToString(), DateTime.Now.ToString(), uname, mail, mob, pos/*,dsno*/};
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Companyusers";
                                ad.Comp_Sno = long.Parse(Session["CompID"].ToString());
                                ad.Newvalues = list1[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }

                        }
                        return Json(ssno, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (sno > 0)
                {
                    
                    cu.CompuserSno = sno;
                    var ed = cu.EditCompanyUsers(sno);
                    if (ed != null)
                    {
                        String[] list2 = new String[8] { ed.CompuserSno.ToString(), ed.Compmassno.ToString(), ed.Username, ed.Usertype, ed.CreatedDate.ToString(), ed.ExpiryDate.ToString(),
              Session["UserID"].ToString(), ed.PostedDate.ToString() };
                        String[] list1 = new String[8] { sno.ToString(), Session["CompID"].ToString(), auname, chname, System.DateTime.Now.ToString(), System.DateTime.Now.AddMonths(3).ToString(),
              Session["UserID"].ToString(), DateTime.Now.ToString() };
                        for (int i = 0; i < list.Count(); i++)
                        {
                            ad.Audit_Type = "Update";
                            ad.Columnsname = list[i];
                            ad.Table_Name = "Companyusers";
                            ad.Oldvalues = list2[i];
                            ad.Newvalues = list1[i];
                            ad.AuditBy = Session["UserID"].ToString();
                            ad.Comp_Sno = long.Parse(Session["CompID"].ToString());
                            ad.Audit_Date = DateTime.Now;
                            ad.Audit_Time = DateTime.Now;
                            ad.AddAudit(ad);
                        }
                    }
                    
                    cu.UpdateCompanyUsers(cu);
                    ssno = sno;
                    return Json(ssno, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public ActionResult CheckdupliacteEmail(String name)
        {
            try
            {
                var check = cu.ValidateduplicateEmail(name);
                if (check == true)
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }

                return Json(check, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public ActionResult Checkdupliacte(String name)
        {
            try
            {
                var check = cu.Validateduplicateuser(name);
                if (check == true)
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }

                return Json(check, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public ActionResult DeleteCompanyUser(long sno)
        {
            try
            {
                if (sno > 0)
                {
                    //var check = ch.ValidateDeletion(sno);
                    //if (check == true)
                    //{
                    //    return Json("exist", JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{
                        cu.CompuserSno = sno;
                    var ed = cu.EditCompanyUsers(sno);
                    if (ed != null)
                    {
                        String[] list2 = new String[8] { ed.CompuserSno.ToString(), ed.Compmassno.ToString(), ed.Username, ed.Usertype, ed.CreatedDate.ToString(), ed.ExpiryDate.ToString(),
              ed.PostedBy, ed.PostedDate.ToString() };

                        for (int i = 0; i < list.Count(); i++)
                        {
                            ad.Audit_Type = "Delete";
                            ad.Columnsname = list[i];
                            ad.Table_Name = "Companyusers";
                            ad.Oldvalues = list2[i];
                            ad.AuditBy = Session["UserID"].ToString();
                            ad.Comp_Sno = long.Parse(Session["CompID"].ToString());
                            ad.Audit_Date = DateTime.Now;
                            ad.Audit_Time = DateTime.Now;
                            ad.AddAudit(ad);
                        }
                    }
                    cu.DeleteCompany(sno);

                        //cu.DeleteChruchReg(sno);
                   // }
                    return Json(sno, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
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
                    var m = ss.getSMTPText();
                    var data = em.GetLatestEmailTextsListByFlow("1");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Local_subject;
                    drt = data.Local_subject;
                    var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Loginnew", "Loginnew"),
                       Query = null,
                   };

                    Uri uri = urlBuilder.Uri;
                    //string url = "web_url";
                    string weburl = System.Web.Configuration.WebConfigurationManager.AppSettings["web_url"].ToString();
                    string url = "<a href='" + weburl + "' target='_blank'>" + weburl + "</a>";
                    //location.href = '/Loginnew/Loginnew';
                    String body = data.Local_Text.Replace("}+cName+{", uname).Replace("}+uname+{", auname).Replace("}+pwd+{", pwd).Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");
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
               // Utilites.logfile("Instituion user", drt, Ex.ToString());
            }

        }










    }
}