using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Services.setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JichangeApi.Services
{
    public class SmsTextService
    {
        private readonly static List<string> TABLE_COLUMNS = new List<string> { "sno", "flow_id", "sms_text", "effective_date", "sms_sub", "sms_sub_local", "sms_text_other", "posted_by", "posted_date" };
        private readonly static string TABLE_NAME = "SMS_TEXT";

        private void AppendInsertAuditTrail(long sno, SMS_TEXT smsText, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), smsText.Flow_Id, smsText.SMS_Text, smsText.Effective_Date.ToString(), smsText.SMS_Subject, smsText.SMS_Local, smsText.SMS_Other, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, SmsTextService.TABLE_NAME, SmsTextService.TABLE_COLUMNS);
        }

        private void AppendUpdateAuditTrail(long sno, SMS_TEXT oldSmsText, SMS_TEXT newSmsText, long userid)
        {
            List<string> oldValues = new List<string> { sno.ToString(), oldSmsText.Flow_Id, oldSmsText.SMS_Text, oldSmsText.Effective_Date.ToString(), oldSmsText.SMS_Subject, oldSmsText.SMS_Local, oldSmsText.SMS_Other, userid.ToString(), DateTime.Now.ToString() };
            List<string> newValues = new List<string> { sno.ToString(), newSmsText.Flow_Id, newSmsText.SMS_Text, newSmsText.Effective_Date.ToString(), newSmsText.SMS_Subject, newSmsText.SMS_Local, newSmsText.SMS_Other, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, SmsTextService.TABLE_NAME, SmsTextService.TABLE_COLUMNS);
        }
        private void AppendDeleteAuditTrail(long sno, SMS_TEXT smsText, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), smsText.Flow_Id, smsText.SMS_Text, smsText.Effective_Date.ToString(), smsText.SMS_Subject, smsText.SMS_Local, smsText.SMS_Other, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, SmsTextService.TABLE_NAME, SmsTextService.TABLE_COLUMNS);
        }
        private SMS_TEXT CreateSmsText(AddEmailForm addEmailForm)
        {
            SMS_TEXT smsText = new SMS_TEXT();
            smsText.Flow_Id = ((long)addEmailForm.flow).ToString();
            smsText.SMS_Text = Utilites.RemoveSpecialCharacters(addEmailForm.text);
            smsText.SMS_Other = Utilites.RemoveSpecialCharacters(addEmailForm.loctext);
            smsText.SMS_Subject = Utilites.RemoveSpecialCharacters(addEmailForm.sub);
            smsText.SMS_Local = Utilites.RemoveSpecialCharacters(addEmailForm.subloc);
            smsText.SNO = (long) addEmailForm.sno;
            smsText.AuditBy = addEmailForm.userid.ToString();
            return smsText;
        }
        public List<SMS_TEXT> GetSmsTextList()
        {
            try
            {
                List<SMS_TEXT> smsTexts = new SMS_TEXT().GetSMS();
                return smsTexts ?? new List<SMS_TEXT>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SMS_TEXT FindSmsText(long sno)
        {
            try
            {
                SMS_TEXT smsText = new SMS_TEXT();
                SMS_TEXT found = smsText.EditSMS(sno);
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

        public SMS_TEXT InsertSmsText(AddEmailForm addEmailForm)
        {
            try
            {
                SMS_TEXT smsText = CreateSmsText(addEmailForm);
                bool invalid = smsText.ValidateFlowId(smsText.Flow_Id);
                if (invalid) throw new ArgumentException("Flow Id already exists");
                long addedSmsText = smsText.AddSMS(smsText);
                AppendInsertAuditTrail(addedSmsText, smsText, (long) addEmailForm.userid);
                return FindSmsText(addedSmsText);
            }
            catch(ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SMS_TEXT UpdateSmsText(AddEmailForm addEmailForm)
        {
            try
            {
                SMS_TEXT smsText = CreateSmsText(addEmailForm);
                SMS_TEXT found = smsText.EditSMS((long)addEmailForm.sno);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                bool invalid = smsText.ValidateFlowIdDuplicate(smsText.Flow_Id, (long)addEmailForm.sno);
                if (invalid) throw new ArgumentException("Flow Id already exists");
                smsText.UpdateSMS(smsText);
                AppendUpdateAuditTrail((long) addEmailForm.sno,found,smsText, (long)addEmailForm.userid);
                return FindSmsText((long)addEmailForm.sno);
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

        public long DeleteSmsText(long sno,long userid)
        {
            try
            {
                var found = FindSmsText((long)sno);
                found.DeleteSMS(sno);
                AppendDeleteAuditTrail(sno, found, userid);
                return found.SNO;
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
