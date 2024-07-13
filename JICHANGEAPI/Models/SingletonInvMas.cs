using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SingletonInvMas
    {
       // string invno, int invmasno
       [Required(ErrorMessage ="Missing Invoice Number", AllowEmptyStrings =false)]
        public string invno { get; set; }
        [Required(ErrorMessage = "Missing Invoice Master Sno", AllowEmptyStrings = false)]
        public int invmasno { get; set; }
    }
}