using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Models.form
{
    public class AuditTrailForm
    {
        [Required(ErrorMessage = "Missing table name")]
        public string tbname {  get; set; }
        [Required(ErrorMessage = "Missing start date")]
        public string Startdate {  get; set; }
        [Required(ErrorMessage = "Missing end date")]
        public string Enddate {  get; set; }
        [Required(ErrorMessage = "Missing action")]
        public string act {  get; set; }
        [Required(ErrorMessage = "Missing branch")]
        public long? branch { get; set; }
    }
}
