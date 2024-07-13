using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
using System.Text;
namespace BIZINVOICING.Controllers
{
    public class CountryController : AdminBaseController
    {
        // GET: Country
        COUNTRY cty = new COUNTRY();
        EMP_DET ed = new EMP_DET();
        private readonly dynamic returnNull = null;
        Auditlog ad = new Auditlog();
        String[] list = new String[2] { "country_sno", "country_name" };

        public ActionResult Country()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }
        public ActionResult AddCount(string country_name,long sno,bool dummy)
        {
            try
            {
                cty.Country_Name =country_name;
                cty.SNO = sno;
                 long ssno = 0;
                if (sno == 0)
                {
                    var result = cty.ValidateLicense(country_name.ToLower());
                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ssno = cty.Addcountries(cty);
                        if (ssno > 0)
                        {
                            String[] list1 = new String[2] { ssno.ToString(), cty.Country_Name };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Country";
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
                        var dd = cty.Editcountries(sno);
                        if (dd != null)
                        {
                            String[] list2 = new String[2] { dd.SNO.ToString(), dd.Country_Name };
                            String[] list1 = new String[2] { sno.ToString(), cty.Country_Name };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Update";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Country";
                                ad.Oldvalues = list2[i];
                                ad.Newvalues = list1[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }
                        cty.Updatecountries(cty);
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
        public ActionResult DeleteCount(long sno)
        {

            try
            {

                cty.SNO = sno;
                var name = cty.ValidateCount(sno);
                if (name == true)
                {
                    return Json(name, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    if (sno > 0)
                    {
                        var dd = cty.Editcountries(sno);
                        if (dd != null)
                        {
                            String[] list2 = new String[2] { dd.SNO.ToString(), dd.Country_Name };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Delete";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Country";
                                ad.Oldvalues = list2[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;

                                ad.AddAudit(ad);
                            }
                        }
                        cty.Deletecountries(sno);
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


                var result = cty.ValidateCount(sno);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }
        public ActionResult GetCounts()
        {

            try
            {
                var result = cty.GETcountries();
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
