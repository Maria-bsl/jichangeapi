using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class InvDetRepModel
    {
        [Required(ErrorMessage = "Missing Company Id", AllowEmptyStrings =false)]
        public long? Comp {get; set;}
        [Required(ErrorMessage = "Missing Invoice", AllowEmptyStrings = false)]
        public long invs {get; set;}
        [Required(ErrorMessage = "Missing Start Date", AllowEmptyStrings = true)]
        public string stdate {get; set;}
        [Required(ErrorMessage = "Missing End Date", AllowEmptyStrings = true)]
        public string enddate {get; set;}
        [Required(ErrorMessage = "Missing Customer", AllowEmptyStrings = false)]
        public long? Cust {get; set;}
    }
}