﻿using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CompanyController : SetupBaseController
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
        String drt;
        String pwd;
        // GET: Company


        #region  Companys and S

        /*  [HttpPost]
          public HttpResponseMessage GetCompanys(string stat)
          {
              try
              {
                  *//*var result = c.GetCompany();
                  if (result != null)
                  {
                      return Request.CreateResponse(new { response = result, message = new List<string> { } });
                  }
                  else
                  {
                      var d = 0;
                      return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
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
        }*//*
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
                result = c.GetCompany1_Branch_A(long.Parse(branch.ToString()));
            }
            else
            {
                result = c.GetCompany1_Branch_A_Q(long.Parse(branch.ToString()), company);
            }
        }
        if (result != null)
        {
            return Request.CreateResponse(new { response = result, message = new List<string> { } });
        }
        else
        {
            var d = 0;
            return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
        }
    }
    catch (Exception Ex)
    {
        Ex.ToString();
    }
    return returnNull;
}
[HttpPost]
public HttpResponseMessage GetApp()
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

            result = c.GetCompany1_Branch_D(long.Parse(branch.ToString()));


        }
        if (result != null)
        {
            return Request.CreateResponse(new { response = result, message = new List<string> { } });
        }
        else
        {
            var d = 0;
            return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
        }
    }
    catch (Exception Ex)
    {
        Ex.ToString();
    }
    return returnNull;
}
*/


        #endregion 

        [HttpPost]
        public HttpResponseMessage GetCompanys_S()
        {
            try
            {
                var result = c.GetCompany_S(/*sno*/);
                if (result != null)
                {
                    return Request.CreateResponse(new { response = result, message = new List<string> { } });
                }
                else
                {
                    var d = 0;
                    return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public HttpResponseMessage GetCompanys_A()
        {
            try
            {
                var result = c.GetCompany_A(/*sno*/);
                if (result != null)
                {
                    return Request.CreateResponse(new { response = result, message = new List<string> { } });
                }
                else
                {
                    var d = 0;
                    return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public HttpResponseMessage GetCompanys_SU(SingletonComp com)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
                {
                    var result = c.GetCompany_Suspense((long)com.compid);
                    if (result != null)
                    {
                        //return Request.CreateResponse(new { response = result, message = new List<string> { } });
                        return GetSuccessResponse(result);
                    }
                    else
                    {
                        return GetNotFoundResponse();
                    }
                }
                catch (Exception Ex)
                {
                    // return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                    return GetServerErrorResponse(Ex.Message.ToString());
                
                }
            //return returnNull;
        }
        [HttpPost]
        public HttpResponseMessage GetCompanys_SUS()
        {
            try
            {
                var result = c.GetCompany_Sus();
                if (result != null)
                {
                    return Request.CreateResponse(new { response = result, message = new List<string> { } });
                }
                else
                {
                    var d = 0;
                    return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpPost]
        public HttpResponseMessage GetAccount(SingletonComp co)
        {
            if (ModelState.IsValid) {  
                try
                {
                    var result = c.GetBank_S((long)co.compid);
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        var d = 0;
                        return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
                    }
                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });

                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            //return returnNull;
        }
        [HttpPost]
        public HttpResponseMessage GetBanks(SingletonSno sno)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = c.GetBank((long)sno.Sno);
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        var d = 0;
                        return Request.CreateResponse(new { response = d, message = new List<string> { "Failed" } });
                    }
                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });

                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }


            //return returnNull;
        }

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage CheckAccount(SingletonAcc a)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var check = c.Validateaccount(a.acc);
                    if (check == true)
                    {
                        return Request.CreateResponse(new { response = check, message = new List<string> { } });
                    }
                    else { return Request.CreateResponse(new { response = check, message = new List<string> { } }); }

                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });

                }
            
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        

            //return returnNull;
        }

        [HttpPost]
        public HttpResponseMessage GetDetailsindivi(SingletonSno sno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
                    {
                        var result = c.EditCompanyss((long)sno.Sno);
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    catch (Exception Ex)
                    {
                        Ex.ToString();
                    }
                    return returnNull;
        }
        [HttpPost]
        public HttpResponseMessage DeleteCompanyBank(long sno)
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
                    return Request.CreateResponse(new { response = sno, message = new List<string> { } });
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }
        [HttpGet]
        public HttpResponseMessage GetRegionDetails()
        {
            try
            {
                var result = r.GetReg();
                return Request.CreateResponse(new { response = result, message = new List<string> { } });
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }//need to check get methods


        [HttpPost]
        public HttpResponseMessage GetDistDetails(SingletonSno Sno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
                {
                    string ash = null;
                    if (Sno.Sno == Convert.ToInt64(ash))
                    {
                        return Request.CreateResponse(new { response = Sno, message = new List<string> { } });
                    }
                    else
                    {
                        var result = d.GetDistrictActive((long)Sno.Sno);
                        if (result == null)
                        {
                            int d = 0;
                            return Request.CreateResponse(new { response = d, message = new List<string> { "Failed" } });
                        }
                        else
                        {
                            return Request.CreateResponse(new { response = result, message = new List<string> { } });
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
        public HttpResponseMessage GetWard(long sno)
        {
            try
            {
                var result = w.GetWARDAct(sno);
                if (result == null)
                {
                    int d = 0;
                    return Request.CreateResponse(new { response = d, message = new List<string> {"Failed" } });
                }
                else
                {
                    return Request.CreateResponse(new { response = result, message = new List<string> { } });
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
        public HttpResponseMessage AddCompanyBank(CompanyBankAddModel m)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    c.CompSno = m.compsno;
                    c.CompName = m.compname;
                    c.PostBox = m.pbox;
                    c.Address = m.addr;
                    c.RegId = m.rsno;
                    c.DistSno = m.dsno;
                    c.WardSno = m.wsno;
                    c.TinNo = m.tin;
                    c.VatNo = m.vat;
                    c.DirectorName = m.dname;
                    c.Email = m.email;
                    c.TelNo = m.telno;
                    c.FaxNo = m.fax;
                    c.MobNo = m.mob;
                    c.Branch_Sno = m.branch;
                    c.Checker = m.check_status;
                    //c.CompLogo = clogo;
                    //c.DirectorSig = sig;
                    //c.Postedby = string.Empty;//Session["UserID"].ToString();
                    c.Status = "Pending";
                    long ssno = 0;
                    if (m.compsno == 0)
                    {
                        //var chk = sgd.Validateduplicatechecking(long.Parse(Session["Facili_Reg_No"].ToString()), desc, dt);
                        //if (chk == false)
                        //{
                        var result = c.ValidateCount(m.compname.ToLower(), m.tin);

                        if (result == true)
                        {
                            return Request.CreateResponse(new { response = result, message = new List<string> { } });
                        }
                        //else
                        //{

                        if (cu.ValidateduplicateEmail1(m.email))
                        {
                            return Request.CreateResponse(new { response = "Email already exist", message = new List<string> { "Failed" } });
                        }
                        else
                        if (cu.ValidateMobile(m.mob))
                        {
                            return Request.CreateResponse(new { response = "Mobile number already exist", message = new List<string> { "Failed" } });
                        }
                        //else if (cu.Validateduplicateuser1(email.Split('@')[0]))
                        else if (cu.Validateduplicateuser1(m.mob))
                        {
                            return Request.CreateResponse(new { response = "User already exist", message = new List<string> { "Failed" } });
                        }
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
                            cu.Email = m.email;
                            cu.Usertype = "001";
                            cu.Mobile = m.mob;
                            cu.Flogin = "false";
                            //cu.Fullname = email.Split('@')[0];
                            cu.Fullname = m.mob;
                            //cu.Username = email.Split('@')[0];
                            cu.Username = m.mob;
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
                                String[] list1 = new String[16] { ssno.ToString(), m.compname, m.pbox, m.addr, m.rsno.ToString(), m.dsno.ToString(), m.wsno.ToString(), m.tin, m.vat, m.dname, m.email, m.telno, m.fax, m.mob, "1", DateTime.Now.ToString() };
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
                                // Add SMS Method here
                                SendActivationEmail(m.email, m.mob, cu.Password, m.mob);
                            }
                        }
                        if (ssno > 0)
                        {
                            for (int i = 0; i < m.details.Count(); i++)
                            {
                                if (m.details[i].AccountNo != null)
                                {
                                    c.CompSno = ssno;
                                    //c.BankSno = details[i].BankSno;
                                    //c.BankName = details[i].BankName;
                                    //c.BankBranch = details[i].BankBranch;
                                    c.AccountNo = m.details[i].AccountNo;
                                    //c.Swiftcode = details[i].Swiftcode;

                                    long detsno = c.AddBank(c);
                                    var getcom = c.EditBank(m.compsno);
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
                            return Request.CreateResponse(new { response = ssno, message = new List<string> { } });
                        }
                    }
                    else if (m.compsno > 0)
                    {
                        var getcom = c.EditCompany(m.compsno);
                        bool flag = true;
                        bool cmp = true;
                        if (getcom.MobNo == m.mob)
                        {
                            flag = false;
                        }
                        if (getcom.CompName == m.compname)
                        {
                            cmp = false;
                        }
                        if (cu.ValidateMobile(m.mob) && flag == true)
                        {
                            return Request.CreateResponse(new { response = "Mobile number already exist", message = new List<string> { "Failed" } });
                        }
                        else if (c.ValidateCount(m.compname.ToLower(), m.tin) && cmp == true)
                        {
                            return Request.CreateResponse(new { response = "Company name already exist", message = new List<string> { "Failed" } });
                        }
                        else
                        {
                            //getcom = c.EditCompany(compsno);
                            //var dd = des.Editdesignation(sno);
                            if (getcom != null)
                            {
                                String[] list1 = new String[16] { ssno.ToString(), m.compname, m.pbox, m.addr, m.rsno.ToString(), m.dsno.ToString(), m.wsno.ToString(), m.tin, m.vat, m.dname, m.email, m.telno, m.fax, m.mob, m.userid.ToString(), DateTime.Now.ToString() };
                                String[] list2 = new String[16] { getcom.CompSno.ToString(), getcom.CompName,getcom.PostBox, getcom.Address, getcom.RegId.ToString(),getcom.DistSno.ToString(),getcom.WardSno.ToString(),getcom.TinNo,getcom.VatNo,getcom.DirectorName,
                                       getcom.Email,getcom.TelNo,getcom.FaxNo,getcom.MobNo, string.Empty, getcom.Posteddate.ToString() };//Session["UserID"].ToString()
                                for (int i = 0; i < list.Count(); i++)
                                {
                                    ad.Audit_Type = "Update";
                                    ad.Columnsname = list[i];
                                    ad.Table_Name = "Company";
                                    ad.Oldvalues = list2[i];
                                    ad.Newvalues = list1[i];
                                    ad.AuditBy = m.userid.ToString();
                                    ad.Audit_Date = DateTime.Now;
                                    ad.Audit_Time = DateTime.Now;
                                    ad.AddAudit(ad);
                                }
                            }

                            c.UpdateCompany(c);
                            ssno = m.compsno;
                            if (ssno > 0)
                            {
                                c.CompSno = ssno;
                                c.DeleteBank(c);
                                for (int i = 0; i < m.details.Count(); i++)
                                {
                                    //if (details[i].Term_Sno != 0 && details[i].Currency_Sno != 0)
                                    //{
                                    if (m.details[i].AccountNo != null)
                                    {

                                        c.CompSno = ssno;
                                        //c.BankSno = m.details[i].BankSno;
                                        // c.BankName = m.details[i].BankName;
                                        //c.BankBranch = m.details[i].BankBranch;
                                        c.AccountNo = m.details[i].AccountNo;
                                        //c.Swiftcode = m.details[i].Swiftcode;
                                        var getcom1 = c.EditBank(m.compsno);
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
                            return Request.CreateResponse(new { response = ssno, message = new List<string> { } });
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
                    //Ex.ToString();
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });

                }

            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            return returnNull;
        }



        [HttpPost]
        public HttpResponseMessage AddCompanyBankL(AddCompanyBankL mc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    c.CompSno = mc.compsno;
                    c.CompName = mc.compname;
                    c.PostBox = mc.pbox;
                    c.Address = mc.addr;
                    c.RegId = mc.rsno;
                    c.DistSno = mc.dsno;
                    c.WardSno = mc.wsno;
                    c.TinNo = mc.tin;
                    c.VatNo = mc.vat;
                    c.DirectorName = mc.dname;
                    c.Email = mc.email;
                    c.TelNo = mc.telno;
                    c.FaxNo = mc.fax;
                    c.MobNo = mc.mob;
                    c.Branch_Sno = mc.branch;
                    c.Checker = mc.check_status;
                    //c.CompLogo = clogo;
                    //c.DirectorSig = sig;
                    //c.Postedby = string.Empty;//Session["UserID"].ToString();
                    c.Status = "Pending";
                    long ssno = 0;
                    if (mc.compsno == 0)
                    {
                        //var chk = sgd.Validateduplicatechecking(long.Parse(Session["Facili_Reg_No"].ToString()), desc, dt);
                        //if (chk == false)
                        //{
                        var result = c.ValidateCount(mc.compname.ToLower(), mc.tin);

                        if (result == true)
                        {
                            return Request.CreateResponse(new { response = result, message = new List<string> { } });
                        }
                        //else
                        //{

                        if (cu.ValidateduplicateEmail1(mc.email))
                        {
                            return Request.CreateResponse(new { response = "Email already exist", message = new List<string> { "Failed" } });
                        }
                        else
                        if (cu.ValidateMobile(mc.mob))
                        {
                            return Request.CreateResponse(new { response = "Mobile number already exist", message = new List<string> { "Failed" } });
                        }
                        //else if (cu.Validateduplicateuser1(email.Split('@')[0]))
                        else if (cu.Validateduplicateuser1(mc.mob))
                        {
                            return Request.CreateResponse(new { response = "User already exist", message = new List<string> { " Failed " } });
                        }
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
                            cu.Email = mc.email;
                            cu.Usertype = "001";
                            cu.Mobile = mc.mob;
                            cu.Flogin = "false";
                            //cu.Fullname = email.Split('@')[0];
                            cu.Fullname = mc.mob;
                            //cu.Username = email.Split('@')[0];
                            cu.Username = mc.mob;
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
                                String[] list1 = new String[16] { ssno.ToString(), mc.compname, mc.pbox, mc.addr, mc.rsno.ToString(), mc.dsno.ToString(), mc.wsno.ToString(), mc.tin, mc.vat, mc.dname, mc.email, mc.telno, mc.fax, mc.mob, "1", DateTime.Now.ToString() };
                                for (int i = 0; i < list.Count(); i++)
                                {
                                    ad.Audit_Type = "Insert";
                                    ad.Columnsname = list[i];
                                    ad.Table_Name = "Company";
                                    ad.Newvalues = list1[i];
                                    // ad.AuditBy = mc.userid.ToString();
                                    ad.Audit_Date = DateTime.Now;
                                    ad.Audit_Time = DateTime.Now;
                                    ad.AddAudit(ad);
                                }
                            }

                            if (ssno > 0)
                            {
                                // Sms Method goes here
                                SendActivationEmail(mc.email, mc.mob, cu.Password, mc.mob);
                            }
                        }
                        if (ssno > 0)
                        {

                            c.CompSno = ssno;
                            //c.BankSno = details[i].BankSno;
                            //c.BankName = details[i].BankName;
                            //c.BankBranch = details[i].BankBranch;
                            c.AccountNo = mc.accno;
                            //c.Swiftcode = details[i].Swiftcode;

                            long detsno = c.AddBank(c);



                            return Request.CreateResponse(new { response = ssno, message = new List<string> { } });
                        }
                    }
                    else if (mc.compsno > 0)
                    {
                        var getcom = c.EditCompany(mc.compsno);
                        bool flag = true;
                        bool cmp = true;
                        if (getcom.MobNo == mc.mob)
                        {
                            flag = false;
                        }
                        if (getcom.CompName == mc.compname)
                        {
                            cmp = false;
                        }
                        if (cu.ValidateMobile(mc.mob) && flag == true)
                        {
                            return Request.CreateResponse(new { response = "Mobile number already exist", message = new List<string> { "Failed" } });
                        }
                        else if (c.ValidateCount(mc.compname.ToLower(), mc.tin) && cmp == true)
                        {
                            return Request.CreateResponse(new { response = "Company name already exist", message = new List<string> { "Failed" } });
                        }
                        else
                        {
                            //getcom = c.EditCompany(compsno);
                            //var dd = des.Editdesignation(sno);
                            if (getcom != null)
                            {
                                String[] list1 = new String[16] { ssno.ToString(), mc.compname, mc.pbox, mc.addr, mc.rsno.ToString(), mc.dsno.ToString(), mc.wsno.ToString(), mc.tin, mc.vat, mc.dname, mc.email, mc.telno, mc.fax, mc.mob, mc.userid.ToString(), DateTime.Now.ToString() };
                                String[] list2 = new String[16] { getcom.CompSno.ToString(), getcom.CompName,getcom.PostBox, getcom.Address, getcom.RegId.ToString(),getcom.DistSno.ToString(),getcom.WardSno.ToString(),getcom.TinNo,getcom.VatNo,getcom.DirectorName,
                                   getcom.Email,getcom.TelNo,getcom.FaxNo,getcom.MobNo, string.Empty, getcom.Posteddate.ToString() };//Session["UserID"].ToString()
                                for (int i = 0; i < list.Count(); i++)
                                {
                                    ad.Audit_Type = "Update";
                                    ad.Columnsname = list[i];
                                    ad.Table_Name = "Company";
                                    ad.Oldvalues = list2[i];
                                    ad.Newvalues = list1[i];
                                    ad.AuditBy = mc.userid.ToString();
                                    ad.Audit_Date = DateTime.Now;
                                    ad.Audit_Time = DateTime.Now;
                                    ad.AddAudit(ad);
                                }
                            }

                            c.UpdateCompany(c);
                            ssno = mc.compsno;
                            if (ssno > 0)
                            {
                                c.CompSno = ssno;
                                c.DeleteBank(c);

                            }
                            return Request.CreateResponse(new { response = ssno, message = new List<string> { } });
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
                    //Ex.ToString();
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });

                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
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
                    var data = em.getEMAILst("1");
                    mm.To.Add(email);
                    mm.From = new MailAddress(m.From_Address);
                    mm.Subject = data.Local_subject;
                    drt = data.Local_subject;
                    /*var urlBuilder =
                   new System.UriBuilder(Request.Url.AbsoluteUri)
                   {
                       Path = Url.Action("Loginnew", "Loginnew"),
                       Query = null,
                   };

                    Uri uri = urlBuilder.Uri;*/
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
