using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace JichangeApi.Services.setup
{
    public class DistrictService
    {
        private static readonly List<string> tableColumns = new List<string> { "district_sno", "district_name", "region_id", "district_status", "posted_by", "posted_date" };
        private static readonly string tableName = "District";
        private void AppendInsertDistrictAuditTrail(long districtSno, DISTRICTS district, long userid)
        {
            List<string> values = new List<string> { districtSno.ToString(), district.District_Name, district.Region_Id.ToString(), district.District_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, DistrictService.tableName, DistrictService.tableColumns);
        }
        private void AppendUpdateDistrictAuditTrail(long districtSno, DISTRICTS oldValue, DISTRICTS newValue, long userid)
        {
            List<string> oldValues = new List<string> { districtSno.ToString(), oldValue.District_Name, oldValue.Region_Id.ToString(), oldValue.District_Status, userid.ToString(), DateTime.Now.ToString() };
            List<string> newValues = new List<string> { districtSno.ToString(), newValue.District_Name, newValue.Region_Id.ToString(), newValue.District_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, DistrictService.tableName, DistrictService.tableColumns);
        }
        private void AppendDeleteRegionAuditTrail(long districtSno, DISTRICTS district, long userid)
        {
            List<string> values = new List<string> { districtSno.ToString(), district.District_Name, district.Region_Id.ToString(), district.District_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, DistrictService.tableName, DistrictService.tableColumns);
        }
        private DISTRICTS CreateDistrict(AddDistrictForm addDistrictForm)
        {
            try
            {
                REGION foundRegion = new REGION().GetReg().Find(c => c.Region_SNO == addDistrictForm.region_id);
                if (foundRegion == null) throw new ArgumentException("Region not found");
                DISTRICTS district = new DISTRICTS();
                district.SNO = (long)addDistrictForm.sno;
                district.District_Name = addDistrictForm.district_name;
                district.Region_Id = foundRegion.Region_SNO;
                district.Region_Name = foundRegion.Region_Name;
                district.AuditBy = addDistrictForm.userid.ToString();
                district.District_Status = addDistrictForm.district_status;
                return district;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public List<DISTRICTS> GetActiveDistrict(long districtSno)
        {
            try
            {
                DISTRICTS distrcits = new DISTRICTS();
                List<DISTRICTS> results = distrcits.GetDistrictActive(districtSno);
                if (results != null) { return results; }
                return new List<DISTRICTS>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DISTRICTS GetDistrictById(long districtId)
        {
            try
            {
                DISTRICTS district = new DISTRICTS();
                DISTRICTS found = district.EditDISTRICTS(districtId);
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
        public DISTRICTS InsertDistrict(AddDistrictForm addDistrictForm)
        {
            try
            {
                DISTRICTS district = CreateDistrict(addDistrictForm);
                var isExistDistrict = district.Validateduplicatechecking(addDistrictForm.district_name);
                if (isExistDistrict) throw new ArgumentException(SetupBaseController.ALREADY_EXISTS_MESSAGE);
                long addedRegion = district.AddDistrict(district);
                AppendInsertDistrictAuditTrail(addedRegion, district, (long)addDistrictForm.userid);
                return FindDistrict(addedRegion);
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
        public DISTRICTS UpdateDistrict(AddDistrictForm addDistrictForm)
        {
            try
            {
                DISTRICTS found = FindDistrict((long) addDistrictForm.sno);
                DISTRICTS district = CreateDistrict(addDistrictForm);
                bool isDuplicate = district.isDuplicateDistrict(district.District_Name, district.SNO, district.Region_Id);
                DISTRICTS oldDistrict = district.EditDISTRICTS((long)addDistrictForm.sno);
                long updatedDistrict = district.UpdateDISTRICTS(district);
                AppendUpdateDistrictAuditTrail(updatedDistrict, oldDistrict, district, (long)addDistrictForm.userid);
                return FindDistrict(updatedDistrict);
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
        public DISTRICTS FindDistrict(long sno)
        {
            try
            {
                DISTRICTS found = new DISTRICTS().EditDISTRICTS(sno);
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
        public long DeleteDistrict(long sno,long userid)
        {
            try
            {
                DISTRICTS found = FindDistrict(sno);
                AppendDeleteRegionAuditTrail(sno, found, userid);
                found.DeleteDISTRICTS(sno);
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
        public List<DISTRICTS> GetDistrictList()
        {
            try
            {
                var results = new DISTRICTS().GetDistrict();
                return results != null ? results : new List<DISTRICTS>();   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
