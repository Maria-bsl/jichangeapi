using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class DesginationController : Controller
    {
        DESIGNATION des = new DESIGNATION();
        private readonly dynamic returnNull = null;
        // GET: Desgination
        EMP_DET ed = new EMP_DET();
        String[] list = new String[4] { "desg_id", "desg_name", "posted_by", "posted_date" };
        public ActionResult Designation()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }
        
        public ActionResult GetdesDetails()
        {
            try
            {
                var result = des.GetDesignation();
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

        [HttpPost]
        public ActionResult Adddesg(string desg,long sno,bool dummy)
        {
            try
            {
                des.Desg_Name = desg;
                des.Desg_Id = sno;
                des.AuditBy= Session["UserID"].ToString();
                long ssno = 0;
                if (sno == 0)
                {
                    var result = des.ValidateDesignation(desg.ToLower());
                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                        ssno = des.AddUser(des);
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
                        des.UpdateDesignation(des);
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
        public ActionResult Deletedesg(long sno)
        {
            try
            {
                var check = des.ValidateDeletion(sno);
                if(check==true)
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                else if(sno > 0)
                {
                    des.DeleteDesignation(sno);
                }
                var result = sno;

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return returnNull;
        }
    }
}