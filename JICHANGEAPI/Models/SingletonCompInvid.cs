using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class SingletonCompInvid
    {
        [Required(ErrorMessage = "Missing Company ID", AllowEmptyStrings =false)]
        public long? compid { get; set; }
        [Required(ErrorMessage = "Missing Invoice Id", AllowEmptyStrings =false )]
        public int invid { get; set; }
    }
}