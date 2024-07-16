using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SingletonControl
    {
        [Required(ErrorMessage = "Missing Control number", AllowEmptyStrings =false)]
        public string control { get; set; }
    }
}