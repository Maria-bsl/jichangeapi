using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form.setup.insert
{
    public class AddWardForm : MainForm
    {
        [Required(ErrorMessage = "Missing ward",AllowEmptyStrings = false)]
        public string ward_name { get; set; }
        [Required(ErrorMessage = "Missing region")]
        public long? region_id { get; set; }
        [Required(ErrorMessage = "Missing district")]
        public long? district_sno { get; set; }
        [Required(ErrorMessage = "Missing SNO")]
        public long? sno { get; set; }
        [Required(ErrorMessage = "Missing status",AllowEmptyStrings = false)]
        public string ward_status { get; set; }
        public bool? dummy { get; set; }
    }
}