using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class RepZController : AdminBaseController
    {
        // GET: RepZ

        INVOICE inv = new INVOICE();
        public ActionResult RepZ()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }

        [HttpPost]
        public ActionResult GetInvReport( string stdate, string enddate)
        {

            try
            {

                var result = inv.GetZrep( stdate, enddate);
                
                
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