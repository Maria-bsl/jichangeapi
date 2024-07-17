using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace JichangeApi.Models.form
{
    public class AddRegionForm : MainForm
    {
        [Required(ErrorMessage = "Missing name",AllowEmptyStrings = false)]
        public string region { get; set; }
        [Required(ErrorMessage = "Missing SNO")]
        public long sno { get; set; }
        [Required(ErrorMessage = "Missing country sno")]
        public long csno { get; set; }
        [Required(ErrorMessage = "Missing status")]
        public string Status { get; set; }

    }
}