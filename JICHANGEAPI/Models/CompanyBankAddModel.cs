using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Models.form;
using JichangeApi.Models.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class CompanyBankAddModel : MainForm
    {
        [Required(ErrorMessage ="Missing Company Id", AllowEmptyStrings =false)]
        public long compsno  {get; set;}

        [Required(ErrorMessage = "Missing Company Name", AllowEmptyStrings = false)]
        public string compname  {get; set;} 
        public string pbox  {get; set;} 
        public string addr  {get; set;}
        //[Required(ErrorMessage = "Missing region")]
        public long rsno  {get; set;}
        //[Required(ErrorMessage = "Missing distict")]
        public long dsno  {get; set;}
        //[Required(ErrorMessage = "Missing ward")]
        public long wsno  {get; set;} 
        public string tin  {get; set;} 
        public string vat  {get; set;} 
        public string dname  {get; set;}

        [Required(ErrorMessage = "Missing Email", AllowEmptyStrings = false)]
        public string email  {get; set;} 
        public string telno  {get; set;}
        public string fax  {get; set;}

        [Required(ErrorMessage = "Missing Mobile Number", AllowEmptyStrings = false)]
        public string mob  {get; set;} 
        public byte[] clogo  {get; set;} 
        public byte[] sig  {get; set;} 
        public bool dummy  {get; set;} 
        public int lastrow  {get; set;}

        //[Required(ErrorMessage = "Missing Bank details ")]
        //[Required, MinLength(1, ErrorMessage = "Missing Bank details")]
        [RequiredList(ErrorMessage = "Missing Bank details")]
        public List<CompanyBankMaster> details  {get; set;}

        [Required(ErrorMessage = "Missing Branch Id", AllowEmptyStrings = false)]
        public long branch  {get; set;} 
        public string check_status  {get; set;}

        //public string occupation { get; set; }
    }
}