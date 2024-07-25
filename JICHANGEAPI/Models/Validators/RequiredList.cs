using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Models.Validators
{
    public class RequiredList : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            return list != null && list.Count > 0;
        }
    }
}
