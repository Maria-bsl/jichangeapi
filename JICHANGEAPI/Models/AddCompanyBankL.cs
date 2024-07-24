using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class AddCompanyBankL : MainForm
    {

        [Required(ErrorMessage ="Missing Company Id", AllowEmptyStrings =false)]
        public long compsno  {get; set;}
        [Required(ErrorMessage = "Missing Company Name", AllowEmptyStrings = false)]
        public string compname  {get; set;} 
        public string pbox  {get; set;} 
        public string addr  {get; set;}
        public long rsno  {get; set;} 
        public long dsno  {get; set;} 
        public long wsno  {get; set;} 
        public string tin  {get; set;} 
        public string vat  {get; set;} 
        public string dname  {get; set;}
        [Required(ErrorMessage = "Missing email", AllowEmptyStrings = false)]
        public string email  {get; set;} 
        public string telno  {get; set;}
        public string fax  {get; set;}
        [Required(ErrorMessage = "Missing Mobile number", AllowEmptyStrings = false)]
        public string mob  {get; set;} 
        public bool dummy  {get; set;}
        [Required(ErrorMessage = "Missing Account Number", AllowEmptyStrings = false)]
        public string accno  {get; set;}
        [Required(ErrorMessage = "Missing Branch", AllowEmptyStrings = false)]
        public long branch  {get; set;}
        [Required(ErrorMessage = "Missing Checker Status", AllowEmptyStrings = false)]
        public string check_status  {get; set;}

        
    }
}