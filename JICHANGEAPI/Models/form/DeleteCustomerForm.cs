using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form
{
    public class DeleteCustomerForm : MainForm
    {
        [Required(ErrorMessage = "Missing SNO")]
        public long? sno { get; set; }
    }
}