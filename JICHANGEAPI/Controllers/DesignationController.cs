using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DesignationController : ApiController
    {
        private readonly List<string> tableColumns = new List<string> { "desg_id", "desg_name", "posted_by", "posted_date" };

        [HttpPost]
        public HttpResponseMessage GetdesgDetails()
        {
            try
            {
                var dg = new DESIGNATION();
                var result = dg.GetDesignation();
                if (result != null)
                {
                    return Request.CreateResponse(new { response = result, message = new List<string>() });
                }
                else
                {
                    return Request.CreateResponse(new { response = new List<DESIGNATION>(), message = new List<string>() });
                }
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }

        [HttpPost]
        public HttpResponseMessage Adddesg(AddDesignationForm addDesignationForm)
        {
            if (ModelState.IsValid)
            {
                DESIGNATION designation = new DESIGNATION();
                designation.Desg_Name = addDesignationForm.desg;
                designation.Desg_Id = (long)addDesignationForm.sno;
                designation.AuditBy = addDesignationForm.userid.ToString();
                try
                {
                    if ((long) addDesignationForm.sno == 0)
                    {
                        var isExist = designation.isExistDesignation((long) addDesignationForm.sno);
                        if (isExist)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Already exists." } });
                        }
                        else
                        {
                            var addedDesignation = designation.AddUser(designation);
                            var insertAudits = new List<string> { addedDesignation.ToString(), addDesignationForm.desg, addDesignationForm.userid.ToString(), DateTime.Now.ToString() };
                            Auditlog.insertAuditTrail(insertAudits, (long) addDesignationForm.userid, "Designation",tableColumns);
                            return Request.CreateResponse(new { response = addedDesignation, message = new List<string>() });
                        }
                    }
                    else
                    {
                        var design = designation.getDesignationText((long)addDesignationForm.sno);
                        
                        designation.UpdateDesignation(designation);
                        
                        var oldValues = new List<string> { design.Desg_Id.ToString(), design.Desg_Name, design.AuditBy, design.Audit_Date.ToString() };
                        var newValues = new List<string> { design.Desg_Id.ToString(), addDesignationForm.desg, addDesignationForm.userid.ToString(), DateTime.Now.ToString() };
                        Auditlog.updateAuditTrail(oldValues, newValues, (long) addDesignationForm.userid, "Designation", tableColumns);
                        return Request.CreateResponse(new { response = addDesignationForm.sno, message = new List<string>() });
                    }
                }
                catch(Exception ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        }

        [HttpPost]
        public HttpResponseMessage Deletedesg(DeleteDesignationForm deleteDesignationForm)
        {
            if (ModelState.IsValid)
            {
                var designation = new DESIGNATION();
                try
                {
                    var isExists = designation.isExistDesignation(deleteDesignationForm.sno);
                    if (isExists)
                    {
                        var design = designation.getDesignationText(deleteDesignationForm.sno);
                        var values = new List<string> { design.Desg_Id.ToString(), design.Desg_Name.ToString(), design.AuditBy, design.Audit_Date.ToString() };
                        Auditlog.deleteAuditTrail(values, (long)deleteDesignationForm.userid, "Designation", this.tableColumns);
                        designation.DeleteDesignation(deleteDesignationForm.sno);
                        return Request.CreateResponse(new { response = deleteDesignationForm.sno, message = new List<string>() });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Designation does not exist." } });
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        }
    }
}