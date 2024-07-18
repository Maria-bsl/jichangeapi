using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form
{
    public class AddSuspenseAccountForm : MainForm
    {
        [Required(ErrorMessage = "Missing SNO")]
        public long? sno { get; set; }
        [Required(ErrorMessage = "Missing account", AllowEmptyStrings = false)]
        public string account { get; set; } 
        [Required(ErrorMessage = "Missing status", AllowEmptyStrings = false)]
        public string status { get; set; }
    }
}