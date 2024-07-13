using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class BranchRef
    {

        [Required(ErrorMessage = "Branch Sno is missing", AllowEmptyStrings = false)]
        public long? branch { get; set; }
    }
}