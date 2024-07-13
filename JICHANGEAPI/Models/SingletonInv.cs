using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SingletonInv
    {
        [Required(ErrorMessage = "Missing Invoice Id", AllowEmptyStrings =false)]
        public int invid { get; set; }
    }
}