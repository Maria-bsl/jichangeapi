using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services.Reports
{
    public class InvoiceRepService
    {
        Payment pay = new Payment();
        public List<INVOICE> GetApprovedCustomers(SingletonSno singleton)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetCustomers((long)singleton.Sno);
                return result ?? new List<INVOICE>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<INVOICE> GetCustomers(SingletonSno singleton)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetCustomers111((long)singleton.Sno);
                return result ?? new List<INVOICE>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<CustomerMaster> GetCompanyCustomers(CustomerDetailsForm customerDetailsForm)
        {
            try
            {
                CustomerMaster customerMaster = new CustomerMaster();
                //var result = customerMaster.GetCust1((long)singleton.Sno);
                var result = customerMaster.SelectCustomersByCompanyIds(customerDetailsForm.companyIds);
                return result ?? new List<CustomerMaster>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<CompanyBankMaster> GetCompanyList()
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                var result = companyBankMaster.CompGet1();
                return result ?? new List<CompanyBankMaster> ();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<CompanyBankMaster> GetCompaniesListByBranch(long branchId)
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                var result = companyBankMaster.GetApprovedCompaniesByBranch(branchId,"approved");
                return result ?? new List<CompanyBankMaster>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }


        public List<CompanyBankMaster> GetLatestCompaniesListByBranch(long branchId)
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                var result = companyBankMaster.GetLatestApprovedCompaniesByBranch(branchId, "approved");
                return result ?? new List<CompanyBankMaster>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public List<CompanyBankMaster> GetAllCompaniesListByBranch(long branchId)
        {
            try
            {
                List<CompanyBankMaster> companies = new CompanyBankMaster().GetCompany1_Branch_A(branchId);
                return companies ?? new List<CompanyBankMaster>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }


        public List<INVOICE> GetInvoiceNumbersList(SingletonSno singletonSno)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetInvoiceNos((long)singletonSno.Sno);
                return result ?? new List<INVOICE>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<INVOICE> GetInvoiceReport(InvoiceDetailsForm invoiceDetailsForm)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetInvRep(invoiceDetailsForm.customerIds, invoiceDetailsForm.customerIds, invoiceDetailsForm.stdate, invoiceDetailsForm.enddate,invoiceDetailsForm.allowCancelInvoice);
                return result ?? new List<INVOICE>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<INVOICE> GetInvoiceDetailsReport(InvDetRepModel invDetRepModel)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetInvDetRep((long)invDetRepModel.Comp, invDetRepModel.invs, invDetRepModel.stdate, invDetRepModel.enddate, (long)invDetRepModel.Cust);
                return result ?? new List<INVOICE>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
    }
}
