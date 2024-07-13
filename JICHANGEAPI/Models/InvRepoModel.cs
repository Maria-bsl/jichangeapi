using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class InvRepoModel
    {
        [Required(ErrorMessage ="Missing Company Id", AllowEmptyStrings =false)]
        public long? Comp {get; set;}
        [Required(ErrorMessage = "Missing Customer Id", AllowEmptyStrings = false)]
        public long? cusid { get; set;}
        [Required(ErrorMessage = "Missing Start date", AllowEmptyStrings = true)]
        public string stdate {get; set;}
        [Required(ErrorMessage = "Missing End date", AllowEmptyStrings = true)]
        public string enddate { get; set; }
    }
}