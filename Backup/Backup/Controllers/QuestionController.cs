using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace COFFERING.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        REG_QSTN cty = new REG_QSTN();
        private readonly dynamic returnNull = null;
        String[] list = new String[5] {"sno", "q_name", "q_status", "posted_by", "posted_date" };
        public ActionResult Question()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
          
            return View();
        }
        public ActionResult AddQues(
             string q_name,
             string q_qtatus,
             string auditby,
             long sno,
             bool dummy
             )
        {

            try
            {

                cty.Q_Name = q_name;
                cty.Q_Status = q_qtatus;
                cty.AuditBy= Session["UserID"].ToString();
                cty.SNO = sno;
                
                    long ssno = 0;
                if (sno == 0)
                {
                    var result = cty.ValidateQSTN(q_name.ToLower());
                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

                         ssno = cty.AddREG_QSTN(cty);
                       
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
                        var dd = cty.EditREG_QSTN(sno);
                      
                        cty.UpdateREG_QSTN(cty);
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
        public ActionResult DeleteQues(
           long sno, string qname
           )
        {

            try
            {

                cty.SNO = sno;
                var name = cty.CheckQues(sno);
                if (name == true)
                {
                    return Json(name, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (sno > 0)
                    {
                        var dd = cty.EditREG_QSTN(sno);
                       
                        cty.DeleteREG_QSTN(sno);
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
      
        public ActionResult GetQues()
        {

            try
            {
                var result = cty.GetQSTN();
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