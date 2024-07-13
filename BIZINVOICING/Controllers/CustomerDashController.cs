using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;

namespace BIZINVOICING.Controllers
{
    public class CustomerDashController : LangcoController
    {
        // GET: CustomerDash
        CustomerMaster cm = new CustomerMaster();
        CompanyBankMaster c = new CompanyBankMaster();
        //COUNTRY c = new COUNTRY();
        REGION r = new REGION();
        DISTRICTS d = new DISTRICTS();
        WARD w = new WARD();
        private readonly dynamic returnNull = null;
        
        public ActionResult CustomerDash()
        {
            if (Session["sessComp"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }
        [HttpPost]
        public ActionResult GetCusts()
        {
            try
            {

                var result = cm.CustGet();
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
        public ActionResult GetRegion(string rn)
        {
            try
            {
                long rid = 0;
                if (string.IsNullOrEmpty(rn))
                {

                }
                else
                {
                    rid = long.Parse(rn);
                }
                var result = r.EditREGION(rid);
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
        public ActionResult GetComp()
        {
            try
            {

                var result = c.CompGet(long.Parse(Session["CompID"].ToString()));
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
        public ActionResult GetDistrict(string dn)
        {
            try
            {
                long rid = 0;
                if (string.IsNullOrEmpty(dn))
                {

                }
                else
                {
                    rid = long.Parse(dn);
                }
                var result = d.EditDISTRICTS(rid);
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
        public ActionResult GetWard(string wn)
        {
            try
            {
                long rid = 0;
                if (string.IsNullOrEmpty(wn))
                {

                }
                else
                {
                    rid = long.Parse(wn);
                }
                var result = w.EditWARD(rid);
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
        public ActionResult GetRegionDetails1(long Sno)
        {
            try
            {
                var result = r.GetReg(Sno);
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
        public ActionResult GetWardDetails(long sno = 1)
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
        public ActionResult GetTIN(long sno)
        {
            try
            {
                var result = cm.getTIN(sno);
                if (result == null)
                {
                    int d = 0;
                    return Json(d, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(result.TinNo, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }



        [HttpPost]
        public ActionResult AddCustomer(long CSno, /*long Compsno,*/ String CName, String PostboxNo, String Address, long regid, long distsno, long wardsno,
            String Tinno, String VatNo, String CoPerson, String Mail, String Mobile_Number, bool dummy)
        {
            try
            {
                cm.Cust_Sno = CSno;
                cm.Cust_Name = CName;
                cm.PostboxNo = PostboxNo;
                cm.Address = Address;
                cm.CompanySno = long.Parse((Session["CompID"]).ToString());
                if (regid > 0)
                {
                    cm.Region_SNO = regid;
                }
                if (distsno > 0)
                {
                    cm.DistSno = distsno;
                }
                if (wardsno > 0)
                {
                    cm.WardSno = wardsno;
                }

                cm.TinNo = Tinno;
                cm.VatNo = VatNo;
                cm.ConPerson = CoPerson;
                cm.Email = Mail;
                cm.Phone = Mobile_Number;
                //sd.Status = Session["admin1"].ToString() == "Admin" ? "Approved" : "Pending";
                //sd.Facility_reg_Sno = long.Parse(Session["Facili_Reg_No"].ToString());
                //sd.Facility_Name = Session["Facili_Name"].ToString();
                cm.Posted_by = Session["UserID"].ToString();

                long ssno = 0;
                if (CSno == 0)
                {
                    var result = cm.ValidateCount(CName.ToLower(), Tinno);

                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ssno = cm.CustAdd(cm);

                        //String[] list1 = new String[13] { ssno.ToString(), sd.Division_Name, sd.Address1, sd.Address2, sd.Loc, sd.Phone, sd.Status, sd.Facility_reg_Sno.ToString(), sd.Facility_Name, sd.Posted_by, sd.Posted_Date.ToString(), sd.Checked_By, sd.Checked_Date.ToString() };
                        //if (ssno > 0)
                        //{

                        //    for (int i = 0; i < list.Count(); i++)
                        //    {
                        //        al.Audit_Type = "Insert";
                        //        al.Columnsname = list[i];
                        //        al.Table_Name = "standard_divisions";
                        //        al.Newvalues = list1[i];
                        //        al.AuditBy = Session["UserID"].ToString();
                        //        al.Audit_Date = DateTime.Now;
                        //        al.Audit_Time = DateTime.Now;
                        //        al.Facility_Sno = long.Parse(Session["Facili_Reg_No"].ToString());
                        //        al.AddAudit(al);
                        //    }

                        //}

                        return Json(ssno, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (CSno > 0)
                {
                    var update = cm.ValidateDeleteorUpdate(CSno);
                    if (update == false)
                    {

                        if (dummy == false)
                        {
                            return Json(dummy, JsonRequestBehavior.AllowGet);
                        }

                        else
                        {

                            var dd = cm.EditCust(CSno);
                            //if (dd != null)
                            //{
                            //    String[] list2 = new String[13] { dd.Division_Sno.ToString(), dd.Division_Name, dd.Address1, dd.Address2, dd.Loc, dd.Phone, dd.Status, dd.Facility_reg_Sno.ToString(), dd.Facility_Name, dd.Posted_by, dd.Posted_Date.ToString(), dd.Checked_By, dd.Checked_Date.ToString() };
                            //    String[] list1 = new String[13] { ssno.ToString(), sd.Division_Name, sd.Address1, sd.Address2, sd.Loc, sd.Phone, sd.Status, sd.Facility_reg_Sno.ToString(), sd.Facility_Name, sd.Posted_by, sd.Posted_Date.ToString(), sd.Checked_By, sd.Checked_Date.ToString() };


                            //    for (int i = 0; i < list.Count(); i++)
                            //    {
                            //        al.Audit_Type = "Update";
                            //        al.Columnsname = list[i];
                            //        al.Table_Name = "standard_divisions";
                            //        al.Oldvalues = list2[i];
                            //        al.Newvalues = list1[i];
                            //        al.AuditBy = Session["UserID"].ToString();
                            //        al.Audit_Date = DateTime.Now;
                            //        al.Audit_Time = DateTime.Now;
                            //        al.Facility_Sno = long.Parse(Session["Facili_Reg_No"].ToString());
                            //        al.AddAudit(al);
                            //    }
                            //}
                            cm.CustUpdate(cm);
                            ssno = CSno;
                            return Json(ssno, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(update, JsonRequestBehavior.AllowGet);
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
        public ActionResult DeleteCust(long sno)
        {
            try
            {

                //var name = sd.ValidateDeleteorUpdate(sno, long.Parse(Session["Facili_Reg_No"].ToString()));
                //if (name == true)
                //{
                //    return Json(name, JsonRequestBehavior.AllowGet);
                //}
                //else
                //{
                if (sno > 0)
                {
                    var dd = cm.EditCust(sno);
                    //if (dd != null)
                    //{
                    //    String[] list2 = new String[13] { dd.Division_Sno.ToString(), dd.Division_Name, dd.Address1, dd.Address2, dd.Loc, dd.Phone, dd.Status, dd.Facility_reg_Sno.ToString(), dd.Facility_Name, dd.Posted_by, dd.Posted_Date.ToString(), dd.Checked_By, dd.Checked_Date.ToString() };
                    //    for (int i = 0; i < list.Count(); i++)
                    //    {
                    //        al.Audit_Type = "Delete";
                    //        al.Columnsname = list[i];
                    //        al.Table_Name = "standard_divisions";
                    //        al.Oldvalues = list2[i];
                    //        al.AuditBy = Session["UserID"].ToString();
                    //        al.Audit_Date = DateTime.Now;
                    //        al.Audit_Time = DateTime.Now;
                    //        al.Facility_Sno = long.Parse(Session["Facili_Reg_No"].ToString());
                    //        al.AddAudit(al);
                    //    }
                    //}
                    //al.Facility_Sno = sno;
                    cm.CustDelete(sno);
                    return Json(sno, JsonRequestBehavior.AllowGet);
                }
                //}
            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }
            return returnNull;
        }

        
    }
}