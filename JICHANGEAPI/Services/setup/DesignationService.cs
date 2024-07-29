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
using static iTextSharp.text.pdf.PdfDocument;

namespace JichangeApi.Services.setup
{
    public class DesignationService
    {
        private static readonly List<string> tableColumns = new List<string> { "desg_id", "desg_name", "posted_by", "posted_date" };
        private static readonly string tableName = "Designation";
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
            Auditlog.InsertAuditTrail(values, userid, DesignationService.tableName, DesignationService.tableColumns);
        }

        private void AppendUpdateAuditTrail(long designationSno, DESIGNATION oldDesignation, DESIGNATION newDesignation, long userid)
        {
            var oldValues = new List<string> { designationSno.ToString(), oldDesignation.Desg_Name, oldDesignation.AuditBy, oldDesignation.Audit_Date.ToString() };
            var newValues = new List<string> { designationSno.ToString(), newDesignation.Desg_Name, newDesignation.AuditBy, DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, DesignationService.tableName, DesignationService.tableColumns);

        }

        private void AppendDeleteAuditTrail(long designationSno, DESIGNATION designation, long userid)
        {
            List<string> values = new List<string> { designationSno.ToString(), designation.Desg_Name, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, DesignationService.tableName, DesignationService.tableColumns);
        }
        public DESIGNATION FindDesignation(long sno)
        {
            try
            {
                DESIGNATION found = new DESIGNATION().Editdesignation(sno);
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
        public DESIGNATION InsertDesignation(AddDesignationForm addDesignationForm)
        {
            try
            {
                DESIGNATION designation = CreateDesignation(addDesignationForm);
                var isExist = designation.ValidateDesignation(addDesignationForm.desg);
                if (isExist) throw new ArgumentException(SetupBaseController.ALREADY_EXISTS_MESSAGE);
                var addedDesignation = designation.AddUser(designation);
                AppendInsertAuditTrail(addedDesignation, designation, (long)addDesignationForm.userid);
                return FindDesignation(addedDesignation);
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
        public DESIGNATION UpdateDesignation(AddDesignationForm addDesignationForm)
        {
            try
            {
                DESIGNATION designation = CreateDesignation(addDesignationForm);
                var isExist = designation.isExistDesignation((long)addDesignationForm.sno);
                if (!isExist) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
                var isDuplicate = designation.isDuplicate(addDesignationForm.desg, (long)addDesignationForm.sno);
                if (isDuplicate) { throw new ArgumentException(SetupBaseController.ALREADY_EXISTS_MESSAGE); }
                DESIGNATION oldDesignation = designation.getDesignationText((long)addDesignationForm.sno);
                long updateDesignation = designation.UpdateDesignation(designation);
                AppendUpdateAuditTrail(updateDesignation, oldDesignation, designation, (long)addDesignationForm.userid);
                return FindDesignation(updateDesignation);
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
        public long DeleteDesignation(long sno,long userid)
        {
            DESIGNATION designation = new DESIGNATION();
            try
            {
                DESIGNATION found = designation.Editdesignation(sno);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                AppendDeleteAuditTrail(sno, found, userid);
                designation.DeleteDesignation(sno);
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
        public List<DESIGNATION> GetDesignationList()
        {
            try
            {
                var results = new DESIGNATION().GetDesignation();
                return results != null ? results : new List<DESIGNATION>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
