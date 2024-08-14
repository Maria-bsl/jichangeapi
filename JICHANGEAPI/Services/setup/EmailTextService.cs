using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace JichangeApi.Services.setup
{
    public class EmailTextService
    {
        Payment pay = new Payment();
        private static readonly List<string> TABLE_COLUMNS = new List<string> { "sno", "flow_id", "email_text", "effective_date", "posted_by", "posted_date", "email_sub", "email_sub_local", "email_text_local" };
        private static readonly string TABLE_NAME = "Email Text";
        public static readonly List<string> FLOWS = new List<string> { "On Registration", "On Invoice Generation", "On Receipt", "On Invoice Cancellation", "On Invoice Ammendent", "On OTP", "On User Registration" };

        private void AppendInsertAuditTrail(long sno, EMAIL email, long userid)
        {
            var values = new List<string> { sno.ToString(), email.Flow_Id, email.Email_Text, DateTime.Now.ToString(), userid.ToString(), DateTime.Now.ToString(), email.Subject, email.Local_subject, email.Local_Text };
            Auditlog.InsertAuditTrail(values, userid, EmailTextService.TABLE_NAME, EmailTextService.TABLE_COLUMNS);
        }

        private void AppendUpdateAuditTrail(long sno,EMAIL oldEmail,EMAIL newEmail,long userid)
        {
            List<string> oldValues = new List<string> { sno.ToString(), oldEmail.Flow_Id, oldEmail.Email_Text, oldEmail.Effective_Date.ToString(), userid.ToString(), oldEmail.Audit_Date.ToString(), oldEmail.Subject, oldEmail.Local_subject, oldEmail.Local_Text };
            List<string> newValues = new List<string> { sno.ToString(), newEmail.Flow_Id, newEmail.Email_Text, DateTime.Now.ToString(), userid.ToString(), DateTime.Now.ToString(), newEmail.Subject, newEmail.Local_subject, newEmail.Local_Text };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, EmailTextService.TABLE_NAME, EmailTextService.TABLE_COLUMNS);
        }

        private void AppendDeleteAuditTrail(long sno, EMAIL fetchedEmail, long userid)
        {
            var values = new List<string> { sno.ToString(), fetchedEmail.Flow_Id, fetchedEmail.Email_Text, fetchedEmail.Effective_Date.ToString(), fetchedEmail.AuditBy, fetchedEmail.Audit_Date.ToString(), fetchedEmail.Subject, fetchedEmail.Local_subject, fetchedEmail.Local_Text };
            Auditlog.deleteAuditTrail(values, userid, EmailTextService.TABLE_NAME, EmailTextService.TABLE_COLUMNS);
        }
        private EMAIL CreateEmail(AddEmailForm addEmailForm)
        {
            EMAIL email = new EMAIL();
            email.Flow_Id = ((long)addEmailForm.flow).ToString();
            email.Email_Text = Utilites.RemoveSpecialCharacters(addEmailForm.text);
            email.Local_Text = Utilites.RemoveSpecialCharacters(addEmailForm.loctext);
            email.Subject = Utilites.RemoveSpecialCharacters(addEmailForm.sub);
            email.Local_subject = Utilites.RemoveSpecialCharacters(addEmailForm.subloc);
            email.SNO = (long)addEmailForm.sno;
            email.AuditBy = addEmailForm.userid.ToString();
            return email;
        }

        public EMAIL FindEmail(long sno)
        {
            try
            {
                EMAIL found = new EMAIL().FindEmail(sno);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                return found;
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        } 

        public EMAIL InsertEmail(AddEmailForm addEmailForm)
        {
            try
            {
                EMAIL email = CreateEmail(addEmailForm);
                bool isDuplicateFlow = email.IsExistFlowId(addEmailForm.flow.ToString());
                if (isDuplicateFlow) throw new ArgumentException("Flow already exists");
                long addedEmail = email.AddEMAIL(email);
                AppendInsertAuditTrail(addedEmail, email, (long)addEmailForm.userid);
                return FindEmail(addedEmail);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public EMAIL UpdateEmail(AddEmailForm addEmailForm)
        {
            try
            {
                EMAIL found = FindEmail((long)addEmailForm.sno);
                bool isDuplicateFlow = found.IsDuplicateFlow(addEmailForm.flow.ToString(), (long)addEmailForm.sno);
                if (isDuplicateFlow) throw new ArgumentException("Flow already exists");
                EMAIL email = CreateEmail(addEmailForm);
                long updatedEmail = email.UpdateEMAIL(email);
                AppendUpdateAuditTrail(updatedEmail, found, email, (long)addEmailForm.userid);
                return FindEmail(updatedEmail);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public JsonArray GetFlows()
        {
            try
            {
                JsonArray flows = new JsonArray();
                for (int i = 0; i < EmailTextService.FLOWS.Count(); i++)
                {
                    JsonObject flow = new JsonObject { 
                        { "label", EmailTextService.FLOWS.ElementAt(i) },
                        { "flow", i + 1 }
                    };
                    flows.Add(flow);
                }
                return flows;
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public List<EMAIL> GetEmailList()
        {
            try
            {
                List<EMAIL> results = new EMAIL().GetLatestEmailTextsList();
                return results != null ? results : new List<EMAIL>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public long DeleteEmail(long sno,long userid)
        {
            try
            {
                EMAIL found = FindEmail(sno);
                found.DeleteEMAIL(sno);
                AppendDeleteAuditTrail(sno, found, userid);
                return sno;
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
    }
}
