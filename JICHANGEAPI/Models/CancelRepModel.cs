using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class CancelRepModel
    {
        [Required(ErrorMessage = "Missing Invoice Number", AllowEmptyStrings = false)]
        public string invno {get; set;}
        [Required(ErrorMessage = "Missing Start Date", AllowEmptyStrings = false)]
        public string stdate {get; set;}
        [Required(ErrorMessage = "Missing End date", AllowEmptyStrings = true)]
        public string enddate {get; set;}
        [Required(ErrorMessage = "Missing Customer Id", AllowEmptyStrings = true)]
        public long cust {get; set;}
        [Required(ErrorMessage = "Missing Company Id", AllowEmptyStrings = false)]
        public long compid {get; set;}
    }
}