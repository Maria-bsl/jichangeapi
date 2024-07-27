using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using JichangeApi.Models.form;
using JichangeApi.Services;
using JichangeApi.Services.setup;
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
    public class CustomerController : SetupBaseController
    {
        CustomerMaster cm = new CustomerMaster();
        CompanyBankMaster cbm = new CompanyBankMaster();
        CompanyUsers cu = new CompanyUsers();
        //COUNTRY c = new COUNTRY();
        Auditlog ad = new Auditlog();
        REGION r = new REGION();
        DISTRICTS d = new DISTRICTS();
        WARD w = new WARD();
        private readonly dynamic returnNull = null;
        //AuditLogs al = new AuditLogs();
        private static readonly List<string> tableColumns = new List<string> { "cust_mas_sno", "customer_name", "pobox_no", "physical_address", "region_id", "district_sno", "ward_sno",
            "tin_no", "vat_no","contact_person","email_address","mobile_no", "posted_by", "posted_date", "comp_mas_sno" };
        private static readonly string tableName = "Customers";
        private readonly CustomerService customerService = new CustomerService();
        private readonly RegionService regionService = new RegionService();
        private readonly DistrictService districtService = new DistrictService();
        private readonly WardService wardService = new WardService();


        [HttpPost]
        public HttpResponseMessage GetCusts(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<CustomerMaster> customers = customerService.GetCompanyCustomersList((long) singletonComp.compid);
                return GetSuccessResponse(customers);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetCustbyId(CompSnoModel compSnoModel)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                CustomerMaster customerMaster = customerService.GetCustomerById(compSnoModel);
                return GetSuccessResponse(customerMaster);
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetComp(SingletonComp singletonComp)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                List<CompanyBankMaster> companies = customerService.GetCompanyNamesList((long) singletonComp.compid);
                return GetSuccessResponse(companies);
            }
            catch (Exception ex)
            {
                return GetServerErrorResponse(ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage GetRegion(long rn)
        {
            try
            {
                REGION region = regionService.GetRegionById(rn);
                return GetSuccessResponse(region);
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
        }

        [HttpPost]
        public HttpResponseMessage GetDistrict(long dn)
        {
            try
            {
                DISTRICTS district = districtService.GetDistrictById(dn);
                return GetSuccessResponse(district);
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
        }

        [HttpPost]
        public HttpResponseMessage GetWard(long wn)
        {
            try
            {
                WARD ward = wardService.GetWardById(wn);
                return GetSuccessResponse(ward);
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
            //return returnNull;
        }

        [HttpGet]
        public HttpResponseMessage GetRegionDetails()
        {
            try
            {
                List<REGION> regions = regionService.GetRegionsList();
                return GetSuccessResponse(regions);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
        }

        [HttpPost]
        public HttpResponseMessage GetDistDetails(long Sno)
        {
            try
            {
                List<DISTRICTS> districts = districtService.GetActiveDistrict(Sno);
                return GetSuccessResponse(districts);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
        }

        [HttpPost]
        public HttpResponseMessage GetWardDetails(long sno = 1)
        {
            try
            {
                List<WARD> wards = wardService.GetActiveWard(sno);
                return GetSuccessResponse(wards);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
        }

        [HttpPost]
        public HttpResponseMessage GetTIN(long sno)
        {
            try
            {
                CustomerMaster customer = customerService.GetCustomerTinNumberById(sno);
                return GetSuccessResponse(customer);
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
        }

        [HttpPost]
        public HttpResponseMessage AddCustomer(CustomersForm customersForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                if (customersForm.CSno == 0) 
                { 
                    CustomerMaster customer = customerService.InsertCustomer(customersForm); 
                    return GetSuccessResponse(customer);
                }
                else 
                {
                    CustomerMaster customer = customerService.UpdateCustomer(customersForm);  
                    return GetSuccessResponse(customer);
                }
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage FindCustomer(long companyid,long customerId)
        {
            try
            {
                CustomerMaster customer = customerService.FindCustomer(companyid, customerId);
                return GetSuccessResponse(customer);
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }


        [HttpPost]
        public HttpResponseMessage DeleteCust(DeleteCustomerForm deleteCustomerForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetCustomErrorMessageResponse(modelStateErrors); }
            try
            {
                long removedId = customerService.RemoveCustomer(deleteCustomerForm);
                return GetSuccessResponse(removedId);
            }
            catch (ArgumentException ex)
            {
                List<string> messages = new List<string> { ex.Message };
                return this.GetCustomErrorMessageResponse(messages);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}
