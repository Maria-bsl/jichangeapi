using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using JichangeApi.Services;
using JichangeApi.Services.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JichangeApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserLogController : SetupBaseController
    {
        private readonly dynamic returnNull = null;
        EMP_DET ed = new EMP_DET();
        TRACK_DET td = new TRACK_DET();
        private readonly UserLogService userLogService = new UserLogService();
        // GET: Userlog

       

        [HttpPost]
        public HttpResponseMessage LogtimeRep(ReportDates reportDates)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<TRACK_DET> logs = userLogService.GetLoginTimesReport(reportDates.stdate, reportDates.stdate);
                return GetSuccessResponse(logs);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
    }
}
