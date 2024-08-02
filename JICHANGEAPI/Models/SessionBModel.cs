using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SessionBModel
    {
        [Required(ErrorMessage ="Missing Session ID", AllowEmptyStrings =false)]
        public string sessB { get; set; }
    }
}