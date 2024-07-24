using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SingletonAcc
    {
        [Required(ErrorMessage ="Missing Account Number", AllowEmptyStrings =false)]
        public string acc { get; set; }
    }
}