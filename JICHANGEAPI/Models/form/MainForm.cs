using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JichangeApi.Models.form
{
    public class MainForm
    {
        [Required(ErrorMessage = "Missing user id")]
        public long? userid { get; set; }
    }
}