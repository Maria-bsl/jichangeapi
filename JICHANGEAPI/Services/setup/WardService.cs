using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
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
        public WARD GetWardById(long wardId)
        {
            try
            {
                WARD ward = new WARD();
                WARD found = ward.EditWARD(wardId);
                if (found == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                return found;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
