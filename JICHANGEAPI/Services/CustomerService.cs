﻿using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers;
using JichangeApi.Controllers.setup;
using JichangeApi.Models;
using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services
{
    public class CustomerService
    {
        private static readonly List<string> tableColumns = new List<string> { "cust_mas_sno", "customer_name", "pobox_no", "physical_address", "region_id", "district_sno", "ward_sno",
            "tin_no", "vat_no","contact_person","email_address","mobile_no", "posted_by", "posted_date", "comp_mas_sno" };
        private static readonly string tableName = "Customers";
        private void AppendInsertAuditTrail(long sno, CustomerMaster customerMaster, long userid)
        {
            var values = new List<string> { sno.ToString(), customerMaster.Cust_Name, customerMaster.PostboxNo, customerMaster.Address, customerMaster.Region_SNO.ToString(), customerMaster.DistSno.ToString(), customerMaster.WardSno.ToString(),
                                    customerMaster.TinNo, customerMaster.VatNo, customerMaster.ConPerson, customerMaster.Email, customerMaster.Phone, userid.ToString(), DateTime.Now.ToString(),(customerMaster.CompanySno).ToString() };
            Auditlog.InsertAuditTrail(values, userid, CustomerService.tableName, CustomerService.tableColumns);
        }

        private void AppendUpdateAuditTrail(long sno, CustomerMaster oldCustomer, CustomerMaster newCustomer, long userid)
        {
            var oldValues = new List<string> { sno.ToString(), oldCustomer.Cust_Name, oldCustomer.PostboxNo, oldCustomer.Address, oldCustomer.Region_SNO.ToString(), oldCustomer.DistSno.ToString(), oldCustomer.WardSno.ToString(),
                                        oldCustomer.TinNo, oldCustomer.VatNo,oldCustomer.ConPerson,oldCustomer.Email,oldCustomer.Phone, userid.ToString(),  DateTime.Now.ToString(),oldCustomer.CompanySno.ToString() };

            var newValues = new List<string> { sno.ToString(), newCustomer.Cust_Name, newCustomer.PostboxNo, newCustomer.Address, newCustomer.Region_SNO.ToString(), newCustomer.DistSno.ToString(), newCustomer.WardSno.ToString(),
                                        newCustomer.TinNo, newCustomer.VatNo,newCustomer.ConPerson,newCustomer.Email,newCustomer.Phone, userid.ToString(),  DateTime.Now.ToString(),newCustomer.CompanySno.ToString() };
            Auditlog.UpdateAuditTrail(oldValues, newValues, userid, CustomerService.tableName, CustomerService.tableColumns);
        }

        private void AppendDeleteAuditTrail(long sno, CustomerMaster customerMaster, long userid)
        {
            var values = new List<string> { sno.ToString(), customerMaster.Cust_Name, customerMaster.PostboxNo, customerMaster.Address, customerMaster.Region_SNO.ToString(), customerMaster.DistSno.ToString(), customerMaster.WardSno.ToString(),
                                    customerMaster.TinNo, customerMaster.VatNo, customerMaster.ConPerson, customerMaster.Email, customerMaster.Phone, userid.ToString(), DateTime.Now.ToString(),(customerMaster.CompanySno).ToString() };
            Auditlog.deleteAuditTrail(values, userid, CustomerService.tableName, CustomerService.tableColumns);
        }
        public CustomerMaster FindCustomer(long compid,long custid)
        {
            try
            {
                var compSnoModel = new CompSnoModel();
                compSnoModel.Sno = custid;
                compSnoModel.compid = compid;
                return GetCustomerById(compSnoModel);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
        public List<CustomerMaster> GetCompanyCustomersList(SingletonComp singleton)
        {
            try
            {
                CustomerMaster customerMaster = new CustomerMaster();
                List<CustomerMaster> result = customerMaster.CustGet((long) singleton.compid);
                return result != null ? result : new List<CustomerMaster>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CustomerMaster GetCustomerById(CompSnoModel compSnoModel) 
        {
            try
            {
                CustomerMaster customerMaster = new CustomerMaster();
                var result = customerMaster.CustGetId((long)compSnoModel.compid, (long)compSnoModel.Sno);
                if (result == null) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
                return result;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CompanyBankMaster> GetCompanyNamesList(SingletonComp singletonComp)
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                List<CompanyBankMaster> result = companyBankMaster.CompGet((long) singletonComp.compid);
                return result != null ? result : new List<CompanyBankMaster>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CustomerMaster GetCustomerTinNumberById(long customerId)
        {
            try
            {
                CustomerMaster customerMaster = new CustomerMaster();
                var result = customerMaster.getTIN(customerId);
                if (result == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                return result;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CustomerMaster InsertCustomer(CustomersForm customersForm)
        {
            try
            {
                CustomerMaster customerMaster = CreateCustomer(customersForm);
                string errors = customerMaster.IsDuplicateCustomer(customerMaster.Cust_Name, customerMaster.Phone, customerMaster.Email, customerMaster.TinNo);
                if (errors.Length > 0) throw new ArgumentException(errors);
                long addedCustomer = customerMaster.CustAdd(customerMaster);
                AppendInsertAuditTrail(addedCustomer, customerMaster, (long)customersForm.userid);
                return FindCustomer(customerMaster.CompanySno, addedCustomer);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CustomerMaster UpdateCustomer(CustomersForm customersForm)
        {
            try
            {
                CustomerMaster customerMaster = CreateCustomer(customersForm);
                CustomerMaster found = FindCustomer((long) customersForm.compid, (long) customersForm.CSno);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                bool isValidUpdate = customerMaster.ValidateDeleteorUpdate(customerMaster.Cust_Sno);
                if (isValidUpdate) throw new ArgumentException("Customer has invoice");
                string exists = customerMaster.IsDuplicateCustomer(customerMaster.Cust_Name, customerMaster.Phone, customerMaster.Email, customerMaster.TinNo, customerMaster.Cust_Sno);
                if (exists != null && exists.Length > 0) throw new ArgumentException(exists);
                AppendUpdateAuditTrail(customerMaster.Cust_Sno, found, customerMaster, (long)customersForm.userid);
                customerMaster.CustUpdate(customerMaster);
                return FindCustomer((long)customersForm.compid, customersForm.CSno);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public long RemoveCustomer(DeleteCustomerForm deleteCustomerForm)
        {
            try
            {
                CustomerMaster customerMaster = new CustomerMaster();
                bool isExist = customerMaster.isExistCustomer((long)deleteCustomerForm.sno);
                if (!isExist) { throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE); }
                CustomerMaster found = customerMaster.FindCustomer((long)deleteCustomerForm.sno);
                AppendDeleteAuditTrail((long)deleteCustomerForm.sno, found, (long)deleteCustomerForm.userid);
                customerMaster.CustDelete((long)deleteCustomerForm.sno);
                return (long) deleteCustomerForm.sno;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Customers> GetCustomersS(long compid)
        {
            try
            {
                Customers customers =  new Customers();
                var result = customers.GetCustomersS(compid);
                return result != null ? result : new List<Customers>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Customers> GetCutomers()
        {
            try
            {
                Customers customers = new Customers();
                var result = customers.GetCustomers();
                return result != null ? result : new List<Customers>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
