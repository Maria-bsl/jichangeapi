using BL.BIZINVOICING.BusinessEntities.ConstantFile;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Controllers.smsservices;
using JichangeApi.Models.form;
using JichangeApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JichangeApi.Controllers
{
    public class SMSSETTINGController : SetupBaseController
    {
        private readonly SmsSettingsService smsSettingsService = new SmsSettingsService();
        // GET: SMSSETNGS
        SMS_SETTING smtp = new SMS_SETTING();
        EMP_DET ed = new EMP_DET();
        Payment pay = new Payment();
        private readonly dynamic returnNull = null;
        //Arights act = new Arights();

        [HttpPost]
        public HttpResponseMessage AddSMTP(AddSmtpModel addSmtpModel)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                if (addSmtpModel.sno == 0)
                {
                    var sms = smsSettingsService.InsertSmsSetting(addSmtpModel);
                    return GetSuccessResponse(sms);
                }
                else
                {
                    
                    var sms = smsSettingsService.UpdateSmsSetting(addSmtpModel);
                    return GetSuccessResponse(sms);
                }
            }
            catch (ArgumentException Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                Ex.Message.ToString();
                List<string> messages = new List<string> { Ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);
                ex.ToString();
                return GetServerErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteSMTP(long sno,long userid)
        {
            try
            {

                var p = smsSettingsService.DeleteSmsSetting(sno,userid);
                return GetSuccessResponse(sno);
            }
            catch (ArgumentException Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                Ex.Message.ToString();
                List<string> messages = new List<string> { Ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                Ex.Message.ToString();
                return GetServerErrorResponse(Ex.Message);
            }

            return returnNull;
        }
        [HttpPost]
        public HttpResponseMessage GetSMTPDetails()
        {
            try
            {
                List<SMS_SETTING> response = smsSettingsService.GetSmsSettingsList();
                return GetSuccessResponse(response);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                Ex.Message.ToString();
                return GetServerErrorResponse(Ex.Message);
            }

            return returnNull;
        }

        [HttpGet]
        public HttpResponseMessage FindSmsSetting(long sno)
        {
            try
            {
                SMS_SETTING smsSetting = smsSettingsService.FindSmsSetting(sno);
                return GetSuccessResponse(smsSetting);
            }
            catch (ArgumentException Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                Ex.Message.ToString();
                List<string> messages = new List<string> { Ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                Ex.Message.ToString();
                return GetServerErrorResponse(Ex.Message);
            }
        }

    }
}
