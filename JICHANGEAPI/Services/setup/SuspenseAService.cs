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
    public class SuspenseAService
    {
        private static readonly string TABLE_NAME = "Suspense_Account";
        private static readonly List<string> TABLE_COLUMNS = new List<string> { "sus_acc_sno", "sus_acc_no", "sus_acc_status", "posted_by", "posted_date" };

        private void AppendInsertAuditTrail(long sno, S_Account susAcc, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), susAcc.Sus_Acc_No,susAcc.Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, TABLE_NAME, TABLE_COLUMNS);
        }

        private void AppendUpdateAuditTrail(long sno, S_Account oldSusAcc, S_Account newSusAcc, long userid)
        {
            List<string> oldValues = new List<string> { sno.ToString(), oldSusAcc.Sus_Acc_No, oldSusAcc.Status, userid.ToString(), DateTime.Now.ToString() };
            List<string> newValues = new List<string> { sno.ToString(), newSusAcc.Sus_Acc_No, oldSusAcc.Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, TABLE_NAME, TABLE_COLUMNS);
        }

        private void AppendDeleteAuditTrail(long sno, S_Account account, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), account.Sus_Acc_No, account.Status, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, TABLE_NAME, TABLE_COLUMNS);
        }

        public List<S_Account> GetAccounts()
        {
            try
            {
                var results = new S_Account().GetAccounts();
                return results != null ? results : new List<S_Account>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<S_Account> GetActiveAccounts()
        {
            try
            {
                var results = new S_Account().GetAccounts_Active();
                return results != null ? results : new List<S_Account>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private S_Account CreateSuspenseAccount(AddSuspenseAccountForm addSuspenseAccountForm)
        {
            S_Account suspenseAccount = new S_Account();
            suspenseAccount.Sus_Acc_No = addSuspenseAccountForm.account;
            suspenseAccount.Status = addSuspenseAccountForm.status;
            suspenseAccount.AuditBy = addSuspenseAccountForm.userid.ToString();
            suspenseAccount.Sus_Acc_Sno = (long)addSuspenseAccountForm.sno;
            return suspenseAccount;
        }

        public S_Account InsertSuspenseAccount(AddSuspenseAccountForm addSuspenseAccountForm)
        {
            try
            {
                var suspenseAccount = CreateSuspenseAccount(addSuspenseAccountForm);
                bool isExist = suspenseAccount.ValidateAccount(suspenseAccount.Sus_Acc_No.ToLower());
                if (isExist) throw new ArgumentException(SetupBaseController.ALREADY_EXISTS_MESSAGE);
                long addedSuspenseAccount = suspenseAccount.AddAccount(suspenseAccount);
                AppendInsertAuditTrail(addedSuspenseAccount, suspenseAccount,(long) addSuspenseAccountForm.userid);
                return FindSuspenseAccount(addedSuspenseAccount);
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

        public S_Account UpdateSuspenseAccount(AddSuspenseAccountForm addSuspenseAccountForm)
        {
            try
            {
                var found = FindSuspenseAccount((long) addSuspenseAccountForm.sno);
                var suspenseAccount = CreateSuspenseAccount(addSuspenseAccountForm);
                bool isDuplicate = suspenseAccount.isDuplicateAccountNumber(addSuspenseAccountForm.account, (long)addSuspenseAccountForm.sno);
                if (isDuplicate) { throw new ArgumentException(SetupBaseController.ALREADY_EXISTS_MESSAGE); }
                long updatedSno = suspenseAccount.UpdateAccount(suspenseAccount);
                AppendUpdateAuditTrail((long)addSuspenseAccountForm.sno, found, suspenseAccount, (long)addSuspenseAccountForm.userid);
                return FindSuspenseAccount((long)addSuspenseAccountForm.sno);
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

        public S_Account FindSuspenseAccount(long sno)
        {
            try
            {
                S_Account found = new S_Account().EditAccount(sno);
                if (found == null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);  }
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

        public long DeleteSuspenseAccount(long sno,long userid)
        {
            try
            {
                var found = FindSuspenseAccount(sno);
                AppendDeleteAuditTrail(sno, found, userid);
                found.DeleteAccount(sno);
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
