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
    public class CurrencyController : SetupBaseController
    {
        private static readonly List<string> tableColumns = new List<string> { "currency_code", "currency_name", "posted_by", "posted_date" };
        private static readonly string tableName = "Currency";

        [HttpPost]
        public HttpResponseMessage GetCurrencyDetails()
        {
            CURRENCY currency = new CURRENCY();
            try
            {
                List<CURRENCY> currencies = currency.GetCURRENCY();
                return this.GetList<List<CURRENCY>, CURRENCY>(currencies);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
        private CURRENCY CreateCurrency(AddCurrencyForm addCurrencyForm)
        {
            CURRENCY currency = new CURRENCY();
            currency.Currency_Code = addCurrencyForm.code;
            currency.Currency_Name = addCurrencyForm.cname;
            currency.AuditBy = addCurrencyForm.userid.ToString();
            return currency;
        }
        private void AppendInsertAuditTrail(string currencyCode, CURRENCY currency, long userid)
        {
            List<string> values = new List<string> { currency.Currency_Code, currency.Currency_Name, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, CurrencyController.tableName, CurrencyController.tableColumns);
        }
        private void AppendUpdateAuditTrail(string currencyCode, CURRENCY oldCurrency, CURRENCY newCurrency, long userid)
        {
            List<string> oldValues = new List<string> { currencyCode, oldCurrency.Currency_Name, userid.ToString(), DateTime.Now.ToString() };
            List<string> newValues = new List<string> { currencyCode, newCurrency.Currency_Name, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, CurrencyController.tableName, CurrencyController.tableColumns);

        }
        private void AppendDeleteAuditTrail(string currencyCode, CURRENCY currency, long userid)
        {
            List<string> values = new List<string> { currency.Currency_Code, currency.Currency_Name, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, CurrencyController.tableName, CurrencyController.tableColumns);
        }
        private HttpResponseMessage InsertCurrency(CURRENCY currency,AddCurrencyForm addCurrencyForm)
        {
            try
            {
                bool exists = currency.isExistCurrencyCode(addCurrencyForm.code);
                if (exists) return this.GetAlreadyExistsErrorResponse();
                string currencyCode = currency.AddCURRENCY(currency);
                AppendInsertAuditTrail(currencyCode, currency, (long) addCurrencyForm.userid);
                return FindCurrency(currencyCode);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private HttpResponseMessage UpdateCurrency(CURRENCY currency,AddCurrencyForm addCurrencyForm)
        {
            try
            {
                bool exists = currency.isExistCurrencyCode(addCurrencyForm.code);
                if (!exists) return this.GetNotFoundResponse();
                CURRENCY found = currency.getCURRENCYText(addCurrencyForm.code);
                string code = currency.UpdateCURRENCY(currency);
                AppendUpdateAuditTrail(code, found, currency, (long) addCurrencyForm.userid);
                return FindCurrency(code);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddCurrency(AddCurrencyForm addCurrencyForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            CURRENCY currency = CreateCurrency(addCurrencyForm);
            if (addCurrencyForm.sno == 0) { return InsertCurrency(currency,addCurrencyForm);  }
            else { return UpdateCurrency(currency, addCurrencyForm); }
        }

        [HttpGet]
        public HttpResponseMessage FindCurrency(string code)
        {
            try
            {
                CURRENCY currency = new CURRENCY();
                bool exists = currency.isExistCurrencyCode(code);
                if (!exists) return this.GetNotFoundResponse();
                CURRENCY found = currency.getCURRENCYText(code);
                return this.GetSuccessResponse(found);
            }
            catch (Exception ex)
            {
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
                bool isExist = currency.isExistCurrencyCode(deleteCurrencyForm.code);
                CURRENCY found = currency.getCURRENCYText(deleteCurrencyForm.code);
                AppendDeleteAuditTrail(deleteCurrencyForm.code, found, (long) deleteCurrencyForm.userid);
                currency.DeleteCURRENCY(deleteCurrencyForm.code);
                return this.GetSuccessResponse(deleteCurrencyForm.code);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}