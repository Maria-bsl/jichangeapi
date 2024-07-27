using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SingletonVOtp : SingletonMobile
    {

        [Required(ErrorMessage = "Missing Otp Code", AllowEmptyStrings =false)]
        public string otp_code { get; set; }
    }
}