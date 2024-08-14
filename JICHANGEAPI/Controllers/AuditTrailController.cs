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
            try
            {
                var results = auditTrailService.GetAuditTrailReport(auditTrailForm);
                return GetSuccessResponse(results);
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
