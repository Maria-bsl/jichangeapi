using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class AuthLog
    {
        [Required(ErrorMessage = "Username required", AllowEmptyStrings =false)]
        public string userName { get; set; }
        [Required(ErrorMessage = "Password required", AllowEmptyStrings = false)]
        public string password { get; set; }
    }
}