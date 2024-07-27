using BL.BIZINVOICING.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services.Reports
{
    public class RepCustomerService
    {
        public List<CustomerMaster> GetCustomerDetailsReport(long compid,long regionId,long districtId)
        {
            try
            {
                var results = new CustomerMaster().CustGetrep(compid, regionId, districtId);
                return results != null ? results : new List<CustomerMaster>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
