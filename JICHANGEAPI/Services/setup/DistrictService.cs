using BL.BIZINVOICING.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace JichangeApi.Services.setup
{
    public class DistrictService
    {
        public List<DISTRICTS> GetActiveDistrict(long districtSno)
        {
            try
            {
                DISTRICTS distrcits = new DISTRICTS();
                List<DISTRICTS> results = distrcits.GetDistrictActive(districtSno);
                if (results != null) { return results; }
                return new List<DISTRICTS>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
