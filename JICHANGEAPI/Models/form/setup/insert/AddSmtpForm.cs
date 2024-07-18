using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form.setup.insert
{
    public class AddSmtpForm : MainForm
    {
        [Required(ErrorMessage = "Missing from address", AllowEmptyStrings = false)]
        public string from_address { get; set; }
        [Required(ErrorMessage = "Missing smtp address", AllowEmptyStrings = false)]
        public string smtp_address { get; set; }
        [Required(ErrorMessage = "Missing smtp port", AllowEmptyStrings = false)]
        public string smtp_port { get; set; }
        [Required(ErrorMessage = "Missing smtp username", AllowEmptyStrings = false)]
        public string smtp_uname { get; set; }
        //[Required(ErrorMessage = "Missing smtp password", AllowEmptyStrings = false)]
        public string smtp_pwd { get; set; }
        [Required(ErrorMessage = "Missing SNO")]
        public long? sno { get; set; }
        [Required(ErrorMessage = "Missing gender")]
        public string gender { get; set; }
    }
}