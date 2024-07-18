using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form.setup.insert
{
    public class AddDistrictForm : MainForm
    {
        [Required(ErrorMessage = "Missing district name",AllowEmptyStrings = false)]
        public string district_name { get; set; }
        [Required(ErrorMessage = "Missing region")]
        public long? region_id { get; set; }
        [Required(ErrorMessage = "Missing status", AllowEmptyStrings = false)]
        public string district_status { get; set; }
        [Required(ErrorMessage = "Missing SNO")]
        public long? sno { get; set; }
        [Required(ErrorMessage = "Missing Dummy")]
        public bool? dummy { get; set; }
    }
}