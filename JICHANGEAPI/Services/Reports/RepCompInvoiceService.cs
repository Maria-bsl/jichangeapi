using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services.Reports
{
    public class RepCompInvoiceService
    {
        public List<INVOICE> GetApprovedInvoiceCustomers(SingletonSno singletonSno)
        {
            try
            {
                INVOICE invoice = new INVOICE();
                var result = invoice.GetCustomers1((long) singletonSno.Sno);
                return result != null ? result : new List<INVOICE>();   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<INVOICE> GetInvoiceNumbersByCustomerId(long customerId) 
        {
            try
            {
                INVOICE invoice = new INVOICE();
                List<INVOICE> result = invoice.GetInvoiceNos_(customerId);
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
                invRepoModel.cusid = invRepoModel.cusid.ToString().ToLower() == "all" ? "0" : invRepoModel.cusid;
                invRepoModel.Comp = invRepoModel.Comp.ToString().ToLower() == "all" ? 0 : invRepoModel.Comp;
                var result = new INVOICE().GetInvRep1((long)invRepoModel.Comp, long.Parse(invRepoModel.cusid), invRepoModel.stdate, invRepoModel.enddate);
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
                var result = new INVOICE().GetInvDetRep_1((long)invDetRepModel.Comp, invDetRepModel.invs.ToString(), invDetRepModel.stdate, invDetRepModel.enddate, (long)invDetRepModel.Cust);
                return result != null ? result : new List<INVOICE>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
