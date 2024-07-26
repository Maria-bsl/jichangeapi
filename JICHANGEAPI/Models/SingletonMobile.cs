using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SingletonMobile
    {
        [Required(ErrorMessage ="Missing Mobile Number", AllowEmptyStrings =false)]
        public string mobile { get; set; }
    }
}