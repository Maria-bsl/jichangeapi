using BL.BIZINVOICING.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services.Reports
{
    public class RepCustomerService
    {
        Payment pay = new Payment();
        public List<CustomerMaster> GetCustomerDetailsReport(long compid,long regionId,long districtId)
        {
            try
            {
                var results = new CustomerMaster().CustGetrep(compid, regionId, districtId);
                return results != null ? results : new List<CustomerMaster>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }

        public List<CustomerMaster> GetCustomerDetailsReport(List<long> vendors)
        {
            try
            {
                var results = new CustomerMaster().GetCustomerReportByCompanies(vendors); //new CustomerMaster().CustGetrep(vendors, regions, districts);
                return results != null ? results : new List<CustomerMaster>();
            }
            catch (Exception ex)
            {
                pay.Message = ex.ToString();
                pay.AddErrorLogs(pay);

                throw new Exception(ex.Message);
            }
        }
    }
}
