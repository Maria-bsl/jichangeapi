using JichangeApi.Models.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Models
{
    public class CustomerDetailsForm
    {
        [RequiredList(ErrorMessage = "Missing company Ids")]
        public List<long> companyIds {  get; set; }
    }
}
