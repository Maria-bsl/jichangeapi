using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;

namespace BIZINVOICING.Controllers
{
    public class DepositAController : AdminBaseController
    {
        C_Deposit cd = new C_Deposit();
        CompanyBankMaster cbm = new CompanyBankMaster();
        // GET: DepositA
        private readonly dynamic returnNull = null;
        
        public ActionResult DepositA()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();

        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetDeposits()
        {
            try
            {
                var result = cd.GetAccounts();
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
        
        [HttpPost]
        public ActionResult AddAccount(long csno, string account, string reason, string sno, bool dummy)
        {

            try
            {

                cd.Deposit_Acc_No = account;
                cd.Comp_Mas_Sno = csno;
                cd.Reason = reason;
                cd.AuditBy = Session["UserID"].ToString();
                string ssno = "0";
                if (sno == "0")
                {
                    /*var result = sa.ValidateAccount(account);
                    if (result)
                    {

                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {*/
                     long asno = cd.AddAccount(cd);
                    /*cbm.CompSno = csno;
                    cbm.Sus_Ac_SNo = asno;
                    cbm.UpdateAccount(cbm);*/
                        return Json(ssno, JsonRequestBehavior.AllowGet);
                    //}
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