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
    public class CurrencyController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetCurrencyDetails()
        {
            var currency = new CURRENCY();
            try
            {
                var currencies = currency.GetCURRENCY();
                if (currencies != null)
                {
                    return Request.CreateResponse(new { response = currencies, message = new List<string>() });
                }
                else
                {
                    return Request.CreateResponse(new { response = new List<string>(), message = new List<string> { "Failed to retrieve ward list." } });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server." } });
            }
        }

        [HttpPost]
        public HttpResponseMessage AddCurrency(AddCurrencyForm addCurrencyForm)
        {
            if (ModelState.IsValid)
            {
                var currency = new CURRENCY();
                currency.Currency_Code = addCurrencyForm.code;
                currency.Currency_Name = addCurrencyForm.cname;
                currency.AuditBy = addCurrencyForm.userid.ToString();
                try
                {
                    if (addCurrencyForm.sno == 0)
                    {
                        var isExist = currency.isExistCurrencyCode(addCurrencyForm.code);
                        if (isExist)
                        {
                            return Request.CreateResponse(new { response = 0, message = new List<string> { "Already exists." } });
                        }
                        else
                        {
                            currency.AddCURRENCY(currency);
                            return Request.CreateResponse(new { response = "1", message = new List<string>() });
                        }
                    }
                    else
                    {
                        currency.UpdateCURRENCY(currency);
                        return Request.CreateResponse(new { response = "1", message = new List<string>() });
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

        [HttpPost]
        public HttpResponseMessage Deletecurrency(DeleteCurrencyForm deleteCurrencyForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var currency = new CURRENCY();
                    var isExist = currency.isExistCurrencyCode(deleteCurrencyForm.code);
                    if (isExist)
                    {
                        currency.DeleteCURRENCY(deleteCurrencyForm.code);
                        return Request.CreateResponse(new { response = deleteCurrencyForm.code, message = new List<string>() });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "Currency does not exist." } });
                    }
                }
                catch(Exception ex)
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