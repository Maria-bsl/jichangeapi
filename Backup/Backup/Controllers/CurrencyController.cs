using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class CurrencyController : Controller
    {
        // GET: Currency
        CURRENCY cy = new CURRENCY();
     
        String[] list = new String[4] { "currency_code", "currency_name", "posted_by", "posted_date" };
        private readonly dynamic returnNull = null;
        EMP_DET ed = new EMP_DET();
        public ActionResult Currency()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }
        [HttpPost]
        public ActionResult GetCurrencyDetails()
        {
            try
            {
                var result = cy.GetCURRENCY();
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
        public ActionResult GetCurrencyDetails1()
        {
            try
            {
                var result = cy.GetCURRENCY();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }

        [HttpPost]
        public ActionResult AddCurrency(string code, String cname, string sno,bool dummy)
        {

            try
            {

                cy.Currency_Code = code.ToUpper();
                cy.Currency_Name = cname;
                cy.AuditBy= Session["UserID"].ToString();
                string ssno = "0";
                if (sno == "0")
                {
                    var result = cy.ValidateCURRENCY(cname.ToLower(),code.ToLower());
                    if (result!=null)
                    {
                        
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        cy.AddCURRENCY(cy);
                        return Json(ssno, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (sno != "0")
                {
                    if (dummy == false)
                    {
                        return Json(dummy, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        var result = cy.ValidateCname(cname.ToLower());
                        if (result != null)
                        {

                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            cy.UpdateCURRENCY(cy);
                           ssno = sno;
                           return Json(ssno, JsonRequestBehavior.AllowGet);
                            
                        }

                    }
                }
                 
               
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return returnNull;
        }
        public ActionResult Deletecurrency(String sno)
        {
            try
            {
                var check = cy.ValidateDelete(sno);
                if (check == false)
                {
                    cy.DeleteCURRENCY(sno);
                    var result = sno;
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return returnNull;
        }

    }
}