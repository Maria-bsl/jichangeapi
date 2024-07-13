using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
using System.Text;

namespace BIZINVOICING.Controllers
{
    public class VatperController : AdminBaseController
    {
        // GET: Vatper
        VatPercentage vatper = new VatPercentage();
        private readonly dynamic returnNull = null;
        Auditlog ad = new Auditlog();
        String[] list = new String[6] { "vat_per_sno", "vat_percentage",  "posted_by", "posted_date","vat_category", "vat_description" };
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
                        if (ssno > 0)
                        {
                            //String[] list = new String[6] { "vat_per_sno", "vat_percentage", "posted_by", "posted_date", "vat_category", "vat_description" };
                            String[] list1 = new String[6] { ssno.ToString(), vat_percentageValue.ToString(), Session["UserID"].ToString(), DateTime.Now.ToString(), vat_cat, vat_desc };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Vat Percentage";
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
                        
                        var dd = vatper.Editcountries(sno);
                        if (dd != null)
                        {
                            String[] list1 = new String[6] { ssno.ToString(), vat_percentageValue.ToString(), Session["UserID"].ToString(), DateTime.Now.ToString(), vat_cat, vat_desc };
                            String[] list2 = new String[6] { dd.vat_per_sno.ToString(), dd.vat_percentageValue.ToString(), Session["UserID"].ToString(), DateTime.Now.ToString(), dd.Vat_Category,dd.Vat_Description };
                           // String[] list1 = new String[5] { dd.SNO.ToString(), q_name, q_qtatus, Session["UserID"].ToString(), DateTime.Now.ToString() };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Update";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Vat Percentage";
                                ad.Oldvalues = list2[i];
                                ad.Newvalues = list1[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }
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
                        var dd = vatper.Editcountries(sno);
                        if (dd != null)
                        {
                            String[] list2 = new String[6] { dd.vat_per_sno.ToString(), dd.vat_percentageValue.ToString(), Session["UserID"].ToString(), DateTime.Now.ToString(), dd.Vat_Category, dd.Vat_Description };

                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Delete";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Vat Percentage";
                                ad.Oldvalues = list2[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }
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