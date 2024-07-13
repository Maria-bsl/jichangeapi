using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class QuestionController : AdminBaseController
    {
        // GET: Question
        REG_QSTN cty = new REG_QSTN();
        private readonly dynamic returnNull = null;
        Auditlog ad = new Auditlog();
        String[] list = new String[5] {"sno", "q_name", "q_status", "posted_by", "posted_date" };
        public ActionResult Question()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
          
            return View();
        }
        public ActionResult AddQues(
             string q_name,
             string q_qtatus,
             string auditby,
             long sno,
             bool dummy
             )
        {

            try
            {

                cty.Q_Name = q_name;
                cty.Q_Status = q_qtatus;
                cty.AuditBy= Session["UserID"].ToString();
                cty.SNO = sno;
                
                    long ssno = 0;
                if (sno == 0)
                {
                    var result = cty.ValidateQSTN(q_name.ToLower());
                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                         ssno = cty.AddREG_QSTN(cty);
                        if (ssno > 0)
                        {
                            String[] list1 = new String[5] { ssno.ToString(), q_name, q_qtatus, Session["UserID"].ToString(), DateTime.Now.ToString() };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Questions";
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
                    if (dummy == false)
                    {
                        return Json(dummy, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var dd = cty.EditREG_QSTN(sno);
                        if (dd != null)
                        {
                            String[] list2 = new String[5] { dd.SNO.ToString(), dd.Q_Name, dd.Q_Status, dd.AuditBy, dd.Audit_Date.ToString() };
                            String[] list1 = new String[5] { dd.SNO.ToString(), q_name, q_qtatus, Session["UserID"].ToString(), DateTime.Now.ToString() };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Update";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Questions";
                                ad.Oldvalues = list2[i];
                                ad.Newvalues = list1[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }
                        cty.UpdateREG_QSTN(cty);
                        ssno = sno;
                        return Json(ssno, JsonRequestBehavior.AllowGet);
                      
                    }
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public ActionResult DeleteQues(
           long sno, string qname
           )
        {

            try
            {

                cty.SNO = sno;
                var name = cty.CheckQues(sno);
                if (name == true)
                {
                    return Json(name, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (sno > 0)
                    {
                        var dd = cty.EditREG_QSTN(sno);
                        if (dd != null)
                        {
                            String[] list2 = new String[5] { dd.SNO.ToString(), dd.Q_Name, dd.Q_Status, dd.AuditBy, dd.Audit_Date.ToString() };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Delete";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Questions";
                                ad.Oldvalues = list2[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }
                        cty.DeleteREG_QSTN(sno);
                    }
                    var result = sno;

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }
      
        public ActionResult GetQues()
        {

            try
            {
                var result = cty.GetQSTN();
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

    }
}