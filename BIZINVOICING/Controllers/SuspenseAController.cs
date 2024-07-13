using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;

namespace BIZINVOICING.Controllers
{

    public class SuspenseAController : AdminBaseController
    {
        S_Account sa = new S_Account();
        private readonly dynamic returnNull = null;
        // GET: SuspenseA
        public ActionResult SuspenseA()
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
        public ActionResult GetAccount()
        {
            try
            {
                var result = sa.GetAccounts();
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
        public ActionResult GetAccount_Active()
        {
            try
            {
                var result = sa.GetAccounts_Active();
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
        public ActionResult AddAccount(string account,  string status, string sno, bool dummy)
        {

            try
            {

                sa.Sus_Acc_No = account;
                sa.Status = status;
                sa.AuditBy = Session["UserID"].ToString();
                string ssno = "0";
                if (sno == "0")
                {
                    var result = sa.ValidateAccount(account);
                    if (result)
                    {

                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        sa.AddAccount(sa);
                        /*var dd = bm.EditBranch(code);
                        if (dd != null)
                        {
                            String[] list1 = new String[4] { dd.Currency_Code.ToString(), dd.Currency_Name, Session["UserID"].ToString(), DateTime.Now.ToString() };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Currency";
                                ad.Newvalues = list1[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }*/
                        return Json(ssno, JsonRequestBehavior.AllowGet);
                    }
                }
                /*else if (sno != "0")
                {
                    long bbno = long.Parse(sno);
                    var getD = bm.EditBranch(bbno);
                    string bname = getD.Name;
                    string bloc = getD.Location;
                    bool bflag = true;
                    if (bname == branch)//&& bloc == location
                    {
                        bflag = false;
                    }
                    
                    var result = sa.ValidateAccount(branch);
                    if (bm.ValidateBranch(branch) && bflag == true)
                    {

                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        
                        bm.Sno = long.Parse(sno);
                        bm.UpdateBranch(bm);
                        ssno = sno;
                        return Json(ssno, JsonRequestBehavior.AllowGet);

                    }

                    
                }*/


            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }
    }
}