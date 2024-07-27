using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services
{
    public class InvoiceRepService
    {
        public List<INVOICE> GetApprovedCustomers(SingletonSno singleton)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetCustomers((long)singleton.Sno);
                return result != null ? result : new List<INVOICE>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<INVOICE> GetCustomers(SingletonSno singleton)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetCustomers111((long)singleton.Sno);
                return result != null ? result : new List<INVOICE>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CustomerMaster> GetCompanyCustomers(SingletonSno singleton)
        {
            try
            {
                CustomerMaster customerMaster = new CustomerMaster();
                var result = customerMaster.GetCust1((long)singleton.Sno);
                return result != null ? result : new List<CustomerMaster>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CompanyBankMaster> GetCompanyList()
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                var result = companyBankMaster.CompGet1();
                return result != null ? result : new List<CompanyBankMaster> ();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CompanyBankMaster> GetPendingCompanyListByBranch(long branchId)
        {
            try
            {
                CompanyBankMaster companyBankMaster = new CompanyBankMaster();
                var result = companyBankMaster.GetCompany1_Branch(branchId);
                return result != null ? result : new List<CompanyBankMaster>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<INVOICE> GetInvoiceNumbersList(SingletonSno singletonSno)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetInvoiceNos((long)singletonSno.Sno);
                return result != null ? result : new List<INVOICE>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<INVOICE> GetInvoiceReport(InvRepoModel invRepoModel)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetInvRep((long)invRepoModel.Comp, long.Parse(invRepoModel.cusid), invRepoModel.stdate, invRepoModel.enddate);
                return result != null ? result : new List<INVOICE>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<INVOICE> GetInvoiceDetailsReport(InvDetRepModel invDetRepModel)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetInvDetRep((long)invDetRepModel.Comp, invDetRepModel.invs, invDetRepModel.stdate, invDetRepModel.enddate, (long)invDetRepModel.Cust);
                return result != null ? result : new List<INVOICE>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
