//using BL.BIZINVOICING.BusinessEntities.Masters;
using BL.BIZINVOICING.BusinessEntities.Masters;
using JichangeApi.Controllers.setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Services.Companies
{
    public class CompanyService
    {
        public List<Company> GetCompanyList()
        {
            try
            {
                Company company = new Company();
                var result = company.GetCompanyMas();
                return result != null ? result : new List<Company>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Company GetCompanyById(long compid)
        {
            try
            {
                Company company = new Company();
                var result = company.GetCompanyS(compid);
                if (result == null) throw new ArgumentException(SetupBaseController.NOT_FOUND_MESSAGE);
                return result;
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
