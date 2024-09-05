using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models.form.setup.insert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services.setup
{
    public class BranchService
    {
        private static readonly List<string> TABLE_COLUMNS = new List<string> { "sno", "name", "location", "status" ,"posted_by", "posted_date" };
        private static readonly string TABLE_NAME = "Branch_Name";
        public static void AppendInsertAuditTrail(long sno, BranchM branch, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), branch?.Name, branch?.Location, branch?.Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, TABLE_NAME, TABLE_COLUMNS);
        }
        public static void AppendUpdateAuditTrail(long sno, BranchM oldBranch, BranchM newBranch, long userid)
        {
            List<string> oldValues = new List<string> { sno.ToString(), oldBranch?.Name, oldBranch?.Location, oldBranch?.Status, userid.ToString(), DateTime.Now.ToString() };
            List<string> newValues = new List<string> { sno.ToString(), newBranch?.Name, newBranch?.Location, newBranch?.Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, TABLE_NAME, TABLE_COLUMNS);

        }
        public static void AppendDeleteAuditTrail(long sno, BranchM branch, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), branch?.Name, branch?.Location, branch?.Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, TABLE_NAME, TABLE_COLUMNS);
        }
        private BranchM createBranchM(AddBranchForm addBranchForm)
        {
            BranchM branchM = new BranchM();
            branchM.Sno = addBranchForm.Branch_Sno;
            branchM.Name = addBranchForm.Name;
            branchM.Location = addBranchForm.Location;
            branchM.Status = addBranchForm.Status;
            branchM.AuditBy = addBranchForm.AuditBy;
            return branchM;
        }

        public BranchM FindBranchById(long sno)
        {
            try
            {
                BranchM found = new BranchM().EditBranch(sno);
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

        public BranchM InsertBranch(AddBranchForm addBranchForm)
        {
            try
            {
                BranchM branchM = createBranchM(addBranchForm);
                bool existsBranch = branchM.ValidateBranch(branchM.Name);
                if (existsBranch) throw new ArgumentException(SetupBaseController.ALREADY_EXISTS_MESSAGE);
                long addedBranch = branchM.AddBranch(branchM);
                AppendInsertAuditTrail(addedBranch, branchM, long.Parse(addBranchForm.AuditBy));
                return FindBranchById(addedBranch);
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
        public BranchM UpdateBranch(AddBranchForm addBranchForm)
        {
            try
            {
                var found = FindBranchById(addBranchForm.Branch_Sno);
                BranchM branchM = createBranchM(addBranchForm);
                bool isDuplicatedName = branchM.IsDuplicatedName(branchM.Name, branchM.Sno);
                if (isDuplicatedName) throw new ArgumentException(SetupBaseController.ALREADY_EXISTS_MESSAGE);
                long updatedBranch = branchM.UpdateBranch(branchM);
                AppendUpdateAuditTrail(addBranchForm.Branch_Sno, found, branchM, long.Parse(addBranchForm.AuditBy));
                return FindBranchById(updatedBranch);
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

        public long DeleteBranch(long sno,long userid)
        {
            try
            {
                var found = FindBranchById(sno);
                AppendDeleteAuditTrail(sno, found, userid);
                found.DeleteBranch(sno);
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

        public List<BranchM> GetBranchLists()
        {
            var branch = new BranchM();
            try
            {
                var results = new BranchM().GetBranches();
                return results != null ? results : new List<BranchM>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
