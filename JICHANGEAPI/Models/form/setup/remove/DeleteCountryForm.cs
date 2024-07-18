using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models.form.setup.remove
{
    public class DeleteCountryForm : MainForm
    {
        [Required(ErrorMessage = "Missing country id.")]
        public long sno { get; set; }
    }
}