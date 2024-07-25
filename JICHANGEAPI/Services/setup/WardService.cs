using BL.BIZINVOICING.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services.setup
{
    public class WardService
    {
        public List<WARD> GetActiveWard(long wardSno)
        {
            try
            {
                WARD ward = new WARD();
                List<WARD> result = ward.GetWARDAct(wardSno);
                if (result != null) return result;
                return new List<WARD>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
