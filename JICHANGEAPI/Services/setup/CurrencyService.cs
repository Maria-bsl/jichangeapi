using BL.BIZINVOICING.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services.setup
{
    public class CurrencyService
    {
        public List<CURRENCY> GetCurrenciesList()
        {
            try
            {
                CURRENCY currency = new CURRENCY();
                var results = currency.GetCURRENCY();
                return results != null ? results : new List<CURRENCY>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
