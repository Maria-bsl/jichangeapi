using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services
{
    public class PaymentService
    {
        public List<Payment> GetPaymentReport(CancelRepModel cancelRepModel)
        {
            try
            {
                Payment payment = new Payment();
                var result = payment.GetReport(cancelRepModel.compid, cancelRepModel.invno, cancelRepModel.stdate, cancelRepModel.enddate, cancelRepModel.cust);
                return result != null ? result : new List<Payment>();
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
