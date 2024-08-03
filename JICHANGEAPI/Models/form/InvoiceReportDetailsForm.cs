using JichangeApi.Models.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Models.form
{
    public class InvoiceReportDetailsForm : InvoiceDetailsForm
    {
        [RequiredList(ErrorMessage = "Missing invoice ids")]
        public List<long> invoiceIds { get; set; }
    }
}
