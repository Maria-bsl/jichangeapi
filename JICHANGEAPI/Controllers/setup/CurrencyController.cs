using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;
using JichangeApi.Services.setup;
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
    public class CurrencyController : SetupBaseController
    {
        private readonly CurrencyService currencyService = new CurrencyService();
        Payment pay = new Payment();

        [HttpPost]
        public HttpResponseMessage GetCurrencyDetails()
        {
            CURRENCY currency = new CURRENCY();
            try
            {
                List<CURRENCY> currencies = currencyService.GetCurrenciesList();
                return GetSuccessResponse(currencies);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                return this.GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage AddCurrency(AddCurrencyForm addCurrencyForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                if (addCurrencyForm.sno == 0) 
                { 
                    CURRENCY currency = currencyService.InsertCurrency(addCurrencyForm); 
                    return GetSuccessResponse(currency);
                }
                else
                {
                    CURRENCY currency = currencyService.UpdateCurrency(addCurrencyForm);
                    return GetSuccessResponse(currency);
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

                return GetServerErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage FindCurrency(string code)
        {
            try
            {
                CURRENCY currency = currencyService.FindCurrency(code);
                return GetSuccessResponse(currency);
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

        [HttpPost]
        public HttpResponseMessage Deletecurrency(DeleteCurrencyForm deleteCurrencyForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            CURRENCY currency = new CURRENCY();
            try
            {
                string code = currencyService.Deletecurrency(deleteCurrencyForm.code,(long) deleteCurrencyForm.userid);
                return GetSuccessResponse(code);
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
    }
}