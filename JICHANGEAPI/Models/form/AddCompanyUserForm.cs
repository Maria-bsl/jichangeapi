using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Models.form
{
    public class AddCompanyUserForm : MainForm
    {
        [Required(ErrorMessage = "Missing pos", AllowEmptyStrings = false)]
        public string pos { get; set; }
        [Required(ErrorMessage = "Missing auname", AllowEmptyStrings = true)]
        public string auname { get; set; }
        [Required(ErrorMessage = "Missing mob", AllowEmptyStrings = false)]        
        public string mob { get; set; }
        [Required(ErrorMessage = "Missing uname", AllowEmptyStrings = true)]
        public string uname { get; set; }
        [Required(ErrorMessage = "Missing mail", AllowEmptyStrings = true)]
        [EmailAddress(ErrorMessage = "Invalid mail")]
        public string mail { get; set; }
        [Required(ErrorMessage = "Missing sno")]
        public long? sno { get; set; }
        [Required(ErrorMessage = "Missing compid")]
        public long? compid { get; set; }
        public string chname { get; set; }
    }
}
