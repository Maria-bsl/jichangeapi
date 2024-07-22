using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SingletonSnoUser : MainForm
    {

        [Required(ErrorMessage = "Missing Sno", AllowEmptyStrings = false)]
        public long? Sno { get; set; }
    }
}