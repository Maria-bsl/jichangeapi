using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using BL.BIZINVOICING.BusinessEntities.Masters;
using BL.BIZINVOICING.BusinessEntities.ConstantFile;

namespace BIZINVOICING.Controllers
{
    public class CompanyController : AdminBaseController
    {
        CompanyBankMaster c = new CompanyBankMaster();
        REGION r = new REGION();
        CompanyUsers cu = new CompanyUsers();
        DISTRICTS d = new DISTRICTS();
        WARD w = new WARD();
        Auditlog ad = new Auditlog();
        private readonly dynamic returnNull = null;
        BranchM bm = new BranchM();
        EMAIL em = new EMAIL();
        S_SMTP ss = new S_SMTP();
        langcompany lc = new langcompany();
        String[] list = new String[16] { "comp_mas_sno", "company_name", "pobox_no", "physical_address", "region_id", "district_sno", "ward_sno",
            "tin_no","vat_no","director_name","email_address","telephone_no","fax_no","mobile_no", "posted_by", "posted_date" };
        String  drt;
        String pwd;
        // GET: Company
        public ActionResult Company()
        {
                if (Session["sessB"] == null)
                {
                    return RedirectToAction("Loginnew", "Loginnew");
                }
            string stat = string.Empty;
            try
            { 
            if (!string.IsNullOrEmpty(Request.QueryString["stat"]))
                stat = Request.QueryString["stat"].ToString();
            else
                stat = string.Empty;
            }
            catch
            {
                stat = string.Empty;
            }
            if(stat == "app")
            {
                stat = "Approved";
            }
            else if (stat == "pen")
            {
                stat = "Pending";
            }
            else
            {
                stat = string.Empty;
            }
            ViewData["STT"] = stat;
            return View();
           
        }
        public ActionResult CompanyR()
        {
            /*if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }*/
            return View();

        }
        [HttpPost]
        public ActionResult GetCompanys(string stat)
        {
            try
            {
                /*var result = c.GetCompany();
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var d = 0;
                    return Json(d, JsonRequestBehavior.AllowGet);
                }*/
                /*string stat = string.Empty;
                try
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["stat"]))
                        stat = Request.QueryString["stat"].ToString();
                    else
                        stat = string.Empty;
                }
                catch
                {
                    stat = string.Empty;
                }*/
                if (stat == "app")
                {
                    stat = "Approved";
                }
                else if (stat == "pen")
                {
                    stat = "Pending";
                }
                else
                {
                    stat = string.Empty;
                }
                string company = stat;
                var result = c.GetCompany1();
                if (Session["desig"].ToString() == "Administrator" && company == "")
                {
                    result = c.GetCompany1_A();
                }
                else if (Session["desig"].ToString() == "Administrator" && company != "")
                {
                    result = c.GetCompany1_A_Q(company);
                }
                else
                {
                    if (company == "")
                    {
                        result = c.GetCompany1_Branch_A(long.Parse(Session["BRAID"].ToString()));
                    }
                    else
                    {
                        result = c.GetCompany1_Branch_A_Q(long.Parse(Session["BRAID"].ToString()),company);
                    }
                }
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
        public ActionResult GetApp()
        {
            try
            {
                
                
                var result = c.GetCompany1();
                if (Session["desig"].ToString() == "Administrator")
                {
                    result = c.GetCompany1_D();
                }
                else
                {
                    
                        result = c.GetCompany1_Branch_D(long.Parse(Session["BRAID"].ToString()));
                    
                    
                }
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
        public ActionResult GetCompanys_S()
        {
            try
            {
                var result = c.GetCompany_S(/*sno*/);
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
        public ActionResult GetCompanys_A()
        {
            try
            {
                var result = c.GetCompany_A(/*sno*/);
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
        public ActionResult GetCompanys_SU(long cno)
        {
            try
            {
                var result = c.GetCompany_Suspense(cno);
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
        public ActionResult GetCompanys_SUS()
        {
            try
            {
                var result = c.GetCompany_Sus();
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
        public ActionResult GetAccount(long cno)
        {
            try
            {
                var result = c.GetBank_S(cno);
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
        private static string CreateRandomPassword(int length)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        public static string GetEncryptedData(string value)
        {
            byte[] encData_byte = new byte[value.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(value);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

        [HttpPost]
        public ActionResult AddCompanyBank(long compsno,string compname, string pbox, string addr,
            long rsno,long dsno, long wsno, string tin, string vat, string dname, string email, string telno,
            string fax, string mob, /*byte[] clogo, byte[] sig,*/ bool dummy, int lastrow, List<CompanyBankMaster> details, long branch, string check_status)
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
                c.Branch_Sno = branch;
                c.Checker = check_status;
                //c.CompLogo = clogo;
                //c.DirectorSig = sig;
                //c.Postedby = string.Empty;//Session["UserID"].ToString();
                c.Status = "Pending";
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
                    
                    /*if (cu.ValidateduplicateEmail1(email))
                    {
                        return Json("Exist", JsonRequestBehavior.AllowGet);
                    }
                    else*/ if (cu.ValidateMobile(mob))
                    {
                        return Json("MExist", JsonRequestBehavior.AllowGet);
                    }
                    //else if (cu.Validateduplicateuser1(email.Split('@')[0]))
                    /*else if (cu.Validateduplicateuser1(mob))
                    {
                        return Json("UExist", JsonRequestBehavior.AllowGet);
                    }*/
                    else
                    {
                        ssno = c.AddCompany(c);
                        var glang = lc.GetlocalengI();
                        foreach (langcompany li in glang)
                        {
                            lc.Loc_Eng = li.Loc_Eng;
                            lc.Loc_Eng1 = li.Loc_Eng1;
                            lc.Table_name = li.Table_name;
                            lc.Col_name = li.Col_name;
                            lc.Loc_Oth1 = li.Dyn_Swa;
                            lc.comp_no = ssno;
                            lc.AddLang(lc);
                        }
                        cu.Compmassno = ssno;
                        cu.Email = email;
                        cu.Usertype = "001";
                        cu.Mobile = mob;
                        cu.Flogin = "false";
                        //cu.Fullname = email.Split('@')[0];
                        cu.Fullname = mob;
                        //cu.Username = email.Split('@')[0];
                        cu.Username = mob;
                        pwd = CreateRandomPassword(8);
                        cu.Password = GetEncryptedData(pwd);
                        cu.CreatedDate = DateTime.Now;
                        cu.PostedDate = DateTime.Now;
                        cu.ExpiryDate = System.DateTime.Now.AddMonths(3);
                        long adcompsno = 0;
                        adcompsno = cu.AddCompanyUsers1(cu);
                        if (ssno > 0)
                        {

                            //String[] list1 = new String[16] { ssno.ToString(), compname, pbox, addr, rsno.ToString(), dsno.ToString(), wsno.ToString(), tin, vat, dname, email, telno, fax, mob, Session["UserID"].ToString(), DateTime.Now.ToString() };
                            String[] list1 = new String[16] { ssno.ToString(), compname, pbox, addr, rsno.ToString(), dsno.ToString(), wsno.ToString(), tin, vat, dname, email, telno, fax, mob, "1", DateTime.Now.ToString() };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Company";
                                ad.Newvalues = list1[i];
                                //ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }
                        
                        if (ssno > 0)
                        {

                            SendActivationEmail(email, email.Split('@')[0], pwd, email.Split('@')[0]);
                        }
                    }
                        if (ssno > 0)
                        {
                            for (int i = 0; i < details.Count(); i++)
                            {
                            if (details[i].AccountNo != null)
                            {
                                c.CompSno = ssno;
                                //c.BankSno = details[i].BankSno;
                                //c.BankName = details[i].BankName;
                                //c.BankBranch = details[i].BankBranch;
                                c.AccountNo = details[i].AccountNo;
                                //c.Swiftcode = details[i].Swiftcode;

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
                    var getcom = c.EditCompany(compsno);
                    bool flag = true;
                    bool cmp = true;
                    if(getcom.MobNo == mob)
                    {
                        flag = false;
                    }
                    if (getcom.CompName == compname)
                    {
                        cmp = false;
                    }
                    if (cu.ValidateMobile(mob) && flag == true)
                    {
                        return Json("MExist", JsonRequestBehavior.AllowGet);
                    }
                    else if (c.ValidateCount(compname.ToLower(), tin) && cmp == true)
                    {
                        return Json("CExist", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //getcom = c.EditCompany(compsno);
                        //var dd = des.Editdesignation(sno);
                        if (getcom != null)
                        {
                            String[] list1 = new String[16] { ssno.ToString(), compname, pbox, addr, rsno.ToString(), dsno.ToString(), wsno.ToString(), tin, vat, dname, email, telno, fax, mob, Session["UserID"].ToString(), DateTime.Now.ToString() };
                            String[] list2 = new String[16] { getcom.CompSno.ToString(), getcom.CompName,getcom.PostBox, getcom.Address, getcom.RegId.ToString(),getcom.DistSno.ToString(),getcom.WardSno.ToString(),getcom.TinNo,getcom.VatNo,getcom.DirectorName,
                               getcom.Email,getcom.TelNo,getcom.FaxNo,getcom.MobNo, string.Empty, getcom.Posteddate.ToString() };//Session["UserID"].ToString()
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Update";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Company";
                                ad.Oldvalues = list2[i];
                                ad.Newvalues = list1[i];
                                //ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }
                        
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
                                if (details[i].AccountNo != null )
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

        [HttpPost]
        public ActionResult AddCompanyBankL(long compsno, string compname, string pbox, string addr,
           long rsno, long dsno, long wsno, string tin, string vat, string dname, string email, string telno,
           string fax, string mob,  bool dummy, string accno,  long branch, string check_status)
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
                c.Branch_Sno = branch;
                c.Checker = check_status;
                //c.CompLogo = clogo;
                //c.DirectorSig = sig;
                //c.Postedby = string.Empty;//Session["UserID"].ToString();
                c.Status = "Pending";
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

                    /*if (cu.ValidateduplicateEmail1(email))
                    {
                        return Json("Exist", JsonRequestBehavior.AllowGet);
                    }
                    else*/
                    if (cu.ValidateMobile(mob))
                    {
                        return Json("MExist", JsonRequestBehavior.AllowGet);
                    }
                    //else if (cu.Validateduplicateuser1(email.Split('@')[0]))
                    /*else if (cu.Validateduplicateuser1(mob))
                    {
                        return Json("UExist", JsonRequestBehavior.AllowGet);
                    }*/
                    else
                    {
                        ssno = c.AddCompany(c);
                        var glang = lc.GetlocalengI();
                        foreach (langcompany li in glang)
                        {
                            lc.Loc_Eng = li.Loc_Eng;
                            lc.Loc_Eng1 = li.Loc_Eng1;
                            lc.Table_name = li.Table_name;
                            lc.Col_name = li.Col_name;
                            lc.Loc_Oth1 = li.Dyn_Swa;
                            lc.comp_no = ssno;
                            lc.AddLang(lc);
                        }
                        cu.Compmassno = ssno;
                        cu.Email = email;
                        cu.Usertype = "001";
                        cu.Mobile = mob;
                        cu.Flogin = "false";
                        //cu.Fullname = email.Split('@')[0];
                        cu.Fullname = mob;
                        //cu.Username = email.Split('@')[0];
                        cu.Username = mob;
                        pwd = CreateRandomPassword(8);
                        cu.Password = GetEncryptedData(pwd);
                        cu.CreatedDate = DateTime.Now;
                        cu.PostedDate = DateTime.Now;
                        cu.ExpiryDate = System.DateTime.Now.AddMonths(3);
                        long adcompsno = 0;
                        adcompsno = cu.AddCompanyUsers1(cu);
                        if (ssno > 0)
                        {

                            //String[] list1 = new String[16] { ssno.ToString(), compname, pbox, addr, rsno.ToString(), dsno.ToString(), wsno.ToString(), tin, vat, dname, email, telno, fax, mob, Session["UserID"].ToString(), DateTime.Now.ToString() };
                            String[] list1 = new String[16] { ssno.ToString(), compname, pbox, addr, rsno.ToString(), dsno.ToString(), wsno.ToString(), tin, vat, dname, email, telno, fax, mob, "1", DateTime.Now.ToString() };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Company";
                                ad.Newvalues = list1[i];
                                //ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }

                        if (ssno > 0)
                        {

                            SendActivationEmail(email, email.Split('@')[0], pwd, email.Split('@')[0]);
                        }
                    }
                    if (ssno > 0)
                    {
                        
                                c.CompSno = ssno;
                                //c.BankSno = details[i].BankSno;
                                //c.BankName = details[i].BankName;
                                //c.BankBranch = details[i].BankBranch;
                                c.AccountNo = accno;
                                //c.Swiftcode = details[i].Swiftcode;

                                long detsno = c.AddBank(c);
                               
                               
                       
                        return Json(ssno, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (compsno > 0)
                {
                    var getcom = c.EditCompany(compsno);
                    bool flag = true;
                    bool cmp = true;
                    if (getcom.MobNo == mob)
                    {
                        flag = false;
                    }
                    if (getcom.CompName == compname)
                    {
                        cmp = false;
                    }
                    if (cu.ValidateMobile(mob) && flag == true)
                    {
                        return Json("MExist", JsonRequestBehavior.AllowGet);
                    }
                    else if (c.ValidateCount(compname.ToLower(), tin) && cmp == true)
                    {
                        return Json("CExist", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        //getcom = c.EditCompany(compsno);
                        //var dd = des.Editdesignation(sno);
                        if (getcom != null)
                        {
                            String[] list1 = new String[16] { ssno.ToString(), compname, pbox, addr, rsno.ToString(), dsno.ToString(), wsno.ToString(), tin, vat, dname, email, telno, fax, mob, Session["UserID"].ToString(), DateTime.Now.ToString() };
                            String[] list2 = new String[16] { getcom.CompSno.ToString(), getcom.CompName,getcom.PostBox, getcom.Address, getcom.RegId.ToString(),getcom.DistSno.ToString(),getcom.WardSno.ToString(),getcom.TinNo,getcom.VatNo,getcom.DirectorName,
                               getcom.Email,getcom.TelNo,getcom.FaxNo,getcom.MobNo, string.Empty, getcom.Posteddate.ToString() };//Session["UserID"].ToString()
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Update";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Company";
                                ad.Oldvalues = list2[i];
                                ad.Newvalues = list1[i];
                                //ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }

                        c.UpdateCompany(c);
                        ssno = compsno;
                        if (ssno > 0)
                        {
                            c.CompSno = ssno;
                            c.DeleteBank(c);
                            
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
        private void SendActivationEmail(String email, String auname, String pwd, String uname)
        {
            try
            {
                Guid activationCode = Guid.NewGuid();
                SmtpClient smtp = new SmtpClient();

                using (MailMessage mm = new MailMessage())
                {
                    var m = ss.getSMTPText();
                    var data = em.GetLatestEmailTextsListByFlow("1");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Local_subject;
                    drt = data.Local_subject;
                    var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Loginnew", "Loginnew"),
                       Query = null,
                   };

                    Uri uri = urlBuilder.Uri;
                    //string url = "web_url";
                    string weburl = System.Web.Configuration.WebConfigurationManager.AppSettings["web_url"].ToString();
                    string url = "<a href='" + weburl + "' target='_blank'>" + weburl + "</a>";
                    //location.href = '/Loginnew/Loginnew';
                    String body = data.Local_Text.Replace("}+cName+{", uname).Replace("}+uname+{", auname).Replace("}+pwd+{", pwd).Replace("}+actLink+{", url).Replace("{", "").Replace("}", "");
                    //m1(weburl);
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    if (string.IsNullOrEmpty(m.SMTP_UName))
                    {
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.Host = m.SMTP_Address;
                    }
                    else
                    {
                        smtp.Host = m.SMTP_Address;
                        smtp.Port = Convert.ToInt16(m.SMTP_Port);
                        smtp.EnableSsl = Convert.ToBoolean(m.SSL_Enable);
                        NetworkCredential NetworkCred = new NetworkCredential(m.SMTP_UName, Utilites.DecodeFrom64(m.SMTP_Password));
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                    }
                    smtp.Send(mm);
                }
            }
            catch (Exception Ex)
            {
                //long errorLogID = ApplicationError.ErrorHandling(Ex, Request.Url.ToString(), Request.Browser.Type);
                // Utilites.logfile("lcituion user", drt, Ex.ToString());
            }

        }
    }
}