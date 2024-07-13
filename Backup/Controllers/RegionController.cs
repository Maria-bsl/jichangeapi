using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class RegionController : Controller
    {
        // GET: RegionMaster
        REGION r = new REGION();
        COUNTRY cty = new COUNTRY();
        private readonly dynamic returnNull = null;
        EMP_DET ed = new EMP_DET();
        //String[] list = new String[7] { "region_sno", "region_name","country_sno","country_name","region_status","posted_by","posted_date" };
        public object StatusCodes { get; private set; }

        public ActionResult Region()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
         //   ed.Detail_Id = Convert.ToInt64(Session["UserID"].ToString());
           // ed.UpdateOnlyflsgtrue(ed);
            return View();
        }
       
        [HttpPost]
        public ActionResult GetRegionDetails()
        {
            try
            {
                var result = r.GetREGION();
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
        [HttpGet]
        public ActionResult GetCountryDetails()
        {
            try
            {
                var items = cty.GETcountries();
                return Json(items, JsonRequestBehavior.AllowGet);
               
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return returnNull;
        }

        [HttpPost]
        public ActionResult AddRegion(string region, string country, long csno, string Status, string posted_by, long sno, bool dummy)
        {

            r.Region_Name = region;
            r.Country_Sno = csno;
            r.Country_Name = country;
            r.Region_Status = Status;
            r.AuditBy = Session["UserID"].ToString(); 
            r.Region_SNO = sno;

            long ssno = 0;
            if (sno == 0)
            {
                var dupicate = r.Validatedupicate(region.ToLower());
                if (dupicate == false)
                {
                    var result = r.ValidateREGION(region.ToLower(),csno);
                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ssno = r.AddREGION(r);
                        //if (ssno > 0)
                        //{
                        //    String[] list1 = new String[7] { ssno.ToString(), region, csno.ToString(), country, Status, Session["UserID"].ToString(), DateTime.Now.ToString() };
                        //    for (int i = 0; i < list.Count(); i++)
                        //    {
                        //        ad.Audit_Type = "Insert";
                        //        ad.Columnsname = list[i];
                        //        ad.Table_Name = "Region";
                        //        ad.Newvalues = list1[i];
                        //        ad.AuditBy = Session["UserID"].ToString();
                        //        ad.Audit_Date = DateTime.Now;
                        //        ad.Audit_Time = DateTime.Now;
                        //        ad.AddAudit(ad);
                        //    }
                        //}
                        return Json(ssno, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(dupicate, JsonRequestBehavior.AllowGet);
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
                    var dd = r.EditREGION(sno);
                    //if (dd != null)
                    //{
                    //    String[] list2 = new String[7] { dd.Region_SNO.ToString(), dd.Region_Name, dd.Country_Sno.ToString(), dd.Country_Name, dd.Region_Status, dd.AuditBy, dd.Audit_Date.ToString() };
                    //    String[] list1 = new String[7] { ssno.ToString(), region, csno.ToString(), country, Status, Session["UserID"].ToString(), DateTime.Now.ToString() };
                    //    for (int i = 0; i < list.Count(); i++)
                    //    {
                    //        ad.Audit_Type = "Update";
                    //        ad.Columnsname = list[i];
                    //        ad.Table_Name = "Region";
                    //        ad.Oldvalues = list2[i];
                    //        ad.Newvalues = list1[i];
                    //        ad.AuditBy = Session["UserID"].ToString();
                    //        ad.Audit_Date = DateTime.Now;
                    //        ad.Audit_Time = DateTime.Now;
                    //        ad.AddAudit(ad);
                    //    }
                    //}

                    r.UpdateREGION(r);
                     ssno = sno;
                     return Json(ssno, JsonRequestBehavior.AllowGet);
                   
                }
            }


            return returnNull;
        }



        public ActionResult DeleteRegion(long sno)
        {
            try
            {
                var check = r.ValidateDeletion(sno);
                if (check == true)
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    r.Region_SNO = sno;
                    //var dd = r.EditREGION(sno);
                    //if (sno > 0)
                    //{
                    //    String[] list2 = new String[7] { dd.Region_SNO.ToString(), dd.Region_Name, dd.Country_Sno.ToString(), dd.Country_Name, dd.Region_Status, dd.AuditBy, dd.Audit_Date.ToString() };
                    //    for (int i = 0; i < list.Count(); i++)
                    //    {
                    //        ad.Audit_Type = "Delete";
                    //        ad.Columnsname = list[i];
                    //        ad.Table_Name = "Region";
                    //        ad.Oldvalues = list2[i];
                    //        ad.AuditBy = Session["UserID"].ToString();
                    //        ad.Audit_Date = DateTime.Now;
                    //        ad.Audit_Time = DateTime.Now;
                    //        ad.AddAudit(ad);
                    //    }
                    //    r.DeleteREGION(sno);
                    //}
                    var result = sno;
                    r.DeleteREGION(sno);
                    return Json(result, JsonRequestBehavior.AllowGet);
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