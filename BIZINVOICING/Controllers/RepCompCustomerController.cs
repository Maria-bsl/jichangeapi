using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class RepCompCustomerController : LangcoController
    {
        // GET: RepCompCustomer
        CustomerMaster cm = new CustomerMaster();
        CompanyBankMaster c = new CompanyBankMaster();
        public ActionResult RepCompCustomer()
        {
            if (Session["sessComp"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CompList()
        {

            try
            {

                var result = c.CompGet(long.Parse(Session["CompID"].ToString()));
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
        public ActionResult GetcustDetReport(long Comp,long reg, long dist)
        {

            try
            {

                var result = cm.CustGetrep1(Comp,reg, dist);
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