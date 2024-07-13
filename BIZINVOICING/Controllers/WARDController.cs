using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
namespace BIZINVOICING.Controllers
{
    public class WARDController : AdminBaseController
    {
        // GET: WARD
       
        //   S_SMTP smtp = new S_SMTP();
        DISTRICTS dst = new DISTRICTS();
        REGION reg = new REGION();
        WARD wd = new WARD();
        private readonly dynamic returnNull = null;
        EMP_DET ed = new EMP_DET();
        Auditlog ad = new Auditlog();
        String[] list = new String[7] { "ward_sno", "ward_name", "region_id","district_sno", "ward_status", "posted_by", "posted_date" };
        public ActionResult WARD()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
          //  ed.Detail_Id = Convert.ToInt64(Session["UserID"].ToString());
            //ed.UpdateOnlyflsgtrue(ed);
            return View();
        }

        // GET: SMTP/Details/5

        [HttpPost]
        public ActionResult Addward(
            string ward_name,
            long region_id,

            long district_sno,
            string ward_status,
            string posted_by,
            long sno, bool dummy
            )
        {

            try
            {

                wd.Ward_Name = ward_name;
                wd.Region_Id = region_id;
                wd.District_Sno = district_sno;
                wd.Ward_Status = ward_status;
                wd.AuditBy = Session["UserID"].ToString();
                wd.SNO = sno;
                var result = wd.ValidateWARD(district_sno, ward_name.ToLower(), region_id);

                long ssno = 0;
                if (sno == 0)
                {
                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ssno = wd.AddWARD(wd);
                        var dd = reg.EditREGION(region_id);
                        var dd1 = dst.EditDISTRICTS(district_sno);
                        if (ssno > 0)
                        {
                            String[] list1 = new String[7] { ssno.ToString(), ward_name, dd.Region_Name, dd1.District_Name, ward_status, Session["UserID"].ToString(), DateTime.Now.ToString() };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Ward";
                                ad.Newvalues = list1[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }
                        return Json(ssno, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (sno > 0)
                {
                    var res = wd.ValidateWARD1(district_sno, ward_name.ToLower(), region_id);
                    if (res == false)
                    {
                        if (dummy == false)
                        {
                            return Json(dummy, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var gt = wd.EditWARD(sno);
                            var dd = reg.EditREGION(gt.Region_Id);
                            var dd1 = dst.EditDISTRICTS(gt.District_Sno);
                            if (gt != null)
                            {
                                String[] list2 = new String[7] { gt.SNO.ToString(), gt.Ward_Name, dd.Region_Name, dd1.District_Name, gt.Ward_Status, Session["UserID"].ToString(), DateTime.Now.ToString() };
                                String[] list1 = new String[7] { gt.SNO.ToString(), ward_name, dd.Region_Name, dd1.District_Name, ward_status, Session["UserID"].ToString(), DateTime.Now.ToString() };
                                for (int i = 0; i < list.Count(); i++)
                                {
                                    ad.Audit_Type = "Update";
                                    ad.Columnsname = list[i];
                                    ad.Table_Name = "Ward";
                                    ad.Oldvalues = list2[i];
                                    ad.Newvalues = list1[i];
                                    ad.AuditBy = Session["UserID"].ToString();
                                    ad.Audit_Date = DateTime.Now;
                                    ad.Audit_Time = DateTime.Now;
                                    ad.AddAudit(ad);
                                }
                            }
                            wd.UpdateWARD(wd);
                            ssno = sno;
                            return Json(ssno, JsonRequestBehavior.AllowGet);
                            // }

                        }
                    }
                    else
                    {
                        var res1 = wd.ValidateWARD1(district_sno, ward_name.ToLower(), region_id);
                        if (res1 == false)
                        {
                            return Json(dummy, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var gt = wd.EditWARD(sno);
                            var dd = reg.EditREGION(gt.Region_Id);
                            var dd1 = dst.EditDISTRICTS(gt.District_Sno);
                            if (gt != null)
                            {
                                String[] list2 = new String[7] { gt.SNO.ToString(), gt.Ward_Name, dd.Region_Name, dd1.District_Name, gt.Ward_Status, Session["UserID"].ToString(), DateTime.Now.ToString() };
                                String[] list1 = new String[7] { gt.SNO.ToString(), ward_name, dd.Region_Name, dd1.District_Name, ward_status, Session["UserID"].ToString(), DateTime.Now.ToString() };
                                for (int i = 0; i < list.Count(); i++)
                                {
                                    ad.Audit_Type = "Update";
                                    ad.Columnsname = list[i];
                                    ad.Table_Name = "Ward";
                                    ad.Oldvalues = list2[i];
                                    ad.Newvalues = list1[i];
                                    ad.AuditBy = Session["UserID"].ToString();
                                    ad.Audit_Date = DateTime.Now;
                                    ad.Audit_Time = DateTime.Now;
                                    ad.AddAudit(ad);
                                }
                            }
                            wd.UpdateWARD(wd);
                            ssno = sno;
                            return Json(ssno, JsonRequestBehavior.AllowGet);
                        }
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
        public ActionResult DeleteWard(
           long sno
           )
        {

            try
            {

                wd.SNO = sno;
                var name = wd.checkward(sno);
                if (name == true)
                {
                    return Json(name, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (sno > 0)
                    {
                        var gt = wd.EditWARD(sno);
                        var dd = reg.EditREGION(gt.Region_Id);
                        var dd1 = dst.EditDISTRICTS(gt.District_Sno);
                        if (gt != null)
                        {
                            String[] list2 = new String[7] { gt.SNO.ToString(), gt.Ward_Name, dd.Region_Name, dd1.District_Name, gt.Ward_Status, Session["UserID"].ToString(), DateTime.Now.ToString() };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Delete";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Ward";
                                ad.Oldvalues = list2[i];
                                ad.AuditBy = Session["UserID"].ToString();
                                ad.Audit_Date = DateTime.Now;
                                ad.Audit_Time = DateTime.Now;
                                ad.AddAudit(ad);
                            }
                        }
                        wd.DeleteWARD(sno);
                        return Json(sno, JsonRequestBehavior.AllowGet);
                    }
                    //  var result = sno;


               }

            }
            catch (Exception Ex)
            {
                Ex.ToString();
            }

            return returnNull;
        }
        [HttpPost]


        public ActionResult Getward()
        {

            try
            {
                var result = wd.GetWARD();
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
        [HttpPost]

        public ActionResult EditWARDs(long Sno)
        {

            try
            {
                var result = wd.EditWARD(Sno);
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
        public ActionResult GetDist(long sno)
        {

            try
            {
                var result = dst.GetDistrictActive(sno);
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int d = 0;
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
