using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form.setup.insert
{
    public class AddBranchForm
    {
        [Required(ErrorMessage = "Missing name", AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Missing location", AllowEmptyStrings = false)]
        public string Location { get; set; }
        [Required(ErrorMessage = "Missing status", AllowEmptyStrings = false)]
        public string Status { get; set; }
        [Required(ErrorMessage = "Missing Audit by")]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid Audit by")]
        public string AuditBy { get; set; }
        [Required(ErrorMessage = "Missing Branch sno")]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid sno")]
        public long Branch_Sno { get; set; }
    }
}