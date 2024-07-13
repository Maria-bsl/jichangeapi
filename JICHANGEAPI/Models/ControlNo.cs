using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class ControlNo
    {
        [Required(ErrorMessage = "Control Number is missing", AllowEmptyStrings = false)]
        public string control { get; set; }
    }
}