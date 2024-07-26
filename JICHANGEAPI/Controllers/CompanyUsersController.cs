using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CompanyUsersController : SetupBaseController
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
       

        [HttpPost]
        public HttpResponseMessage GetCompanyUserss(SingletonComp c)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
                {
                    cu.Compmassno = long.Parse(c.compid.ToString());
                    var result = cu.GetCompanyUsers1(long.Parse(c.compid.ToString()));
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        var d = 0;
                        return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
                    }
                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                }
                return returnNull;
        }
        [HttpPost]
        public HttpResponseMessage EditCompanyUserss(SingletonSno Sno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
                {
                    //var result = cu.EditCompanyUsers(long.Parse(userid.ToString()));
                    var result = cu.EditCompanyUsers((long)Sno.Sno);
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        var d = 0;
                        return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
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
        public HttpResponseMessage AddCompanyUser(String chname, string auname, String mob, String mail, String uname,string userid, string pos, long compid, long sno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }

            try
                    {

                        cu.Compmassno = long.Parse(compid.ToString());
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
                        cu.PostedBy = userid.ToString();
                        long ssno = 0;
                if (sno == 0)
                {
                    if (cu.ValidateduplicateEmail(mail))
                    {
                        return Request.CreateResponse(new { response = "Email already exist", message = new List<string> { } });
                    }
                    else if (cu.Validateduplicateuser(auname))
                    {
                        return Request.CreateResponse(new { response = "User already exist", message = new List<string> { } });
                    }
                    else
                    {
                        ssno = cu.AddCompanyUsers(cu);
                        if (ssno > 0)
                        {

                            SendActivationEmail(mail, auname, pwd, uname);
                            //SendSms(mob, auname, pwd, uname);
                            String[] list1 = new String[8] { ssno.ToString(), compid.ToString(), auname, chname, System.DateTime.Now.ToString(), System.DateTime.Now.AddMonths(3).ToString(),
                      userid.ToString(), DateTime.Now.ToString() };
                            //String[] list1 = new String[11] { ssno.ToString(), auname, chname, DateTime.Now.ToString(), DateTime.Now.AddMonths(3).ToString(), userid.ToString(), DateTime.Now.ToString(), uname, mail, mob, pos/*,dsno*/};
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Companyusers";
                                ad.Comp_Sno = long.Parse(compid.ToString());
                                ad.Newvalues = list1[i];
                                ad.AuditBy = userid.ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }

                        }
                        return Request.CreateResponse(new { response = ssno, message = new List<string> { } });
                    }
                }
                else if (sno > 0)
                {

                    cu.CompuserSno = sno;
                    var ed = cu.EditCompanyUsers(sno);
                    if (ed != null)
                    {
                        String[] list2 = new String[8] { ed.CompuserSno.ToString(), ed.Compmassno.ToString(), ed.Username, ed.Usertype, ed.CreatedDate.ToString(), ed.ExpiryDate.ToString(),
                      userid.ToString(), ed.PostedDate.ToString() };
                        String[] list1 = new String[8] { sno.ToString(), compid.ToString(), auname, chname, System.DateTime.Now.ToString(), System.DateTime.Now.AddMonths(3).ToString(),
                     userid.ToString(), DateTime.Now.ToString() };
                        for (int i = 0; i < list.Count(); i++)
                        {
                            ad.Audit_Type = "Update";
                            ad.Columnsname = list[i];
                            ad.Table_Name = "Companyusers";
                            ad.Oldvalues = list2[i];
                            ad.Newvalues = list1[i];
                            ad.AuditBy = userid.ToString();
                            ad.Comp_Sno = long.Parse(compid.ToString());
                            ad.Audit_Date = DateTime.Now;
                            ad.Audit_Time = DateTime.Now;
                            ad.AddAudit(ad);
                        }
                    }

                    cu.UpdateCompanyUsers(cu);
                    ssno = sno;
                    return Request.CreateResponse(new {response = ssno, message = new List<string> { } });
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
        public HttpResponseMessage CheckdupliacteEmail(String name)
        {
            try
            {
                var check = cu.ValidateduplicateEmail(name);
                if (check == true)
                {
                    return Request.CreateResponse(new { response = check, message = new List<string> {"Failed" } });
                }

                return Request.CreateResponse(new { response = check, message = new List<string> {"Failed" } });
            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public HttpResponseMessage Checkdupliacte(String name)
        {
            try
            {
                var check = cu.Validateduplicateuser(name);
                if (check == true)
                {
                    return Request.CreateResponse(new { response = check, message = new List<string> {"Failed" } });
                }

                return Request.CreateResponse(new { response = check, message = new List<string> {"Failed" } });
            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public HttpResponseMessage DeleteCompanyUser(long sno, string userid, long compid)
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
                            ad.AuditBy = userid.ToString();
                            ad.Comp_Sno = long.Parse(compid.ToString());
                            ad.Audit_Date = DateTime.Now;
                            ad.Audit_Time = DateTime.Now;
                            ad.AddAudit(ad);
                        }
                    }
                    cu.DeleteCompany(sno);

                    //cu.DeleteChruchReg(sno);
                    // }
                    return Request.CreateResponse(new { response = sno, message = new List<string> { } });
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
                    var data = em.getEMAILst("1");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Local_subject;
                    drt = data.Local_subject;
                   /* var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Loginnew", "Loginnew"),
                       Query = null,
                   };

                    Uri uri = urlBuilder.Uri;*/
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
