using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models.form;
using JichangeApi.Services.setup;
using Microsoft.Ajax.Utilities;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services
{
    public class SmsSettingsService
    {
        private readonly static List<string> TABLE_COLUMNS = new List<string> {"sno", "username", "password", "from_address", "mobile_service", "effective_date", "posted_by", "posted_date"};
        private readonly static string TABLE_NAME = "SMS Settings";

        private void AppendInsertAuditTrail(long sno, SMS_SETTING smsSettings, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), smsSettings.USER_Name, smsSettings.Password, smsSettings.From_Address, smsSettings.Mobile_Service, DateTime.Now.ToString(),userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, SmsSettingsService.TABLE_NAME, SmsSettingsService.TABLE_COLUMNS);
        }

        private void AppendUpdateAuditTrail(long sno, SMS_SETTING oldSmsSetting, SMS_SETTING newSmsSetting, long userid)
        {
            List<string> oldValues = new List<string> { sno.ToString(), oldSmsSetting.USER_Name, oldSmsSetting.Password, oldSmsSetting.From_Address, oldSmsSetting.Mobile_Service, DateTime.Now.ToString(), userid.ToString(), DateTime.Now.ToString() };
            List<string> newValues = new List<string> { sno.ToString(), newSmsSetting.USER_Name, newSmsSetting.Password, newSmsSetting.From_Address, newSmsSetting.Mobile_Service, DateTime.Now.ToString(), userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, SmsSettingsService.TABLE_NAME, SmsSettingsService.TABLE_COLUMNS);
        }

        private void AppendDeleteAuditTrail(long sno, SMS_SETTING smsSettings, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), smsSettings.USER_Name, smsSettings.Password, smsSettings.From_Address, smsSettings.Mobile_Service, DateTime.Now.ToString(), userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, SmsSettingsService.TABLE_NAME, SmsSettingsService.TABLE_COLUMNS);
        }

        private SMS_SETTING CreateSmsSetting(AddSmtpModel addSmtpModel)
        {
            SMS_SETTING smtp = new SMS_SETTING();
            smtp.From_Address = addSmtpModel.from_address;
            smtp.USER_Name = addSmtpModel.smtp_uname;
            //smtp.Password = Utilites.GetEncryptedData(addSmtpModel.smtp_pwd);
            if ((long) addSmtpModel.sno == 0)
            {
                smtp.Password = Utilites.GetEncryptedData(addSmtpModel.smtp_pwd);
            }
            smtp.Mobile_Service = addSmtpModel.smtp_mob;
            smtp.AuditBy = addSmtpModel.userid.ToString();
            smtp.SNO = addSmtpModel.sno;
            smtp.AuditBy = addSmtpModel.userid.ToString();
            return smtp;
        }

        public List<SMS_SETTING> GetSmsSettingsList()
        {
            try
            {
                List<SMS_SETTING> smsSettings = new SMS_SETTING().GetSMS();
                return smsSettings ?? new List<SMS_SETTING>();
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public long DeleteSmsSetting(long smsSettingId,long userid)
        {
            try
            {
                SMS_SETTING smsSettings = new SMS_SETTING();
                var found = smsSettings.EditSMS(smsSettingId);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                smsSettings.DeleteSMS(smsSettingId);
                AppendDeleteAuditTrail(smsSettingId, found, userid);
                return smsSettingId;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SMS_SETTING FindSmsSetting(long smsSettingId)
        {
            try
            {
                SMS_SETTING smsSettings = new SMS_SETTING();
                var found = smsSettings.EditSMS(smsSettingId);
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

        public SMS_SETTING UpdateSmsSetting(AddSmtpModel addSmtpModel)
        {
            try
            {
                SMS_SETTING smsSetting = CreateSmsSetting(addSmtpModel);
                var found = smsSetting.EditSMS(addSmtpModel.sno);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                smsSetting.UpdateSMS(smsSetting);
                AppendUpdateAuditTrail((long) addSmtpModel.sno,found,smsSetting,(long) addSmtpModel.userid);
                return FindSmsSetting(smsSetting.SNO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SMS_SETTING InsertSmsSetting(AddSmtpModel addSmtpModel)
        {
            try
            {
                SMS_SETTING smsSetting = CreateSmsSetting(addSmtpModel);
                var addedSms = smsSetting.AddSMS(smsSetting);
                AppendInsertAuditTrail(addedSms, smsSetting,(long) addSmtpModel.userid);
                return FindSmsSetting(addedSms);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
