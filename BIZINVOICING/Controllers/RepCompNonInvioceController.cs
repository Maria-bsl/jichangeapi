using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class RepCompNonInvioceController : LangcoController
    {
        // GET: RepCompNonInvioce
        INVOICE inv = new INVOICE();
        CustomerMaster cm = new CustomerMaster();
        private readonly dynamic returnNull = null;
        
        public ActionResult RepCompNonInvioce()
        {
            if (Session["sessComp"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CustList()
        {

            try
            {

                var result = inv.GetCustomers11(long.Parse(Session["CompID"].ToString()));
                if (result != null)
                {

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

            return null;
        }
        [HttpPost]
        public ActionResult InvList(long Sno)
        {
            try
            {
                string ash = null;
                if (Sno == Convert.ToInt64(ash))
                {
                    return Json(Sno, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = inv.GetInvoiceNos(Sno);
                    if (result == null)
                    {
                        int d = 0;
                        return Json(d, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                Ex.ToString();
            }
            return returnNull;
        }
        //[HttpPost]
        //public ActionResult InvList(long Sno)
        //{

        //    try
        //    {

        //        var result = inv.GetInvoiceNos(Sno);
        //        if (result != null)
        //        {

        //            return Json(result, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(0, JsonRequestBehavior.AllowGet);
        //        }


        //    }
        //    catch (Exception Ex)
        //    {
        //        Ex.ToString();
        //    }

        //    return null;
        //}
        [HttpPost]
        public ActionResult customerList()
        {

            try
            {

                var result = cm.CustGet();
                if (result != null)
                {

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

            return null;
        }
        [HttpPost]
        public ActionResult GetInvReport(long Comp,long cusid, string stdate, string enddate)
        {

            try
            {

                var result = inv.GetInvRep11(Comp,cusid, stdate, enddate);
                if (result != null)
                {

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                // Message = "The cast to value type 'System.Decimal' failed because the materialized value is null. Either the result type's generic parameter or the query must use a nullable type."

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return null;
        }
        [HttpPost]
        public ActionResult GetInvDetReport(long invs, string stdate, string enddate, long Cust)
        {

            try
            {

                var result = inv.GetInvDetRep(invs, stdate, enddate, Cust);
                if (result != null)
                {

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

            return null;

        }











    }
}