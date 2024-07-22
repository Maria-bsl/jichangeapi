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
    public class RegionController : SetupBaseController
    {
        private static readonly List<string> tableColumns = new List<string> { "region_sno", "region_name", "country_sno", "country_name", "region_status", "posted_by", "posted_date" };
        private static readonly string tableName = "Region";


        [HttpPost]
        public HttpResponseMessage GetRegionDetails()
        {
            REGION region = new REGION();
            try
            {
                var results = region.GetReg();
                return this.GetList<List<REGION>, REGION>(results);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        private void AppendInsertRegionAuditTrail(long regionSno, REGION region, long userid)
        {
            List<string> insertAudits = new List<string> { regionSno.ToString(), region.Region_Name, region.Country_Sno.ToString(), region.Country_Name, region.Region_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(insertAudits, userid, RegionController.tableName, RegionController.tableColumns);
        }
        private void AppendUpdateRegionAuditTrail(long regionSno, REGION oldRegion, REGION newRegion, long userid)
        {
            List<string> oldValues = new List<string> { regionSno.ToString(), oldRegion.Region_Name, oldRegion.Country_Sno.ToString(), oldRegion.Country_Name, oldRegion.Region_Status, userid.ToString(), DateTime.Now.ToString() };
            List<string> newValues = new List<string> { regionSno.ToString(), newRegion.Region_Name, newRegion.Country_Sno.ToString(), newRegion.Country_Name, newRegion.Region_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, RegionController.tableName, RegionController.tableColumns);
        }
        private void AppendDeleteRegionAuditTrail(long regionSno,REGION region,long userid)
        {
            List<string> values = new List<string> { regionSno.ToString(), region.Region_Name, region.Country_Sno.ToString(), region.Country_Name, region.Region_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, RegionController.tableName, RegionController.tableColumns);
        }
        private REGION CreateRegion(AddRegionForm addRegionForm,COUNTRY country)
        {            
            REGION region = new REGION();
            region.Region_SNO = addRegionForm.sno;
            region.Region_Name = addRegionForm.region;
            region.Country_Sno = addRegionForm.csno;
            region.Country_Name = country.Country_Name;
            region.Region_Status = addRegionForm.Status;
            region.AuditBy = addRegionForm.userid.ToString();
            return region;
        }
        private HttpResponseMessage InsertRegion(REGION region, AddRegionForm addRegionForm)
        {
            try
            {
                var isExistRegion = region.Validatedupicate(region.Region_Name.ToLower());
                if (isExistRegion) return this.GetAlreadyExistsErrorResponse();
                long addedRegion = region.AddREGION(region);
                AppendInsertRegionAuditTrail(addedRegion, region, (long) addRegionForm.userid);
                return FindRegion(addedRegion);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        private HttpResponseMessage UpdateRegion(REGION region, AddRegionForm addRegionForm)
        {
            try
            {
                bool exists = region.isExistRegion(region.Region_SNO);
                if (!exists) return this.GetNotFoundResponse();
                bool isDuplicateRegion = region.isDuplicatedRegion(region.Region_Name, region.Region_SNO, region.Country_Sno);
                if (isDuplicateRegion) return this.GetAlreadyExistsErrorResponse();
                REGION oldRegion = region.EditREGION(addRegionForm.sno);
                long updatedRegion = region.UpdateREGION(region);
                AppendUpdateRegionAuditTrail(updatedRegion, oldRegion, region, (long)addRegionForm.userid);
                return this.FindRegion(region.Region_SNO);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddRegion(AddRegionForm addRegionForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                COUNTRY foundCountry = new COUNTRY().GETcountries().Find(c => c.SNO == addRegionForm.csno);
                if (foundCountry == null)
                {
                    var messages = new List<string> { "Country not found" };
                    this.GetCustomErrorMessageResponse(messages);
                }
                REGION region = this.CreateRegion(addRegionForm, foundCountry);
                if (addRegionForm.sno == 0) { return InsertRegion(region,addRegionForm); }
                else { return UpdateRegion(region,addRegionForm); }
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage FindRegion(long sno)
        {
            try
            {
                REGION region = new REGION();
                bool exists = region.isExistRegion(sno);
                if (!exists) return this.GetNotFoundResponse();
                REGION found = region.EditREGION(sno);
                return this.GetSuccessResponse(found);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage DeleteRegion(DeleteRegionForm deleteRegionForm)
        {
            try
            {
                List<string> modelStateErrors = this.ModelStateErrors();
                if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
                REGION region = new REGION();
                var isExistRegion = region.isExistRegion(deleteRegionForm.sno);
                if (!isExistRegion) return this.GetNotFoundResponse();
                REGION found = region.EditREGION((long) deleteRegionForm.sno);
                AppendDeleteRegionAuditTrail(deleteRegionForm.sno, found, (long) deleteRegionForm.userid);
                region.DeleteREGION(deleteRegionForm.sno);
                return this.GetSuccessResponse(deleteRegionForm.sno);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}