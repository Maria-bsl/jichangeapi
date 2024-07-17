using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form
{
    public class DeleteCurrencyForm : MainForm
    {
        [Required(ErrorMessage = "Missing currency code")]
        public string code { get; set; }
    }
}