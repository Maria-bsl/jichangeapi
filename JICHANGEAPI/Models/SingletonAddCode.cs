using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SingletonAddCode
    {
        [Required(ErrorMessage ="Missing invoice number", AllowEmptyStrings =false)]
        public long? sno { get; set; }
        [Required(ErrorMessage = "Missing user id", AllowEmptyStrings = false)]
        public long? user_id { get; set; }
    }
}