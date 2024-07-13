using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;
using BL.BIZINVOICING.BusinessEntities.Masters;
using BL.BIZINVOICING.BusinessEntities.ConstantFile;
namespace BIZINVOICING.Controllers
{
    public class EMPLOYDETController : AdminBaseController
    {
        private readonly dynamic returnNull = null;
        EMP_DET dt = new EMP_DET();
        DESIGNATION dg = new DESIGNATION();
        S_SMTP stp = new S_SMTP();
        EMAIL em = new EMAIL();
        EMP_DET ed = new EMP_DET();
        Auditlog ad = new Auditlog();
        BranchM bm = new BranchM();
        String pwd = "", drt;
        String[] list = new String[15] { "emp_detail_id", "emp_id_no","fullname","first_name", "middle_name", "last_name", "desg_id","email_id", "mobile_no",
            "created_date","expiry_date", "emp_status","posted_by", "posted_date","username"};

        public ActionResult EMPLOYDET()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }

            return View();
        }
        [HttpPost]
        public ActionResult GetEMPDetails()
        {
            try
            {
                var result = dt.GetEMP();
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
        [HttpGet]
        public ActionResult GetdesgDetails()
        {
            try
            {
                var result = dg.GetDesignation();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }
        [HttpGet]
        public ActionResult GetBranchDetails()
        {
            try
            {
                 var result = bm.GetBranches_Active();
                if (Session["desig"] == null)
                {
                    result = bm.GetBranches_Active();
                }
                else
                {
                    if (Session["desig"].ToString() == "Administrator")
                    {
                        result = bm.GetBranches_Active();
                    }
                    else
                    {
                        result = bm.GetBranches_Active(long.Parse(Session["BRAID"].ToString()));
                    }
                }
                
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }
        public ActionResult GetBranchDetails1()
        {
            try
            {
                var result = bm.GetBranches_Active();
                

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }

        [HttpPost]
        public ActionResult GetEmpindivi(long Sno)
        {
            try
            {
                var result = dt.EditEMP(Sno);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public ActionResult AddEmp(string empid, string fullname, String fname, String mname, String lname, long desg,String email, String mobile, String user, String gender, long sno, bool dummy, long branch)
        {
            try
            {
                dt.Emp_Id_No = empid;
                dt.First_Name = fname;
                dt.Middle_name = mname;
                dt.Last_name = lname;
                dt.Full_Name = fname + " " + mname + " " + lname;
                dt.Desg_Id = desg;
                dt.Email_Address = email;
                dt.Mobile_No = mobile;
                dt.User_name = user;
                dt.Emp_Status = gender;
                dt.Branch_Sno = branch;
                dt.AuditBy = Session["UserID"].ToString();
                dt.Created_Date = System.DateTime.Now;
                dt.Expiry_Date = System.DateTime.Now.AddMonths(3);
                dt.Detail_Id = sno;
                long ssno = 0;
                if (sno == 0)
                {
                    var result = dt.Validateuser(empid.ToLower());
                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        pwd = CreateRandomPassword(8);
                        var des = dg.Editdesignation(desg);
                        dt.Password = GetEncryptedData(pwd);
                       
                        if (ssno > 0)
                        {
                            String[] list1 = new String[15] { ssno.ToString(), dt.Emp_Id_No,dt.Full_Name,dt.First_Name, dt.Middle_name,  dt.Last_name,des.Desg_Name.ToString(),dt.Email_Address,
                                dt.Mobile_No,dt.Created_Date.ToString(),dt.Expiry_Date.ToString(), dt.Emp_Status,Session["UserID"].ToString(), DateTime.Now.ToString(),dt.User_name };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Bank User";
                                ad.Newvalues = list1[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }


                        }
                        //SendActivationEmail(email, fullname, pwd, user);
                        ssno = dt.AddEMP(dt);
                        return Json(ssno, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (sno > 0)
                {
                    if (dummy == false)
                    {
                        return Json(dummy, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        dt.Detail_Id = sno;
                        var des = dg.Editdesignation(desg);
                        //var brn = br.Editbranch(bsno);
                       var  dd = dt.EditEMP(sno);
                        if (dd != null)
                        {
                            String[] list2 = new String[15] { dd.Detail_Id.ToString(), dd.Emp_Id_No,dd.Full_Name,dd.First_Name, dd.Middle_name,  dd.Last_name,dd.Desg_name.ToString(),dd.Email_Address,
                                dd.Mobile_No,dd.Created_Date.ToString(),dd.Expiry_Date.ToString(), dd.Emp_Status,dd.AuditBy,dd.Audit_Date.ToString(),dd.User_name };

                            String[] list1 = new String[15] { ssno.ToString(), dt.Emp_Id_No,dt.Full_Name,dt.First_Name, dt.Middle_name,  dt.Last_name,des.Desg_Name.ToString(),dt.Email_Address,
                                dt.Mobile_No,dt.Created_Date.ToString(),dt.Expiry_Date.ToString(), dt.Emp_Status,Session["UserID"].ToString(), DateTime.Now.ToString(),dt.User_name };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Update";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Bank User";
                                ad.Oldvalues = list2[i];
                                ad.Newvalues = list1[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }

                            dt.UpdateEMP(dt);
                        }
                        //dt.UpdateEMP(dt);
                        return Json(sno, JsonRequestBehavior.AllowGet);
                    }
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

            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        private void SendActivationEmail(String email, String fullname, String pwd, String uname)
        {
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();

                using (MailMessage mm = new MailMessage())
                {
                    var m = stp.getSMTPText();
                    var data = em.getEMAILst("2");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Subject;
                    drt = data.Subject;
                    var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Loginnew", "Loginnew"),
                       Query = null,
                   };

                    Uri uri = urlBuilder.Uri;
                    string url = urlBuilder.ToString();
                    String body = data.Email_Text.Replace("}+cName+{", fullname).Replace("}+uname+{", uname).Replace("}+pwd+{", pwd).Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");

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
            catch (Exception Ex)
            {
                Utilites.logfile("Bank", drt, Ex.ToString());
            }

        }
        public static string GetEncryptedData(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
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
        [HttpPost]
        public ActionResult DeleteEmp(string id, long sno)
        {
            try
            {
                if (sno > 0)
                {
                    var check = dt.Validateuser(id);
                    if (check == true)
                    {
                        return Json(check, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var dd = dt.EditEMP(sno);
                        if (dd != null)
                        {
                            /*String[] list2 = new String[16] { dd.Detail_Id.ToString(), dd.Emp_Id_No,dd.Full_Name,dd.First_Name, dd.Middle_name,  dd.Last_name,dd.Desg_name.ToString(),dd.Email_Address,
                                dd.Mobile_No,dd.Created_Date.ToString(),dd.Expiry_Date.ToString(), dd.Emp_Status,dd.AuditBy,dd.Audit_Date.ToString(),dd.User_name,dd.Branch_Name.ToString() };

                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Delete";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Bank User";
                                ad.Oldvalues = list2[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }*/
                            dt.DeleteEMP(sno);
                        }
                        //dt.DeleteEMP(sno);
                        return Json(sno, JsonRequestBehavior.AllowGet);
                    }


                }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }

    }
}