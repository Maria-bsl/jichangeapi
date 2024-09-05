using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Models.form
{
    public class AuditTrailForm : MainForm
    {
        //[Required(ErrorMessage = "Missing table name",AllowEmptyStrings = false)]
        public string tbname {  get; set; }
        public string Startdate {  get; set; }
        public string Enddate {  get; set; }
        //[Required(ErrorMessage = "Missing audit type", AllowEmptyStrings = false)]
        public string act {  get; set; }
        [Required(ErrorMessage = "Missing branch")]
        public long? branch { get; set; }
        [Required(ErrorMessage = "Missing page number"),Range(1, int.MaxValue, ErrorMessage = "The page number must be greater than or equal to 1.")]
        public int? pageNumber { get; set; }
        [Required(ErrorMessage = "Missing page size"), Range(1, int.MaxValue, ErrorMessage = "The page size must be greater than or equal to 1.")]
        public int? pageSize { get; set; }
    }
}
