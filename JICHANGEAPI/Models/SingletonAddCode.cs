using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SingletonAddCode
    {
        [Required(ErrorMessage ="Missing invoice number", AllowEmptyStrings =false)]
        public string sno { get; set; }
        [Required(ErrorMessage = "Missing code", AllowEmptyStrings = false)]
        public long? code { get; set; }
    }
}