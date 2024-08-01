using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class TransactBankModel
    {
        [Required(ErrorMessage = "Missing Company Id", AllowEmptyStrings = false)]
        public string Compid { get; set; }
        [Required(ErrorMessage = "Missing Customer Id", AllowEmptyStrings = true)]
        public string cusid { get; set; }
        [Required(ErrorMessage = "Missing Start date", AllowEmptyStrings = true)]
        public string stdate { get; set; }
        [Required(ErrorMessage = "Missing End date", AllowEmptyStrings = true)]
        public string enddate { get; set; }

        [Required(ErrorMessage = "Missing Branch Id", AllowEmptyStrings = true)]
        public long? branch { get; set; }
    }
}