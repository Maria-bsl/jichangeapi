using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form
{
    public class AddCurrencyForm : MainForm
    {
        [Required(ErrorMessage = "Missing code")]
        public string code { get; set; }
        [Required(ErrorMessage = "Missing currency")]
        public string cname { get; set; }
        [Required(ErrorMessage = "Missing SNO")]
        public long? sno { get; set; }
        public bool dummy { get; set; }
    }
}