using JichangeApi.Models.form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JichangeApi.Models
{
    public class AddCompanyApproveModel :MainForm
    {

        public long compsno  { get; set; } 
        public string pfx  { get; set; }
        public string pwd  { get; set; } 
        public long ssno  { get; set; }

    }
}