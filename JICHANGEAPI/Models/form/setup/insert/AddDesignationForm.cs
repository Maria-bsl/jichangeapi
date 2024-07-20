using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form.setup.insert
{
    public class AddDesignationForm : MainForm
    {
        [Required(ErrorMessage = "Missing designation")]
        public string desg { get; set; }
        [Required(ErrorMessage = "Missing SNO")]
        public long? sno { get; set; }
        public bool? dummy { get; set; }
    }
}