using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using JichangeApi.Models.form;
using JichangeApi.Services.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services
{
    public class PaymentService
    {

        private CompanyBankService companyBankService = new CompanyBankService();
        Payment pay = new Payment();
        public List<Payment> GetPaymentReport(CancelRepModel cancelRepModel)
        {
            try
            {
                Payment payment = new Payment();

                var compid = cancelRepModel.compid.ToString() == "all" ? 0 : cancelRepModel.compid;
                var branch = compid.Equals("0") ? 0 : companyBankService.GetCompanyDetail(long.Parse(compid.ToString())).Branch_Sno;
                var cust = cancelRepModel.cust.ToString().ToLower() == "all" ? 0 : cancelRepModel.cust;
                var result = payment.GetReport((long) cancelRepModel.compid, cancelRepModel.invno, cancelRepModel.stdate, cancelRepModel.enddate, (long) cust);
                return result ?? new List<Payment>();
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
        public List<Payment> GetPaymentReport(InvoiceReportDetailsForm invoiceReportDetailsForm)
        {
            try
            {
                var results = new Payment().GetReport(invoiceReportDetailsForm.companyIds, invoiceReportDetailsForm.customerIds, invoiceReportDetailsForm.invoiceIds, invoiceReportDetailsForm.stdate, invoiceReportDetailsForm.enddate);
                return results ?? new List<Payment>(); 
            }
            catch (ArgumentException ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new ArgumentException(ex.Message);
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
