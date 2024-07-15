using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class CompSnoModel
    {
        [Required(ErrorMessage = "Company ID is missing", AllowEmptyStrings = false)]
        public long? compid { get; set; }
        [Required(ErrorMessage = "Missing Sno", AllowEmptyStrings = false)]
        public long? Sno { get; set; }
    }
}