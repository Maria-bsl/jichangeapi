using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class CountryForm : MainForm
    {
        [Required(ErrorMessage ="Missing Name", AllowEmptyStrings =false)]
        public string country_name { get; set; }
        [Required(ErrorMessage = "Missing Sno")]
        public long sno { get; set; }

        [Required(ErrorMessage = "Missing Dummy")]
        public bool dummy { get; set; }

        
    }
}