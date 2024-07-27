using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Models.form
{
    public class DeleteCompanyUserForm : MainForm
    {
        [Required(ErrorMessage = "Missing sno")]
        public long? sno {  get; set; }
        [Required(ErrorMessage = "Missing compid")]
        public long? compid { get; set; }
    }
}
