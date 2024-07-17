using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SMTPController : ApiController
    {
        private readonly List<string> tableColumns = new List<string> { "sno", "from_address", "smtp_address", "smtp_port", "username", "ssl_enable", "effective_date", "posted_by", "posted_date" };

        [HttpPost]
        public HttpResponseMessage GetSmtpDetails()
        {
            try
            {
                var smtp = new S_SMTP();
                var result = smtp.GetSMTPS();
                return Request.CreateResponse(new { response = result, message = new List<string>() });
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }

        [HttpPost]
        public HttpResponseMessage AddSMTP(AddSmtpForm addSmtpForm)
        {
            if (ModelState.IsValid)
            {
                var smtp = new S_SMTP();
                smtp.From_Address = addSmtpForm.from_address;
                smtp.SMTP_Address = addSmtpForm.smtp_address;
                smtp.SMTP_Port = addSmtpForm.smtp_port;
                smtp.SMTP_UName = addSmtpForm.smtp_uname;
                smtp.SMTP_Password = addSmtpForm.smtp_pwd;
                smtp.SSL_Enable = addSmtpForm.gender;
                smtp.AuditBy = addSmtpForm.userid.ToString();
                smtp.SNO = (long) addSmtpForm.sno;
                if (addSmtpForm.sno == 0)
                {
                    var addedSno = smtp.AddSMTP(smtp);
                    var insertAudits = new List<string> { addedSno.ToString(), addSmtpForm.from_address, addSmtpForm.smtp_address, addSmtpForm.smtp_port, addSmtpForm.smtp_uname, addSmtpForm.gender, DateTime.Now.ToString(), addSmtpForm.userid.ToString(), DateTime.Now.ToString() };
                    Auditlog.insertAuditTrail(insertAudits, (long)addSmtpForm.userid, "Smtp Settings", tableColumns);
                    return Request.CreateResponse(new { response = addedSno, message = new List<string>() });
                }
                else
                {
                    var fetchedSMTP = smtp.EditSMTP((long) addSmtpForm.sno);
                    smtp.UpdateSMTP(smtp);
                    var oldValues = new List<string> { fetchedSMTP.SNO.ToString(), fetchedSMTP.From_Address, fetchedSMTP.SMTP_Address, fetchedSMTP.SMTP_Port, fetchedSMTP.SMTP_UName, fetchedSMTP.SSL_Enable, fetchedSMTP.Effective_Date.ToString(), fetchedSMTP.AuditBy, fetchedSMTP.Audit_Date.ToString() };
                    var newValues = new List<string> { fetchedSMTP.SNO.ToString(), addSmtpForm.from_address, addSmtpForm.smtp_address, addSmtpForm.smtp_port, addSmtpForm.smtp_uname, addSmtpForm.gender, DateTime.Now.ToString(), addSmtpForm.userid.ToString(), DateTime.Now.ToString() };
                    Auditlog.updateAuditTrail(oldValues, newValues, (long) addSmtpForm.userid, "Smtp Settings", tableColumns);
                    return Request.CreateResponse(new { response = (long)addSmtpForm.userid, message = new List<string>() });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteSMTP(DeleteSmtpForm deleteSmtpForm)
        {
            if (ModelState.IsValid)
            {
                var smtp = new S_SMTP();
                var isExist = smtp.isExistSMTP(deleteSmtpForm.sno);
                if (isExist)
                {
                    var fetchedSMTP = smtp.EditSMTP((long)deleteSmtpForm.sno);
                    var values = new List<string> { fetchedSMTP.SNO.ToString(), fetchedSMTP.From_Address, fetchedSMTP.SMTP_Address, fetchedSMTP.SMTP_Port, fetchedSMTP.SMTP_UName, fetchedSMTP.SSL_Enable, fetchedSMTP.Effective_Date.ToString(), fetchedSMTP.AuditBy, fetchedSMTP.Audit_Date.ToString() };
                    Auditlog.deleteAuditTrail(values, (long)deleteSmtpForm.userid, "Smtp Settings", tableColumns);
                    smtp.DeleteSMTP((long)deleteSmtpForm.sno);
                    return Request.CreateResponse(new { response = deleteSmtpForm.sno, message = new List<string>() });
                }
                else
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "SMTP does not exist." } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        }
    }
}