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
                return GetServerErrorResponse(Ex.Message);
            }
        }
    }
}
