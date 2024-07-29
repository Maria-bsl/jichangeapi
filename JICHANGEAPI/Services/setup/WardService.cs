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

namespace JichangeApi.Services.setup
{
    public class WardService
    {
        private readonly static List<string> tableColumns = new List<string> { "ward_sno", "ward_name", "region_id", "district_sno", "ward_status", "posted_by", "posted_date" };
        private readonly static string tableName = "Ward";
        private void AppendInsertAuditTrail(long wardSno, WARD ward, long userid)
        {
            List<string> values = new List<string> { wardSno.ToString(), ward.Ward_Name, ward.Region_Id.ToString(), ward.District_Sno.ToString(), ward.Ward_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, WardService.tableName, WardService.tableColumns);
        }
        private void AppendUpdateAuditTrail(long wardSno, WARD oldWard, WARD newWard, long userid)
        {
            List<string> oldValues = new List<string> { wardSno.ToString(), oldWard.Ward_Name, oldWard.Region_Id.ToString(), oldWard.District_Sno.ToString(), oldWard.Ward_Status, userid.ToString(), DateTime.Now.ToString() };
            List<string> newValues = new List<string> { wardSno.ToString(), newWard.Ward_Name, newWard.Region_Id.ToString(), newWard.District_Sno.ToString(), newWard.Ward_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, WardService.tableName, WardService.tableColumns);
        }
        private void AppendDeleteAuditTrail(long wardSno, WARD ward, long userid)
        {
            List<string> values = new List<string> { wardSno.ToString(), ward.Ward_Name, ward.Region_Id.ToString(), ward.District_Sno.ToString(), ward.Ward_Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, WardService.tableName, WardService.tableColumns);
        }
        private WARD CreateWard(AddWardForm addWardForm)
        {
            try
            {
                DISTRICTS foundDistrict = new DISTRICTS().GetDistrict().Find(c => c.SNO == addWardForm.district_sno);
                if (foundDistrict == null) throw new ArgumentException("District not found");
                WARD ward = new WARD();
                ward.SNO = (long)addWardForm.sno;
                ward.Ward_Name = addWardForm.ward_name;
                ward.Region_Name = foundDistrict.Region_Name;
                ward.District_Name = foundDistrict.District_Name;
                ward.Region_Id = foundDistrict.Region_Id;
                ward.District_Sno = foundDistrict.SNO;
                ward.Ward_Status = addWardForm.ward_status;
                return ward;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public List<WARD> GetActiveWard(long wardSno)
        {
            try
            {
                WARD ward = new WARD();
                List<WARD> result = ward.GetWARDAct(wardSno);
                if (result != null) return result;
                return new List<WARD>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public WARD GetWardById(long wardId)
        {
            try
            {
                WARD ward = new WARD();
                WARD found = ward.EditWARD(wardId);
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
        public List<WARD> GetWardList()
        {
            try
            {
                var results = new WARD().GetWARD();
                return results != null ? results : new List<WARD>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public WARD FindWard(long sno)
        {
            try
            {
                WARD found = new WARD().EditWARD(sno);
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
        public WARD InsertWard(AddWardForm addWardForm)
        {
            try
            {
                WARD ward = CreateWard(addWardForm);
                var isExistWard = ward.ValidateWARD(ward.District_Sno, ward.Ward_Name, ward.Region_Id);
                if (isExistWard) throw new ArgumentException(SetupBaseController.ALREADY_EXISTS_MESSAGE);
                long addedWard = ward.AddWARD(ward);
                AppendInsertAuditTrail(addedWard, ward, (long)addWardForm.userid);
                return FindWard(addedWard);
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
        public WARD UpdateWard(AddWardForm addWardForm)
        {
            try
            {
                WARD found = FindWard((long)addWardForm.sno);
                WARD ward = CreateWard(addWardForm);
                bool isDuplicate = ward.isDuplicateWard(ward.District_Sno, ward.Ward_Name, ward.Region_Id);
                long updateWard = ward.UpdateWARD(ward);
                AppendUpdateAuditTrail(updateWard, found, ward, (long)addWardForm.userid);
                return FindWard(updateWard);
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
        public long DeleteWard(long sno,long userid)
        {
            try
            {
                WARD found = FindWard((long)sno);
                AppendDeleteAuditTrail(sno, found, userid);
                found.DeleteWARD(sno);
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
