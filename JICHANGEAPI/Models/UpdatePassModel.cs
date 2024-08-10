using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class UpdatePassModel
    {
        [Required(ErrorMessage ="Missing User type", AllowEmptyStrings =false)]
        public string type { get; set; }
        [Required(ErrorMessage = "Missing User password", AllowEmptyStrings = false)]
        public string pwd { get; set; }
        [Required(ErrorMessage = "Missing User confirm password", AllowEmptyStrings = false)]
        public string confirmPwd { get; set; }
        [Required(ErrorMessage = "Missing User id", AllowEmptyStrings = false)]
        public int userid { get; set; }

}
}