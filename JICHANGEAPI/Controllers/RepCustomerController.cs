using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
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
    public class RepCustomerController : SetupBaseController
    {
        // GET: RepCustomer
     

        [HttpPost]
        public HttpResponseMessage GetcustDetReport(SingletonGetCustDetRepModel c)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                CustomerMaster customerMaster = new CustomerMaster();
                var results = customerMaster.CustGetrep(c.Comp, c.reg, c.dist);
                return this.GetList<List<CustomerMaster>, CustomerMaster>(results);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}
