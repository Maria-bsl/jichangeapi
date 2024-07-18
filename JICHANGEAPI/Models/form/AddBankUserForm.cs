using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form
{
    public class AddBankUserForm : MainForm
    {
        [Required(ErrorMessage = "Missing employee number",AllowEmptyStrings = false)]
        public string empid { get; set; }
        [Required(ErrorMessage = "Missing first name", AllowEmptyStrings = false)]
        public string fname { get; set; }
        public string mname { get; set; }
        [Required(ErrorMessage = "Missing last name", AllowEmptyStrings = false)]
        public string lname { get; set; }
        [Required(ErrorMessage = "Missing designation")]
        public long? desg { get; set; }
        [Required(ErrorMessage = "Missing email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Missing mobile number", AllowEmptyStrings = false)]
        public string mobile { get; set; }
        [Required(ErrorMessage = "Missing username", AllowEmptyStrings = false)]
        public string user { get; set; }
        [Required(ErrorMessage = "Missing status")]
        public string gender { get; set; }
        [Required(ErrorMessage = "Missing branch")]
        public long? branch { get; set; }
        [Required(ErrorMessage = "Missing SNO")]
        public long? sno { get; set; }
        public bool? dummy;
    }
}
