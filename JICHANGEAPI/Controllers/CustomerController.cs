using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using JichangeApi.Models.form;
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
        CompanyBankMaster c = new CompanyBankMaster();
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

    
        [HttpPost]
        public HttpResponseMessage GetCusts(SingletonComp c)
        {
            if (ModelState.IsValid) { 
                    try
                    {

                        var result = cm.CustGet(long.Parse(c.compid.ToString()));
                        if (result != null)
                        {
                            return Request.CreateResponse(new { response = result, message = new List<string> { } });
                        }
                        else
                        {
                            var d = 0;
                            return Request.CreateResponse(new { response = d, message = new List<string> { "Failed"} });
                        }

                    }
                    catch (Exception Ex)
                    {
                        return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                    }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages });
            }
            return returnNull;
        }


        [HttpPost]
        public HttpResponseMessage GetCustbyId(CompSnoModel d)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                CustomerMaster customerMaster = new CustomerMaster();
                var result = customerMaster.CustGetId(long.Parse(d.compid.ToString()), (long)d.Sno);
                if (result == null) { return this.GetNotFoundResponse();  }
                return this.GetSuccessResponse(result);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetComp(SingletonComp d)
        {
            if (ModelState.IsValid) { 
                try
                {

                    var result = c.CompGet(long.Parse(d.compid.ToString()));
                    if (result != null)
                    {
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                    else
                    {
                        var t = 0;
                        return Request.CreateResponse(new {response = t, message ="Failed"});
                    }

                }
                catch (Exception Ex)
                {
                    return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Request.CreateResponse(new { response = 0, message = errorMessages});
            }
            return returnNull;

        }


        [HttpPost]
        public HttpResponseMessage GetRegion(string rn)
        {
            try
            {
                long rid = 0;
                if (string.IsNullOrEmpty(rn))
                {

                }
                else
                {
                    rid = long.Parse(rn);
                }
                var result = r.EditREGION(rid);
                if (result != null)
                {
                    return Request.CreateResponse(new { response = result, message = new List<string> { } });
                }
                else
                {
                    var d = 0;
                    return Request.CreateResponse(new { response = d, message = new List<string> { "Failed"} });
                }

            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
            return returnNull;
        }

        [HttpPost]
        public HttpResponseMessage GetDistrict(string dn)
        {
            try
            {
                long rid = 0;
                if (string.IsNullOrEmpty(dn))
                {

                }
                else
                {
                    rid = long.Parse(dn);
                }
                var result = d.EditDISTRICTS(rid);
                if (result != null)
                {
                    return Request.CreateResponse(new { response = result, message = new List<string> { } });
                }
                else
                {
                    var d = 0;
                    return Request.CreateResponse(new { response = d, message = new List<string> { "Failed"} });
                }

            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
            return returnNull;
        }

        [HttpPost]
        public HttpResponseMessage GetWard(string wn)
        {
            try
            {
                long rid = 0;
                if (string.IsNullOrEmpty(wn))
                {

                }
                else
                {
                    rid = long.Parse(wn);
                }
                var result = w.EditWARD(rid);
                if (result != null)
                {
                    return Request.CreateResponse(new { response = result, message = new List<string> { } });
                }
                else
                {
                    var d = 0;
                    return Request.CreateResponse(new { response = d, message = new List<string> { "Failed"} });
                }

            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
            return returnNull;
        }

        [HttpGet]
        public HttpResponseMessage GetRegionDetails()
        {
            try
            {
                var result = r.GetReg();
                return Request.CreateResponse(new { response = result, message = new List<string> { } });
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
            return returnNull;
        }//need to check get methods

        [HttpPost]
        public HttpResponseMessage GetRegionDetails1(long Sno)
        {
            try
            {
                var result = r.GetReg(Sno);
                return Request.CreateResponse(new { response = result, message = new List<string> { } });
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
            return returnNull;
        }//need to check get methods


        [HttpPost]
        public HttpResponseMessage GetDistDetails(long Sno)
        {
            try
            {
                string ash = null;
                if (Sno == Convert.ToInt64(ash))
                {
                    return Request.CreateResponse(new { response = Sno, message = new List<string> { "Failed" } });
                }
                else
                {
                    var result = d.GetDistrictActive(Sno);
                    if (result == null)
                    {
                        int d = 0;
                        return Request.CreateResponse(new { response = d, message = new List<string> { "Failed"} });
                    }
                    else
                    {
                        return Request.CreateResponse(new { response = result, message = new List<string> { } });
                    }
                }
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
            return returnNull;
        }

        [HttpPost]
        public HttpResponseMessage GetWardDetails(long sno = 1)
        {
            try
            {
                var result = w.GetWARDAct(sno);
                if (result == null)
                {
                    int d = 0;
                    return Request.CreateResponse(new { response = d, message = new List<string> { "Failed"} });
                }
                else
                {
                    return Request.CreateResponse(new { response = result, message = new List<string> { } });
                }
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
            return returnNull;
        }

        [HttpPost]
        public HttpResponseMessage GetTIN(long sno)
        {
            try
            {
                var result = cm.getTIN(sno);
                if (result == null)
                {
                    int d = 0;
                    return Request.CreateResponse(new { response = d, message = new List<string> { "Failed"} });
                }
                else
                {
                    return Request.CreateResponse(new { result.TinNo, message = new List<string> { } });
                }
            }
            catch (Exception Ex)
            {
                return Request.CreateResponse(new { response = 0, message = new List<string> { "An error occured on the server", Ex.ToString() } });
            }
            return returnNull;
        }

        private CustomerMaster CreateCustomer(CustomersForm customersForm)
        {
            CustomerMaster customer = new CustomerMaster();
            customer.Cust_Sno = customersForm.CSno;
            customer.Cust_Name = customersForm.CName;
            customer.PostboxNo = customersForm.PostboxNo;
            customer.Address = customersForm.Address;
            customer.CompanySno = long.Parse((customersForm.compid).ToString());
            if (customersForm.regid > 0) { customer.Region_SNO = customersForm.regid; }
            if (customersForm.distsno > 0) { customer.DistSno = customersForm.distsno; }
            if (customersForm.wardsno > 0) { customer.WardSno = customersForm.wardsno; }
            customer.TinNo = customersForm.Tinno;
            customer.VatNo = customersForm.VatNo;
            customer.ConPerson = customersForm.CoPerson;
            customer.Email = customersForm.Mail;
            customer.Phone = customersForm.Mobile_Number;
            customer.Posted_by = customersForm.userid.ToString();
            customer.Checker = customersForm.check_status;
            return customer;
        }

        private void AppendInsertAuditTrail(long sno, CustomerMaster customerMaster, long userid)
        {
            var values = new List<string> { sno.ToString(), customerMaster.Cust_Name, customerMaster.PostboxNo, customerMaster.Address, customerMaster.Region_SNO.ToString(), customerMaster.DistSno.ToString(), customerMaster.WardSno.ToString(),
                                    customerMaster.TinNo, customerMaster.VatNo, customerMaster.ConPerson, customerMaster.Email, customerMaster.Phone, userid.ToString(), DateTime.Now.ToString(),(customerMaster.CompanySno).ToString() };
            Auditlog.InsertAuditTrail(values, userid, CustomerController.tableName, CustomerController.tableColumns);
        }

        private void AppendUpdateAuditTrail(long sno, CustomerMaster oldCustomer, CustomerMaster newCustomer, long userid)
        {
            var oldValues = new List<string> { sno.ToString(), oldCustomer.Cust_Name, oldCustomer.PostboxNo, oldCustomer.Address, oldCustomer.Region_SNO.ToString(), oldCustomer.DistSno.ToString(), oldCustomer.WardSno.ToString(),
                                        oldCustomer.TinNo, oldCustomer.VatNo,oldCustomer.ConPerson,oldCustomer.Email,oldCustomer.Phone, userid.ToString(),  DateTime.Now.ToString(),oldCustomer.CompanySno.ToString() };
            
            var newValues = new List<string> { sno.ToString(), newCustomer.Cust_Name, newCustomer.PostboxNo, newCustomer.Address, newCustomer.Region_SNO.ToString(), newCustomer.DistSno.ToString(), newCustomer.WardSno.ToString(),
                                        newCustomer.TinNo, newCustomer.VatNo,newCustomer.ConPerson,newCustomer.Email,newCustomer.Phone, userid.ToString(),  DateTime.Now.ToString(),newCustomer.CompanySno.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, CustomerController.tableName, CustomerController.tableColumns);
        }

        private void AppendDeleteAuditTrail(long sno, CustomerMaster customerMaster, long userid)
        {
            var values = new List<string> { sno.ToString(), customerMaster.Cust_Name, customerMaster.PostboxNo, customerMaster.Address, customerMaster.Region_SNO.ToString(), customerMaster.DistSno.ToString(), customerMaster.WardSno.ToString(),
                                    customerMaster.TinNo, customerMaster.VatNo, customerMaster.ConPerson, customerMaster.Email, customerMaster.Phone, userid.ToString(), DateTime.Now.ToString(),(customerMaster.CompanySno).ToString() };
            Auditlog.deleteAuditTrail(values, userid, CustomerController.tableName, CustomerController.tableColumns);
        }

        private HttpResponseMessage InsertCustomer(CustomerMaster customerMaster,CustomersForm customersForm)
        {
            try
            {
                string exists = customerMaster.IsDuplicateCustomer(customerMaster.Cust_Name, customerMaster.Phone,customerMaster.Email,customerMaster.TinNo);
                if (exists.Length > 0) {
                    var messages = new List<string> { exists };
                    return this.GetCustomErrorMessageResponse(messages);
                }
                long addedCustomer = customerMaster.CustAdd(customerMaster);
                AppendInsertAuditTrail(addedCustomer, customerMaster, (long) customersForm.userid);
                return FindCustomer((long)customersForm.compid, addedCustomer);

            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        private HttpResponseMessage UpdateCustomer(CustomerMaster customerMaster,CustomersForm customersForm)
        {
            try
            {
                bool isExist = customerMaster.isExistCustomer(customerMaster.Cust_Sno);
                if (!isExist) { return this.GetNotFoundResponse();  }
                bool isValidUpdate = customerMaster.ValidateDeleteorUpdate(customerMaster.Cust_Sno);
                if (isValidUpdate)
                {
                    var messages = new List<string> { "Customer has invoice" };
                    return this.GetCustomErrorMessageResponse(messages);
                }
                string exists = customerMaster.IsDuplicateCustomer(customerMaster.Cust_Name, customerMaster.Phone, customerMaster.Email, customerMaster.TinNo,customerMaster.Cust_Sno);
                if (exists.Length > 0)
                {
                    var messages = new List<string> { exists };
                    return this.GetCustomErrorMessageResponse(messages);
                }
                CustomerMaster found = customerMaster.FindCustomer(customerMaster.Cust_Sno);
                AppendUpdateAuditTrail(customerMaster.Cust_Sno, found, customerMaster, (long) customersForm.userid);
                customerMaster.CustUpdate(customerMaster);
                return FindCustomer((long) customersForm.compid, customersForm.CSno);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddCustomer(CustomersForm customersForm)
        {
            List<string> modelStateErrors = this.ModelStateErrors();
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                CustomerMaster customerMaster = CreateCustomer(customersForm);
                if (customersForm.CSno == 0) { return InsertCustomer(customerMaster,customersForm); }
                else { return UpdateCustomer(customerMaster,customersForm);  }
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage FindCustomer(long companyid,long customerId)
        {
            try
            {
                CustomerMaster customerMaster = new CustomerMaster();
                CustomerMaster found = customerMaster.CustGetId(companyid, customerId);
                if (found == null) { return this.GetNotFoundResponse(); }
                return this.GetSuccessResponse(found);
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
            if (modelStateErrors.Count() > 0) { return this.GetInvalidModelStateResponse(modelStateErrors); }
            try
            {
                CustomerMaster customerMaster = new CustomerMaster();
                bool isExist = customerMaster.isExistCustomer((long)deleteCustomerForm.sno);
                if (!isExist) { return this.GetNotFoundResponse();  }
                CustomerMaster found = cm.FindCustomer((long)deleteCustomerForm.sno);
                AppendDeleteAuditTrail((long)deleteCustomerForm.sno, found, (long)deleteCustomerForm.userid);
                customerMaster.CustDelete((long)deleteCustomerForm.sno);
                return this.GetSuccessResponse((long)deleteCustomerForm.sno);
            }
            catch (Exception ex)
            {
                return this.GetServerErrorResponse(ex.Message);
            }
        }
    }
}
