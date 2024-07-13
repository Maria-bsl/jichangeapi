using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class DistrictController : Controller
    {
        S_SMTP smtp = new S_SMTP();
        DISTRICTS dst = new DISTRICTS();
        REGION reg = new REGION();
        private readonly dynamic returnNull = null;
        EMP_DET ed = new EMP_DET();
      //  String[] list = new String[6] { "district_sno", "district_name", "region_id", "district_status","posted_by", "posted_date" };
        public ActionResult District()
        {
            if (Session["sessB"] == null)
            {
              return  RedirectToAction("Loginnew", "Loginnew");
            }
          
            return View();
        }
       
        // GET: SMTP/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDistrict(string district_name,long region_id,string district_status,string posted_by,long sno,bool dummy)
        {

            try
            {

                dst.District_Name = district_name;
                dst.Region_Id = region_id;
                dst.District_Status = district_status;
                dst.AuditBy = Session["UserID"].ToString();
                dst.SNO = sno;


                long ssno = 0;
                if (sno == 0)
                {
                    var chk = dst.Validateduplicatechecking(district_name.ToLower());
                    if (chk == false)
                    {
                        var result = dst.Validatedistrict(district_name.ToLower(), region_id);
                        if (result == false)
                        {
                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            ssno = dst.AddDistrict(dst);
                            return Json(ssno, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(chk, JsonRequestBehavior.AllowGet);
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
                        dst.UpdateDISTRICTS(dst);
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
        public ActionResult DeleteDist(
           long sno
           )
        {

            try
            {

                dst.SNO = sno;
                var name = dst.Checkdistrict(sno);
                var dd = dst.EditDISTRICTS(sno);
                if (name == true)
                {
                    var retur = new { name, dname = dd.District_Name };
                    return Json(retur, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (sno > 0)
                    {
                      
                        dst.DeleteDISTRICTS(sno);
                        var result = sno;
                        return Json(dd.District_Name, JsonRequestBehavior.AllowGet);
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
        public ActionResult CheckDist(
           long sno
           )
        {

            try
            {


                var result = dst.Checkdistrict(sno);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }
        [HttpPost]
        public ActionResult GetDist()
        {
            
            try
            {
                var result = dst.GetDistrict();
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
        public ActionResult GetRegi()
        {
            
            try
            {
                var result = reg.GetREG();

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
