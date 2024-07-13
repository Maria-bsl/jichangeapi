using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
using System.Text;

namespace BIZINVOICING.Controllers
{
    public class VatperController : Controller
    {
        // GET: Vatper
        VatPercentage vatper = new VatPercentage();
        private readonly dynamic returnNull = null;

        public ActionResult VatPercentage()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
             return View();
        }


        public ActionResult GetCounts()
        {

            try
            {
                var result = vatper.GetVatPercentage();
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


        public ActionResult AddCount(int vat_percentageValue, long sno, bool dummy, string vat_cat, string vat_desc)
        {
            try
            {
                vatper.vat_percentageValue = vat_percentageValue;
                vatper.vat_per_sno = sno;
                vatper.Vat_Category = vat_cat;
                vatper.Vat_Description = vat_desc;
                long ssno = 0;
                if (sno == 0)
                {
                    var result = vatper.ValidateLicense(vat_cat);
                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ssno = vatper.AddVat(vatper);
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
                        vatper.UpdateVatpercentage(vatper);
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


        public ActionResult DeleteCount(long sno)
        {

            try
            {

                vatper.vat_per_sno = sno;
                var name = vatper.ValidateCount(sno);
                if (name == true)
                {
                    return Json(name, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    if (sno > 0)
                    {
                        vatper.DeleteVatper(sno);
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
        [HttpPost]
        public ActionResult CheckCount(
           long sno
           )
        {

            try
            {


                var result = vatper.ValidateCount(sno);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }


    }
}