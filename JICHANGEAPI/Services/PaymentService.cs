using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
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
        public List<Payment> GetPaymentReport(CancelRepModel cancelRepModel)
        {
            try
            {
                Payment payment = new Payment();

                var compid = cancelRepModel.compid.ToString() == "all" ? 0 : cancelRepModel.compid;
                var branch = compid.Equals("0") ? 0 : companyBankService.GetCompanyDetail(long.Parse(compid.ToString())).Branch_Sno;
                var cusid = cancelRepModel.cust.ToString().ToLower() == "all" ? 0 : cancelRepModel.cust;
                var result = payment.GetReport(cancelRepModel.compid, cancelRepModel.invno, cancelRepModel.stdate, cancelRepModel.enddate, cancelRepModel.cust);
                return result ?? new List<Payment>();
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
    }
}
