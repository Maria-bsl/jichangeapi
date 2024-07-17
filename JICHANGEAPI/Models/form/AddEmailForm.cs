using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form
{
    public class AddEmailForm : MainForm
    {
        [Required(ErrorMessage = "Missing flow")]
        public long? flow { get; set; }
        [Required(ErrorMessage = "Missing text", AllowEmptyStrings = false)]
        public string text { get; set; }
        [Required(ErrorMessage = "Missing text in swahili", AllowEmptyStrings = false)]
        public string loctext { get; set; }
        [Required(ErrorMessage = "Missing subject", AllowEmptyStrings = false)]
        public string sub { get; set; }
        [Required(ErrorMessage = "Missing subject in swahili", AllowEmptyStrings = false)]
        public string subloc { get; set; }
        [Required(ErrorMessage = "Missing SNO")]
        public long? sno { get; set; }
    }
}