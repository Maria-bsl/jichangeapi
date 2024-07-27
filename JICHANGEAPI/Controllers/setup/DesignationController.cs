using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers.setup
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DesignationController : SetupBaseController
    {
        private static readonly List<string> tableColumns = new List<string> { "desg_id", "desg_name", "posted_by", "posted_date" };
        private static readonly string tableName = "Designation";

        [HttpPost]
        public HttpResponseMessage GetdesgDetails()
        {
            DESIGNATION designation = new DESIGNATION();
            try
            {
                var results = designation.GetDesignation();
                return this.GetList<List<DESIGNATION>, DESIGNATION>(results);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private DESIGNATION CreateDesignation(AddDesignationForm addDesignationForm)
        {
            DESIGNATION designation = new DESIGNATION();
            designation.Desg_Name = addDesignationForm.desg;
            designation.Desg_Id = (long)addDesignationForm.sno;
            designation.AuditBy = addDesignationForm.userid.ToString();
            return designation;
        }

        private void AppendInsertAuditTrail(long designationSno, DESIGNATION designation, long userid)
        {
            List<string> values = new List<string> { designationSno.ToString(), designation.Desg_Name, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values,userid, DesignationController.tableName, DesignationController.tableColumns);
        }

        private void AppendUpdateAuditTrail(long designationSno, DESIGNATION oldDesignation, DESIGNATION newDesignation, long userid)
        {
            var oldValues = new List<string> { designationSno.ToString(), oldDesignation.Desg_Name, oldDesignation.AuditBy, oldDesignation.Audit_Date.ToString() };
            var newValues = new List<string> { designationSno.ToString(), newDesignation.Desg_Name, newDesignation.AuditBy, DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, DesignationController.tableName, DesignationController.tableColumns);

        }

        private void AppendDeleteAuditTrail(long designationSno, DESIGNATION designation, long userid)
        {
            List<string> values = new List<string> { designationSno.ToString(), designation.Desg_Name, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, DesignationController.tableName, DesignationController.tableColumns);
        }

        private HttpResponseMessage InsertDesignation(DESIGNATION designation,AddDesignationForm addDesignationForm)
        {
            try
            {
                var isExist = designation.ValidateDesignation(addDesignationForm.desg);
                if (isExist) return this.GetAlreadyExistsErrorResponse();
                var addedDesignation = designation.AddUser(designation);
                AppendInsertAuditTrail(addedDesignation, designation, (long)addDesignationForm.userid);
                return FindDesignation(addedDesignation);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private HttpResponseMessage UpdateDesignation(DESIGNATION designation,AddDesignationForm addDesignationForm)
        {
            try
            {
                var isExist = designation.isExistDesignation((long)addDesignationForm.sno);
                if (!isExist) { return this.GetNotFoundResponse();  }
                var isDuplicate = designation.isDuplicate(addDesignationForm.desg, (long)addDesignationForm.sno);
                if (isDuplicate) { return this.GetAlreadyExistsErrorResponse();  }
                DESIGNATION oldDesignation = designation.getDesignationText((long)addDesignationForm.sno);
                long updateDesignation = designation.UpdateDesignation(designation);
                AppendUpdateAuditTrail(updateDesignation, oldDesignation, designation, (long)addDesignationForm.userid);
                return FindDesignation(updateDesignation);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage Adddesg(AddDesignationForm addDesignationForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                DESIGNATION designation = CreateDesignation(addDesignationForm);
                if (addDesignationForm.sno == 0) { return InsertDesignation(designation,addDesignationForm); }
                else { return UpdateDesignation(designation,addDesignationForm);  }
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage FindDesignation(long sno)
        {
            try
            {
                DESIGNATION designation = new DESIGNATION();
                bool isExist = designation.isExistDesignation(sno);
                if (!isExist) return this.GetNotFoundResponse();
                DESIGNATION found = designation.Editdesignation(sno);
                return this.GetSuccessResponse(found);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage Deletedesg(DeleteDesignationForm deleteDesignationForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            DESIGNATION designation = new DESIGNATION();
            try
            {
                bool isExists = designation.isExistDesignation(deleteDesignationForm.sno);
                if (!isExists) return this.GetNotFoundResponse();
                DESIGNATION found = designation.Editdesignation(deleteDesignationForm.sno);
                AppendDeleteAuditTrail(deleteDesignationForm.sno, found, (long)deleteDesignationForm.userid);
                designation.DeleteDesignation(deleteDesignationForm.sno);
                return this.GetSuccessResponse((long) deleteDesignationForm.sno);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}