using BL.BIZINVOICING.BusinessEntities.ConstantFile;
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
    public class EmailController : ApiController
    {
        private readonly List<string> tableColumns = new List<string> { "sno", "flow_id", "email_text", "effective_date", "posted_by", "posted_date", "email_sub", "email_sub_local", "email_text_local" };

        [HttpPost]
        public HttpResponseMessage GetEmailDetails()
        {
            var email = new EMAIL();
            try
            {
                var results = email.GetEMAIL();
                return Request.CreateResponse(new { response = results, message = new List<string>() });
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }

        [HttpPost]
        public HttpResponseMessage AddEmail(AddEmailForm addEmailForm)
        {
            if (ModelState.IsValid)
            {
                var email = new EMAIL();
                email.Flow_Id = ((long)addEmailForm.flow).ToString();
                email.Email_Text = Utilites.RemoveSpecialCharacters(addEmailForm.text);
                email.Local_Text = Utilites.RemoveSpecialCharacters(addEmailForm.loctext);
                email.Subject = Utilites.RemoveSpecialCharacters(addEmailForm.sub);
                email.Local_subject = Utilites.RemoveSpecialCharacters(addEmailForm.subloc);
                email.SNO = (long)addEmailForm.sno;
                email.AuditBy = addEmailForm.userid.ToString();
                if (addEmailForm.sno == 0)
                {
                    /*var isValidEmail = email.ValidateEMAIL(email.Flow_Id);
                    if (isValidEmail)
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Cannot add email. Flow exists." } });
                    }*/
                    var addedEmail = email.AddEMAIL(email);
                    var values = new List<string> { addedEmail.ToString(), email.Flow_Id, email.Email_Text, DateTime.Now.ToString(), addEmailForm.userid.ToString(), DateTime.Now.ToString(), email.Subject, email.Local_subject, email.Local_Text };
                    Auditlog.insertAuditTrail(values, (long)addEmailForm.userid, "Email Text", tableColumns);
                    return Request.CreateResponse(new { response = addedEmail, message = new List<string>() });
                }
                else
                {
                    var fetchedEmail = email.EditEMAIL((long)addEmailForm.sno);
                    var oldValues = new List<string> { fetchedEmail.SNO.ToString(), fetchedEmail.Flow_Id, fetchedEmail.Email_Text, fetchedEmail.Effective_Date.ToString(), fetchedEmail.AuditBy, fetchedEmail.Audit_Date.ToString(), fetchedEmail.Subject, fetchedEmail.Local_subject, fetchedEmail.Local_Text };
                    var newValues = new List<string> { fetchedEmail.SNO.ToString(), email.Flow_Id, fetchedEmail.Email_Text, DateTime.Now.ToString(), ((long) addEmailForm.userid).ToString(), DateTime.Now.ToString(), email.Subject, email.Local_subject, email.Local_Text };
                    email.UpdateEMAIL(email);
                    Auditlog.updateAuditTrail(oldValues, newValues, (long)addEmailForm.userid, "Email Text", tableColumns);
                    return Request.CreateResponse(new { response = addEmailForm.sno, message = new List<string>() });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteEmail(DeleteEmailTextForm deleteEmailTextForm)
        {
            if (ModelState.IsValid)
            {
                var email = new EMAIL();
                try
                {
                    var exists = email.isExistEmail(deleteEmailTextForm.sno);
                    if (exists)
                    {
                        var fetchedEmail = email.EditEMAIL(deleteEmailTextForm.sno);
                        var values = new List<string> { ((long) deleteEmailTextForm.sno).ToString(), fetchedEmail.Flow_Id, fetchedEmail.Email_Text, fetchedEmail.Effective_Date.ToString(), fetchedEmail.AuditBy, fetchedEmail.Audit_Date.ToString(), fetchedEmail.Subject, fetchedEmail.Local_subject, fetchedEmail.Local_Text };
                        email.DeleteEMAIL(deleteEmailTextForm.sno);
                        Auditlog.deleteAuditTrail(values, (long)deleteEmailTextForm.userid, "Email Text", this.tableColumns);
                        return Request.CreateResponse(new { response = (long)deleteEmailTextForm.sno, message = new List<string>() });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Email does not exist." } });
                    }
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
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