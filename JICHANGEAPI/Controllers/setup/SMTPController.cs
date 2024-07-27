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
    public class SMTPController : SetupBaseController
    {
        private static readonly List<string> tableColumns = new List<string> { "sno", "from_address", "smtp_address", "smtp_port", "username", "ssl_enable", "effective_date", "posted_by", "posted_date" };
        private static readonly string tableName = "Smtp Settings"; 

        [HttpPost]
        public HttpResponseMessage GetSmtpDetails()
        {
            S_SMTP smtp = new S_SMTP();
            try
            {
                var results = smtp.GetSMTPS();
                return this.GetList<List<S_SMTP>, S_SMTP>(results);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private S_SMTP CreateSmtp(AddSmtpForm addSmtpForm)
        {
            S_SMTP smtp = new S_SMTP();
            smtp.From_Address = addSmtpForm.from_address;
            smtp.SMTP_Address = addSmtpForm.smtp_address;
            smtp.SMTP_Port = addSmtpForm.smtp_port;
            smtp.SMTP_UName = addSmtpForm.smtp_uname;
            smtp.SMTP_Password = addSmtpForm.smtp_pwd;
            smtp.SSL_Enable = addSmtpForm.gender;
            smtp.AuditBy = addSmtpForm.userid.ToString();
            smtp.SNO = (long)addSmtpForm.sno;
            return smtp;
        }

        private void AppendInsertAuditTrail(long smtpSno, S_SMTP smtp, long userid)
        {
            List<string> values = new List<string> { smtpSno.ToString(), smtp.From_Address, smtp.SMTP_Address, smtp.SMTP_Port, smtp.SMTP_UName, smtp.SSL_Enable, DateTime.Now.ToString(), userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, SMTPController.tableName, SMTPController.tableColumns);
        }

        private void AppendUpdateAuditTrail(long sno, S_SMTP oldSmtp, S_SMTP newSmtp, long userid)
        {
            List<string> oldValues = new List<string> { sno.ToString(), oldSmtp.From_Address, oldSmtp.SMTP_Address, oldSmtp.SMTP_Port, oldSmtp.SMTP_UName, oldSmtp.SSL_Enable, oldSmtp.Effective_Date.ToString(), userid.ToString(), DateTime.Now.ToString() };
            List<string> newValues = new List<string> { sno.ToString(), newSmtp.From_Address, newSmtp.SMTP_Address, newSmtp.SMTP_Port, newSmtp.SMTP_UName, newSmtp.SSL_Enable, DateTime.Now.ToString(), userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, SMTPController.tableName, SMTPController.tableColumns);
        }

        private void AppendDeleteAuditTrail(long sno, S_SMTP smtp, long userid)
        {
            List<string> values = new List<string> { sno.ToString(), smtp.From_Address, smtp.SMTP_Address, smtp.SMTP_Port, smtp.SMTP_UName, smtp.SSL_Enable, smtp.Effective_Date.ToString(), userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, SMTPController.tableName, SMTPController.tableColumns);
        }



        private HttpResponseMessage InsertSmtp(S_SMTP smtp,AddSmtpForm addSmtpForm)
        {
            try
            {
                bool isExistFromAddress = smtp.isExistFromAddress(addSmtpForm.from_address);
                if (isExistFromAddress)
                {
                    var messages = new List<string> { "From Address already exists" };
                    return this.GetCustomErrorMessageResponse(messages);
                }
                bool isDuplicateData = smtp.Validateduplicatedata(addSmtpForm.smtp_uname);
                if (isDuplicateData)
                {
                    var messages = new List<string> { "Username already exists" };
                    return this.GetCustomErrorMessageResponse(messages);
                }
                var addedSno = smtp.AddSMTP(smtp);
                AppendInsertAuditTrail(addedSno, smtp, (long)addSmtpForm.userid);
                return FindSmtp(addedSno);
            }
            catch(Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private HttpResponseMessage UpdateSmtp(S_SMTP smtp,AddSmtpForm addSmtpForm)
        {
            try
            {
                var isExist = smtp.isExistSMTP((long) addSmtpForm.sno);
                if (!isExist) { return this.GetNotFoundResponse(); }
                S_SMTP fetchedSMTP = smtp.EditSMTP((long)addSmtpForm.sno);
                long updatedSmtp = smtp.UpdateSMTP(smtp);
                AppendUpdateAuditTrail(updatedSmtp, fetchedSMTP, smtp, (long)addSmtpForm.userid);
                return FindSmtp(updatedSmtp);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddSMTP(AddSmtpForm addSmtpForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                S_SMTP smtp = CreateSmtp(addSmtpForm);
                if (addSmtpForm.sno == 0) { return InsertSmtp(smtp, addSmtpForm); }
                else { return UpdateSmtp(smtp,addSmtpForm); }
            }
            catch(Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage FindSmtp(long sno)
        {
            try
            {
                S_SMTP smtp = new S_SMTP();
                bool isExist = smtp.isExistSMTP(sno);
                if (!isExist) return this.GetNotFoundResponse();
                S_SMTP found = smtp.EditSMTP(sno);
                return this.GetSuccessResponse(found);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteSMTP(DeleteSmtpForm deleteSmtpForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                S_SMTP smtp = new S_SMTP();
                bool isExist = smtp.isExistSMTP((long) deleteSmtpForm.sno);
                if (!isExist) return this.GetNotFoundResponse();
                S_SMTP found = smtp.EditSMTP((long)deleteSmtpForm.sno);
                AppendDeleteAuditTrail((long)deleteSmtpForm.sno, found, (long)deleteSmtpForm.userid);
                smtp.DeleteSMTP((long)deleteSmtpForm.sno);
                return this.GetSuccessResponse((long)deleteSmtpForm.sno);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}