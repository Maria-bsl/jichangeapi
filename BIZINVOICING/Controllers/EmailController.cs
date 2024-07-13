﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.BIZINVOICING.BusinessEntities.Masters;
using BL.BIZINVOICING.BusinessEntities.ConstantFile;
namespace BIZINVOICING.Controllers
{
    public class EmailController : AdminBaseController
    {
        EMAIL cy = new EMAIL();
        EMP_DET ed = new EMP_DET();
        Auditlog ad = new Auditlog();
        String[] list = new String[9] { "sno", "flow_id", "email_text", "effective_date", "posted_by", "posted_date", "email_sub", "email_sub_local", "email_text_local" };
        private readonly dynamic returnNull = null;
        // GET: Email
        public ActionResult Email()
        {
            if (Session["sessB"] == null)
            {
                return RedirectToAction("Loginnew", "Loginnew");
            }
            return View();
        }
        [HttpPost]
        public ActionResult GetEmailDetails()
        {
            try
            {
                var result = cy.GetEMAIL();
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
        public ActionResult AddEmail(string flow, String text, String loctext, String sub, String subloc, long sno)
        {

            try
            {

                cy.Flow_Id = flow;
                cy.Email_Text = Utilites.RemoveSpecialCharacters(text);
                cy.Local_Text = Utilites.RemoveSpecialCharacters(loctext);
                cy.Subject = Utilites.RemoveSpecialCharacters(sub);
                cy.Local_subject = Utilites.RemoveSpecialCharacters(subloc);
                cy.SNO = sno;
                cy.AuditBy= Session["UserID"].ToString();
                long ssno = 0;
                if (sno == 0)
                {
                    var result = cy.ValidateEMAIL(flow);
                    if (result == true)
                    {
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        ssno = cy.AddEMAIL(cy);
                        if (ssno > 0)
                        {
                            String[] list1 = new String[9] { ssno.ToString(), cy.Flow_Id, cy.Email_Text, DateTime.Now.ToString(), Session["UserID"].ToString(), DateTime.Now.ToString(), cy.Subject, cy.Local_subject, cy.Local_Text };
                            for (int i = 0; i < list.Count(); i++)
                            {
                                ad.Audit_Type = "Insert";
                                ad.Columnsname = list[i];
                                ad.Table_Name = "Email Text";
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
                    var dd = cy.EditEMAIL(sno);
                    if (dd != null)
                    {
                        String[] list2 = new String[9] { dd.SNO.ToString(), dd.Flow_Id, dd.Email_Text, dd.Effective_Date.ToString(), dd.AuditBy, dd.Audit_Date.ToString(), dd.Subject, dd.Local_subject, dd.Local_Text };
                        String[] list1 = new String[9] { dd.SNO.ToString(), cy.Flow_Id, cy.Email_Text, DateTime.Now.ToString(), Session["UserID"].ToString(), DateTime.Now.ToString(), cy.Subject, cy.Local_subject, cy.Local_Text };
                        for (int i = 0; i < list.Count(); i++)
                        {
                            ad.Audit_Type = "Update";
                            ad.Columnsname = list[i];
                            ad.Table_Name = "Email Text";
                            ad.Oldvalues = list2[i];
                            ad.Newvalues = list1[i];
                            ad.AuditBy = Session["UserID"].ToString();
                            ad.Audit_Date = DateTime.Now;
                            ad.Audit_Time = DateTime.Now;
                            ad.AddAudit(ad);
                        }
                    }
                    cy.UpdateEMAIL(cy);
                    ssno = sno;
                    return Json(ssno, JsonRequestBehavior.AllowGet);

                     //}
                }


            }
            catch (Exception Ex)
            {
                Ex.Message.ToString();
            }

            return returnNull;
        }
        public ActionResult DeleteEmail(int Sno,String name)
        {
            try
            {
                var dd = cy.EditEMAIL(Sno);
                if (dd != null)
                {
                    String[] list2 = new String[9] { dd.SNO.ToString(), dd.Flow_Id, dd.Email_Text, dd.Effective_Date.ToString(), dd.AuditBy, dd.Audit_Date.ToString(), dd.Subject, dd.Local_subject, dd.Local_Text };
                    for (int i = 0; i < list.Count(); i++)
                    {
                        ad.Audit_Type = "Delete";
                        ad.Columnsname = list[i];
                        ad.Table_Name = "Email Text";
                        ad.Oldvalues = list2[i];
                        ad.AuditBy = Session["UserID"].ToString();
                        ad.Audit_Date = DateTime.Now;
                        ad.Audit_Time = DateTime.Now;
                        ad.AddAudit(ad);
                    }
                }
                cy.DeleteEMAIL(Sno);
                    var result = Sno;
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