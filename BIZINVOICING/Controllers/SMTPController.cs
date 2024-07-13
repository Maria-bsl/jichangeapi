using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;

namespace BIZINVOICING.Controllers
{
    public class SMTPController : AdminBaseController
    {
        S_SMTP smtp = new S_SMTP();
        EMP_DET ed = new EMP_DET();
        Auditlog ad = new Auditlog();
        private readonly dynamic returnNull = null;
        String[] list = new String[9] { "sno", "from_address", "smtp_address","smtp_port","username","ssl_enable","effective_date", "posted_by", "posted_date" };
        // GET: SMTP
        public ActionResult SMTP()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }

        // GET: SMTP/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSMTP(string from_address, string smtp_address, string smtp_port, string gender, string smtp_uname, string smtp_pwd, string posted_by, long sno)
        {
            try
            {
                smtp.From_Address = from_address;
                smtp.SMTP_Address = smtp_address;
                smtp.SMTP_Port = smtp_port;
                smtp.SMTP_UName = smtp_uname;
                smtp.SMTP_Password = smtp_pwd;
                smtp.SSL_Enable = gender;
                smtp.AuditBy = Session["UserID"].ToString();
                smtp.SNO = sno;
                 long ssno = 0;
                if (sno == 0)
                {
                    ssno = smtp.AddSMTP(smtp);
                    if (ssno > 0)
                    {
                        String[] list1 = new String[9] { ssno.ToString(), from_address, smtp_address, smtp_port, smtp_uname, gender, DateTime.Now.ToString(), Session["UserID"].ToString(), DateTime.Now.ToString() };
                        for (int i = 0; i < list.Count(); i++)
                        {
                            ad.Audit_Type = "Insert";
                            ad.Columnsname = list[i];
                            ad.Table_Name = "Smtp Settings";
                            ad.Newvalues = list1[i];
                            ad.AuditBy = Session["UserID"].ToString();
                            ad.Audit_Date = DateTime.Now;
                            ad.Audit_Time = DateTime.Now;
                            ad.AddAudit(ad);
                        }
                    }
                    return Json(ssno, JsonRequestBehavior.AllowGet);
                }
                else if (sno > 0)
                {
                    var dd = smtp.EditSMTP(sno);
                    if (dd != null)
                    {
                        String[] list2 = new String[9] { dd.SNO.ToString(), dd.From_Address, dd.SMTP_Address, dd.SMTP_Port, dd.SMTP_UName, dd.SSL_Enable, dd.Effective_Date.ToString(), dd.AuditBy, dd.Audit_Date.ToString() };
                        String[] list1 = new String[9] { dd.SNO.ToString(), from_address, smtp_address, smtp_port, smtp_uname, gender, DateTime.Now.ToString(), Session["UserID"].ToString(), DateTime.Now.ToString() };
                        for (int i = 0; i < list.Count(); i++)
                        {
                            ad.Audit_Type = "Update";
                            ad.Columnsname = list[i];
                            ad.Table_Name = "Smtp Settings";
                            ad.Oldvalues = list2[i];
                            ad.Newvalues = list1[i];
                            ad.AuditBy = Session["UserID"].ToString();
                            ad.Audit_Date = DateTime.Now;
                            ad.Audit_Time = DateTime.Now;
                            ad.AddAudit(ad);
                        }
                    }
                    smtp.UpdateSMTP(smtp);
                    ssno = sno;
                    return Json(ssno, JsonRequestBehavior.AllowGet);
                }
                   
                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public ActionResult DeleteSMTP(long sno)
        {
            try
            {

                smtp.SNO = sno;
                if (sno > 0)
                {
                    var dd = smtp.EditSMTP(sno);
                    if (dd != null)
                    {
                        String[] list2 = new String[9] { dd.SNO.ToString(), dd.From_Address, dd.SMTP_Address, dd.SMTP_Port, dd.SMTP_UName, dd.SSL_Enable, dd.Effective_Date.ToString(), dd.AuditBy, dd.Audit_Date.ToString() };
                        for (int i = 0; i < list.Count(); i++)
                        {
                            ad.Audit_Type = "Delete";
                            ad.Columnsname = list[i];
                            ad.Table_Name = "Smtp Settings";
                            ad.Oldvalues = list2[i];
                            ad.AuditBy = Session["UserID"].ToString();
                            ad.Audit_Date = DateTime.Now;
                            ad.Audit_Time = DateTime.Now;
                            ad.AddAudit(ad);
                        }
                    }
                    smtp.DeleteSMTP(sno);
                    var result = sno;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
              
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public ActionResult ValidateSMTP(string email)
        {
            try
            {
                var result = smtp.ValidateFromName(email);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public ActionResult GetSMTPDetails()
        {
            try
            {
                var result = smtp.GetSMTPS();
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
                Ex.Message.ToString();
            }

            return returnNull;
        }

        [HttpPost]
        public ActionResult Checkdupliacte(String name)
        {
            try
            {
                var check = smtp.Validateduplicatedata(name);
                if (check == true)
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }

                return Json(check, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }
    }
}
