using BL.BIZINVOICING.BusinessEntities.ConstantFile;
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
    public class EmailController : SetupBaseController
    {
        private static readonly List<string> tableColumns = new List<string> { "sno", "flow_id", "email_text", "effective_date", "posted_by", "posted_date", "email_sub", "email_sub_local", "email_text_local" };
        private static readonly string tableName = "Email Text";

        [HttpPost]
        public HttpResponseMessage GetEmailDetails()
        {
            EMAIL email = new EMAIL();
            try
            {
                List<EMAIL> results = email.GetEMAIL();
                return this.GetList<List<EMAIL>, EMAIL>(results);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private void AppendInsertAuditTrail(long sno, EMAIL email, long userid)
        {
            var values = new List<string> { sno.ToString(), email.Flow_Id, email.Email_Text, DateTime.Now.ToString(), userid.ToString(), DateTime.Now.ToString(), email.Subject, email.Local_subject, email.Local_Text };
            Auditlog.InsertAuditTrail(values,userid, EmailController.tableName, EmailController.tableColumns);
        }

        private void AppendDeleteAuditTrail(long sno, EMAIL fetchedEmail, long userid)
        {
            var values = new List<string> { sno.ToString(), fetchedEmail.Flow_Id, fetchedEmail.Email_Text, fetchedEmail.Effective_Date.ToString(), fetchedEmail.AuditBy, fetchedEmail.Audit_Date.ToString(), fetchedEmail.Subject, fetchedEmail.Local_subject, fetchedEmail.Local_Text };
            Auditlog.deleteAuditTrail(values, userid, EmailController.tableName, EmailController.tableColumns);
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

        private HttpResponseMessage InsertEmail(EMAIL email,AddEmailForm addEmailForm)
        {
            try
            {
                bool isDuplicateFlow = email.ValidateEMAIL(email.Flow_Id);
                if (isDuplicateFlow)
                {
                    var messages = new List<string> { "Flow already exists" };
                    this.GetCustomErrorMessageResponse(messages);
                }
                long addedEmail = email.AddEMAIL(email);
                AppendInsertAuditTrail(addedEmail, email, (long)addEmailForm.userid);
                return FindEmail(addedEmail);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private HttpResponseMessage UpdateEmail(EMAIL email,AddEmailForm addEmailForm)
        {
            try
            {
                bool isExist = email.isExistEmail((long)addEmailForm.sno);
                if (!isExist) return this.GetNotFoundResponse();
                bool isDuplicateFlow = email.isDuplicateFlow(addEmailForm.flow.ToString(), (long)addEmailForm.sno);
                if (isDuplicateFlow)
                {
                    var messages = new List<string> { "Flow already exists" };
                    this.GetCustomErrorMessageResponse(messages);
                }
                EMAIL fetchedEmail = email.EditEMAIL((long)addEmailForm.sno);
                List<string> oldValues = new List<string> { fetchedEmail.SNO.ToString(), fetchedEmail.Flow_Id, fetchedEmail.Email_Text, fetchedEmail.Effective_Date.ToString(), fetchedEmail.AuditBy, fetchedEmail.Audit_Date.ToString(), fetchedEmail.Subject, fetchedEmail.Local_subject, fetchedEmail.Local_Text };
                List<string> newValues = new List<string> { fetchedEmail.SNO.ToString(), email.Flow_Id, fetchedEmail.Email_Text, DateTime.Now.ToString(), ((long)addEmailForm.userid).ToString(), DateTime.Now.ToString(), email.Subject, email.Local_subject, email.Local_Text };
                long updatedEmail = email.UpdateEMAIL(email);
                Auditlog.UpdateAuditTrail(oldValues, newValues, (long)addEmailForm.userid, "Email Text", tableColumns);
                return FindEmail(updatedEmail);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddEmail(AddEmailForm addEmailForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                EMAIL email = CreateEmail(addEmailForm);
                if ((long) addEmailForm.sno == 0) { return InsertEmail(email, addEmailForm); }
                else { return UpdateEmail(email, addEmailForm); }
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        public HttpResponseMessage FindEmail(long sno)
        {
            try
            {
                EMAIL email = new EMAIL();
                bool isExist = email.isExistEmail(sno);
                if (!isExist) return this.GetNotFoundResponse();
                EMAIL found = email.EditEMAIL(sno);
                return this.GetSuccessResponse(found);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage DeleteEmail(DeleteEmailTextForm deleteEmailTextForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                EMAIL email = new EMAIL();
                bool exists = email.isExistEmail(deleteEmailTextForm.sno);
                if (!exists) return this.GetNotFoundResponse();
                var fetchedEmail = email.EditEMAIL(deleteEmailTextForm.sno);
                AppendDeleteAuditTrail((long)deleteEmailTextForm.sno, fetchedEmail, (long)deleteEmailTextForm.userid);
                email.DeleteEMAIL((long)deleteEmailTextForm.sno);
                return this.GetSuccessResponse((long)deleteEmailTextForm.sno);

            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}