using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace JichangeApi.Controllers
{
    public class SmsTextController : SetupBaseController
    {
        private readonly SmsTextService smsTextService = new SmsTextService();
        Payment pay = new Payment();

        [HttpPost]
        public HttpResponseMessage GetSmsTextList()
        {
            try
            {
                List<SMS_TEXT> texts = smsTextService.GetSmsTextList();
                return GetSuccessResponse(texts);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddSmsText(AddEmailForm addEmailForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                if (addEmailForm.sno == 0)
                {
                    var response = smsTextService.InsertSmsText(addEmailForm);
                    return GetSuccessResponse(response);
                }
                else
                {
                    var response = smsTextService.UpdateSmsText(addEmailForm);
                    return GetSuccessResponse(response);
                }
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage FindSmsText(long sno)
        {
            try
            {
                var response = smsTextService.FindSmsText(sno);
                return GetSuccessResponse(response);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteSmsText(long sno,long userid)
        {
            try
            {
                var response = smsTextService.DeleteSmsText(sno, userid);
                return GetSuccessResponse(response);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }



            //return FindSmsText(sno);
            /*try
            {
                var response = smsTextService.DeleteSmsText(sno);
                return GetSuccessResponse(response);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }*/
        }
    }

}
