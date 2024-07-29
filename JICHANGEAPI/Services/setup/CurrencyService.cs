using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models.form.setup.insert;
using JichangeApi.Models.form.setup.remove;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services.setup
{
    public class CurrencyService
    {
        private static readonly List<string> tableColumns = new List<string> { "currency_code", "currency_name", "posted_by", "posted_date" };
        private static readonly string tableName = "Currency";
        private void AppendInsertAuditTrail(string currencyCode, CURRENCY currency, long userid)
        {
            List<string> values = new List<string> { currency.Currency_Code, currency.Currency_Name, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.InsertAuditTrail(values, userid, CurrencyService.tableName, CurrencyService.tableColumns);
        }
        private void AppendUpdateAuditTrail(string currencyCode, CURRENCY oldCurrency, CURRENCY newCurrency, long userid)
        {
            List<string> oldValues = new List<string> { currencyCode, oldCurrency.Currency_Name, userid.ToString(), DateTime.Now.ToString() };
            List<string> newValues = new List<string> { currencyCode, newCurrency.Currency_Name, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, CurrencyService.tableName, CurrencyService.tableColumns);

        }
        private void AppendDeleteAuditTrail(string currencyCode, CURRENCY currency, long userid)
        {
            List<string> values = new List<string> { currency.Currency_Code, currency.Currency_Name, userid.ToString(), DateTime.Now.ToString() };
            Auditlog.deleteAuditTrail(values, userid, CurrencyService.tableName, CurrencyService.tableColumns);
        }
        private CURRENCY CreateCurrency(AddCurrencyForm addCurrencyForm)
        {
            CURRENCY currency = new CURRENCY();
            currency.Currency_Code = addCurrencyForm.code;
            currency.Currency_Name = addCurrencyForm.cname;
            currency.AuditBy = addCurrencyForm.userid.ToString();
            return currency;
        }
        public List<CURRENCY> GetCurrenciesList()
        {
            try
            {
                CURRENCY currency = new CURRENCY();
                var results = currency.GetCURRENCY();
                return results != null ? results : new List<CURRENCY>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CURRENCY InsertCurrency(AddCurrencyForm addCurrencyForm)
        {
            try
            {
                CURRENCY currency = CreateCurrency(addCurrencyForm);
                bool exists = currency.isExistCurrencyCode(addCurrencyForm.code);
                if (exists) throw new ArgumentException(SetupBaseController.ALREADY_EXISTS_MESSAGE);
                string currencyCode = currency.AddCURRENCY(currency);
                AppendInsertAuditTrail(currencyCode, currency, (long)addCurrencyForm.userid);
                return FindCurrency(currencyCode);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CURRENCY UpdateCurrency(AddCurrencyForm addCurrencyForm)
        {
            try
            {
                /*CURRENCY currency = CreateCurrency(addCurrencyForm);
                CURRENCY found = currency.getCURRENCYText(addCurrencyForm.code);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                string code = currency.UpdateCURRENCY(currency);
                AppendUpdateAuditTrail(code, found, currency, (long)addCurrencyForm.userid);
                return FindCurrency(code);*/

                CURRENCY found = new CURRENCY().getCURRENCYText(addCurrencyForm.code);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                string deletedCurrency = Deletecurrency(found.Currency_Code, (long)addCurrencyForm.userid);
                return InsertCurrency(addCurrencyForm);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CURRENCY FindCurrency(string code)
        {
            try
            {
                CURRENCY found = new CURRENCY().getCURRENCYText(code);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                return found;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string Deletecurrency(string code,long userid)
        {
            CURRENCY currency = new CURRENCY();
            try
            {
                CURRENCY found = new CURRENCY().getCURRENCYText(code);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                AppendDeleteAuditTrail(code, found, userid);
                currency.DeleteCURRENCY(code);
                return code;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
