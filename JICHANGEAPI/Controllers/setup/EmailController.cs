using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;
using JichangeApi.Services.setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers.setup
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmailController : SetupBaseController
    {
        private readonly EmailTextService emailTextService = new EmailTextService();

        [HttpPost]
        public HttpResponseMessage GetEmailDetails()
        {
            EMAIL email = new EMAIL();
            try
            {
                List<EMAIL> results = emailTextService.GetEmailList();
                return GetSuccessResponse(results);
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
                if ((long) addEmailForm.sno == 0)
                {
                    EMAIL email = emailTextService.InsertEmail(addEmailForm);
                    return GetSuccessResponse(email);
                }
                else
                {
                    EMAIL email = emailTextService.UpdateEmail(addEmailForm);
                    return GetSuccessResponse(email);
                }
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetFlows()
        {
            try
            {
                JsonArray flows = emailTextService.GetFlows();
                return SuccessJsonResponse(flows);
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
                EMAIL found = emailTextService.FindEmail(sno);
                return GetSuccessResponse(found);
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
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
                long deletedEmail = emailTextService.DeleteEmail((long) deleteEmailTextForm.sno,(long) deleteEmailTextForm.userid);
                return GetSuccessResponse(deletedEmail);

            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}