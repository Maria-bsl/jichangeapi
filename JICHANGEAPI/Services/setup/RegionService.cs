using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace JichangeApi.Services.setup
{
    public class RegionService
    {
        private static readonly List<string> tableColumns = new List<string> { "region_sno", "region_name", "country_sno", "country_name", "region_status", "posted_by", "posted_date" };
        private static readonly string tableName = "Region";
        private void AppendInsertRegionAuditTrail(long regionSno, REGION region, long userid)
        {
            List<string> insertAudits = new List<string> { regionSno.ToString(), region.Region_Name, region.Country_Sno.ToString(), region.Country_Name, region.Region_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(insertAudits, userid, RegionService.tableName, RegionService.tableColumns);
        }
        private void AppendUpdateRegionAuditTrail(long regionSno, REGION oldRegion, REGION newRegion, long userid)
        {
            List<string> oldValues = new List<string> { regionSno.ToString(), oldRegion.Region_Name, oldRegion.Country_Sno.ToString(), oldRegion.Country_Name, oldRegion.Region_Status, userid.ToString(), DateTime.Now.ToString() };
            List<string> newValues = new List<string> { regionSno.ToString(), newRegion.Region_Name, newRegion.Country_Sno.ToString(), newRegion.Country_Name, newRegion.Region_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, RegionService.tableName, RegionService.tableColumns);
        }
        private void AppendDeleteRegionAuditTrail(long regionSno, REGION region, long userid)
        {
            List<string> values = new List<string> { regionSno.ToString(), region.Region_Name, region.Country_Sno.ToString(), region.Country_Name, region.Region_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, RegionService.tableName, RegionService.tableColumns);
        }
        private REGION CreateRegion(AddRegionForm addRegionForm)
        {
            try
            {
                COUNTRY foundCountry = new COUNTRY().GETcountries().Find(c => c.SNO == addRegionForm.csno);
                if (foundCountry == null) throw new ArgumentException("Country not found");
                REGION region = new REGION();
                region.Region_SNO = addRegionForm.sno;
                region.Region_Name = addRegionForm.region;
                region.Country_Sno = addRegionForm.csno;
                region.Country_Name = foundCountry.Country_Name;
                region.Region_Status = addRegionForm.Status;
                region.AuditBy = addRegionForm.userid.ToString();
                return region;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public List<REGION> GetRegionsList()
        {
            try
            {
                var results = new REGION().GetReg();
                return results != null ? results : new List<REGION>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public REGION GetRegionById(long regionId)
        {
            try
            {
                REGION region = new REGION();
                bool exists = region.isExistRegion(regionId);
                if (!exists) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                REGION found = region.EditREGION(regionId);
                return found;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public REGION InsertRegion(AddRegionForm addRegionForm)
        {
            try
            {
                REGION region = this.CreateRegion(addRegionForm);
                var isExistRegion = region.Validatedupicate(region.Region_Name.ToLower());
                if (isExistRegion) throw new ArgumentException(SetupBaseController.ALREADY_EXISTS_MESSAGE);
                long addedRegion = region.AddREGION(region);
                AppendInsertRegionAuditTrail(addedRegion, region, (long)addRegionForm.userid);
                return FindRegion(addedRegion);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public REGION UpdateRegion(AddRegionForm addRegionForm)
        {
            try
            {
                REGION found = FindRegion(addRegionForm.sno);
                REGION region = this.CreateRegion(addRegionForm);
                bool isDuplicateRegion = region.isDuplicatedRegion(region.Region_Name, region.Region_SNO, region.Country_Sno);
                if (isDuplicateRegion) throw new ArgumentException(SetupBaseController.ALREADY_EXISTS_MESSAGE);
                REGION oldRegion = region.EditREGION(addRegionForm.sno);
                long updatedRegion = region.UpdateREGION(region);
                AppendUpdateRegionAuditTrail(updatedRegion, oldRegion, region, (long)addRegionForm.userid);
                return this.FindRegion(region.Region_SNO);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public REGION FindRegion(long sno)
        {
            try
            {
                REGION found = new REGION().EditREGION(sno);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                return found;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public long DeleteRegion(long sno,long userid)
        {
            try
            {
                REGION found = FindRegion(sno);
                AppendDeleteRegionAuditTrail(sno, found, userid);
                found.DeleteREGION(sno);
                return sno;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
