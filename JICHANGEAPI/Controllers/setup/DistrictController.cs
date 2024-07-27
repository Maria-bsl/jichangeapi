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
    public class DistrictController : SetupBaseController
    {
        private static readonly List<string> tableColumns = new List<string> { "district_sno", "district_name", "region_id", "district_status", "posted_by", "posted_date" };
        private static readonly string tableName = "District";

        [HttpPost]
        public HttpResponseMessage GetdIST()
        {
            DISTRICTS district = new DISTRICTS();
            try
            {
                List<DISTRICTS> results = district.GetDistrict();
                return this.GetList<List<DISTRICTS>, DISTRICTS>(results);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        private DISTRICTS CreateDistrict(AddDistrictForm addDistrictForm,REGION region)
        {
            DISTRICTS district = new DISTRICTS();
            district.SNO = (long)addDistrictForm.sno;
            district.District_Name = addDistrictForm.district_name;
            district.Region_Id = region.Region_SNO;
            district.Region_Name = region.Region_Name;
            district.AuditBy = addDistrictForm.userid.ToString();
            district.District_Status = addDistrictForm.district_status;
            return district;
        }
        private void AppendInsertDistrictAuditTrail(long districtSno, DISTRICTS district, long userid)
        {
            List<string> values = new List<string> { districtSno.ToString(), district.District_Name, district.Region_Id.ToString(), district.District_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, DistrictController.tableName, DistrictController.tableColumns);
        }
        private void AppendUpdateDistrictAuditTrail(long districtSno,DISTRICTS oldValue,DISTRICTS newValue,long userid)
        {
            List<string> oldValues = new List<string> { districtSno.ToString(), oldValue.District_Name, oldValue.Region_Id.ToString(), oldValue.District_Status, userid.ToString(), DateTime.Now.ToString() };
            List<string> newValues = new List<string> { districtSno.ToString(), newValue.District_Name, newValue.Region_Id.ToString(), newValue.District_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, DistrictController.tableName, DistrictController.tableColumns);
        }
        private void AppendDeleteRegionAuditTrail(long districtSno, DISTRICTS district, long userid)
        {
            List<string> values = new List<string> { districtSno.ToString(), district.District_Name, district.Region_Id.ToString(), district.District_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, DistrictController.tableName, DistrictController.tableColumns);
        }
        private HttpResponseMessage InsertDistrict(DISTRICTS district,AddDistrictForm addDistrictForm)
        {
            try
            {
                var isExistDistrict = district.Validateduplicatechecking(addDistrictForm.district_name);
                if (isExistDistrict) return this.GetAlreadyExistsErrorResponse();
                long addedRegion = district.AddDistrict(district);
                AppendInsertDistrictAuditTrail(addedRegion, district, (long)addDistrictForm.userid);
                return FindDistrict(addedRegion);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        private HttpResponseMessage UpdateDistrict(DISTRICTS district,AddDistrictForm addDistrictForm)
        {
            try
            {
                bool exists = district.isExistDistrict((long) addDistrictForm.sno);
                if (!exists) return this.GetNotFoundResponse();
                bool isDuplicate = district.isDuplicateDistrict(district.District_Name, district.SNO, district.Region_Id);
                DISTRICTS oldDistrict = district.EditDISTRICTS((long) addDistrictForm.sno);
                long updatedDistrict = district.UpdateDISTRICTS(district);
                AppendUpdateDistrictAuditTrail(updatedDistrict, oldDistrict, district, (long) addDistrictForm.userid);
                return FindDistrict(updatedDistrict);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddDistrict(AddDistrictForm addDistrictForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                REGION foundRegion = new REGION().GetReg().Find(c => c.Region_SNO == addDistrictForm.region_id);
                if (foundRegion == null)
                {
                    var messages = new List<string> { "Region not found" };
                    this.GetCustomErrorMessageResponse(messages);
                }
                DISTRICTS district = CreateDistrict(addDistrictForm, foundRegion);
                if (addDistrictForm.sno == 0) { return InsertDistrict(district, addDistrictForm); }
                else { return UpdateDistrict(district,addDistrictForm); }
            }
            catch(Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage FindDistrict(long sno)
        {
            try
            {
                DISTRICTS district = new DISTRICTS();
                bool exists = district.isExistDistrict(sno);
                if (!exists) return this.GetNotFoundResponse();
                DISTRICTS found = district.EditDISTRICTS(sno);
                return this.GetSuccessResponse(found);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage DeleteDist(DeleteDistrictForm deleteDistrictForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                DISTRICTS district = new DISTRICTS();
                bool isExistDistrict = district.isExistDistrict(deleteDistrictForm.sno);
                if (!isExistDistrict) return this.GetNotFoundResponse();
                DISTRICTS found = district.EditDISTRICTS((long)deleteDistrictForm.sno);
                AppendDeleteRegionAuditTrail((long)deleteDistrictForm.sno, found, (long) deleteDistrictForm.userid);
                district.DeleteDISTRICTS(deleteDistrictForm.sno);
                return this.GetSuccessResponse(deleteDistrictForm.sno);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}