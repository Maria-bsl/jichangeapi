using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class TransactInvoiceNo
    {

        [Required(ErrorMessage = "Missing Invoice Number", AllowEmptyStrings = false)]
        public string invoice_sno { get; set; }
    }
}