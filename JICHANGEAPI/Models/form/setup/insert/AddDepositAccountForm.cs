using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form.setup.insert
{
    public class AddDepositAccountForm : MainForm
    {
        [Required(ErrorMessage = "Missing account number", AllowEmptyStrings = false)]
        public string account { get; set; }
    }
}