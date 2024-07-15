using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form
{
    public class CustomersForm : MainForm
    {
        public long CSno {get; set;}
        /*long Compsno{get; set;}*/
        [Required(ErrorMessage = "Missing Company Id", AllowEmptyStrings = false)]

        public long? compid { get; set; }
        [Required(ErrorMessage = "Missing Customer name", AllowEmptyStrings = false)]
        public string CName {get; set;} 
        public string PostboxNo {get; set;} 
        public string Address {get; set;} 
        public long regid {get; set;} 
        public long distsno {get; set;}
        public long wardsno {get; set;}
        public string Tinno {get; set;} 
        public string VatNo {get; set;} 
        public string CoPerson {get; set;} 
        public string  Mail {get; set;}
        [Required(ErrorMessage ="Missing Mobile Number", AllowEmptyStrings =false)]
        public string Mobile_Number {get; set;} 
        public bool dummy {get; set;} 
        public string check_status {get; set;}
    }
}