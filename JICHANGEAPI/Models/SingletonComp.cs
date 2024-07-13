using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SingletonComp
    {

        [Required(ErrorMessage = "Company ID is missing", AllowEmptyStrings = false)]
        public long? compid { get; set; }
    }
}