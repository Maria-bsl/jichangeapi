using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;

namespace BIZINVOICING.Controllers
{
    public class AuditTrailController : AdminBaseController
    {
        // GET: AuditTrail
        TableDetails tb = new TableDetails();
        EMP_DET ed = new EMP_DET();
        Auditlog ad = new Auditlog();
        
        public ActionResult AuditTrail()
        {
            if (Session["sessB"] == null && (Session["sessComp"] == null)) 
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }

        [HttpPost]
        public ActionResult getdet(string tbname, string Startdate, string Enddate, string act)
        {
            try
            {
                List<Object> Time = new List<Object>();
                string dc;
                string date1 = DateTime.ParseExact(Startdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                string date2 = DateTime.ParseExact(Enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                DateTime FromDateVal = DateTime.Parse(date1);
                DateTime toDateVal = DateTime.Parse(date2);
                var dd = tb.Getlog(tbname);
                var objlistmem = ad.GetBloglist(FromDateVal, toDateVal, tbname, act, dd.Relation);
                if (objlistmem != null)
                {
                    foreach (Auditlog c in objlistmem)
                    {
                        Time.Add(new
                        {

                            ovalue = c.Oldvalues,
                            nvalue = c.Newvalues,//*.Split(' ')[1] == "12:00:00" ? c.Newvalues: c.Newvalues*//*,
                            atype = c.Audit_Type,
                            colname = c.Columnsname,
                            aby = c.AuditBy,
                            adate = c.Audit_Date,

                        });
                    }
                    return Json(Time, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception Ex)
            {
                //Ex.ToString();
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
            }

            return null;
        }[HttpPost]
        public ActionResult getdet1(string tbname, string Startdate, string Enddate, string act)
        {
            try
            {
                List<Object> Time = new List<Object>();
                string dc;
                string date1 = DateTime.ParseExact(Startdate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                string date2 = DateTime.ParseExact(Enddate, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");
                DateTime FromDateVal = DateTime.Parse(date1);
                DateTime toDateVal = DateTime.Parse(date2);
                var dd = tb.Getlog(tbname);
                var objlistmem = ad.GetBloglist1(FromDateVal, toDateVal, tbname, act, dd.Relation, long.Parse(Session["CompID"].ToString()));
                if (objlistmem != null)
                {
                    foreach (Auditlog c in objlistmem)
                    {
                        Time.Add(new
                        {

                            ovalue = c.Oldvalues,
                            nvalue = c.Newvalues,//*.Split(' ')[1] == "12:00:00" ? c.Newvalues: c.Newvalues*//*,
                            atype = c.Audit_Type,
                            colname = c.Columnsname,
                            aby = c.AuditBy,
                            adate = c.Audit_Date,

                        });
                    }
                    return Json(Time, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception Ex)
            {
                //Ex.ToString();
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
            }

            return null;
        }
    }
}