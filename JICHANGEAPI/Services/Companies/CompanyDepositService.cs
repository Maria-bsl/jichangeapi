using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services.Companies
{
    public class CompanyDepositService
    {
        public C_Deposit GetCompanyDepositAccount(long compid)
        {
            try
            {
                C_Deposit c_Deposit = new C_Deposit();
                var result = c_Deposit.GetMAccount(compid);
                if (result == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                return result;
            }
            catch(ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
