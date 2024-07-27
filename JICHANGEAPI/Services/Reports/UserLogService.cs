using BL.BIZINVOICING.BusinessEntities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services.Reports
{
    public class UserLogService
    {
        public List<TRACK_DET> GetLoginTimesReport(string startDate,string endDate)
        {
            try
            {
                var result = new TRACK_DET().Getfunctiontrackdet(startDate, endDate);
                return result != null ? result : new List<TRACK_DET>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }
    }
}
