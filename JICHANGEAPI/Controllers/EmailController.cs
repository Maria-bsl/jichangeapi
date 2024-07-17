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

        /*[HttpPost]
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
                email.SNO = (long) addEmailForm.sno;
                email.AuditBy = addEmailForm.userid.ToString();
                if (addEmailForm.sno == 0)
                {
                    var isValidEmail = email.ValidateEMAIL(email.Flow_Id);

                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
        }*/
    }
}