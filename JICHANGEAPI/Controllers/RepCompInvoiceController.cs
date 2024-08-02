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
    public class RepCompInvoiceController : SetupBaseController
    {

        // GET: RepCompInvoice
        private readonly RepCompInvoiceService repCompInvoiceService = new RepCompInvoiceService();
        private readonly CustomerService customerService = new CustomerService();

        [HttpPost]
        public HttpResponseMessage CustList(SingletonSno singletonSno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> customers = repCompInvoiceService.GetApprovedInvoiceCustomers(singletonSno);
                return GetSuccessResponse(customers);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage CompList(SingletonSno singletonSno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<CompanyBankMaster> customers = customerService.GetCompanyNamesList((long) singletonSno.Sno);
                return GetSuccessResponse(customers);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage InvList(SingletonSno singletonSno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> customers = repCompInvoiceService.GetInvoiceNumbersByCustomerId((long)singletonSno.Sno);
                return GetSuccessResponse(customers);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage customerList()
        {
            try
            {
                List<CustomerMaster> customers = customerService.GetAllCustomersList();
                return GetSuccessResponse(customers);
            }
            catch (Exception Ex)
            {
                return GetServerErrorResponse(Ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetInvReport(InvoiceDetailsForm invoiceDetailsForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> invoices = repCompInvoiceService.GetInvoiceReport(invoiceDetailsForm);
                return GetSuccessResponse(invoices);    
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetInvDetReport(InvDetRepModel invDetRepModel)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> invoices = repCompInvoiceService.GetInvoiceDetailsReport(invDetRepModel);
                return GetSuccessResponse(invoices);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}
