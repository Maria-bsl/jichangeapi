using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SingletonDeliveryCode
    {

        [Required(ErrorMessage = "Missing code", AllowEmptyStrings = false)]
        public long? code { get; set; }
        [Required(ErrorMessage = "Missing mobile number", AllowEmptyStrings = false)]
        public string  mobile_no { get; set; }
    }
}