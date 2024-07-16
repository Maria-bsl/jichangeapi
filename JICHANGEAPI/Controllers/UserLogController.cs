using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
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
    public class UserLogController : ApiController
    {
        private readonly dynamic returnNull = null;
        EMP_DET ed = new EMP_DET();
        TRACK_DET td = new TRACK_DET();
        // GET: Userlog

       

        [HttpPost]
        public HttpResponseMessage LogtimeRep(ReportDates r)
        {
            if (ModelState.IsValid) {
                try
                {

                    var result = td.Getfunctiontrackdet(r.stdate, r.enddate);
                    if (result != null)
                    {

                        return Request.CreateResponse(new { response = result,  message = new List<string> { } }); 
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = result, message = new List<string> { "Failed" } });
                    }


                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                    // long errorLogID = ApplicationError.ErrorHandling(Ex, Request.RequestUri.ToString(), Request.Url.ToString(), Request.Browser.Type);
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }

            //return returnNull;
        }

    }
}
