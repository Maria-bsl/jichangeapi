using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class CompanyController : Controller
    {
        CompanyBankMaster c = new CompanyBankMaster();
        REGION r = new REGION();
        DISTRICTS d = new DISTRICTS();
        WARD w = new WARD();
        private readonly dynamic returnNull = null;
        // GET: Company
        public ActionResult Company()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
           
        }
        [HttpPost]
        public ActionResult GetCompanys()
        {
            try
            {
                var result = c.GetCompany(/*sno*/);
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
        public ActionResult GetBanks(long sno)
        {
            try
            {
                var result = c.GetBank(sno);
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
        public ActionResult CheckAccount(string acc)
        {
            try
            {
                var check = c.Validateaccount(acc);
                if (check == true)
                {
                    return Json(check, JsonRequestBehavior.AllowGet);
                }
                else { return Json(check, JsonRequestBehavior.AllowGet); }
               
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }

        [HttpPost]
        public ActionResult GetDetailsindivi(long sno)
        {
            try
            {
                var result = c.EditCompanyss(sno);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public ActionResult DeleteCompanyBank(long sno)
        {
            try
            {
                if (sno > 0)
                {
                    var getcom = c.EditCompanys(/*sno*/);
                    //String[] list2 = new String[11] { getcom.Grade_Sal_Sno.ToString(), getcom.Sheet_Date.ToString(), getcom.Grade_Sno.ToString(), getcom.Grade_Desc, getcom.Status, getcom.Facility_reg_Sno.ToString(), getcom.Facility_Name, getcom.Posted_by, getcom.Posted_Date.ToString(), getcom.Checked_By, getcom.Checked_Date.ToString() };
                    //for (int i = 0; i < list.Count(); i++)
                    //{
                    //    ad.Audit_Type = "Delete";
                    //    ad.Columnsname = list[i];
                    //    ad.Table_Name = "grades_sal_master";
                    //    ad.Oldvalues = list2[i];
                    //    ad.AuditBy = Session["UserID"].ToString();
                    //    ad.Audit_Date = DateTime.Now;
                    //    ad.Audit_Time = DateTime.Now;
                    //    ad.Facility_Sno = long.Parse(Session["Facili_Reg_No"].ToString());
                    //    ad.AddAudit(ad);
                    //}
                    var dsno = c.EditBank(sno);
                    //foreach (Standard_Grade_Details vc in dsno)
                    //{
                    //    String[] det1 = new String[7] { vc.Grade_Sal_Det_Sno.ToString(), sno.ToString(), vc.Term_Sno.ToString(), vc.Term_Name, vc.Term_Type, vc.Currency_Code.ToString(), vc.Term_Amount.ToString() };
                    //    for (int p = 0; p < detlist.Count(); p++)
                    //    {
                    //        ad.Audit_Type = "Delete";
                    //        ad.Columnsname = detlist[p];
                    //        ad.Table_Name = "grades_sal_details";
                    //        ad.Oldvalues = det1[p];
                    //        ad.AuditBy = Session["UserID"].ToString();
                    //        ad.Audit_Date = DateTime.Now;
                    //        ad.Audit_Time = DateTime.Now;
                    //        ad.Facility_Sno = long.Parse(Session["Facili_Reg_No"].ToString());
                    //        ad.AddAudit(ad);
                    //    }
                    //}
                    c.CompSno = sno;
                    c.DeleteBank(c);
                    c.DeleteCompany(sno);
                    return Json(sno, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpGet]
        public ActionResult GetRegionDetails()
        {
            try
            {
                var result = r.GetReg();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }//need to check get methods


        [HttpPost]
        public ActionResult GetDistDetails(long Sno)
        {
            try
            {
                string ash = null;
                if (Sno == Convert.ToInt64(ash))
                {
                    return Json(Sno, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = d.GetDistrictActive(Sno);
                    if (result == null)
                    {
                        int d = 0;
                        return Json(d, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
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
        public ActionResult GetWard(long sno)
        {
            try
            {
                var result = w.GetWARDAct(sno);
                if (result == null)
                {
                    int d = 0;
                    return Json(d, JsonRequestBehavior.AllowGet);
                }
                else
                {
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
        public ActionResult AddCompanyBank(long compsno,string compname, string pbox, string addr,
            long rsno,long dsno, long wsno, string tin, string vat, string dname, string email, string telno,
            string fax, string mob, /*byte[] clogo, byte[] sig,*/ bool dummy, int lastrow, List<CompanyBankMaster> details)
        {
            try
            {
                c.CompSno = compsno;
                c.CompName = compname;
                c.PostBox = pbox;
                c.Address = addr;
                c.RegId = rsno;
                c.DistSno = dsno;
                c.WardSno = wsno;
                c.TinNo = tin;
                c.VatNo = vat;
                c.DirectorName = dname;
                c.Email = email;
                c.TelNo = telno;
                c.FaxNo = fax;
                c.MobNo = mob;
                //c.CompLogo = clogo;
                //c.DirectorSig = sig;
                c.Postedby = Session["UserID"].ToString();
               
                long ssno = 0;
                if (compsno == 0)
                {
                    //var chk = sgd.Validateduplicatechecking(long.Parse(Session["Facili_Reg_No"].ToString()), desc, dt);
                    //if (chk == false)
                    //{
                    var result = c.ValidateCount(compname.ToLower(), tin);

                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    //else
                    //{
                        ssno = c.AddCompany(c);
                        if (ssno > 0)
                        {
                            for (int i = 0; i < details.Count(); i++)
                            {
                            if (details[i].BankName != null && details[i].BankBranch != null && details[i].AccountNo != null && details[i].Swiftcode != null)
                            {
                                c.CompSno = ssno;
                                c.BankSno = details[i].BankSno;
                                c.BankName = details[i].BankName;
                                c.BankBranch = details[i].BankBranch;
                                c.AccountNo = details[i].AccountNo;
                                c.Swiftcode = details[i].Swiftcode;

                                long detsno = c.AddBank(c);
                                var getcom = c.EditBank(compsno);
                                //String[] li = new String[7] { getcom.ToString(), ssno.ToString(), details[i].Term_Sno.ToString(), details[i].Term_Name, details[i].Term_Type, details[i].Currency_Code.ToString(), details[i].Term_Amount.ToString() };
                                //String[] det1 = new String[7] { detsno.ToString(), ssno.ToString(), details[i].Term_Sno.ToString(), details[i].Term_Name, details[i].Term_Type, details[i].Currency_Code.ToString(), details[i].Term_Amount.ToString() };
                                //for (int j = 0; j < detlist.Count(); j++)
                                //{
                                //    ad.Audit_Type = "Insert";
                                //    ad.Columnsname = detlist[i];
                                //    ad.Table_Name = "grades_sal_details";
                                //    ad.Newvalues = det1[i];
                                //    ad.Oldvalues = li[i];
                                //    ad.AuditBy = Session["UserID"].ToString();
                                //    ad.Audit_Date = DateTime.Now;
                                //    ad.Audit_Time = DateTime.Now;
                                //    ad.Facility_Sno = long.Parse(Session["Facili_Reg_No"].ToString());
                                //    ad.AddAudit(ad);
                                //}
                                //}
                           }
                        }
                        //}
                        //else
                        //{
                        //    return Json(chk, JsonRequestBehavior.AllowGet);
                        //}
                        //String[] list1 = new String[11] { ssno.ToString(), sgd.Sheet_Date.ToString(), sgd.Grade_Sno.ToString(), sgd.Grade_Desc, sgd.Status, sgd.Facility_reg_Sno.ToString(), sgd.Facility_Name, sgd.Posted_by, sgd.Posted_Date.ToString(), sgd.Checked_By, sgd.Checked_Date.ToString() };
                        //for (int i = 0; i < list.Count(); i++)
                        //{
                        //    ad.Audit_Type = "Insert";
                        //    ad.Columnsname = list[i];
                        //    ad.Table_Name = "grades_sal_master";
                        //    ad.Newvalues = list1[i];
                        //    ad.AuditBy = Session["UserID"].ToString();
                        //    ad.Audit_Date = DateTime.Now;
                        //    ad.Audit_Time = DateTime.Now;
                        //    ad.Facility_Sno = long.Parse(Session["Facili_Reg_No"].ToString());
                        //    ad.AddAudit(ad);
                        //}
                        return Json(ssno, JsonRequestBehavior.AllowGet);
                   }
                }
                else if (compsno > 0)
                {
                    //var chk = sgd.Validateduplicatechecking(long.Parse(Session["Facili_Reg_No"].ToString()), desc, dt);
                    //if (chk == false)
                    //{
                    //var result = sgd.CheckValidation(long.Parse(Session["Facili_Reg_No"].ToString()), desc, dt);
                    //if (result == true)
                    //{
                    //    return Json(result, JsonRequestBehavior.AllowGet);
                    //}
                    //else
                    //{
                    if (dummy == false)
                    {
                        return Json(dummy, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var getcom = c.EditCompany(compsno);
                        //String[] list2 = new String[11] { getcom.Grade_Sal_Sno.ToString(), getcom.Sheet_Date.ToString(), getcom.Grade_Sno.ToString(), getcom.Grade_Desc, getcom.Status, getcom.Facility_reg_Sno.ToString(), getcom.Facility_Name, getcom.Posted_by, getcom.Posted_Date.ToString(), getcom.Checked_By, getcom.Checked_Date.ToString() };
                        //String[] list1 = new String[11] { ssno.ToString(), sgd.Sheet_Date.ToString(), sgd.Grade_Sno.ToString(), sgd.Grade_Desc, sgd.Status, sgd.Facility_reg_Sno.ToString(), sgd.Facility_Name, sgd.Posted_by, sgd.Posted_Date.ToString(), sgd.Checked_By, sgd.Checked_Date.ToString() };
                        //for (int i = 0; i < list.Count(); i++)
                        //{
                        //    ad.Audit_Type = "Update";
                        //    ad.Columnsname = list[i];
                        //    ad.Table_Name = "grades_sal_master";
                        //    ad.Oldvalues = list2[i];
                        //    ad.Newvalues = list1[i];
                        //    ad.AuditBy = Session["UserID"].ToString();
                        //    ad.Audit_Date = DateTime.Now;
                        //    ad.Audit_Time = DateTime.Now;
                        //    ad.Facility_Sno = long.Parse(Session["Facili_Reg_No"].ToString());
                        //    ad.AddAudit(ad);
                        //}
                        c.UpdateCompany(c);
                        ssno = compsno;
                        if (ssno > 0)
                        {
                            c.CompSno = ssno;
                            c.DeleteBank(c);
                            for (int i = 0; i < details.Count(); i++)
                            {
                                //if (details[i].Term_Sno != 0 && details[i].Currency_Sno != 0)
                                //{
                                if (details[i].BankName != null && details[i].BankBranch != null && details[i].AccountNo != null && details[i].Swiftcode != null)
                                {

                                    c.CompSno = ssno;
                                    c.BankSno = details[i].BankSno;
                                    c.BankName = details[i].BankName;
                                    c.BankBranch = details[i].BankBranch;
                                    c.AccountNo = details[i].AccountNo;
                                    c.Swiftcode = details[i].Swiftcode;
                                     var getcom1 = c.EditBank(compsno);
                                     long detsno = c.AddBank(c);
                                  //String[] li = new String[7] { getcom1.ToString(), ssno.ToString(), details[i].Term_Sno.ToString(), details[i].Term_Name, details[i].Term_Type, details[i].Currency_Code.ToString(), details[i].Term_Amount.ToString() };
                                   //String[] det1 = new String[7] { detsno.ToString(), ssno.ToString(), details[i].Term_Sno.ToString(), details[i].Term_Name, details[i].Term_Type, details[i].Currency_Code.ToString(), details[i].Term_Amount.ToString() };
                                 //for (int j = 0; j < detlist.Count(); j++)
                                 //{
                                 //    ad.Audit_Type = "Update";
                                 //    ad.Columnsname = detlist[i];
                                 //    ad.Table_Name = "grades_sal_details";
                                 //    ad.Newvalues = det1[i];
                                 //    ad.Oldvalues = li[i];
                                 //    ad.AuditBy = Session["UserID"].ToString();
                                 //    ad.Audit_Date = DateTime.Now;
                                 //    ad.Audit_Time = DateTime.Now;
                                 //    ad.Facility_Sno = long.Parse(Session["Facili_Reg_No"].ToString());
                                 //    ad.AddAudit(ad);
                                 //}
                                }
                            }
                        }
                        return Json(ssno, JsonRequestBehavior.AllowGet);
                    }
                    //}
                    //}
                    //else
                    //{
                    //    return Json(chk, JsonRequestBehavior.AllowGet);
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