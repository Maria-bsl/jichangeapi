using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPS.API.Models
{
    public interface IPaymentDetailsRepository
    {
        /// <summary>
        /// Add payment details
        /// </summary>
        /// <param name="servicePaymentDetails"></param>
        /// <returns></returns>
        InvoiceDetails GetInvoiceDetails(PaymentDetails servicePaymentDetails);
        long GetIsAuthenticatedInvoice(long invNo,string token);
        long PostAddPayment(PaymentDetails servicePaymentDetails);
        //long PostAddMPayment(PaymentDetails servicePaymentDetails);
        long PostGetDetails(PaymentDetails servicePaymentDetails);

    }
}