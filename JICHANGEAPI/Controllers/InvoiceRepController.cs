using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using JichangeApi.Services;
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
    public class InvoiceRepController : SetupBaseController
    {


        INVOICE inv = new INVOICE();
        CustomerMaster cm = new CustomerMaster();
        CompanyBankMaster cb = new CompanyBankMaster();
        private readonly dynamic returnNull = null;
        private readonly InvoiceRepService invoiceRepService = new InvoiceRepService();
        private readonly CustomerService customerService = new CustomerService();

        // GET: InvoiceRep
        [HttpPost]
        public HttpResponseMessage CustList(SingletonSno singletonSno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> invoices = invoiceRepService.GetApprovedCustomers(singletonSno);
                return GetSuccessResponse(invoices);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage CustList1(SingletonSno singletonSno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> invoices = invoiceRepService.GetCustomers(singletonSno);
                return GetSuccessResponse(invoices);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetCustDetails(SingletonSno singletonSno)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<CustomerMaster> customers = invoiceRepService.GetCompanyCustomers(singletonSno);
                return GetSuccessResponse(customers);   
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage CompList()
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<CompanyBankMaster> companies = invoiceRepService.GetCompanyList();
                return GetSuccessResponse(companies);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage CompListB(BranchRef branchRef)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<CompanyBankMaster> companies = invoiceRepService.GetPendingCompanyListByBranch((long) branchRef.branch);
                return GetSuccessResponse(companies);
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
                List<INVOICE> invoiceNumbers = invoiceRepService.GetInvoiceNumbersList(singletonSno);
                return GetSuccessResponse(invoiceNumbers);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage customerList(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<CustomerMaster> customers = customerService.GetCompanyCustomersList(singletonComp);
                return GetSuccessResponse(customers);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetInvReport(InvRepoModel invRepoModel)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> invoices = invoiceRepService.GetInvoiceReport(invRepoModel);
                return GetSuccessResponse(invoices);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetInvDetReport(InvDetRepModel invDetRepModel)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<INVOICE> invoices = invoiceRepService.GetInvoiceDetailsReport(invDetRepModel);
                return GetSuccessResponse(invoices);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }
    }
}
