using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models.form;
using JichangeApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace JichangeApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AuditTrailController : SetupBaseController
    {
        private readonly AuditTrailService auditTrailService = new AuditTrailService();
        Payment pay = new Payment();

        [HttpPost]
        public HttpResponseMessage report(AuditTrailForm auditTrailForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                var results = auditTrailService.GetAuditTrailReport(auditTrailForm);
                return SuccessJsonResponse(results);
                //return GetSuccessResponse(results);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAvailablePages(string userid)
        {
            try
            {
                var results = auditTrailService.GetTableNamesByAuditBy(userid);
                return GetSuccessResponse(results);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAvailableAuditTypes(string userid)
        {
            try
            {
                var results = auditTrailService.GetAuditTypesBy(userid);
                return GetSuccessResponse(results);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetReport(AuditTrailForm auditTrailForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                var results = auditTrailService.GetAuditTrailsVendorReport(auditTrailForm);
                return SuccessJsonResponse(results);
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                pay.Message = Ex.ToString();
                pay.AddErrorLogs(pay);

                return GetServerErrorResponse(Ex.Message);
            }
        }
    }
}
