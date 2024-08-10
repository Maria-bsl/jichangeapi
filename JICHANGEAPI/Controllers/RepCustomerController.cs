using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using JichangeApi.Models.form;
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
    public class RepCustomerController : SetupBaseController
    {
        // GET: RepCustomer
        private readonly RepCustomerService repCustomerService = new RepCustomerService();

        [HttpPost]
        /*public HttpResponseMessage GetcustDetReport(SingletonGetCustDetRepModel form)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<CustomerMaster> customers = repCustomerService.GetCustomerDetailsReport((long)form.Comp, (long)form.reg, (long)form.dist);
                return GetSuccessResponse(customers);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }*/
        public HttpResponseMessage GetcustDetReport(CustomersDetailsForm form)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<CustomerMaster> customers = repCustomerService.GetCustomerDetailsReport(form.vendors);
                return GetSuccessResponse(customers);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
    }
}
