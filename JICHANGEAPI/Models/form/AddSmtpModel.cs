using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form
{
    public class AddSmtpModel : MainForm
    {
        [Required(ErrorMessage ="Missing From Address", AllowEmptyStrings =false)]
        public string from_address  { get; set; }
        [Required(ErrorMessage = "Missing Username", AllowEmptyStrings = false)]
        public string smtp_uname  { get; set; }
        [Required(ErrorMessage = "Missing Password", AllowEmptyStrings = false)]
        public string smtp_pwd  { get; set; }
        [Required(ErrorMessage = "Missing Mobile", AllowEmptyStrings = false)]
        public string smtp_mob  { get; set; }
       // [Required(ErrorMessage = "Missing From Address", AllowEmptyStrings = false)]
        public string posted_by  { get; set; }
       // [Required(ErrorMessage = "Missing From Address", AllowEmptyStrings = false)]
        public long sno  { get; set; }
    }
}