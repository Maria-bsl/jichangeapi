using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;

namespace BIZINVOICING.Controllers
{
    public class BranchController : AdminBaseController
    {
        BranchM bm = new BranchM();
        EMP_DET ed = new EMP_DET();
        private readonly dynamic returnNull = null;
        // GET: Branch
        public ActionResult Branch()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }
        [HttpPost]
        public ActionResult GetBranchDetails()
        {
            try
            {
                var result = bm.GetBranches();
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
        public ActionResult GetBranchDetails1()
        {
            try
            {
                var result = bm.GetBranches();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }

        [HttpPost]
        public ActionResult AddBranch(string branch, string location, string status,  string sno, bool dummy)
        {

            try
            {

                bm.Name = branch;
                bm.Location = location;
                bm.Status = status;
                bm.AuditBy = Session["UserID"].ToString();
                string ssno = "0";
                if (sno == "0")
                {
                    var result = bm.ValidateBranch(branch);
                    if (result)
                    {

                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        bm.AddBranch(bm);
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
                else if (sno != "0")
                {
                    long bbno = long.Parse(sno);
                    var getD = bm.EditBranch(bbno);
                    string bname = getD.Name;
                    string bloc = getD.Location;
                    bool bflag = true;
                    if(bname == branch)//&& bloc == location
                    {
                        bflag = false;
                    }
                    /*if (dummy == false)
                    {
                        return Json(dummy, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {*/
                        var result = bm.ValidateBranch(branch);
                        if (bm.ValidateBranch(branch) && bflag == true)
                        {

                            return Json(result, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            /*var dd = cy.EditCURRENCY(code);
                            if (dd != null)
                            {
                                String[] list2 = new String[4] { dd.Currency_Code.ToString(), dd.Currency_Name, Session["UserID"].ToString(), DateTime.Now.ToString() };
                                String[] list1 = new String[4] { code, cname, Session["UserID"].ToString(), DateTime.Now.ToString() };
                                for (int i = 0; i < list.Count(); i++)
                                {
                                    ad.Audit_Type = "Update";
                                    ad.Columnsname = list[i];
                                    ad.Table_Name = "Currency";
                                    ad.Oldvalues = list2[i];
                                    ad.Newvalues = list1[i];
                                    ad.AuditBy = Session["UserID"].ToString();
                                    ad.Audit_Date = DateTime.Now;
                                    ad.Audit_Time = DateTime.Now;
                                    ad.AddAudit(ad);
                                }
                            }*/
                            bm.Sno = long.Parse(sno);
                            bm.UpdateBranch(bm);
                            ssno = sno;
                            return Json(ssno, JsonRequestBehavior.AllowGet);

                        }

                    //}
                }


            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return returnNull;
        }
        public ActionResult DeleteBranch(String sno)
        {
            try
            {
                long bsno = long.Parse(sno);
                var check = bm.ValidateDelete(bsno);
                if (check == false)
                {
                    /*var dd = cy.EditCURRENCY(sno);
                    if (dd != null)
                    {
                        String[] list2 = new String[4] { dd.Currency_Code.ToString(), dd.Currency_Name, Session["UserID"].ToString(), DateTime.Now.ToString() };
                        for (int i = 0; i < list.Count(); i++)
                        {
                            ad.Audit_Type = "Delete";
                            ad.Columnsname = list[i];
                            ad.Table_Name = "Currency";
                            ad.Oldvalues = list2[i];
                            ad.AuditBy = Session["UserID"].ToString();
                            ad.Audit_Date = DateTime.Now;
                            ad.Audit_Time = DateTime.Now;
                            ad.AddAudit(ad);
                        }
                    }*/
                    bm.DeleteBranch(bsno);
                    var result = sno;
                    return Json(false, JsonRequestBehavior.AllowGet);
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