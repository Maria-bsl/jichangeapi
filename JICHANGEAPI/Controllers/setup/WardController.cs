using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;

namespace JichangeApi.Controllers.setup
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WardController : SetupBaseController
    {
        private readonly static List<string> tableColumns = new List<string> { "ward_sno", "ward_name", "region_id", "district_sno", "ward_status", "posted_by", "posted_date" };
        private readonly static string tableName = "Ward";

        [HttpPost]
        public HttpResponseMessage GetWard()
        {
            WARD ward = new WARD();
            try
            {
                List<WARD> wards = ward.GetWARD();
                return this.GetList<List<WARD>, WARD>(wards);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        private WARD CreateWard(AddWardForm addWardForm,DISTRICTS district)
        {
            WARD ward = new WARD();
            ward.SNO = (long)addWardForm.sno;
            ward.Ward_Name = addWardForm.ward_name;
            ward.Region_Name = district.Region_Name;
            ward.District_Name = district.District_Name;
            ward.Region_Id = district.Region_Id;
            ward.District_Sno = district.SNO;
            ward.Ward_Status = addWardForm.ward_status;
            return ward;
        }
        private void AppendInsertAuditTrail(long wardSno, WARD ward, long userid)
        {
            List<string> values = new List<string> { wardSno.ToString(), ward.Ward_Name, ward.Region_Id.ToString(), ward.District_Sno.ToString(), ward.Ward_Status , userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, WardController.tableName, WardController.tableColumns);
        }
        private void AppendUpdateAuditTrail(long wardSno, WARD oldWard, WARD newWard, long userid)
        {
            List<string> oldValues = new List<string> { wardSno.ToString(), oldWard.Ward_Name, oldWard.Region_Id.ToString(), oldWard.District_Sno.ToString(), oldWard.Ward_Status, userid.ToString(), DateTime.Now.ToString() };
            List<string> newValues = new List<string> { wardSno.ToString(), newWard.Ward_Name, newWard.Region_Id.ToString(), newWard.District_Sno.ToString(), newWard.Ward_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, WardController.tableName, WardController.tableColumns);
        }
        private void AppendDeleteAuditTrail(long wardSno, WARD ward, long userid)
        {
            List<string> values = new List<string> { wardSno.ToString(), ward.Ward_Name, ward.Region_Id.ToString(), ward.District_Sno.ToString(), ward.Ward_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, WardController.tableName, WardController.tableColumns);
        }
        private HttpResponseMessage InsertWard(WARD ward,AddWardForm addWardForm)
        {
            try
            {
                var isExistWard = ward.ValidateWARD(ward.District_Sno, ward.Ward_Name, ward.Region_Id);
                if (isExistWard) return this.GetAlreadyExistsErrorResponse();
                long addedWard = ward.AddWARD(ward);
                AppendInsertAuditTrail(addedWard, ward, (long) addWardForm.userid);
                return FindWard(addedWard);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        private HttpResponseMessage UpdateWard(WARD ward, AddWardForm addWardForm)
        {
            try
            {
                bool isExist = ward.isExistWard((long) addWardForm.sno);
                if (!isExist) return this.GetNotFoundResponse();
                bool isDuplicate = ward.isDuplicateWard(ward.District_Sno, ward.Ward_Name, ward.Region_Id);
                WARD oldWard = ward.EditWARD((long) addWardForm.sno);
                long updateWard = ward.UpdateWARD(ward);
                AppendUpdateAuditTrail(updateWard, oldWard, ward, (long) addWardForm.userid);
                return FindWard(updateWard);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddWard(AddWardForm addWardForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                DISTRICTS foundDistrict = new DISTRICTS().GetDistrict().Find(c => c.SNO == addWardForm.district_sno);
                if (foundDistrict == null)
                {
                    var messages = new List<string> { "District not found" };
                    this.GetCustomErrorMessageResponse(messages);
                }
                WARD ward = CreateWard(addWardForm, foundDistrict);
                if (addWardForm.sno == 0) { return InsertWard(ward,addWardForm);  }
                else { return UpdateWard(ward,addWardForm); }
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage FindWard(long sno)
        {
            try
            {
                WARD ward = new WARD();
                bool exists = ward.isExistWard(sno);
                if (!exists) return this.GetNotFoundResponse();
                WARD found = ward.EditWARD(sno);
                return this.GetSuccessResponse(found);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage DeleteWard(RemoveWardForm removeWardForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                WARD ward = new WARD();
                bool isExistWard = ward.isExistWard(removeWardForm.sno);
                if (!isExistWard) return this.GetNotFoundResponse();
                WARD found = ward.EditWARD((long) removeWardForm.sno);
                AppendDeleteAuditTrail(removeWardForm.sno, found, (long) removeWardForm.userid);
                ward.DeleteWARD(removeWardForm.sno);
                return this.GetSuccessResponse( (long) removeWardForm.sno);
            } 
            catch(Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}