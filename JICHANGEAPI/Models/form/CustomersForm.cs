using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JichangeApi.Models.form
{
    public class CustomersForm :MainForm
    {
        long CSno{get; set;} /*long Compsno{get; set;}*/ String CName{get; set;} String PostboxNo{get; set;} String Address{get; set;} long regid{get; set;} long distsno{get; set;} long wardsno{get; set;}
            String Tinno{get; set;} String VatNo{get; set;} String CoPerson{get; set;} String Mail{get; set;} String Mobile_Number{get; set;} bool dummy{get; set;} string check_status{get; set;}
    }
}