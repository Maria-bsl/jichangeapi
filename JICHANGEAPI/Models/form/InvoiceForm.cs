using BL.BIZINVOICING.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form
{
    public class InvoiceForm : MainForm
    {
        
        [Required(ErrorMessage = "Comp ID is missing", AllowEmptyStrings = false)]
        public long? compid { get; set; }

        [Required(ErrorMessage = "Invoice Number is missing", AllowEmptyStrings = false)]
        public string invno { get; set; }
        public string auname { get; set; }

        [Required(ErrorMessage = "Invoice Date is missing", AllowEmptyStrings = false)]
        public string date { get; set; }
        [Required(ErrorMessage = "Invoice Expiry Date is missing", AllowEmptyStrings = false)]
        public string edate { get; set; }

        [Required(ErrorMessage = "Invoice Due Date is missing", AllowEmptyStrings = false)]
        public string iedate { get; set; }

        [Required(ErrorMessage = "Payment Type is missing", AllowEmptyStrings = false)]
        public string ptype { get; set; }

        [Required(ErrorMessage = "Customer Id is missing", AllowEmptyStrings = false)]
        public long? chus { get; set; }
        public long comno { get; set; }
        public string ccode { get; set; }
        public string ctype { get; set; }

        //[Required(ErrorMessage = "Invoice Date is missing", AllowEmptyStrings = false)]
        public string cino { get; set; }
        public string twvat { get; set; }
        public string vtamou { get; set; }
        public string total { get; set; }
        public string Inv_remark { get; set; }
        public int lastrow { get; set; }
        [Required(ErrorMessage = "Invoice Details is missing", AllowEmptyStrings = false)]
        public List<INVOICE> details { get; set; }
        public long sno { get; set; }
        public string warrenty { get; set; }
        public string goods_status { get; set; }
        public string delivery_status { get; set; }

        


    }
}